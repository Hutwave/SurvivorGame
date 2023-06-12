using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject
{
    public bool staysAway;
    public float holdRange;

    public int health;
    public int exp;
    public int meso;
    public float speed;

    public float touchDamage;
    public float skillDamage;
    public float skillRange;

    public string element;


    public EnemyObject()
    {
        staysAway = false;
        holdRange = 15f;

        health = 50;
        exp = 10;
        meso = 10;
        speed = 5f;

        touchDamage = 10f;
        skillDamage = 5f;   
        skillRange = 20f;

        element = "Fire"; // enum jos joskus käyttöön
    }

    public EnemyObject(bool staysAway, float holdRange, int health, int exp, int meso, float speed, float touchDmg, float skillDmg, float skillRange)
    {
        this.staysAway = staysAway;
        this.holdRange = holdRange;

        this.health = health;
        this.exp = exp;
        this.meso = meso;
        this.speed = speed;

        this.touchDamage = touchDmg;
        this.skillDamage = skillDmg;
        this.skillRange = skillRange;

        this.element = "Fire";
    }
}