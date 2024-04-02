using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem dust;

    [Header("Stats")]
    public int health = 8;
    [SerializeField] float stamina = 3;
    [SerializeField] float speed = 0f;
    [SerializeField] float attackSpeed = 0f;
    [SerializeField] float luck = 0f;

    public float jumpForce = 30;

    public enum CreatureMovementType { tf, physics };
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.tf;

    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public Vector2 moveDir;
    private bool isGrounded;
    public Transform feetPosition;
    public float feetRadius;
    public LayerMask Platforms;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        inputManagement();
    }

    void FixedUpdate()
    {
        move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }
    }

    public void MoveCreature(Vector3 direction)
    {
        if (movementType == CreatureMovementType.tf)
        {
            MoveCreatureTransform(direction);
        }
        else if (movementType == CreatureMovementType.physics)
        {
            MoveCreatureRb(direction);
        }
    }

    public void MoveCreatureRb(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void MoveCreatureTransform(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    void inputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");   //X Control

        moveDir = new Vector2(moveX, 0).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void move()
    {
        rb.velocity = new Vector2(moveDir.x * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}