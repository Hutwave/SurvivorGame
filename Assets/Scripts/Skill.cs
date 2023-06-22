using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int skillLevel;
    public string name;
    public float damage;
    public ProjectileObject po;

    public float coolDown;
    public float manaCost;

    public float[] damageByLevel;
    public float[] valueByLevel;
    public float[] cdByLevel;
    public float[] manaCostByLevel;

    public int boostInt;
    public int boostIntPerc;
    public int boostExp;

}
