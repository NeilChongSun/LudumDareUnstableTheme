using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoAttackEnemy : Enemy
{
    public override void Attack()
    {
        base.Attack();
        prejectial = Instantiate(prejectialPrefab, transform.position, Quaternion.identity);

        prejectial.GetComponent<Projectile>().damage = transform.GetComponent<Enemy>().damage;
        prejectial.GetComponent<Projectile>().range = transform.GetComponent<Enemy>().attackRange;
        prejectial.GetComponent<Projectile>().startPosition = transform.position;
        prejectial.GetComponent<Projectile>().targetPosition = gameObject.GetComponent<Enemy>().player.transform.position;
    }
}
