using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
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
  

    [Header("DashConfiguración")]
    public float velocityDash;
    public float timeDash;
    private float gravityInicial;
    private bool doDash = true;// para hacer el dash
    private bool moveDash = true;// para que no te puedas mover cuando estes haciedno el dash
    public float dashCooldown = 9f;// lo que dura el cooldown del dash
    private bool canDash = true;// para el cooldown del dash
    public int dashDamage;
    private bool isDashing = true;//para saber si esta haciedno el dash y poder hacer daño
    private bool dashUnlolocked = false;
    private bool dashAttack = false; //es para hacer q solo ataque cuando haga el dash
    public LayerMask dashUnlolockedLayer;




    [Header("GroundConfiguración")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isOnGround = true;

    [Header("CanvasConfiguración")]
    public TextMeshProUGUI pointsText;
    int points = 0;
    public GameObject[] lifeBar;
    public Image dashIcon;
    public Sprite dashIconActivate;
    public Sprite dashIconCooldown;

    [Header("StompConfiguración")]
    public int stompMarioDamage;// daaño al saltar encima de los enemys
    public float bounceMario;//rebote de fuerza al chocar
   


    public TrailRenderer trailRenderer;
    Rigidbody2D rigidBody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        canDash = false;
        dashIcon.sprite = dashIconCooldown;
        isDashing = false;
        dashAttack = false;
        isDeath = false;    
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = health;
        gravityInicial = rigidBody2D.gravityScale; // se guarda la gravedad para poder vovler a introducirla cuando la quitemos en el dash
        Debug.Log("la vida es:" + currentHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (moveDash)
        {
            Movement();
        }
      
        CheckGround();
        if(dashIcon != null)
        {
            if(canDash)
            {
                dashIcon.sprite = dashIconActivate;
            }
            else
            {
                dashIcon.sprite = dashIconCooldown;
            }
        }
      
    }
    public void Movement()
    {
        if(isDeath)
        {
            rigidBody2D.velocity = Vector2.zero; // nose mueva el player
            return;

        }

         float moveHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", Mathf.Abs(moveHorizontal));
        animator.SetFloat("VelocityY", rigidBody2D.velocity.y);
        rigidBody2D.velocity = new Vector2(moveHorizontal * speed, rigidBody2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isOnGround = false;
            Debug.Log("Salto realizado");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && doDash && canDash && dashUnlolocked)
        {
            StartCoroutine(Dash(moveHorizontal));
            Debug.Log("Se hace el dash");

        }
        if (moveHorizontal > 0 && !moveRight)
        {
            Girar();
        }
        else if (moveHorizontal < 0 && moveRight)
        {
            Girar();
        }

    }
    private void Girar()
    {
        moveRight = !moveRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y +180, 0);

    }
    void CheckGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position,groundRadius,groundLayer);//detecta colisiones en un rango circular y he puesot(una posiicon, el radio del circula, una capa)
        animator.SetBool("OnGround", isOnGround);
        //Debug.Log("esta en el suelo");
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
    public void TakeDamage(int damageAmount)
    {
        if (isDashing)
        {
            Debug.Log("No te hace daño");
            return;// hace que vuevla continuamente para q asi no le haga daño
        }
        if (!isDeath)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
            Debug.Log("El jugador se ha dañado. Su vida actual es :" + currentHealth);
            if (currentHealth >= 0 && currentHealth < lifeBar.Length)
            {
                lifeBar[currentHealth].SetActive(false);
            }
            if (currentHealth == 0)
            {
                isDeath = true;
                Debug.Log("Estas muerto");
                Invoke("GameOverScene",1f);
            }
        }
     
    }
    public void Heal(int healAmount)
    {
        if (!isDeath)
        {
            int newHealth = currentHealth;
         currentHealth += healAmount;
         currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
         Debug.Log("El jugador se ha curado. Su vida actual es :" + currentHealth);
            for (int i = newHealth; i < currentHealth; i++)
            {
                if (i < lifeBar.Length)
                {
                    lifeBar[i].SetActive(true);
                }
            }
        }
    }
    private IEnumerator Dash(float direction)
    {
        moveDash = false;
        doDash = false;
        canDash = false;
        isDashing = true;
        rigidBody2D.gravityScale = 0; // esto se hace para que hacer el dash, la gravedad no afecte al movimiento del player y sea horizontal
        dashAttack = true;
        
            if (direction == 1)
            {
                rigidBody2D.velocity = new Vector2(velocityDash, 0);
            }
            else if (direction == -1)
            {
                rigidBody2D.velocity = new Vector2(-velocityDash, 0);
            }
            trailRenderer.emitting = true;

     
        yield return new WaitForSeconds(timeDash); // lo que dura el dash

        moveDash = true;
        dashAttack = false;
        isDashing  = false;
        doDash = true;
        rigidBody2D.gravityScale = gravityInicial;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);// Cooldown del dash

        canDash = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (((1 << collision.gameObject.layer) & dashUnlolockedLayer.value) != 0)
      {
            canDash = true;
            dashUnlolocked = true;
           Debug.Log("Dash desbloqueado");
      }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dashAttack && isDashing && collision.gameObject.GetComponent<Enemy>())
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
           
              if (enemy != null)
              {          
                enemy.TakeDamage(dashDamage);
                Debug.Log("daño :" + dashDamage);
                
              }
        }               
               
    }
  
    public void Jump(float force)
    {
        // anim.SetTrigger("Jump");
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, force); // Rebote hacia arriba
    }
    public void AddPoints(int coin)
    {
        points += coin;
        pointsText.text = points.ToString();
    }
    public void GameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
  
   
}
