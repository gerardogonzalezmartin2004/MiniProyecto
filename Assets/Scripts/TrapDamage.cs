using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damageAmountPerSecond = 1;  
    public float damageInterval = 1f;      
    public bool isDamaging = false;      
    private PlayerMovement player;        

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null && !isDamaging)
        {
            StartCoroutine(DamagePlayerOverTime());
        }
    }

    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (player != null)
        {
            StopDamaging();
        }
    }

   
    IEnumerator DamagePlayerOverTime()
    {
        isDamaging = true;
        while (isDamaging && player != null)
        {
            player.TakeDamage(damageAmountPerSecond);
            yield return new WaitForSeconds(damageInterval);
        }
    }

      void StopDamaging()
      {       isDamaging = false;
        StopCoroutine(DamagePlayerOverTime());
      }
}