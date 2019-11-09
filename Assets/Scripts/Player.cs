using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D body2D;
    public float speed = 140f;
    private float moveInput;
    public float jumpForce = 120f;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Header("Soul components")]
    [Tooltip("Check out PHYSICS2D layer")]
    public int defaultLayer = 0;
    [Tooltip("Check out PHYSICS2D layer")]
    public int soulLayer = 6;
    [Tooltip("Check out PHYSICS2D layer")]
    public int transitionLayer = 7;
    public Sprite defaultSprite;
    public Sprite noSoulBodySprite;
    public Transform noSoulBody;
    public bool isSoulTime;
    public bool isTransitioning;
    public float transitionDuration = 2.0f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        isSoulTime = false;
        isTransitioning = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        body2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        body2D.velocity = new Vector2(moveInput * speed, body2D.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
            return;

        // jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (moveInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveInput < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (isGrounded == true && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            body2D.velocity = Vector2.up * jumpForce;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isJumping == true) {
            if (jumpTimeCounter > 0) {
                body2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }


        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
            isJumping = false;

        if (Input.GetButtonDown("ToggleTime") && !isTransitioning)
        {
            if (isSoulTime)
            {
                // push player here
                gameObject.layer = transitionLayer;
                StartCoroutine(GoToSoul());
            }
            else
            {
                // change player sprite
                spriteRenderer.sprite = noSoulBodySprite;
                noSoulBody.transform.position = transform.position;
                noSoulBody.gameObject.SetActive(true);
                // change physics layer for object
                gameObject.layer = soulLayer;
                // move
            }

            isSoulTime = !isSoulTime;
        }
    }

    IEnumerator GoToSoul()
    {
        isTransitioning = true;
        body2D.isKinematic = true;
        Vector2 startPosition = transform.position;
        float startTime = Time.time;
        float endTime = startTime + transitionDuration;
        Debug.Log("Our time is " + startTime.ToString() + " vs " + endTime.ToString());

        while (Time.time < endTime)
        {
            float change = Time.time - startTime;
            transform.position = Vector2.Lerp(startPosition, noSoulBody.position, change / transitionDuration);
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

        isTransitioning = false;
        body2D.isKinematic = false;
        yield return null;
    }
}
