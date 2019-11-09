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

    // Animations
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        body2D.velocity = new Vector2(moveInput * speed, body2D.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (body2D.velocity.x != 0) {
            animator.SetInteger("AnimState", 1);
        } else {

            animator.SetInteger("AnimState", 0);
        }

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
    }

}
