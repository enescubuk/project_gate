using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody characterRigidbody;

    [Header("Stairs Climbing Settings")]
    [SerializeField] private Vector3 heightOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private float stairHeight = 0.3f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float dist = 1f;
    [SerializeField] private float Div = 2f;
    [SerializeField] private LayerMask stairs;

    private bool isPlayingFootstep = false; // Ayak sesi kontrolü için flag

    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterRigidbody.freezeRotation = true; // Karakterin dönmesini engeller
    }

    private void Update()
    {
        GetPlayerInput();
        HandleFootstepSound();
    }

    private void FixedUpdate()
    {
        ApplyExtraGravity();
        MovePlayer();
        ClimbStairs();
    }

    private void ApplyExtraGravity()
    {
        // Karakterin daha hızlı düşmesini sağlamak için ek yerçekimi uygula
        characterRigidbody.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
    }

    private void GetPlayerInput()
    {
        // Oyuncu girdilerini al
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Hareket yönünü hesapla
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        // Hareketi uygula
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = characterRigidbody.linearVelocity.y; // Düşüş hızını koru
        characterRigidbody.linearVelocity = velocity; // Yeni hız vektörünü uygula
    }

    private void ClimbStairs()
    {
        Debug.DrawRay(transform.position + heightOffset, orientation.forward * dist, Color.red);
        RaycastHit hit;

        // Eğer oyuncu merdivene yaklaşıyorsa ve ileri hareket ediyorsa
        if (Physics.Raycast(transform.position + heightOffset, orientation.forward, out hit, dist, stairs) && verticalInput > 0)
        {
            // Hedef pozisyonu belirle
            Vector3 climbPosition = new Vector3(
                characterRigidbody.position.x,
                characterRigidbody.position.y + stairHeight,
                characterRigidbody.position.z
            );

            // Yumuşak geçişle pozisyonu ayarla
            characterRigidbody.position = Vector3.Lerp(
                characterRigidbody.position,
                climbPosition + orientation.forward / Div,
                climbSpeed * Time.deltaTime
            );
        }
    }

    private void HandleFootstepSound()
    {
        // Karakterin hızını kontrol et
        bool isMoving = characterRigidbody.linearVelocity.magnitude > 0.1f;

        if (isMoving && !isPlayingFootstep)
        {
            // Ayak sesi çal
            isPlayingFootstep = true;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.footstepClip);

            // Sesi belirli bir süre sonra tekrar çal (örneğin, adım aralığına göre)
            Invoke(nameof(ResetFootstep), 0.4f); // 0.5 saniyede bir çal
        }
    }

    private void ResetFootstep()
    {
        isPlayingFootstep = false;
    }
}
