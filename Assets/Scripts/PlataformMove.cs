using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMove : MonoBehaviour
{
    public Transform target;
    public float speed = 1.5f;
    Vector3 start, end;
    
    void Start()
    {
        start = transform.position;
        end = target.position;
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        if (transform.position == target.position)
        {
            if (target.position == start)
            {
                target.position = end;
            }
            else
            {
                target.position = start;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            collision.transform.parent = null;
        }
    }
}