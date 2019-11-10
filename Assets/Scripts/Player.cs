using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Rigidbody2D body2D;
    public float speed = 140f;
    private float moveInput;
    public float jumpForce = 120f;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsGroundSoul;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Soul components")]
    [Tooltip("Check out Layermask layer")]
    public int defaultLayer = 0;
    [Tooltip("Check out Layermask layer")]
    public int soulLayer = 9;
    [Tooltip("Check out Layermask layer")]
    public int transitionLayer = 10;
    public Sprite defaultSprite;
    public Sprite noSoulBodySprite;
    public Transform noSoulBody;
    public bool isSoulTime;
    public bool isTransitioning;
    private bool soulShiftUsable = true;
    public float transitionDuration = 2.0f;
    public float movementIgnoreDuration = 1.0f;

    private bool movementConscious;
    private float movementIgnoreEnd;

    [Header("Pause component")]
    private bool isPause;
    public GameObject pauseCanvas;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem ps;
    // Animations
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movementIgnoreEnd = 0.0f;
        movementConscious = true;
        noSoulBody.gameObject.SetActive(false);
        isSoulTime = false;
        isTransitioning = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;

        isPause = false;
        pauseCanvas.SetActive(false);

        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate() {
        if (isTransitioning || Time.time < movementIgnoreEnd || isPause)
        {
            return;
        }

        moveInput = Input.GetAxis("Horizontal");

        if (!movementConscious && Mathf.Approximately(0.0f, moveInput))
        {
            return;
        }
        else
        {
            movementConscious = true;
        }

        body2D.velocity = new Vector2(moveInput * speed, body2D.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            // freeze time
            // show gui
            // unfreeze time if back
            if (!isPause)
            {
                isPause = true;
                Time.timeScale = 0.0f;
                pauseCanvas.SetActive(true);
            }
            else
            {
                ExitPause();
            }
            return;
        }

        if (Input.GetButtonDown("Restart"))
        {
            RestartLevel();
        }

        if (isTransitioning || isPause)
            return;
        
        if (body2D.velocity.x != 0) {
            animator.SetInteger("AnimState", 1);
        } else {

            animator.SetInteger("AnimState", 0);
        }

        // jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround | whatIsGroundSoul);
        if (isGrounded)
        {
            soulShiftUsable = true;
        }
        if (moveInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveInput < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        float yValue = Input.GetAxisRaw("Vertical");

        if (isGrounded == true && yValue > 0) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            body2D.velocity = Vector2.up * jumpForce;
        }
        if (yValue > 0 && isJumping == true) {
            if (jumpTimeCounter > 0) {
                body2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (yValue > 0)
            isJumping = false;

        if (Input.GetButtonDown("ToggleTime") && !isTransitioning)
        {

            if (isSoulTime)
            {
                // push player here
                StartCoroutine(GoToSoul(true));
                ps.Stop();
            }
            else if (soulShiftUsable)
            {
                ps.Play();
                soulShiftUsable = false;
                // change player sprite
                spriteRenderer.sprite = noSoulBodySprite;
                noSoulBody.transform.position = transform.position;
                noSoulBody.gameObject.SetActive(true);
                // change physics layer for object
                gameObject.layer = soulLayer;
                // move
                isSoulTime = !isSoulTime;
                animator.SetBool("IsSoulMode", true);
            }

        }
    }

    IEnumerator GoToSoul(bool isDefault)
    {
        gameObject.layer = transitionLayer;
        movementConscious = false;
        isTransitioning = true;
        body2D.isKinematic = true;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = noSoulBody.position;
        Vector2 changeVector = endPosition - startPosition;

        if (!isDefault)
        {
            changeVector = Vector2.zero;
        }

        float startTime = Time.time;
        float endTime = startTime + transitionDuration;
        movementIgnoreEnd = endTime + movementIgnoreDuration;

        while (Time.time < endTime)
        {
            float change = Time.time - startTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, change / transitionDuration);
            yield return new WaitForSeconds(1.0f/60.0f);
        }

        transform.position = noSoulBody.position;

        // todo: push the body here???

        // change physics layer for object
        // change player sprite
        spriteRenderer.sprite = defaultSprite;
        noSoulBody.gameObject.SetActive(false);
        // change physics layer for object
        gameObject.layer = defaultLayer;
        animator.SetBool("IsSoulMode", false);

        body2D.isKinematic = false;
        body2D.velocity = changeVector * 3f;
        isTransitioning = false;
        isSoulTime = false;
    }

    public void Die()
    {
        if (isSoulTime)
        {
            ps.Stop();
            StartCoroutine(GoToSoul(false));
        }
        else
        {
            // todo: die animation here??
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitPause()
    {
        isPause = false;
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
    }
}
