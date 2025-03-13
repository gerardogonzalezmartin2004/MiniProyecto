using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    public int pocion = 1;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("daño :" + pocion);
            player.Heal(pocion);
            Destroy(gameObject);
        }
    }
}
