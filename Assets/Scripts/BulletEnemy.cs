using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float velocityBullet;
    public int damage;
    
    private void Update()
    {
        transform.Translate(Time.deltaTime * velocityBullet * Vector2.right);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
