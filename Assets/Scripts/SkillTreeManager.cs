using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    List<SkillInfo> Battlerage;
    List<SkillInfo> Sorcery;
    List<SkillInfo> Archery;
    List<SkillInfo> Shadowplay;
    List<SkillInfo> Witchcraft;

    void Awake()
    {

    }

    void Update()
    {
        
    }

    // 스킬 리스트 초기화
    void InitializeSkillList()
    {
        Battlerage = new List<SkillInfo>();
        Sorcery = new List<SkillInfo>();
        Archery = new List<SkillInfo>();
        Shadowplay = new List<SkillInfo>();
        Witchcraft = new List<SkillInfo>();
    }

    // csv파일로 저장된 스킬정보들을 불러온다.
    void LoadSkillInfo()
    {

    }
}
