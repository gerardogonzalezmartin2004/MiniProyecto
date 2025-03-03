using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    
    public int pointsToAdd = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("El jugador ha tocado el objeto");
            player.AddPoints(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
