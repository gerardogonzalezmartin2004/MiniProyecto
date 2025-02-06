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

    [Header("GroundConfiguración")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isOnGround = true;

    [Header("CanvasConfiguración")]
    public TextMeshProUGUI pointsText;

    Rigidbody2D rigidBody2D;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = health;
        


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGround();

    }
    public void Movement()
    {

         float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rigidBody2D.velocity = new Vector2(moveHorizontal * speed, rigidBody2D.velocity.y);
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isOnGround = false;
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
}
