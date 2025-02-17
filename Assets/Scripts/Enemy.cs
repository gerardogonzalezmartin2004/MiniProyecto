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

    private void Update()
    {
        playerOnRange = Physics2D.Raycast(shootPoint.position, transform.right, distanceLine, playerMask);
        if (playerOnRange)
        {
            if (Time.time > timeBullet +timeLastBullet)
            {
                timeLastBullet = Time.time;
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
            if (collision.contacts[0].normal.y < -0.5f) // Si el jugador salta sobre el enemigo
            {
                Debug.Log("Enemigo eliminado!");
                player.Jump(killForce); // Rebotamos al jugador
                Destroy(gameObject); // Eliminamos al enemigo
            }
            else
            {
                Debug.Log($"El jugador ha recibido {enemyDamage} de daño.");
                player.TakeDamage(enemyDamage); // Si no, le hacemos daño
            }
        }
    }
}
