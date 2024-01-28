using UnityEditor;
using UnityEngine;

public static class Mage1st
{
    public static ProjectileSkill ChainLightning()
    {
        var skill = new ProjectileSkill();
        skill.name = "ChainLightning";
        var ob = new ProjectileObject();
        skill.coolDown = 1f;
        ob.setProj(ProjectileType.Chain, false, true, 7, 7, 1f);
        skill.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/ChainLightning.gif", typeof(Sprite));
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/ChainLightning/ChainLightning.prefab", typeof(GameObject));
        ob.baseDamage = 3f;
        skill.po = ob;
        return skill;
    }

    public static ProjectileSkill FireBall()
    {
        var skill = new ProjectileSkill();
        skill.name = "FireBall";
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Targeted, true, false, 6f);
        skill.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/HolySymbol.gif", typeof(Sprite));
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/FireBallProjectile.prefab", typeof(GameObject));
        ob.baseDamage = 5f;
        skill.po = ob;
        return skill;
    }

    public static ProjectileSkill EnergyBolt()
    {
        var skill = new ProjectileSkill();
        skill.coolDown = 1.5f;
        skill.name = "EnergyBolt";
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Chain, true, false, 6f);
        skill.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/EnergyBolt.png", typeof(Sprite));
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/EnergyBoltProjectile.prefab", typeof(GameObject));
        skill.po = ob;
        return skill;
    }

    public static ProjectileSkill ColdBeam()
    {
        var skill = new ProjectileSkill();
        skill.coolDown = 7f;
        skill.name = "ColdBeam";
        var ob = new ProjectileObject();
        ob.setProj(ProjectileType.Pointed, true, false, 6f);
        skill.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/HolySymbol.gif", typeof(Sprite));
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/EnergyBoltProjectile.prefab", typeof(GameObject));
        skill.po = ob;
        return skill;
    }

    public static ArcSkill Explosion()
    {
        var arcSkill = new ArcSkill();
        arcSkill.name = "Explosion";
        arcSkill.attackArc = 360;
        arcSkill.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/Explosion.png", typeof(Sprite));
        arcSkill.damage = 40f;
        arcSkill.attackRange = 5;
        arcSkill.coolDown = 15f;
        return arcSkill;
    }

    public static InstantSkill IceStrike()
    {
        var ob = new InstantSkill();
        ob.coolDown = 4f;
        ob.name = "IceStrike";
        ob.baseDamage = 11f;
        ob.DefaultOneHit(3, 40f, 1f);
        ob.skillIcon = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Skills/MagePictures/IceStrike.png", typeof(Sprite));
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/IceStrike.prefab", typeof(GameObject));
        return ob;
    }
}


