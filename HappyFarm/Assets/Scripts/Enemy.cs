using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float movementSpeed;
    public int maxHP;
    public int currentHP;
    public int damage;
    public float attackRange;
    public float attactInternal;
    public float attackAnimatingTime;

    public GameObject player;

    private CircleCollider2D attackRangeCollider;

    private Rigidbody2D rb2D;
    private float timer;
    private bool isAttacking = false;
    private Coroutine seekCoroutine;
    private Coroutine attackingCoroutine;


    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
        attackRangeCollider = GetComponent<CircleCollider2D>();

        currentHP = maxHP;
        timer = attactInternal;

        seekCoroutine = StartCoroutine(Seek());
    }

    private void Update()
    {
        if (timer <= 0)
        {
            isAttacking = true;
            Attack();
            if (attackAnimatingTime <= 0)
            {
                isAttacking = false;
            }
            timer = attactInternal;
        }

        attackRangeCollider.radius = attackRange;
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 newPostion = Vector2.MoveTowards(rb2D.position, player.transform.position, movementSpeed * Time.deltaTime);
        rb2D.MovePosition(newPostion);
    }

    abstract public void Attack();

    private IEnumerator Seek()
    {
        while (player != null)
        {
            MoveToPlayer();
            yield return new WaitForFixedUpdate();
        }
    }

    //Player is in attack range, prepare to attack player.
    private IEnumerator Attacking()
    {
        while (true)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator AttackRemainTimer()
    {
        while (isAttacking)
        {
            attackAnimatingTime--;
            yield return new WaitForSeconds(attackAnimatingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (seekCoroutine != null)
            {
                StopCoroutine(seekCoroutine);
            }
            attackingCoroutine = StartCoroutine(Attacking());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (attackingCoroutine != null)
            {
                StopCoroutine(attackingCoroutine);
            }
            seekCoroutine= StartCoroutine(Seek());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
