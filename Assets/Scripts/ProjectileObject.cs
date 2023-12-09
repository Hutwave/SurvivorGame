using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject
{
    public ProjectileType projectileType;
    public bool isExplosive;
    public bool isPiercing;

    public float damage;
    public float baseDamage;
    public float explosionRadius;
    public float range;
    public float lifeTime;
    public float speed;

    public int pierceCount;

    public GameObject projectileGameObject;

    public ProjectileObject()
    {
        projectileType = ProjectileType.Directional;
        isExplosive = false;
        isPiercing = false;
        damage = 1f;
        baseDamage = 1f;
        explosionRadius = 0f;
        range = 100f;
        lifeTime = 5f;
        speed = 10f;

    }

    public ProjectileObject(ProjectileType ptype, bool explode, bool pierce, float baseDmg, float explodeRad, float rang, float lt, float spd)
    {
        projectileType = ptype;
        isExplosive = explode;
        isPiercing = pierce;
        baseDamage = baseDmg;
        explosionRadius = explodeRad;
        range = rang;
        lifeTime = lt;
        speed = spd;
    }

    public void setProj(ProjectileType ptype, bool explode, bool pierce, float dmg, int pierceCount = 1, float radius = 10f)
    {
        switch (ptype)
        {
            case ProjectileType.Tracking:
                DefaultTracking(explode, pierce, dmg);
                break;
            case ProjectileType.Targeted:
                DefaultTargeted(explode, pierce, dmg);
                break;
            case ProjectileType.Directional:
                DefaultDirectional(explode, pierce, dmg);
                break;
            case ProjectileType.Pointed:
                DefaultPointed(explode, pierce, dmg);
                break;
            case ProjectileType.Chain:
                DefaultChain(pierceCount, radius, dmg);
                break;
        }
    }


    public void DefaultTracking(bool explode, bool pierce, float dmg)
    {
        projectileType = ProjectileType.Tracking;
        isExplosive = explode;
        isPiercing = pierce;
        damage = dmg;
        explosionRadius = 1f;
        range = 100f;
        lifeTime = 5f;
        speed = 10f;
    }

    public void DefaultTargeted(bool explode, bool pierce, float dmg)
    {

        projectileType = ProjectileType.Targeted;
        isExplosive = explode;
        isPiercing = pierce;
        damage = dmg;
        explosionRadius = 1f;
        range = 100f;
        lifeTime = 5f;
        speed = 15f;
    }

    public void DefaultDirectional(bool explode, bool pierce, float dmg)
    {
        projectileType = ProjectileType.Directional;
        isExplosive = explode;
        isPiercing = pierce;
        damage = dmg;
        explosionRadius = 1f;
        range = 100f;
        lifeTime = 5f;
        speed = 20f;
    }

    public void DefaultPointed(bool explode, bool pierce, float dmg)
    {
        projectileType = ProjectileType.Pointed;
        isExplosive = explode;
        isPiercing = pierce;
        damage = dmg;
        explosionRadius = 1f;
        range = 100f;
        lifeTime = 5f;
        speed = 8f;
    }

    public void DefaultChain(int pierce, float newRadius, float dmg)
    {
        projectileType = ProjectileType.Chain;
        isExplosive = false;
        isPiercing = true;
        damage = dmg;
        explosionRadius = 0.1f;
        range = 16f;
        lifeTime = 5f;
        speed = 60f;
        pierceCount = pierce-1;
        explosionRadius = newRadius;
    }
}