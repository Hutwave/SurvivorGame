using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public int enemyDamage = 10;

    public int exp;
    public int mesoDrop;

    private HazardGen gameManager;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindAnyObjectByType<HazardGen>();
    }

    public void takeDamage(int dmg)
    {
        this.currentHealth -= dmg;
        if (currentHealth < 1)
        {
            enemyDie();
        }
    }

    public void enemyDie()
    {

        gameManager.getExp(exp);
        // anna mesoja
        Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
