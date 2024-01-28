using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SkillBar : MonoBehaviour
{

    List<Transform> cdElements = new List<Transform>();

    public void Initialize(List<Skill> skillList)
    {
        bool first = true;
        var skillIconObject = gameObject.transform.GetChild(0);

        foreach (Skill skill in skillList)
        {
            if (first)
            {
                first = false;
                skillIconObject.GetComponent<Image>().sprite = skill.skillIcon;
                skillIconObject.name = skill.name;
                cdElements.Add(skillIconObject);
            }
            else
            {
                var newUI = Instantiate(skillIconObject);
                cdElements.Add(newUI);
                newUI.GetComponent<Image>().sprite = skill.skillIcon;
                newUI.name = skill.name;
                newUI.transform.SetParent(this.gameObject.transform);
            }
        }
    }

    public void updateCooldowns(List<Skill> skillList)
    {
        foreach (Skill skill in skillList)
        {
            cdElements.First(x => x.name == skill.name).GetChild(0).GetComponent<Image>().fillAmount = skill.currentCooldown / skill.coolDown;
        }
    }
}
