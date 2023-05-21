using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCheck : MonoBehaviour
{

    private float damage;
    private float radius;

    public void setDmg(float dmg, float dmgRadius)
    {
        damage = dmg;
        radius = dmgRadius;
        Explode();
    }

    public void Explode()
    {
        var sphere = Physics.OverlapSphere(this.gameObject.transform.position, radius);
        foreach(var enemy in sphere)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemy.transform.GetComponent<EnemyStats>().takeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

}
