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
        moveDirection.Normalize();

        characterRigidbody.MovePosition(characterRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}