using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public  ParticleSystem dust;

    [Header("Stats")]
    public int health = 3;
    [SerializeField] float stamina = 3;
    [SerializeField] float speed = 0f;
    [SerializeField] float attackSpeed = 0f;
    [SerializeField] float luck = 0f;


    [SerializeField] float jumpForce = 10;
    

    public enum CreatureMovementType { tf, physics };
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.tf;

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -.5f;
    [SerializeField] float jumpRadius = .25f;

    [Header("Flavor")]
    [SerializeField] string creatureName = "Meepis";
    public GameObject body;
    //[SerializeField] private List<AnimationStateChanger> animationStateChangers;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;
    //[SerializeField] CreatureSO creatureSO;

   [Header("Boundaries")]
   [SerializeField] float minX;
   [SerializeField] float maxX;
   [SerializeField] float minY;
   [SerializeField] float maxY;


    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(health);

    }



    // Update is called once per frame
    void Update()
    {
        // if(creatureSO != null){
        //     creatureSO.health = health;
        //     creatureSO.stamina = stamina;
        // }
        inputManagement();
    }

    void FixedUpdate(){
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        move();
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

        //set animation
        // if(direction.x != 0){
        //     foreach(AnimationStateChanger asc in animationStateChangers){
        //         asc.ChangeAnimationState("Walk",speed);
        //     }
        // }else{
        //     foreach(AnimationStateChanger asc in animationStateChangers){
        //         asc.ChangeAnimationState("Idle");
        //     }
        // }

    }

    public void MoveCreatureToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveCreature(direction.normalized);
    }

    public void Stop(){
        MoveCreature(Vector3.zero);
    }

    public void MoveCreatureRb(Vector3 direction)
    {
        Vector3 currentVelocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = (currentVelocity) + (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
        //rb.AddForce(direction * speed);
        //rb.MovePosition(transform.position + (direction * speed * Time.deltaTime))
    }

    public void MoveCreatureTransform(Vector3 direction)
    {
                transform.position += direction * Time.deltaTime * speed;
    }

    public void Jump()
    {
        if(Physics2D.OverlapCircleAll(transform.position + new Vector3(0,jumpOffset,0),jumpRadius,groundMask).Length > 0){
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private bool IsWithinBoundaries(Vector3 position)
    {
        return position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY;
    }

    void createDust(){
        dust.Play();
    }

    void inputManagement(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0){
            lastHorizontalVector = moveDir.x;
        }

        if(moveDir.y != 0){
            lastVerticalVector = moveDir.y;
        }
    }

    void move(){
        rb.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    }
}