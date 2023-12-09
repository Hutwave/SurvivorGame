using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MageSkills
{

    
    public BuffSkill MpEater;
    public BuffSkill ArcaneOverdrive;
    public BuffSkill Infinity;
    public BuffSkill MagicShell;

    public ProjectileSkill EnergyBolt;
    public ProjectileSkill IceDemon;
    public ProjectileSkill FireArrow()
    {
        ProjectileSkill tempSkill = new ProjectileSkill();
        var ob = new ProjectileObject();

        tempSkill.damage = 1f;
        tempSkill.manaCost = 20f;

        ob.setProj(ProjectileType.Targeted, true, false, tempSkill.damage);
        ob.projectileGameObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Skills/Mage/EnergyBoltProjectile.prefab", typeof(GameObject));
        tempSkill.po = ob;
        return tempSkill;
    }
    public ProjectileSkill FrozenOrb;
    public ProjectileSkill ChainLighting;

    public InstantSkill ShiningRay;
    public InstantSkill LightningBolt;
    public InstantSkill MeteorShower;
    public InstantSkill Explosion;

    public SummonSkill Elquines;
    public SummonSkill Ifrit;

    public Skill Teleport;
    public BuffSkill Meditation()
    {
        BuffSkill tempSkill = new BuffSkill();
        tempSkill.boostAtt = 4;
        tempSkill.boostHpPerc = 7;
        return tempSkill;
    }


    private int hsLevel(int skillLevel)
    {
        return skillLevel * 2;
    }

    private int[] jokuNormi = new int[10] { 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
    
    private int mwLevel(int skillLevel)
    {
        return 10 + (skillLevel * 2);
    }


    public BuffSkill MapleWarrior(int skillLevel)
    {
        BuffSkill mapleWarrior = new BuffSkill();
        mapleWarrior.boostByLevel = new int[30] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30 };
        mapleWarrior.boostStrPerc = 1;
        mapleWarrior.boostDexPerc = 1;
        mapleWarrior.boostIntPerc = 1;
        mapleWarrior.boostLukPerc = 1;
        mapleWarrior.GetDuration = x => { return 30 + x*9; }; // max 300
        return mapleWarrior;
    }

    private BuffSkill HolySymbol(int skillLevel)
    {
        BuffSkill holySymbol = new BuffSkill();

        holySymbol.boostByLevel = new int[10] { 10, 12, 14, 16, 18, 20, 24, 27, 30, 35 };
        holySymbol.boostExpPerc = 1;
        return holySymbol;
    }
    // exp%

    private Skill HyperBody;
    private Skill FuryUnleashed;
    // hp%, flat att, bossdmg%

    private Skill SharpEyes;
    private Skill Haste;
    //critrate%, critdmg%, evasion, speed

    private Skill Bless;
    private Skill MpRecovery;
    //mp, mp recovery, cooldown%, dmg%



    public List<Skill> GetSkills(PlayerStats playerStats)
    {
        return new List<Skill>() { MapleWarrior(playerStats.playerLevel), HolySymbol(playerStats.playerLevel) };
    }


}