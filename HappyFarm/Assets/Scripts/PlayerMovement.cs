using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10;
    private KeyCodeSet keyCodeSet;
    private Vector2 movement = new Vector2();
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        keyCodeSet = KeyCodeManager.instance.currentKeyCodeSet;
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
}
