using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SkillType
{
    Battlerage = 0,     // 격투
    Sorcery,            // 마법
    Archery,            // 야성
    Shadowplay,         // 사명
    Witchcraft,         // 환술

    Max
}

[System.Serializable]
public class SkillInfo
{
    [Header("스킬 타입")]
    public SkillType m_type;

    [Header("스킬 정보")]
    [Header("이름")]        public string m_name;
    [Header("소모 활력")]   public int m_mana;
    [Header("유효거리")]    public float m_range;
    [Header("시전시간")]    public float m_castTime;
    [Header("요구 레벨")]   public int m_requiredLevel;
    [Header("스킬 설명")]   public string m_descryption;
    [Header("스킬 개방")]   public bool m_isUnLocked;
    
    
    public SkillInfo()
    {
        m_type = SkillType.Max;
        m_name = "";
        m_range = 0;
        m_castTime = 0;
        m_requiredLevel = 0;
        m_isUnLocked = false;
    }

    public SkillInfo(   SkillType _type,
                        string _name,
                        int _mana,
                        float _range,
                        float _castTime,
                        int _requiredLevel,
                        string _descryption,
                        bool _isUnlocked    )
    {
        m_type = _type;
        m_name = _name;
        m_mana = _mana;
        m_range = _range;
        m_castTime = _castTime;
        m_requiredLevel = _requiredLevel;
        m_descryption = _descryption;
        m_isUnLocked = _isUnlocked;
    }
}

