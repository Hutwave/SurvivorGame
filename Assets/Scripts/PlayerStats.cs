using System.Collections;
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
    public float intelligence { get; private set; }

    public int playerLevel { get; set; }
    // Other stats
    public float armor { get; private set; }
    public float attack { get; private set; }

    // Percentage stats
    public float allStatPerc { get; private set; }
    public float strPerc { get; private set; }
    public float dexPerc { get; private set; }
    public float intPerc { get; private set; }
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

        switch(pClass){
            case PlayerClass.Warrior:
                strength = 15;
                dexterity = 8;
                intelligence = 4;
                break;
            case PlayerClass.Bowman:
                strength = 8;
                dexterity = 15;
                intelligence = 4;
                break;
            case PlayerClass.Magician:
                strength = 4;
                dexterity = 8;
                intelligence = 15;
                break;
        }

        calculateStats();
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

    public int GetTotalAtt()
    {
        return Mathf.RoundToInt(attack * (1f + (0.01f * attPerc)));
    }

    public int GetTotalArmor()
    {
        return Mathf.RoundToInt(armor * (1f + (0.01f * armorPerc)));
    }

}
