using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float rayDistance = 1.0f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] private ParticleSystem dustParticles;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource yurumeSesi;

    [SerializeField] private AudioClip yurumeSesiDosyasi;
    [SerializeField] private bool kontrollerTersMi = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private Vector2 originalScale;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        originalScale = transform.localScale;

        if (yurumeSesi != null && yurumeSesiDosyasi != null)
        {
            yurumeSesi.clip = yurumeSesiDosyasi; // Kaseti (sesi) kasetçalara kodla tak
            yurumeSesi.loop = true;              // Sürekli tekrarlasın
            yurumeSesi.playOnAwake = false;      // Başlar başlamaz kendi kendine çalmasın
        }
    }
    private void Update()
    {
        CheckGrounded();

        UpdateDustParticles();

        UpdateAnimationAndFlip();

        UpdateWalkingSound();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputValue value)
    {
        Vector2 okunanTusYonu = value.Get<Vector2>();

        if(kontrollerTersMi == true)
        {
            moveInput = new Vector2(-okunanTusYonu.x, okunanTusYonu.y);
        }
        else
        {
            moveInput = okunanTusYonu;
        }
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }
    public void OnJump(InputValue value)
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
      
    }
    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    private void UpdateDustParticles()
    {
        if(dustParticles == null)
        {
            return;
        }

        bool isMoving = Mathf.Abs(moveInput.x) > 0f;

        if(isGrounded && isMoving)
        {
            if(!dustParticles.isPlaying)
            {
                dustParticles.Play();
            }
        }
        else
        {
            if (dustParticles.isPlaying)
            {
                dustParticles.Stop();
            }
        }
    }

    private void UpdateAnimationAndFlip()
    {
        bool isMoving = Mathf.Abs(moveInput.x) > 0f;

        if(animator != null)
        {
            animator.SetBool("isWalking", isMoving && isGrounded);

            animator.SetBool("isJumping", !isGrounded);
        }

        if(moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x),originalScale.y,1f);
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x),originalScale.y,1f);
        }

    }

    private void UpdateWalkingSound()
    {
        if (yurumeSesi == null) return;

        bool isMoving = Mathf.Abs(moveInput.x) > 0f;

        if (isGrounded && isMoving)
        {
            if(!yurumeSesi.isPlaying)
            {
                yurumeSesi.Play();
            }
        }
        else
        {
            if(yurumeSesi.isPlaying)
            {
                yurumeSesi.Pause();
            }
        }
    }
    
}
