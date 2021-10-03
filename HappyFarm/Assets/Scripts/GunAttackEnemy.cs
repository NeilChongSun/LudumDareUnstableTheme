using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackEnemy : Enemy
{
    public override void Attack()
    {
        base.Attack();
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.GetComponent<Projectile>().damage = transform.GetComponent<Enemy>().damage;
        projectile.GetComponent<Projectile>().range = transform.GetComponent<Enemy>().attackRange;
        projectile.GetComponent<Projectile>().startPosition = transform.position;
        projectile.GetComponent<Projectile>().targetPosition = gameObject.GetComponent<Enemy>().player.transform.position;
    }
}
