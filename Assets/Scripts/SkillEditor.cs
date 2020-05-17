using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEditor : MonoBehaviour
{
    public static SkillEditor instance;

    [Header("스킬 정보")]
    [Header("스킬 이름")]           public string m_name;
    [Header("소모 활력")]           public int m_mana;
    [Header("유효거리(m)")]         public float m_range;
    [Header("시전시간(초)")]        public float m_castTime;
    [Header("재사용 대기시간(초)")] public float m_delayTime;
    [Header("요구 레벨")]           public int m_requiredLevel;
    [Header("스킬 설명")]           public string m_descryption;
    [Header("강화 포인트")]         public int m_specificPoint;

    public RectTransform currentSkill = null;

    void Awake ()
    {
        instance = this;
    }

    public void GetData(RectTransform _currentSkill)
    {
        currentSkill = _currentSkill;
        SkillInfo _skillInfo = currentSkill.GetComponent<SkillInfo>();

        m_name = _skillInfo.m_name;
        m_mana = _skillInfo.m_mana;
        m_range = _skillInfo.m_range;
        m_castTime = _skillInfo.m_castTime;
        m_delayTime = _skillInfo.m_delayTime;
        m_requiredLevel = _skillInfo.m_requiredLevel;
        m_descryption = _skillInfo.m_descryption;
        m_specificPoint = _skillInfo.m_specificPoint;
    }

    public void ApllyData()
    {
        currentSkill.GetComponent<SkillInfo>().SetData();
    }
}
