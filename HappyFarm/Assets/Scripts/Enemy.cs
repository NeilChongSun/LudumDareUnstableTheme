using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float movementSpeed;
    public int maxHP;
    private int currentHP;
    public int damage;
    public float attackRange;

    public float attackInternal;
    public float attackAnimationInternal;

    private bool isInRange = false;
    private bool isAttadcking = false;

    private Coroutine seekCoroutine;
    private Coroutine updateAttackCoroutine;
    private Coroutine animationTimerCoroutine;

    protected GameObject prejectial;
    public GameObject prejectialPrefab;
    public GameObject player;
    private CircleCollider2D attackRangeCollider;
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponentInChildren<Rigidbody2D>();
        attackRangeCollider = GetComponentInChildren<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        attackRangeCollider.radius = attackRange;
        currentHP = maxHP;
        seekCoroutine = StartCoroutine(Seek());
    }

    private void FixedUpdate()
    {
        if (isInRange && !isAttadcking)
        {

            MoveToPlayer();
            if (animationTimerCoroutine != null)
            {
                StopCoroutine(animationTimerCoroutine);
            }
        }

        if (player.transform.position.x-rb2D.position.x>0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        if (isAttadcking)
        {
            animator.SetInteger("AnimationState", 1);
        }
        else
        {
            animator.SetInteger("AnimationState", 0);
        }
    }

    private void MoveToPlayer()
    {
        Vector2 newPostion = Vector2.MoveTowards(rb2D.position, player.transform.position, movementSpeed * Time.deltaTime);
        rb2D.MovePosition(newPostion);

    }

    virtual public void Attack()
    {
        isAttadcking = true;
        animationTimerCoroutine = StartCoroutine(AnimationTimer());
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(attackInternal);
        }
    }

    private IEnumerator Seek()
    {
        while (player != null)
        {
            MoveToPlayer();
            yield return new WaitForFixedUpdate();
        }
    }

    //Timer for attack animation, enemy stop moving
    private IEnumerator AnimationTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackAnimationInternal);
            isAttadcking = false; 
        }
    }

    public void DamageEnemy(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
            if (seekCoroutine != null)
            {
                StopCoroutine(seekCoroutine);
            }
            updateAttackCoroutine = StartCoroutine(UpdateAttack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = false;

            if (updateAttackCoroutine != null)
            {
                StopCoroutine(updateAttackCoroutine);
            }

            if (isAttadcking && animationTimerCoroutine != null)
            {
                StopCoroutine(animationTimerCoroutine);
                isAttadcking = false;
            }
            seekCoroutine = StartCoroutine(Seek());

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
