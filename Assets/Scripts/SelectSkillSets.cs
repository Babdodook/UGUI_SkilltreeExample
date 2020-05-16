using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillSets : MonoBehaviour
{

    public SkillType type;

    SkillTreeManager manager;
    void Awake()
    {
        manager = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();

        switch(this.gameObject.name)
        {
            case "Battlerage":
                type = SkillType.Battlerage;
                break;
            case "Sorcery":
                type = SkillType.Sorcery;
                break;
            case "Archery":
                type = SkillType.Archery;
                break;
            case "Shadowplay":
                type = SkillType.Shadowplay;
                break;
            case "Witchcraft":
                type = SkillType.Witchcraft;
                break;            
        }
    }

	public void ClickButton()
    {
        manager.ClickSelectSkillSetButton(this.GetComponent<RectTransform>(), type);
    }
}
