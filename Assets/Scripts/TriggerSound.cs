using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider2D soundTrigger;


    void Start()
    {
       audioSource = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
       audioSource.Play();
       soundTrigger.enabled = false;
    }
}
