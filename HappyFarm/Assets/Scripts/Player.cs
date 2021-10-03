using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    public int maxHP;
    [SerializeField]
    private int currentHP;
    public int damage;
    public float movementSpeed = 10;
    public float meleeRange;
    public float rangeRange;

    public GameObject projectilePrefab;
    private GameObject projectile;

    private KeyCodeSet keyCodeSet;
    private Vector2 movement = new Vector2();
    private Rigidbody2D rb2D;
    private bool isAttacking;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        isAttacking = false;
    }

    private void Update()
    {
        keyCodeSet = KeyCodeManager.instance.currentKeyCodeSet;
        if (Input.GetKeyDown((KeyCode)keyCodeSet.meleeAttackKey))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown((KeyCode)keyCodeSet.rangeAttackKey))
        {
            RangeAttack();
        }
        UpdateState();
    }

    private void FixedUpdate()
    {
        if (rb2D != null && keyCodeSet != null)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        movement = Vector2.zero;
        if (Input.GetKey((KeyCode)keyCodeSet.upKey))
        {
            movement.y = 1;
        }
        if (Input.GetKey((KeyCode)keyCodeSet.downKey))
        {
            movement.y = -1;
        }
        if (Input.GetKey((KeyCode)keyCodeSet.leftKey))
        {
            movement.x = -1;
        }
        if (Input.GetKey((KeyCode)keyCodeSet.rightKey))
        {
            movement.x = 1;
        }

        if (!isAttacking)
        {
            rb2D.velocity = movement * movementSpeed;
        }
    }

    private void UpdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void MeleeAttack()
    {
        animator.SetFloat("attackX", movement.x);
        animator.SetFloat("attackY", movement.y);
        animator.SetTrigger("isAttacking");
    }

    private void RangeAttack()
    {
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().damage = transform.GetComponent<Player>().damage;
        projectile.GetComponent<Projectile>().range = transform.GetComponent<Player>().rangeRange;
        projectile.GetComponent<Projectile>().startPosition = transform.position;
        projectile.GetComponent<Projectile>().speed = movementSpeed * 2;
        Vector3 direction = new Vector3(0, 0, 0);
        if (movement == Vector2.zero)
        {
            direction = new Vector3(-1, movement.y, 0);
        }
        else
        {
            direction = new Vector3(movement.x, movement.y, 0);
        }
        projectile.GetComponent<Projectile>().targetPosition = transform.position + direction * rangeRange;
    }

    public void DamagePlayer(int damage)
    {
        animator.SetTrigger("isAttacked");
        currentHP -= damage;

        if (currentHP <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeRange);
    }
}
