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
        CurrentSkillList = null;
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

    public List<DataSkillInfo> ConvertDataToJson()
    {
        List<DataSkillInfo> newList = new List<DataSkillInfo>();
        SkillInfo _skillInfo;

        for(int i=0 ; i<Skills.Length ; i++)
        {
            _skillInfo = Skills[i].GetComponent<SkillInfo>();

            CurrentSkillList[i].m_name = _skillInfo.m_name;
            CurrentSkillList[i].m_mana = _skillInfo.m_mana;
            CurrentSkillList[i].m_range = _skillInfo.m_range;
            CurrentSkillList[i].m_castTime = _skillInfo.m_castTime;
            CurrentSkillList[i].m_delayTime = _skillInfo.m_delayTime;
            CurrentSkillList[i].m_descryption = _skillInfo.m_descryption;
            CurrentSkillList[i].m_requiredLevel = _skillInfo.m_requiredLevel;
            CurrentSkillList[i].m_specificPoint = _skillInfo.m_specificPoint;
        }

        return CurrentSkillList;
    }

    public SkillType GetCurrentSkillListType()
    {
        if(Skills != null)
        {
            return Skills[0].GetComponent<SkillInfo>().m_type;
        }
        else
            return SkillType.Max;
    }
}
