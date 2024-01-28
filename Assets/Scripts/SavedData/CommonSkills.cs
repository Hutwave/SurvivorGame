using System.Collections.Generic;

public class CommonSkills
{


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
        mapleWarrior.boostByLevel = new int[30] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        mapleWarrior.boostStrPerc = 1;
        mapleWarrior.boostDexPerc = 1;
        mapleWarrior.boostIntPerc = 1;
        mapleWarrior.boostLukPerc = 1;
        mapleWarrior.GetDuration = x => { return 30 + x * 9; }; // max 300
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