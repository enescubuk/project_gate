using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody characterRigidbody;

    [Header("Stairs Climbing Settings")]
    public Vector3 heightOffset;
    public float stairHeight, climbSpeed, dist, Div;
    public LayerMask stairs;

    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterRigidbody.freezeRotation = true; // Karakterin dönmesini engeller
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        ApplyExtraGravity(); // Ek yerçekimi uygula
        MovePlayer();
        ClimbStairs(); // Merdiven tırmanma işlevini çağır
    }

    private void ApplyExtraGravity()
    {
        // Karakterin daha hızlı düşmesi için ek kuvvet uygula
        characterRigidbody.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Hareket yönünü hesapla
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        // Hareketi uygulamak için hız vektörünü güncelle
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = characterRigidbody.linearVelocity.y; // Yere düşmeyi engellemek için mevcut düşüş hızını koru

        characterRigidbody.linearVelocity = velocity; // Yeni hız vektörünü uygula
    }

    private void ClimbStairs()
    {
        Debug.DrawRay(transform.position + heightOffset, orientation.forward * dist, Color.red);
        RaycastHit hit;

        // Eğer merdiven varsa ve oyuncu ileri doğru hareket ediyorsa (W tuşu)
        if (Physics.Raycast(transform.position + heightOffset, orientation.forward, out hit, dist, stairs) && verticalInput > 0)
        {
            // Hedef pozisyonu belirle
            Vector3 climbPosition = new Vector3(
                characterRigidbody.position.x,
                characterRigidbody.position.y + stairHeight,
                characterRigidbody.position.z
            );

            // Yumuşak geçişle pozisyonu yukarı ve ileri taşı
            characterRigidbody.position = Vector3.Lerp(
                characterRigidbody.position,
                climbPosition + orientation.forward / Div,
                climbSpeed * Time.deltaTime
            );
        }
    }
}
