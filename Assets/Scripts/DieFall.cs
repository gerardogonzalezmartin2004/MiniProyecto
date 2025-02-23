using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            player.GameOverScene();
        }
    }
}
