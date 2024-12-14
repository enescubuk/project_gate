using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody characterRigidbody;

    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        characterRigidbody.AddForce(moveDirection.normalized * moveSpeed,ForceMode.Force);
    
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);

        if (isGrounded)
        {
            characterRigidbody.linearDamping = 5;
        }
        else
        {
            characterRigidbody.linearDamping = 0;
        }
    }

}