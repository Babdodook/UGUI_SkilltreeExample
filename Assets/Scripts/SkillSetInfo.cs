using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetInfo : MonoBehaviour
{
    public List<DataSkillInfo> CurrentSkillList;
    public RectTransform rectTransform;
    public Sprite[] ActiveSkills = null;
    public Sprite[] PassiveSkills = null;
    public RectTransform[] Skills = null;
    int CurrentSkillPoint;

    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        CurrentSkillList = new List<DataSkillInfo>();
        this.gameObject.SetActive(false);
    }

    // 액티브 스킬 중에서 몇개나 활성화 하였는지 반환
    public int GetCurrentSkillPoint()
    {
        SkillInfo _skillInfo;
        CurrentSkillPoint = 0;

        for(int i=0 ; i<Skills.Length ; i++)
        {
            _skillInfo = Skills[i].GetComponent<SkillInfo>();
            if(!_skillInfo.m_isPassive && _skillInfo.m_isEnabled)
                CurrentSkillPoint++;
        }

        return CurrentSkillPoint;
    }
}
