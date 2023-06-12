using UnityEditor;
using UnityEngine;

public static class Mage1st
{
    public static ProjectileObject EnergyBolt()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Tracking, true, true, 12f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/EnergyBolt.prefab", typeof(GameObject));
        return ob;
    }

    public static ProjectileObject FireBall()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Directional, true, false, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/EnergyBolt.prefab", typeof(GameObject));
        return ob;
    }

    public static ProjectileObject HolyArrow()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Targeted, true, false, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/EnergyBolt.prefab", typeof(GameObject));
        return ob;
    }
    
    public static ProjectileObject ColdBeam()
    {
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Pointed, true, true, 6f);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/EnergyBolt.prefab", typeof(GameObject));
        return ob;
    }
}


