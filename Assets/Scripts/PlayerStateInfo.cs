using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInfo : MonoBehaviour
{
    [Header("최대 스킬 포인트")]        public int MaxSkillPoint;
    [Header("최소 기술 레벨")]          public int MinSkillLevel;
    [Header("사용 가능 기술 포인트")]   public int AvailableSkillPoint;
    [Header("사용한 기술 포인트")]      public int UsedSkillPoint;

    void Awake()
    {
        MaxSkillPoint = 20;
        AvailableSkillPoint = MaxSkillPoint;
        UsedSkillPoint = MaxSkillPoint - AvailableSkillPoint;
        MinSkillLevel = 0;
    }
}
