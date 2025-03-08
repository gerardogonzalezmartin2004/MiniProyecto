using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MovimientoPrueba : MonoBehaviour
{
    [Header("PlayerConfiguración")]
    public float speed;
    public float jumpForce;
    public int health;
    public int maxHealth;
    public int minHealth;
    public int currentHealth;
    public bool isDeath = true;
    private bool moveRight = true;

    [Header("GroundConfiguración")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isOnGround = true;

    Rigidbody2D rigidBody2D;
    void Start()
    {
      
        isDeath = false;
       
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = health;
       
        Debug.Log("la vida es:" + currentHealth);
        


    }

    // Update is called once per frame
    void Update()
    {
        
            Movement();
        

        CheckGround();
      
    }
    public void Movement()
    {
        if (isDeath)
        {
            rigidBody2D.velocity = Vector2.zero; // nose mueva el player
            return;

        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
       
        rigidBody2D.velocity = new Vector2(moveHorizontal * speed, rigidBody2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isOnGround = false;
            Debug.Log("Salto realizado");
        }
           

    }
    void CheckGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);//detecta colisiones en un rango circular y he puesot(una posiicon, el radio del circula, una capa)
       
    }
}
