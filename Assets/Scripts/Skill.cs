using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Instant,
    Projectile,
    Arc,
    Buff,
    Summon
}

public class Skill
{
    public SkillType skilltype;

    public int skillLevel;
    public string name;
    public float damage;
    public float coolDown;
    public float manaCost;
    public float dotDamage;
    public float dotTime;

    public float[] damageByLevel;
    public float[] valueByLevel;
    public float[] cdByLevel;
    public float[] manaCostByLevel;
    public float[] dotDamageByLevel;

    public delegate int getDamage(int skillLvl);
    public Func<int, int> GetDuration;
}

public class InstantSkill : Skill
{
    public SkillType skillType = SkillType.Instant;
    public GameObject effectWow;
}

public class ProjectileSkill : Skill
{
    public SkillType skillType = SkillType.Projectile;
    public ProjectileObject po;
}

public class ArcSkill : Skill
{
    public SkillType skillType = SkillType.Arc;
    public GameObject effectWow;
    public int attackArc; // 45-180 astetta varmaa
    public int attackRange;
    public float[] arcByLevel;
    public float[] rangeByLevel;
}

public class BuffSkill : Skill
{
    public SkillType skillType = SkillType.Buff;

    public int duration;
    public int[] durationByLevel;
    public int[] boostByLevel;
    public int boostStr;
    public int boostDex;
    public int boostInt;
    public int boostLuk;

    public float boostStrPerc;
    public float boostDexPerc;
    public float boostIntPerc;
    public float boostLukPerc;

    public int boostAtt;
    public int boostAttPerc;

    public int boostHp;
    public int boostMp;
    public int boostHpPerc;
    public int boostMpPerc;

    public int boostHpRegen;
    public int boostMpRegen;
    public int boostHpRegenPerc;
    public int boostMpRegenPerc;

    public int boostDropRatePerc;
    public int boostMesoPerc;
    public int boostExpPerc;
}

public class SummonSkill : Skill // Maybe use BuffSkill at sametime if needed.
{
    public SkillType skillType = SkillType.Summon;
    public GameObject summon;
    public string targetType; // closest, strongest, any ranged

}