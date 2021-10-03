using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Projectile
{
    protected override void DamageTarget(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(damage);
        }
    }
}
