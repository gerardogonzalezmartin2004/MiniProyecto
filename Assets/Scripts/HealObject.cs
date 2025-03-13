using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    public int pocion = 1;
    public AudioClip healSound;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (player.currentHealth < player.maxHealth)
            {
                Debug.Log("daño :" + pocion);
                player.Heal(pocion);
                AudioSource.PlayClipAtPoint(healSound, transform.position);
                Destroy(gameObject);
            }
        }
           
    }
}
