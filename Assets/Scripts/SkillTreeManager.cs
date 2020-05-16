using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;

public class SelectType
{
    public SkillType type;
    public bool isSelected;
}

public class SkillTreeManager : MonoBehaviour
{
    public RectTransform SkillPrototype = null;
    Vector2 prototypePos;

    public RectTransform[] SelectSkillSet = new RectTransform[3];
    public RectTransform[] SkillSet = new RectTransform[3];
    public RectTransform[] Reset = new RectTransform[3];
    public RectTransform[] ChangeSkillSet = new RectTransform[3];

    SelectType[] _selectType = new SelectType[5];

    //데이터 세이브&로드용 리스트
    List<SkillInfo> Battlerage;
    List<SkillInfo> Sorcery;
    List<SkillInfo> Archery;
    List<SkillInfo> Shadowplay;
    List<SkillInfo> Witchcraft;

    void Awake()
    {
        InitializeSkillList();

        for(int i=0 ; i < SkillSet.Length ; i++)
        {
            SelectSkillSet[i].gameObject.SetActive(true);
            SkillSet[i].gameObject.SetActive(false);
            Reset[i].gameObject.SetActive(false);
            ChangeSkillSet[i].gameObject.SetActive(false);
        }

        for(int i = 0 ; i <_selectType.Length ; i++)
        {
            _selectType[i] = new SelectType();
            _selectType[i].type = (SkillType)i;
            _selectType[i].isSelected = false;
        }
        
        prototypePos = SkillPrototype.GetComponent<RectTransform>().anchoredPosition;

        LoadData();
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

    void SaveData()
    {
        string jdata = JsonConvert.SerializeObject(Battlerage, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/SkillData/BattlerageInfo.json", jdata);
    }

    void LoadData()
    {
        string Battlerage_data = File.ReadAllText(Application.dataPath + "/SkillData/BattlerageInfo.json");
        string Sorcery_data = File.ReadAllText(Application.dataPath + "/SkillData/SorceryInfo.json");
        string Archery_data = File.ReadAllText(Application.dataPath + "/SkillData/ArcheryInfo.json");
        string Shadowplay_data = File.ReadAllText(Application.dataPath + "/SkillData/ShadowplayInfo.json");
        string Witchcraft_data = File.ReadAllText(Application.dataPath + "/SkillData/WitchcraftInfo.json");

        Battlerage = JsonConvert.DeserializeObject<List<SkillInfo>>(Battlerage_data);
        Sorcery = JsonConvert.DeserializeObject<List<SkillInfo>>(Sorcery_data);
        Archery = JsonConvert.DeserializeObject<List<SkillInfo>>(Archery_data);
        Shadowplay = JsonConvert.DeserializeObject<List<SkillInfo>>(Shadowplay_data);
        Witchcraft = JsonConvert.DeserializeObject<List<SkillInfo>>(Witchcraft_data);
    }

    // 어떤 스킬셋 버튼 눌렀는지
    public void ClickSelectSkillSetButton(RectTransform _rectTransform, SkillType type)
    {
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

        for(int i=0 ; i < SelectSkillSet.Length ; i++)
        {
            RectTransform[] child = SelectSkillSet[i].GetComponentsInChildren<RectTransform>();
            for(int j = 0 ; j < child.Length ; j++)
            {
                if(_rectTransform == child[j])
                {                        
                    SelectSkillSet[i].gameObject.SetActive(false);
                    SkillSet[i].gameObject.SetActive(true);
                    Reset[i].gameObject.SetActive(true);
                    ChangeSkillSet[i].gameObject.SetActive(true);
                    InstantiateSkills(type,i);
                    break;
                }
            }
        }
    }

    // 리셋 버튼 작동
    public void ClickReset(RectTransform _rectTransform)
    {
        RectTransform _SkillSet = null;
        List<SkillInfo> SkillDataList = new List<SkillInfo>();

        // 어떤 스킬셋 초기화해야하는지 알아오기
        for(int i=0 ; i < Reset.Length ; i++)
        {
            if(_rectTransform == Reset[i])
            {
                _SkillSet = SkillSet[i];
                break;
            }
        }

        // 해당 스킬셋 타입 알아오기
        switch(_SkillSet.GetComponentInChildren<SkillInfo>().m_type)
        {
            case SkillType.Battlerage:
                SkillDataList = Battlerage;
                break;
            case SkillType.Sorcery:
                SkillDataList = Sorcery;
                break;
            case SkillType.Archery:
                SkillDataList = Archery;
                break;
            case SkillType.Shadowplay:
                SkillDataList = Shadowplay;
                break;
            case SkillType.Witchcraft:
                SkillDataList = Witchcraft;
                break;
        }

        

        // 자식 오브젝트 SkillInfo 초기화하기
        for(int i=0 ; i< SkillDataList.Count ; i++)
        {
            // 색 다시 반투명으로
            Color _color = _SkillSet.Find(i.ToString()).GetComponent<Image>().color;
            _color.a = 0.5f;
            _SkillSet.Find(i.ToString()).GetComponent<Image>().color = _color;

            // 정보 초기화
            _SkillSet.Find(i.ToString()).GetComponent<SkillInfo>().SetSkillInfo(SkillDataList[i]);
        }
    }

    // 스킬셋변경 버튼 작동
    public void ClickChangeSkillSet(RectTransform _rectTransform)
    {
        RectTransform _SkillSet = null;
        RectTransform _SelectSkillSet = null;
        List<SkillInfo> SkillDataList = new List<SkillInfo>();


        // 어떤 스킬셋 변경해야하는지 알아오기
        for(int i=0 ; i < ChangeSkillSet.Length ; i++)
        {
            if(_rectTransform == ChangeSkillSet[i])
            {
                _SkillSet = SkillSet[i];
                _SelectSkillSet = SelectSkillSet[i];
                Reset[i].gameObject.SetActive(false);
                ChangeSkillSet[i].gameObject.SetActive(false);
                break;
            }
        }

        // 해당 스킬셋 타입 알아오기
        // 어차피 삭제할거라 상관없지만 나중에 스킬타입별로 개수가 달라지면 (현재는 모두 동일)
        // 스킬타입별로 총 스킬개수가 몇개인지 알아야함
        switch(_SkillSet.GetComponentInChildren<SkillInfo>().m_type)
        {
            case SkillType.Battlerage:
                SkillDataList = Battlerage;
                break;
            case SkillType.Sorcery:
                SkillDataList = Sorcery;
                break;
            case SkillType.Archery:
                SkillDataList = Archery;
                break;
            case SkillType.Shadowplay:
                SkillDataList = Shadowplay;
                break;
            case SkillType.Witchcraft:
                SkillDataList = Witchcraft;
                break;
        }

        for(int i=0 ; i < _selectType.Length ; i++)
        {
            if(_SkillSet.GetComponentInChildren<SkillInfo>().m_type == _selectType[i].type)
            {
                _selectType[i].isSelected = false;
            }
        }

        // 인스턴스한 스킬들 삭제
        for(int i=0 ; i < SkillDataList.Count ; i++)
        {
            Destroy(_SkillSet.Find(i.ToString()).gameObject);
        }

        // 스킬셋 끄고 스킬 선택창 가져오기
        _SkillSet.gameObject.SetActive(false);
        _SelectSkillSet.gameObject.SetActive(true);
    }

    // 스킬 생성
    void InstantiateSkills(SkillType type, int index)
    {
        int j=0;
        int pIndex = 0;
        float yPos = prototypePos.y;
        List<SkillInfo> SkillDataList = new List<SkillInfo>();
        Sprite[] ActiveSkills = null;
        Sprite[] PassiveSkills = null;

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

        // 복제할 오브젝트 끄기
        SkillPrototype.gameObject.SetActive(false);

        for(int i=0 ; i< SkillDataList.Count ; i++)
        {
            var skill = GameObject.Instantiate(SkillPrototype) as RectTransform;
            skill.SetParent(SkillSet[index], false);
            skill.name = i.ToString();
            skill.GetComponent<SkillInfo>().SetSkillInfo(SkillDataList[i]);
            skill.anchoredPosition = new Vector2(prototypePos.x + (j++ * 110), yPos);

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
    }

    
}
