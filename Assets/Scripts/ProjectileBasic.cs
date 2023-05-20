using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private float damage;

    public GameObject explosion;

    private float explosionRadius;

    private float poison = 0;
    private float slow = 0;
    private float fire = 0;
    private float weakened = 0;
    private float regen = 0;
    private float hardened = 0;

    // Update is called once per frame
    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            HitTarget();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void setDamageAndRadius(float dmg, float radius)
    {
        damage = dmg;
        explosionRadius = radius;
    }

    private void HitTarget()
    {
        /*RunnerHealth hp = target.GetComponent<RunnerHealth>();
        if (poison > 0)
        {
            hp.addPoison(100);
        }
        if (slow > 0)
        {
            hp.addSlow(30);
        }
        hp.TakeDamage(damage);
        */

        var smallExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        smallExplosion.GetComponent<ExplosionCheck>().setDmg(damage, explosionRadius);
        Destroy(this.gameObject);
        return;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    /*
   public void addStatus(statusEnums statusEnum, float amount)
   {

       switch (statusEnum)
       {
           case statusEnums.Poison:
               poison = amount;
               break;
           case statusEnums.Fire:
               fire = amount;
               break;
           case statusEnums.Slow:
               slow = amount;
               break;
           case statusEnums.Weakened:
               weakened = amount;
               break;
           case statusEnums.Regen:
               regen = amount;
               break;
           case statusEnums.Hardened:
               hardened = amount;
               break;
           default:
               return;
       }
   }
   */
}
