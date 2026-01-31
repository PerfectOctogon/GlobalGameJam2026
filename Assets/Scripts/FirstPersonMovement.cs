using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    CharacterController characterController;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -12f;
    public float jumpHeight = 4f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip runningClip;

    /* ================= POWER UPS ================= */

    [Header("Power-Ups (Unlock Flags)")]
    public bool canUseSpeedPowerUp = false;
    public bool canUseJumpPowerUp = false;
    public bool canUseDashPowerUp = false;

    private bool speedPowerUpActive = false;
    private bool jumpPowerUpActive = false;
    private bool dashPowerUpActive = false;

    private float baseSpeed;

    /* ================ DOUBLE JUMP ================= */

    private int jumpCount = 0;
    private int maxJumps = 1;

    /* =================== DASH ===================== */

    [Header("Dash")]
    public float dashSpeedMultiplier = 6f;   // how insane the dash is
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTimer;
    private float dashCooldownTimer;
    private float cachedSpeed;

    /* ============================================= */

    [Header("Mask")]
    public MaskUIManager maskUIManager;

    Vector3 velocity;
    bool isGrounded;
    bool isRunning;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        baseSpeed = speed;
    }

    void Update()
    {
        HandleGroundCheck();
        HandlePowerUpInput();
        HandleDash();
        HandleMovement();
        HandleJump();
        ApplyGravity();

        characterController.Move(velocity * Time.deltaTime);
    }

    /* ===================== INPUT ===================== */

    void HandlePowerUpInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseSpeedPowerUp)
            ActivateSpeedPowerUp();

        if (Input.GetKeyDown(KeyCode.Alpha2) && canUseJumpPowerUp)
            ActivateJumpPowerUp();

        if (Input.GetKeyDown(KeyCode.Alpha3) && canUseDashPowerUp)
            ActivateAllPowerUps();
    }

    void EnableSpeedPowerUp()
    {
        this.canUseSpeedPowerUp = true;
        maskUIManager.EnableSpeedMask();
    }

    void EnableDoubleJumpPowerUp()
    {
        this.canUseJumpPowerUp = true;
        maskUIManager.EnableDoubleJumpMask();
    }

    void EnableDashPowerUp()
    {
        this.canUseDashPowerUp = true;
        maskUIManager.EnableUltimateMask();
    }

    void ActivateSpeedPowerUp()
    {
        DisableAllPowerUps();
        maskUIManager.ActivateSpeedMask();
        speedPowerUpActive = true;

        speed = baseSpeed * 2f;
        maxJumps = 1;
    }

    void ActivateJumpPowerUp()
    {
        DisableAllPowerUps();
        maskUIManager.ActivateDoubleJumpMask();
        jumpPowerUpActive = true;

        speed = baseSpeed;
        maxJumps = 2;
    }

    void ActivateDashPowerUp()
    {
        DisableAllPowerUps();
        maskUIManager.ActivateUltimateMask();
        dashPowerUpActive = true;

        speed = baseSpeed;
        maxJumps = 1;
    }

    void DisableAllPowerUps()
    {
        speedPowerUpActive = false;
        jumpPowerUpActive = false;
        dashPowerUpActive = false;

        speed = baseSpeed;
        maxJumps = 1;
    }

    void ActivateAllPowerUps()
    {
        maskUIManager.ActivateUltimateMask();
        
        speedPowerUpActive = true;
        jumpPowerUpActive = true;
        dashPowerUpActive = true;
        
        speed = baseSpeed * 2f;
        maxJumps = 2;
        dashPowerUpActive = true;
    }

    /* =================== MOVEMENT =================== */

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        isRunning = move.magnitude > 0 && isGrounded;

        if (isRunning)
            PlaySound(runningClip);
        else
            audioSource.clip = null;
    }


    /* ===================== DASH ===================== */

    void HandleDash()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (!dashPowerUpActive)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;

            cachedSpeed = speed;
            speed = baseSpeed * dashSpeedMultiplier;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                speed = cachedSpeed;
            }
        }
    }

    /* ===================== JUMP ===================== */

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
        }
    }

    /* ==================== PHYSICS =================== */

    void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }
    }

    void ApplyGravity()
    {
        if (!isDashing)
            velocity.y += gravity * Time.deltaTime;
    }

    /* ===================== AUDIO ==================== */

    void PlaySound(AudioClip clip)
    {
        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /* ===================== DEBUG ==================== */

    public float GetCurrentPlayerSpeed()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        return move.magnitude * speed;
    }
}
