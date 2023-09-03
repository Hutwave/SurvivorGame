using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public int enemyDamage = 10;
    public float speed;

    public int exp;
    public int mesoDrop;

    private VictoriaMobNames poolKey;
    private Action<EnemyStats, VictoriaMobNames> returnToPool;
    private GameLogic gameLogic;

    public void returnToPoolAction(Action<EnemyStats, VictoriaMobNames> action)
    {
        returnToPool = action;
    }

    public void setStats(EnemyObject obj)
    {
        maxHealth = obj.health;
        speed = obj.speed;
        exp = obj.exp;
        mesoDrop = obj.meso;
    }

    public void damagePlayer()
    {
        gameLogic.takeDamage(enemyDamage);
    }

    public void setPool(VictoriaMobNames setPool)
    {
        poolKey = setPool;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameLogic = FindAnyObjectByType<GameLogic>();
    }

    public void takeDamage(int dmg)
    {
        GameObject dmgSkin = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/DamageSkin.prefab", typeof(GameObject));
        Transform dmgNumber = Instantiate(dmgSkin.transform, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+2.25f, gameObject.transform.position.z-0.5f), Quaternion.identity);
        dmgNumber.Rotate(0, 0, UnityEngine.Random.Range(-5, 25));
        dmgNumber.transform.GetComponent<TextMeshPro>().SetText(dmg.ToString());
        
        this.currentHealth -= dmg;
        if (currentHealth < 1)
        {
            enemyDie();
        }
    }

    public void enemyDie()
    {
        gameLogic.getEnemyDrops(new EnemyObject()
        {
            exp = this.exp,
            meso = this.mesoDrop
        });
        currentHealth = maxHealth;
        returnToPool(this, poolKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
