using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerConfiguración")]
    public float speed = 5f;
    public float jumpForce = 6f;
    public int health = 10;
    public int maxHealth = 10;
    public int minHealth = 0;
    public int currentHealth;
    public bool isDeath = true;

    [Header("DashConfiguración")]
    public float velocityDash;
    public float timeDash;
    private float gravityInicial;
    private bool doDash = true;// para hacer el dash
    private bool moveDash = true;// para que no te puedas mover cuando estes haciedno el dash

    [Header("GroundConfiguración")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isOnGround = true;

    [Header("CanvasConfiguración")]
    public TextMeshProUGUI pointsText;

   
    public TrailRenderer trailRenderer;
    Rigidbody2D rigidBody2D;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = health;
        gravityInicial = rigidBody2D.gravityScale;


    }

    // Update is called once per frame
    void Update()
    {
        if (moveDash)
        {
            Movement();
        }
       
        CheckGround();
        
      
    }
    public void Movement()
    {

         float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rigidBody2D.velocity = new Vector2(moveHorizontal * speed, rigidBody2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isOnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && doDash )
        {
            StartCoroutine(Dash(moveHorizontal));

        }

    }
    void CheckGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position,groundRadius,groundLayer);//detecta colisiones en un rango circular y he puesot(una posiicon, el radio del circula, una capa)
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isOnGround = true ;
        }
    }
    */
    private IEnumerator Dash(float direction)
    {
        moveDash = false;
        doDash = false;
        rigidBody2D.gravityScale = 0;
        if( direction == 1)
        {
            rigidBody2D.velocity = new Vector2(velocityDash, 0);
        }
        else if( direction == -1)
        {
            rigidBody2D.velocity = new Vector2(-velocityDash, 0);
        }
        trailRenderer.emitting = true;
        

        yield return new WaitForSeconds(timeDash);

        moveDash = true;
        doDash = true;
        rigidBody2D.gravityScale = gravityInicial;
        trailRenderer.emitting = false;

    }

}
