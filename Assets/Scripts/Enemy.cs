using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform shootPoint;
    public float distanceLine;
    public LayerMask playerMask;
    private bool playerOnRange;
    public int health;
    public GameObject bulletEnemy;
    public float timeBullet;
    public float timeLastBullet;
    public int enemyDamage; // Daño que hace al jugador
    public float killForce; // Fuerza de rebote al matar al enemigo

    public Animator animator;

    private void Update()
    {
        playerOnRange = Physics2D.Raycast(shootPoint.position, transform.right, distanceLine, playerMask); // detecta al plakyer
        if (playerOnRange)
        {
            if (Time.time > timeBullet +timeLastBullet)
            {
                timeLastBullet = Time.time;
                animator.SetTrigger("ShootBullet");
                Invoke("Shoot", timeBullet);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletEnemy, shootPoint.position, shootPoint.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootPoint.position, shootPoint.position + transform.right * distanceLine);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (collision.contacts[0].normal.y < -0.5f) // si el jugador salta sobre el enemigo: (el contact [] es un array que contiene toda l ainforamcion de los diferentes putos que entran encontacto y el [0]. normal, con los primeros puntos con los que tocan al player y es -0.5 es pq la fuerza viene de arriba a bajo
            {
                Debug.Log("Enemigo eliminado!");
                player.Jump(killForce); 
                Destroy(gameObject); 
            }    
              else 
        {
            Debug.Log($"El jugador ha recibido {enemyDamage} de daño.");
            player.TakeDamage(enemyDamage);
        }
    }
        }
      
}
