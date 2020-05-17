using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
    SkillType type;

    SkillTreeManager _SkillTreeManager;
    void Awake()
    {
        _SkillTreeManager = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();

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

	public void SelectSkillSet()
    {
        _SkillTreeManager.ClickSelectSkillSetButton(this.GetComponent<RectTransform>(), type);
    }

    public void Reset()
    {
        _SkillTreeManager.ClickReset(this.GetComponent<RectTransform>());
    }

    public void ChangeSkillSet()
    {
        _SkillTreeManager.ClickChangeSkillSet(this.GetComponent<RectTransform>());
    }

    /*
    public void SaveData()
    {
        _SkillTreeManager.SaveData();
    }
    */
}
