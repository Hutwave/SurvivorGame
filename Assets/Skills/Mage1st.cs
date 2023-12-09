using UnityEditor;
using UnityEngine;

public static class Mage1st
{
    public static ProjectileObject ChainLightning()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Chain, false, true, 13, 7, 1f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/ChainLightning/ChainLightning.prefab", typeof(GameObject));
        return ob;
    }

    public static ProjectileObject FireBall()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Targeted, true, false, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/FireBallProjectile.prefab", typeof(GameObject));
        return ob;
    }

    public static ProjectileObject EnergyBolt()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Chain, true, false, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/EnergyBoltProjectile.prefab", typeof(GameObject));
        return ob;
    }


    
    public static ProjectileObject ColdBeam()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Pointed, true, false, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/EnergyBoltProjectile.prefab", typeof(GameObject));
        return ob;
    }

    public static ArcSkill Explosion()
    {
        var arcSkill = new ArcSkill();
        arcSkill.attackArc = 360;
        arcSkill.damage = 40;
        arcSkill.attackRange = 5;
        return arcSkill;
    }
}


