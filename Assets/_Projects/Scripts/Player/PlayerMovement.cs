using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 60f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 1f;
    [SerializeField] private float jumpForce = 12f;     
    [SerializeField] private float airMultiplier = 0.4f; 

    [Header("Ground Detection")]
    [SerializeField] private float playerHeight = 2f;   
    [SerializeField] private LayerMask whatIsGround;    
    [SerializeField] private bool isGrounded;           

    [Header("References")]
    [SerializeField] private Transform orientation;

    [Header("Animation")]
    [SerializeField] private Animator animator; 

    private PlayerControls inputActions;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
        inputActions.Gameplay.Jump.performed -= ctx => Jump();
    }

    private void Update()
    {
        Vector3 origin = transform.position + new Vector3(0, playerHeight * 0.5f, 0);
        
        float distance = (playerHeight * 0.5f) + 0.2f;

        isGrounded = Physics.SphereCast(origin, 0.4f, Vector3.down, out RaycastHit hit, distance, whatIsGround);

        moveInput = inputActions.Gameplay.Move.ReadValue<Vector2>();

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = airDrag;

        LimitSpeed();

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void Jump()
    {
        // Hanya boleh lompat jika menyentuh tanah
        if (isGrounded)
        {
            // Reset velocity Y agar lompatan konsisten (walau sedang jatuh)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Gunakan ForceMode.Impulse untuk hentakan instan
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    // Fungsi ini untuk menggambar garis bantu di Scene View editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f));
        
        Gizmos.DrawWireSphere(transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f), 0.4f);
    }

    private void UpdateAnimation()
    {
        if (animator != null)
        {
            // 1. Kirim kecepatan lari ke parameter "Speed"
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            animator.SetFloat("Speed", flatVel.magnitude);

            // 2. Kirim status tanah ke parameter "IsGrounded"
            animator.SetBool("IsGrounded", isGrounded);
        }
    }
}