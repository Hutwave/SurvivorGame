using UnityEngine;

public class InstantSkill : Skill
{
    public ProjectileType projectileType;

    public int maxEnemiesHit;
    public float baseDamage;
    public float explosionRadius;
    public float lifeTime;
    public float damageDelay;

    public GameObject projectileGameObject;

    public InstantSkill()
    {
        baseDamage = 1f;
        explosionRadius = 1f;
        lifeTime = 5f;
        maxEnemiesHit = 15;
    }

    public InstantSkill(int enemiesHit, float baseDmg, float explodeRad, float lt)
    {
        maxEnemiesHit = enemiesHit;
        baseDamage = baseDmg;
        explosionRadius = explodeRad;
        lifeTime = lt;
    }

    public void DefaultOneHit(float baseDmg, float explodeRad, float dmgDelay)
    {
        maxEnemiesHit = 15;
        baseDamage = baseDmg;
        explosionRadius = explodeRad;
        lifeTime = 0f;
        damageDelay = dmgDelay;
    }

    public void DefaultTimed(float baseDmg, float explodeRad, float dmgDelay)
    {
        maxEnemiesHit = 15;
        baseDamage = baseDmg;
        explosionRadius = explodeRad;
        lifeTime = 5f;
        damageDelay = dmgDelay;
    }
}