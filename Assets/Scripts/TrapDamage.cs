using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int trapDamage = 1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
      

        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("daño :" + trapDamage);
            player.TakeDamage(trapDamage);
          
        }
    }
}
