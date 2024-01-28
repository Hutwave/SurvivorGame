using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    Warrior,
    Bowman,
    Magician
}

public class PlayerStats : MonoBehaviour
{
    // Main stats
    public float strength { get; private set; }
    public float dexterity { get; private set; }
    public float intelligence { get; private set; } // SHORT WIS -> int is a type
    public float luck { get; private set; }

    public PlayerClass playerClass { get; private set; }
    public int playerLevel { get; set; }
    // Other stats
    public float armor { get; private set; }
    public float attack { get; private set; }

    // Percentage stats
    public float allStatPerc { get; private set; }
    public float strPerc { get; private set; }
    public float dexPerc { get; private set; }
    public float intPerc { get; private set; }

    public float lukPerc { get; private set; }
    public float attPerc { get; private set; }
    public float armorPerc { get; private set; }
    public float coolDownPerc { get; private set; }
    public float dmgPerc { get; private set; }
    public float aoePerc { get; private set; }

    // Critical stats
    public float critRate { get; private set; }
    public float critDmg { get; private set; }

    // Health and mana
    public float healthPoints { get; private set; }
    public float healthPerc { get; private set; }
    public float manaPoints { get; private set; }
    public float manaPerc { get; private set; }

    public void InitializeNewPlayer(PlayerClass pClass)
    {
        allStatPerc = 0;
        strPerc = 0;
        dexPerc = 0;
        intPerc = 0;
        lukPerc = 0;
        attPerc = 0;
        armorPerc = 0;
        coolDownPerc = 0;
        dmgPerc = 0;
        aoePerc = 0;
        critRate = 5;
        critDmg = 50;
        healthPoints = 50;
        healthPerc = 0;
        manaPoints = 0;
        manaPerc = 0;
        playerLevel = 1;
        playerClass = pClass;

        switch (pClass)
        {
            case PlayerClass.Warrior:
                strength = 20;
                dexterity = 8;
                intelligence = 4;
                luck = 4;
                break;
            case PlayerClass.Bowman:
                strength = 8;
                dexterity = 17;
                intelligence = 5;
                luck = 6;
                break;
            case PlayerClass.Magician:
                strength = 4;
                dexterity = 8;
                intelligence = 16;
                luck = 8;
                break;
        }
        calculateStats();
    }

    public void levelUp()
    {
        playerLevel++;
        switch (playerClass)
        {
            case PlayerClass.Warrior:
                strength += 3;
                dexterity += 1;
                if (playerLevel % 5 == 0) intelligence += 1;
                if (playerLevel % 3 == 0) luck += 1;
                break;
            case PlayerClass.Bowman:
                strength += 1;
                dexterity += 3;
                if (playerLevel % 4 == 0) intelligence += 1;
                if (playerLevel % 3 == 0) luck += 1;
                break;
            case PlayerClass.Magician:
                intelligence += 3;
                luck += 1;
                if (playerLevel % 5 == 0) strength += 1;
                if (playerLevel % 3 == 0) dexterity += 1;
                break;
        }
        Debug.LogWarning("Stats:" + string.Join(',', new List<string>() { strength.ToString(), dexterity.ToString(), intelligence.ToString(), luck.ToString() }));
    }

    public void addStats(int str, int dex, int wis, int luk)
    {
        strength += str;
        dexterity += dex;
        intelligence += wis;
        luck += luk;
        Debug.LogWarning("Stats:" + string.Join(',', new List<string>() { strength.ToString(), dexterity.ToString(), intelligence.ToString(), luck.ToString() }));
    }

    public void calculateStats()
    {

    }
    public int GetTotalStr()
    {
        return Mathf.RoundToInt(strength * (1f + (0.01f * (allStatPerc + strPerc))));
    }

    public int GetTotalDex()
    {
        return Mathf.RoundToInt(dexterity * (1f + (0.01f * (allStatPerc + dexPerc))));
    }

    public int GetTotalInt()
    {
        return Mathf.RoundToInt(intelligence * (1f + (0.01f * (allStatPerc + intPerc))));
    }
    public int GetTotalLuk()
    {
        return Mathf.RoundToInt(luck * (1f + (0.01f * (allStatPerc + lukPerc))));
    }

    public int GetTotalAtt()
    {
        return Mathf.RoundToInt(attack * (1f + (0.01f * attPerc)));
    }

    public int GetTotalArmor()
    {
        return Mathf.RoundToInt(armor * (1f + (0.01f * armorPerc)));
    }

}
