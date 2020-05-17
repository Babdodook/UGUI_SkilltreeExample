using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    [Header("스킬 타입")]
    public SkillType m_type;

    [Header("스킬 정보")]
    [Header("이름")]        public string m_name;
    [Header("소모 활력")]   public int m_mana;
    [Header("유효거리")]    public float m_range;
    [Header("시전시간")]    public float m_castTime;
    [Header("재사용 대기시간")]    public float m_delayTime;
    [Header("요구 레벨")]   public int m_requiredLevel;
    [Header("스킬 설명")]   public string m_descryption;
    [Header("스킬 개방")]   public bool m_isUnLocked;
    [Header("지속 스킬")]   public bool m_isPassive;
    [Header("특정 조건 개방 스킬")] public int m_specificPoint;
    [Header("사용 중인 스킬")]  public bool m_isEnabled;

    public void ConvertData(DataSkillInfo info)
    {
        m_type = info.m_type;
        m_name = info.m_name;
        m_mana = info.m_mana;
        m_range = info.m_range;
        m_castTime = info.m_castTime;
        m_delayTime = info.m_delayTime;
        m_requiredLevel = info.m_requiredLevel;
        m_descryption = info.m_descryption;
        m_isUnLocked = info.m_isUnLocked;
        m_isPassive = info.m_isPassive;
        m_specificPoint = info.m_specificPoint;
        m_isEnabled = info.m_isEnabled;
    }

    public string GetTypeToString()
    {
        switch(m_type)
        {
            case SkillType.Battlerage:
                return "격투";
            case SkillType.Sorcery:
                return "마법";
            case SkillType.Archery:
                return "야성";
            case SkillType.Shadowplay:
                return "사명";
            case SkillType.Witchcraft:
                return "환술";
        }

        return null;
    }
}

