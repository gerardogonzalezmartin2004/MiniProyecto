using System.Collections;
using UnityEngine;

public class AreaSoundTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isPlayerInside = false; 

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.loop = true;  
            audioSource.Stop(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            isPlayerInside = true;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            isPlayerInside = false;
            StartCoroutine(StopSoundWithDelay(0.1f)); //  delay por seguridad
        }
    }

    private IEnumerator StopSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isPlayerInside && audioSource.isPlaying) 
        {
            audioSource.Stop();
        }
    }
}
