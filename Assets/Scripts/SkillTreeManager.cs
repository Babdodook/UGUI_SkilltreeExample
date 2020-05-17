using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class SelectType
{
    public SkillType type;
    public bool isSelected;
}

public class SkillTreeManager : MonoBehaviour
{
    // 스킬 프로토타입
    public RectTransform SkillPrototype = null;
    Vector2 prototypePos;

    // 플레이어 스탯
    [Header("수치 표시용")]
    public PlayerStateInfo ExPlayer;
    public Text MinSkillLevel;
    public Text AvailableSkillPoint;
    public Text UsedSkillPoint;
    public bool isMaxPoint = false;

    [Header("스킬탭")]
    public SkillTabInfo[] SkillTabs = new SkillTabInfo[3];
    public Text[] CurrentSkillPointText = new Text[3];
    
    SelectType[] _selectType = new SelectType[5];

    //데이터 세이브&로드용 리스트
    List<DataSkillInfo> Battlerage;
    List<DataSkillInfo> Sorcery;
    List<DataSkillInfo> Archery;
    List<DataSkillInfo> Shadowplay;
    List<DataSkillInfo> Witchcraft;

    void Awake()
    {
        InitializeSkillList();

        for(int i = 0 ; i <_selectType.Length ; i++)
        {
            _selectType[i] = new SelectType();
            _selectType[i].type = (SkillType)i;
            _selectType[i].isSelected = false;
        }
        
        prototypePos = SkillPrototype.GetComponent<RectTransform>().anchoredPosition;

        LoadData();
    }

    void Update()
    {
        UpdateSkillInfo();
        //print(SkillTabs[0].SkillSet.CurrentSkillList);
    }

    // 스킬 리스트 초기화
    void InitializeSkillList()
    {
        Battlerage = new List<DataSkillInfo>();
        Sorcery = new List<DataSkillInfo>();
        Archery = new List<DataSkillInfo>();
        Shadowplay = new List<DataSkillInfo>();
        Witchcraft = new List<DataSkillInfo>();
    }

    public void SaveData()
    {
        // 타입 알아오기
        for(int i=0 ; i<SkillTabs.Length ; i++)
        {
            if(SkillTabs[i].SkillSet.CurrentSkillList != null)
            {
                switch(SkillTabs[i].SkillSet.GetCurrentSkillListType())
                {
                    case SkillType.Battlerage:
                        Battlerage = SkillTabs[i].SkillSet.ConvertDataToJson();
                        break;
                    case SkillType.Sorcery:
                        Sorcery = SkillTabs[i].SkillSet.ConvertDataToJson();
                        break;
                    case SkillType.Archery:
                        Archery = SkillTabs[i].SkillSet.ConvertDataToJson();
                        break;
                    case SkillType.Shadowplay:
                        Shadowplay = SkillTabs[i].SkillSet.ConvertDataToJson();
                        break;
                    case SkillType.Witchcraft:
                        Witchcraft = SkillTabs[i].SkillSet.ConvertDataToJson();
                        break;
                }
            }
        }

        // 저장
        string Battlerage_data = JsonConvert.SerializeObject(Battlerage, Formatting.Indented);
        string Sorcery_data = JsonConvert.SerializeObject(Sorcery, Formatting.Indented);
        string Archery_data = JsonConvert.SerializeObject(Archery, Formatting.Indented);
        string Shadowplay_data = JsonConvert.SerializeObject(Shadowplay, Formatting.Indented);
        string Witchcraft_data = JsonConvert.SerializeObject(Witchcraft, Formatting.Indented);

        File.WriteAllText(Application.dataPath + "/SkillData/BattlerageInfo.json", Battlerage_data);
        File.WriteAllText(Application.dataPath + "/SkillData/SorceryInfo.json", Sorcery_data);
        File.WriteAllText(Application.dataPath + "/SkillData/ArcheryInfo.json", Archery_data);
        File.WriteAllText(Application.dataPath + "/SkillData/ShadowplayInfo.json", Shadowplay_data);
        File.WriteAllText(Application.dataPath + "/SkillData/WitchcraftInfo.json", Witchcraft_data);

        print("저장 완료");
    }

    void LoadData()
    {
        string Battlerage_data = File.ReadAllText(Application.dataPath + "/SkillData/BattlerageInfo.json");
        string Sorcery_data = File.ReadAllText(Application.dataPath + "/SkillData/SorceryInfo.json");
        string Archery_data = File.ReadAllText(Application.dataPath + "/SkillData/ArcheryInfo.json");
        string Shadowplay_data = File.ReadAllText(Application.dataPath + "/SkillData/ShadowplayInfo.json");
        string Witchcraft_data = File.ReadAllText(Application.dataPath + "/SkillData/WitchcraftInfo.json");

        Battlerage = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Battlerage_data);
        Sorcery = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Sorcery_data);
        Archery = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Archery_data);
        Shadowplay = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Shadowplay_data);
        Witchcraft = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Witchcraft_data);
    }

    // 어떤 스킬셋 버튼 눌렀는지
    public void ClickSelectSkillSetButton(RectTransform _rectTransform, SkillType type)
    {
        // 이미 열린 스킬셋은 열지 않기
        for(int i=0 ; i < _selectType.Length ; i++)
        {
            if(_selectType[i].type == type)
            {
                if(_selectType[i].isSelected == true)
                    return;
                else
                    _selectType[i].isSelected = true;
            }
        }

        List<DataSkillInfo> SkillDataList = new List<DataSkillInfo>();
        Sprite[] ActiveSkills = null;
        Sprite[] PassiveSkills = null;

        // 스킬셋 가져오기
        SkillTabInfo _SkillTabInfo = _rectTransform.GetComponentInParent<SkillTabInfo>();
        SkillSetInfo _CurrentSkillSet = _SkillTabInfo.SkillSet;

        switch(type)
        {
            case SkillType.Battlerage:
                SkillDataList = Battlerage;
                ActiveSkills = Resources.LoadAll<Sprite>("격투액티브");
                PassiveSkills = Resources.LoadAll<Sprite>("격투패시브");
                break;
            case SkillType.Sorcery:
                SkillDataList = Sorcery;
                ActiveSkills = Resources.LoadAll<Sprite>("마법액티브");
                PassiveSkills = Resources.LoadAll<Sprite>("마법패시브");
                break;
            case SkillType.Archery:
                SkillDataList = Archery;
                ActiveSkills = Resources.LoadAll<Sprite>("야성액티브");
                PassiveSkills = Resources.LoadAll<Sprite>("야성패시브");
                break;
            case SkillType.Shadowplay:
                SkillDataList = Shadowplay;
                ActiveSkills = Resources.LoadAll<Sprite>("사명액티브");
                PassiveSkills = Resources.LoadAll<Sprite>("사명패시브");
                break;
            case SkillType.Witchcraft:
                SkillDataList = Witchcraft;
                ActiveSkills = Resources.LoadAll<Sprite>("환술액티브");
                PassiveSkills = Resources.LoadAll<Sprite>("환술패시브");
                break;
        }

        // 해당 스킬셋에 리스트 할당
        _CurrentSkillSet.CurrentSkillList = SkillDataList;
        _CurrentSkillSet.ActiveSkills = ActiveSkills;
        _CurrentSkillSet.PassiveSkills = PassiveSkills;

        // 스킬셋 넘기기
        InstantiateSkills(_CurrentSkillSet);
    }

    // 리셋 버튼 작동
    public void ClickReset(RectTransform _rectTransform)
    {
        SkillSetInfo _CurrentSkillSet = _rectTransform.GetComponentInParent<SkillSetInfo>();
        List<DataSkillInfo> SkillDataList = _CurrentSkillSet.CurrentSkillList;
        
        // 자식 오브젝트 SkillInfo 초기화하기
        for(int i=0 ; i< SkillDataList.Count ; i++)
        {
            // 색 다시 반투명으로
            Color _color = _CurrentSkillSet.rectTransform.Find(i.ToString()).GetComponent<Image>().color;
            _color.a = 0.5f;
            _CurrentSkillSet.rectTransform.Find(i.ToString()).GetComponent<Image>().color = _color;

            // 정보 초기화
            _CurrentSkillSet.rectTransform.Find(i.ToString()).GetComponent<SkillInfo>().ConvertData(SkillDataList[i]);
        }
    }

    // 스킬셋변경 버튼 작동
    public void ClickChangeSkillSet(RectTransform _rectTransform)
    {
        SkillSetInfo _CurrentSkillSet = _rectTransform.GetComponentInParent<SkillSetInfo>();

        for(int i=0 ; i < _selectType.Length ; i++)
        {
            if(_CurrentSkillSet.rectTransform.GetComponentInChildren<SkillInfo>().m_type == _selectType[i].type)
            {
                _selectType[i].isSelected = false;
            }
        }

        // 인스턴스한 스킬들 삭제
        for(int i=0 ; i < _CurrentSkillSet.Skills.Length ; i++)
        {
            Destroy(_CurrentSkillSet.Skills[i].gameObject);
        }

        _CurrentSkillSet.Skills = null;
        _CurrentSkillSet.CurrentSkillList = null;
        _CurrentSkillSet.rectTransform.gameObject.SetActive(false);
    }

    // 스킬 생성
    void InstantiateSkills(SkillSetInfo _CurrentSkillSet)
    {
        List<DataSkillInfo> SkillDataList = _CurrentSkillSet.CurrentSkillList;
        Sprite[] ActiveSkills = _CurrentSkillSet.ActiveSkills;
        Sprite[] PassiveSkills = _CurrentSkillSet.PassiveSkills;

        int j=0;
        int pIndex = 0;
        float yPos = prototypePos.y;

        // 복제할 오브젝트 끄기
        SkillPrototype.gameObject.SetActive(false);

        _CurrentSkillSet.Skills = null;
        _CurrentSkillSet.Skills = new RectTransform[SkillDataList.Count];
        // 스킬 생성
        for(int i=0 ; i< SkillDataList.Count ; i++)
        {
            var skill = GameObject.Instantiate(SkillPrototype) as RectTransform;
            skill.SetParent(_CurrentSkillSet.rectTransform, false);
            skill.name = i.ToString();
            skill.GetComponent<SkillInfo>().ConvertData(SkillDataList[i]);
            skill.anchoredPosition = new Vector2(prototypePos.x + (j++ * 110), yPos);

            _CurrentSkillSet.Skills[i] = skill;

            // 색 지정
            Color _color = skill.GetComponent<Image>().color;
            _color.a = 0.5f;
            skill.GetComponent<Image>().color = _color;

            // 이미지 넣어주기
            if(!skill.GetComponent<SkillInfo>().m_isPassive)
                skill.GetComponent<Image>().sprite = ActiveSkills[i];
            else
                skill.GetComponent<Image>().sprite = PassiveSkills[pIndex++];

            skill.gameObject.SetActive(true);

            if(i == 3 || i == 7 || i == 15)
            {
                j = 0;
                yPos -= 110;
            }
            else if(i == 11)
            {
                j = 0;
                yPos = -100;
            }
        }

        // 스킬셋 켜주기
        _CurrentSkillSet.rectTransform.gameObject.SetActive(true);
    }

    // 스킬포인트 업데이트
    // 스킬포인트에 따라 스킬해금 업데이트
    public void UpdateSkillInfo()
    {
        RectTransform[] Skills;
        SkillInfo _skillInfo;
        int CurrentSkillPoint;

        ExPlayer.AvailableSkillPoint = ExPlayer.MaxSkillPoint;
        ExPlayer.MinSkillLevel = 0;

        ExPlayer.UsedSkillPoint = 0;

        for(int i=0 ; i < SkillTabs.Length ; i++)
        {
            if(SkillTabs[i].SkillSet.gameObject.activeSelf)
            {
                Skills = SkillTabs[i].SkillSet.Skills;
                CurrentSkillPoint = 0;

                // 액티브 스킬 중에서 활성화된 스킬 카운트 (패시브 제외)
                for(int j=0; j<Skills.Length ; j++)
                {
                    _skillInfo = Skills[j].GetComponent<SkillInfo>();

                    if(!_skillInfo.m_isPassive && _skillInfo.m_isEnabled)
                    {
                        ExPlayer.AvailableSkillPoint--;
                        CurrentSkillPoint++;

                        if(_skillInfo.m_requiredLevel > ExPlayer.MinSkillLevel)
                            ExPlayer.MinSkillLevel = _skillInfo.m_requiredLevel;
                    }
                }

                // 특정 조건 만족하는 스킬 해금해주기
                // 패시브 스킬은 자동으로 활성화 시켜주기
                for(int z=0 ; z < Skills.Length ; z++)
                {
                    _skillInfo = Skills[z].GetComponent<SkillInfo>();

                    if( _skillInfo.m_specificPoint != 0 
                        && _skillInfo.m_specificPoint <= CurrentSkillPoint )
                    {
                        _skillInfo.m_isUnLocked = true;

                        // 패시브 스킬일 경우 활성화
                        if(_skillInfo.m_isPassive)
                        {
                            Color _color = Skills[z].GetComponent<Image>().color;
                            _color.a = 1;
                            Skills[z].GetComponent<Image>().color = _color;
                            _skillInfo.m_isEnabled = true;
                        }
                    }
                }

                CurrentSkillPointText[i].text = "강화 포인트 (" + CurrentSkillPoint + "/12)";
            }
        }

        ExPlayer.UsedSkillPoint = ExPlayer.MaxSkillPoint - ExPlayer.AvailableSkillPoint;

        MinSkillLevel.text = "최소 기술 레벨: " + ExPlayer.MinSkillLevel;
        AvailableSkillPoint.text = "사용가능 기술 포인트: " + ExPlayer.AvailableSkillPoint;
        UsedSkillPoint.text = "사용한 기술 포인트: " + ExPlayer.UsedSkillPoint;

        // 사용 가능한 스킬포인트가 없다면?
        // 더 이상 스킬 못찍도록 하기
        if(ExPlayer.AvailableSkillPoint == 0)
            isMaxPoint=true;
        else
            isMaxPoint=false;
    }

    public void ClickSkill(SkillInfo _SkillInfo, RectTransform _transform)
    {
        // 이미 활성화된 스킬이라면 다음 코드 실행하지 않기
        // 잠긴 스킬스킬 또한 실행하지 않기
        // 패시브 스킬도 실행하지 않기
        if(_SkillInfo.m_isEnabled || isMaxPoint || !_SkillInfo.m_isUnLocked || _SkillInfo.m_isPassive )
        {
            return;
        }

        Color _color = _transform.GetComponent<Image>().color;
        _color.a = 1;
        _transform.GetComponent<Image>().color = _color;
        _SkillInfo.m_isEnabled = true;
    }

}
