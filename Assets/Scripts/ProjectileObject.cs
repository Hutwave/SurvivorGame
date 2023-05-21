using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject
{
    public ProjectileType? projectileType;
    public bool? isExplosive;
    public bool? isPiercing;

    public float? damage;
    public float? explosionRadius;
    public float? range;
    public float? lifeTime;
    public float? speed;


    public ProjectileObject()
    {
        projectileType = ProjectileType.Directional;
        isExplosive = false;
        isPiercing = false;
        damage = 1f;
        explosionRadius = 0f;
        range = 100f;
        lifeTime = 5f;
        speed = 10f;

    }

    public ProjectileObject(ProjectileType ptype, bool explode, bool pierce, float dmg, float explodeRad, float rang, float lt, float spd)
    {
        projectileType = ptype;
        isExplosive = explode;
        isPiercing = pierce;
        damage = dmg;
        explosionRadius = explodeRad;
        range = rang;
        lifeTime = lt;
        speed = spd;
    }

    public void setProj(ProjectileType ptype, bool explode, bool pierce, float dmg)
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
}