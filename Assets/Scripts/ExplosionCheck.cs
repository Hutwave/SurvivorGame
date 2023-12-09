using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCheck : MonoBehaviour
{

    private float damage;
    private float radius;
    Light[] testLights;

    public void setDmg(float dmg, float dmgRadius)
    {
        damage = dmg;
        radius = dmgRadius;
        Explode();
    }

    public void Awake()
    {
        testLights = this.gameObject.GetComponentsInChildren<UnityEngine.Light>();
    }

    public void Explode()
    {
        var sphere = Physics.OverlapSphere(this.gameObject.transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (var enemy in sphere)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemy.transform.GetComponent<EnemyStats>().takeDamage(Mathf.RoundToInt(damage));
            }
        }
    }

    public void Update()
    {
        if (this.GetComponentInChildren<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }
        foreach(Light oneLight in testLights)
        {
            oneLight.intensity -= float.Parse(oneLight.name.Split('-')[1]);
        }
    }
}
