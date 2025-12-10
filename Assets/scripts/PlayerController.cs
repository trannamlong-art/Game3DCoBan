using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    public Slider healthBar;
    public TMP_Text ammoText;

    [Header("Score")]
    public TMP_Text scoreText;
    private int score = 0;

    [Header("Game Over")]
    public GameObject gameOverCanvas;
    private bool isDead = false;

    public float maxHealth = 100f;
    private float currentHealth;

    private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private Animator animator;

    private bool throwing = false;
    public float throwTime = 2.0f;

    public AudioSource audioSourceMove;
    public AudioSource audioSourceGrenade;
    public AudioSource audioReload;

    private bool reloading = false;
    public HitEffectUI hitEffect;

    // ✅ HỆ SÚNG MỚI
    [Header("Weapon System")]
    public WeaponBase currentWeapon;

    IEnumerator ThrowRoutine()
    {
        throwing = true;
        animator.SetBool("isThrowing", true);

        if (audioSourceGrenade != null)
            audioSourceGrenade.Play();

        yield return new WaitForSeconds(throwTime);

        animator.SetBool("isThrowing", false);
        throwing = false;
    }

    IEnumerator ReloadRoutine()
    {
        reloading = true;

        if (animator != null)
            animator.SetBool("Reload", true);

        if (audioReload != null)
            audioReload.Play();

        yield return new WaitForSeconds(1.2f);

        if (animator != null)
            animator.SetBool("Reload", false);

        if (currentWeapon != null)
            currentWeapon.Reload();

        UpdateUI();
        reloading = false;
    }

    void Start()
    {
        score = 0;
        if (scoreText != null)
            scoreText.text = "Score: 0";

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.maxValue = maxHealth;

        UpdateUI();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        // ✅ BẮN THEO HỆ MỚI
        if (Mouse.current.leftButton.isPressed && currentWeapon != null && !reloading)
        {
            animator.SetBool("Shoot", true);
            currentWeapon.Shoot(this);
        }
        else
        {
            animator.SetBool("Shoot", false);
        }

        // GRENADE
        if (Keyboard.current.rKey.wasPressedThisFrame && !throwing)
            StartCoroutine(ThrowRoutine());

        // RELOAD
        if (Keyboard.current.tKey.wasPressedThisFrame && currentWeapon != null && !reloading)
            StartCoroutine(ReloadRoutine());

        // FOOTSTEPS
        if (moveInput.magnitude > 0.1f)
        {
            if (!audioSourceMove.isPlaying)
                audioSourceMove.Play();
        }
        else
        {
            if (audioSourceMove.isPlaying)
                audioSourceMove.Stop();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
    }

    public void OnMove(InputValue InputValue)
    {
        moveInput = InputValue.Get<Vector2>();
    }

    public void UpdateUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;

        if (ammoText != null && currentWeapon != null)
        {
            ammoText.text =
                currentWeapon.currentAmmo + " | " + currentWeapon.reserveAmmo;
        }

    }

    public void TakeDamage(float damage)
    {
        if (hitEffect != null)
            hitEffect.Flash();

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
            Die();
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        CameraController cam = FindFirstObjectByType<CameraController>();
        if (cam != null)
            cam.UnlockCursor();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (audioSourceMove != null) audioSourceMove.Stop();

        rb.isKinematic = true;
        animator.enabled = false;
    }
}
