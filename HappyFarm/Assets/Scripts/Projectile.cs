using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float range;
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Vector3 targetPosition;
    private Rigidbody2D rb2D;

    private Vector3 direction;
   
    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();      
    }
    private void FixedUpdate()
    {
        MoveMent();
        float distance = Vector3.Distance(startPosition, transform.position);
        if (distance>range)
        {
            DestroyPrejectile();
        }
    }

    private void MoveMent()
    {
        direction = (targetPosition - startPosition).normalized;
        // Vector2 newPostion = Vector2.MoveTowards(rb2D.position, targetPosition, speed * Time.deltaTime);
        // rb2D.MovePosition(newPostion);
        rb2D.velocity = direction * speed;
    }

    abstract protected void DamageTarget(Collision2D collision);

    private void DestroyPrejectile()
    {
        Destroy(gameObject);
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        DamageTarget(collision);
        DestroyPrejectile();
    }
}
