using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP;
    [SerializeField]
    private int currentHP;
    public int damage;
    public float movementSpeed = 10;

    public GameObject projectile;

    private KeyCodeSet keyCodeSet;
    private Vector2 movement = new Vector2();
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    private void Update()
    {
        keyCodeSet = KeyCodeManager.instance.currentKeyCodeSet;
        if (Input.GetKey((KeyCode)keyCodeSet.meleeAttackKey))
        {
            MeleeAttack();
        }

        if (Input.GetKey((KeyCode)keyCodeSet.rangeAttackKey))
        {
            RangeAttack();
        }
    }

    private void FixedUpdate()
    {
        if (rb2D != null&&keyCodeSet!=null)
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
        rb2D.velocity = movement * movementSpeed;
    }

    private void MeleeAttack()
    {
        
    }

    private void RangeAttack()
    {

    }

    public void DamagePlayer(int damage)
    {
        currentHP -= damage;

        if (currentHP<=0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }
}
