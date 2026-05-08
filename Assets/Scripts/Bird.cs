using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5f;
    public float maxFallSpeed = -8f;
    public float tiltUp = 30f;
    public float tiltDown = -90f;
    public float tiltSpeed = 8f;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        // Jump on click or spacebar
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame ||
    UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(0, jumpForce);
            audioSource.PlayOneShot(jumpSound);
        }

        // Tilt bird based on velocity
        float targetAngle = rb.linearVelocity.y > 0 ? tiltUp : tiltDown;
        float angle = Mathf.LerpAngle(
            transform.eulerAngles.z,
            targetAngle,
            tiltSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Fall speed limit
        if (rb.linearVelocity.y < maxFallSpeed)
            rb.linearVelocity = new Vector2(0, maxFallSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Die();
    }

  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ScoreTrigger"))
            return;

        Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        audioSource.PlayOneShot(deathSound);

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 2f; 

        Invoke(nameof(Restart), 1f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}