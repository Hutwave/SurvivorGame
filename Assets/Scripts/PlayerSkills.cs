using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    Dictionary<Skill, int> playerSkillLevels;
    PlayerClass playerClass;

    // Start is called before the first frame update
    public List<Skill> getPlayerSkills()
    {
        List<Skill> playerSkills = new List<Skill>();
        // Load commons
        switch (playerClass)
        {
            case PlayerClass.Warrior:
                // Load warrior
                break;
            case PlayerClass.Bowman:
                // Load bowman
                break;
            case PlayerClass.Magician:
                // Load magician
                break;
        }
        return playerSkills;
    }
}
