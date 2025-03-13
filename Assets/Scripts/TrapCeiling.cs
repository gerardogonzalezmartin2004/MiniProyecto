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

    public AudioClip fallingSound;  
    public AudioClip impactSound;
    private bool hasFallen = false; // Para evitar que el sonido de caída se reproduzca múltiples veces

    private void Update()
    {
        RaycastHit2D infoPlayer = Physics2D.Raycast(transform.position, Vector3.down, distanceLine, playerMask);
        if (infoPlayer && !hasFallen)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            hasFallen = true;
            AudioSource.PlayClipAtPoint(fallingSound, transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceLine);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerMovement>();
        if (((1 << other.gameObject.layer) & groundMask.value) != 0)  //  Convierte el número de capa en una máscara de bits y si el resultado es distinto de 0, significa que el objeto está en una capa permitida por groundMask
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
            Destroy(gameObject);
            
        }

        if (other.gameObject.TryGetComponent(out DestruirJugador destroyplayer))
        {
            destroyplayer.TakeDamageTrap();
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
            player.GameOverScene();
        }
    }
    
}
