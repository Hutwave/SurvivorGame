using UnityEditor;
using UnityEngine;


public static class VictoriaMobs
{
    public static GameObject getEnemy(VictoriaMobNames mobName)
    {
        // EnemyObject: StaysAway / HoldRange / Health / Exp / Speed / TouchDmg / SkillDmg / SkillRange

        EnemyObject tempEnemy = mobName switch
        {
            VictoriaMobNames.Snail => new EnemyObject(false, 0f, 8, 3, 10, 3f, 1, 0, 0),
            VictoriaMobNames.Blue_Snail => new EnemyObject(false, 0f, 15, 5, 10, 4f, 3, 0, 0),
            VictoriaMobNames.Red_Snail => new EnemyObject(false, 0f, 35, 8, 10, 5f, 8, 0, 0),
            VictoriaMobNames.Slime => new EnemyObject(false, 0f, 50, 12, 10, 8f, 12, 0, 0),
            VictoriaMobNames.Bubbling => new EnemyObject(false, 0f, 125, 25, 10, 9f, 35, 0, 0),
            VictoriaMobNames.Stump => new EnemyObject(false, 0f, 120, 18, 10, 4.5f, 22, 0, 0),
            VictoriaMobNames.Dark_Stump => new EnemyObject(false, 0f, 240, 33, 10, 5f, 42, 0, 0),
            VictoriaMobNames.Orange_Mushroom => new EnemyObject(false, 0f, 50, 18, 10, 8.5f, 17, 0, 0),
            VictoriaMobNames.Green_Mushroom => new EnemyObject(false, 0f, 150, 30, 10, 6f, 25, 0, 0),
            VictoriaMobNames.Horned_Mushroom => new EnemyObject(false, 0f, 180, 35, 10, 6f, 32, 0, 0),
            _ => throw new System.NotImplementedException()
        };

        GameObject enemyGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Mobs/{mobName}.prefab", typeof(GameObject));
        if (!enemyGameObject.TryGetComponent<EnemyStats>(out _))
        {
            enemyGameObject.AddComponent<EnemyStats>();
        }

        EnemyStats enemy = enemyGameObject.GetComponent<EnemyStats>();
        enemy.setStats(tempEnemy);
        return enemyGameObject;

    }
}

public enum VictoriaMobNames
{
    Snail,
    Blue_Snail,
    Red_Snail,
    Slime,
    Bubbling,
    Stump,
    Dark_Stump,
    Orange_Mushroom,
    Green_Mushroom,
    Horned_Mushroom
}

// Map jutut kans tänne?