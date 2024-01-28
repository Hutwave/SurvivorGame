using UnityEngine;

public class ExplosionCheck : MonoBehaviour
{

    private float damage;
    private float radius;
    private float damageDelay;
    private bool exploded;
    Light[] testLights;

    public void setDmg(float dmg, float dmgRadius, float dmgDelay = 0f)
    {
        exploded = false;
        damage = dmg;
        radius = dmgRadius;
        damageDelay = dmgDelay;
        Explode();
    }

    public void Awake()
    {
        testLights = this.gameObject.GetComponentsInChildren<UnityEngine.Light>();
    }

    public void Explode()
    {
        if (damageDelay < 0f)
        {
            exploded = true;
            var sphere = Physics.OverlapSphere(this.gameObject.transform.position, radius, LayerMask.GetMask("Enemy"));
            foreach (var enemy in sphere)
            {
                if (enemy.gameObject.tag == "Enemy")
                {
                    enemy.transform.GetComponent<EnemyStats>().takeDamage(Mathf.RoundToInt(damage));
                }
            }
        }
    }

    public void Update()
    {
        if (this.GetComponentInChildren<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }
        foreach (Light oneLight in testLights)
        {
            oneLight.intensity -= float.Parse(oneLight.name.Split('-')[1]);
        }
        damageDelay -= (1f * Time.deltaTime);
        if (!exploded && damageDelay < 0f)
        {
            Explode();
        }
    }
}
