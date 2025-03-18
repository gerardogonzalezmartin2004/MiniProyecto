using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider2D soundTrigger;
    private bool hasPlayed = false;


    void Start()
    {
       audioSource = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (!hasPlayed && player)
        {
            audioSource.Play();
            hasPlayed = true;
            soundTrigger.enabled = false;
        }
       
    }
}
