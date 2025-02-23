using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapCeiling : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float distanceLine;
    public LayerMask playerMask;
    public LayerMask groundMask;
    public int trapDamage;


    private void Update()
    {
        RaycastHit2D infoPlayer = Physics2D.Raycast(transform.position, Vector3.down, distanceLine, playerMask);
        if (infoPlayer)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceLine);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (((1 << collision.gameObject.layer) & groundMask.value) != 0)  //  Convierte el número de capa en una máscara de bits y si el resultado es distinto de 0, significa que el objeto está en una capa permitida por groundMask
        {
            Destroy(gameObject);
        }

        if (player != null)
        {
            player.TakeDamage(trapDamage);
        }
    }
}
