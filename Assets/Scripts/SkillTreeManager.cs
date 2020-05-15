using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;

public class SkillTreeManager : MonoBehaviour
{
    public Transform[] SelectSkillSet = new Transform[3];
    public Transform[] SkillSet = new Transform[3];

    List<SkillInfo> Battlerage;
    List<SkillInfo> Sorcery;
    List<SkillInfo> Archery;
    List<SkillInfo> Shadowplay;
    List<SkillInfo> Witchcraft;

    void Awake()
    {

        InitializeSkillList();

        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "3단 베기", 12, 4, 0, 0, 1, "빠르게 적을 베어 공격하며 연속으로 사용 시 피해가 증가합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "돌격", 8, 12, 0, 12, 3, "순간적으로 적에게 돌격하여 공격합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "가벼운 손놀림", 17, 0, 0, 42, 10, "손목에 긴장을 풀어 손놀림이 가벼워 집니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "회오리 베기", 12, 0, 0, 12, 15, "빠르게 회전하여 적을 베며 연속으로 사용 시 점점 피해가 증가합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "대지 가르기", 92, 0, 0.5f, 16, 20, "강력하게 땅을 내리쳐 대지를 가릅니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "폭주", 55, 0, 0, 90, 25, "폭주 상태가 되어 몸을 사리지 않고 공격합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "결정타", 55, 4, 0, 21, 30, "무기를 강력하게 내려쳐 결정타를 노립니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "폭풍 가르기", 126, 18, 0, 18, 35, "순식간에 적에게 접근하여 폭풍처럼 빠르게 공격합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "속박 해제", 88, 0, 0, 18, 40, "속박을 해제합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "포효", 59, 0, 0, 18, 45, "포효하여 주변 적을 나약하게 만들고 시전자는 강인해집니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "알모람의 망치", 188, 15, 0, 29, 50, "알모람의 망치를 던져 공격합니다", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "적진으로", 157, 20, 0, 21, 55, "지정한 지역으로 도약하여 적진으로 파고듭니다", false));

        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "막고 반격", 3, 
        "무기 막기 성공 시 격투 공격형 기술의 재사용 대기 시간 모두 초기화\n지정된 주기보다 빠르게 막고 반격이 발동되면 12초 동안 발동 억제", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "무모한 돌진", 4, 
        "돌격, 폭풍 가르기, 적진으로 기술 사용 시 무모한 돌진\n지속시간: 4초\n 받는 물리 피해 15% 감소", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "물리 관통", 5, 
        "근접 치명타 명중 시 물리관통", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "공격 속도 연마", 6, "격투 피해량이 발생하는 기술 명중 시 광란", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "무기의 달인", 7, "격투 능력의 공격 기술 피해량 10% 증가", false));
        Battlerage.Add(new SkillInfo(SkillType.Battlerage, "무기 연마", 8, "근접 치명타 확률 6% 증가", false));

        string jdata = JsonConvert.SerializeObject(Battlerage, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/SkillData/BattlerageInfo.json", jdata);
    }

    void Update()
    {
    }

    void SaveData()
    {
        string jdata = JsonConvert.SerializeObject(Battlerage, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/SkillData/Test.json", jdata);
    }

    void LoadData()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Test.json");
        Battlerage = JsonConvert.DeserializeObject<List<SkillInfo>>(jdata);
    }

    public void ClickSelectSkillSetButton(Transform transform)
    {
        for(int i=0 ; i < SelectSkillSet.Length ; i++)
        {
            Transform[] child = SelectSkillSet[i].GetComponentsInChildren<Transform>();

            for(int j = 0 ; j < child.Length ; j++)
            {
                if(transform.GetComponentInParent<Transform>() == child[j])
                {
                    SelectSkillSet[i].gameObject.SetActive(false);
                    SkillSet[i].gameObject.SetActive(true);
                    break;
                }
            }
        }


        //Transform clickedButton =  EventSystem.current.currentSelectedGameObject.transform;
        //print(gameObject.name);

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

    
}
