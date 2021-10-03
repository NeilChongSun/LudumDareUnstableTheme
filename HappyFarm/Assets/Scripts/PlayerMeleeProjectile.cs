using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeProjectile : Projectile
{
    protected override void DamageTarget(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().DamageEnemy(damage);
        }
    }
}
