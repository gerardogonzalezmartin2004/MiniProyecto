using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float dashForce;//Fuerza al rebotar con el enemigo
    

    [Header("GroundConfiguración")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isOnGround = true;

    [Header("CanvasConfiguración")]
    public TextMeshProUGUI pointsText;
    int points = 0;
    public GameObject[] lifeBar;

    [Header("StompConfiguración")]
    public int stompMarioDamage;// daaño al saltar encima de los enemys
    public float bounceMario;//rebote de fuerza al chocar
   


    public TrailRenderer trailRenderer;
    Rigidbody2D rigidBody2D;
    public Animator animator;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
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
        
      
    }
    public void Movement()
    {

         float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rigidBody2D.velocity = new Vector2(moveHorizontal * speed, rigidBody2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
            isOnGround = false;
            Debug.Log("Salto realizado");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && doDash && canDash)
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
        if (!isDeath)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
            Debug.Log("El jugador se ha dañado. Su vida actual es :" + currentHealth);
            if (currentHealth >=0 && currentHealth < lifeBar.Length)
            {
                lifeBar[currentHealth].SetActive(false);
            }
            if (currentHealth == 0)
            {
                isDeath = true;
                Debug.Log("Estas muerto");
                Invoke("GameOverScene",4f);
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
        isDashing  = false;
        doDash = true;
        rigidBody2D.gravityScale = gravityInicial;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);// Cooldown del dash

        canDash = true;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && collision.gameObject.GetComponent<Enemy>())
        {
          
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(dashDamage);
                Debug.Log("daño :" + dashDamage);
                Vector2 BounceDirection = (collision.transform.position -transform.position).normalized;
                Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    enemyRb.AddForce(BounceDirection * dashForce, ForceMode2D.Impulse);
                }
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
    private void GameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
}
