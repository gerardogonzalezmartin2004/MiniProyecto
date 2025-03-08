using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParalaxMovement : MonoBehaviour
{
    public Vector2 velocityMovement;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D rb;
    public LayerMask player;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;

        GameObject player = FindObjectOfType<PlayerMovement>()?.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
       

    }
    private void Update()
    {
        offset = (rb.velocity.x * 0.1f) * velocityMovement * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
 
}

