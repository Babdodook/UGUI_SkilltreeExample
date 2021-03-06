# 아키에이지 스킬트리 시스템

아키에이지의 스킬트리를 약식으로 구현하였습니다.  
스킬 포인트의 분배, 스킬 초기화, 다른 능력 선택 등이 가능합니다.  
능력은 총 5개가 구현되어 있습니다.  

# 개발환경
* Unity 2019.2.21  
* Visual Studio 2019  

# 기능
* 능력 선택  
* 능력 변경  
* 능력 초기화  
* 스킬 정보  
* 에디터 편집

# 기능 설명
## 1. 능력 선택

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 선택1](https://user-images.githubusercontent.com/48229283/104789486-7f95ac80-57d8-11eb-9c9d-d023f6a83c7d.PNG) | ![능력 선택2](https://user-images.githubusercontent.com/48229283/104789489-815f7000-57d8-11eb-8b35-ba254049ac4d.PNG)

능력은 5개가 구현되어있습니다.
능력을 선택하여 해당 능력의 정보를 불러올 수 있습니다.

## 2. 능력 변경

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 변경1](https://user-images.githubusercontent.com/48229283/104790453-90dfb880-57da-11eb-8ddf-22a4dc4a8eac.PNG) | ![능력 변경2](https://user-images.githubusercontent.com/48229283/104789853-75c07900-57d9-11eb-938f-aa6fefaa2334.PNG)

스킬셋 변경 버튼을 눌러 다른 능력으로 변경할 수 있습니다.  
이미 선택된 스킬셋으로는 변경할 수 없습니다.  

## 3. 능력 초기화

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![능력 초기화](https://user-images.githubusercontent.com/48229283/104790536-cdabaf80-57da-11eb-90c8-3aa32243ced9.PNG) | ![능력 초기화2](https://user-images.githubusercontent.com/48229283/104790540-cf757300-57da-11eb-87b1-57005449f65e.PNG)

초기화 버튼을 눌러 스킬 포인트를 초기화 할 수 있습니다.

## 4. 스킬 정보

이미지1 | 이미지2
:-------------------------:|:-------------------------:
![스킬정보1](https://user-images.githubusercontent.com/48229283/104790751-7ce88680-57db-11eb-935b-17b268e91679.png) | ![스킬정보2](https://user-images.githubusercontent.com/48229283/104790755-7eb24a00-57db-11eb-9a86-23a3cf64c5d3.png)

마우스 커서를 올리면 해당 스킬의 정보를 열람할 수 있습니다.

## 5. 에디터 편집

이미지1 | 이미지2 | 이미지3
:-------------------------:|:-------------------------:|:-------------------------:
![스킬에디터1](https://user-images.githubusercontent.com/48229283/104796286-33f2fb00-57f8-11eb-9f8d-27b9bbe3c41a.PNG) | ![스킬에디터3](https://user-images.githubusercontent.com/48229283/104796273-20479480-57f8-11eb-9ecb-a2a51083f7b0.png) | ![스킬에디터4](https://user-images.githubusercontent.com/48229283/104796292-37868200-57f8-11eb-8ae2-aa84aa8df71c.PNG)

엔진의 인스펙터창에서 스킬을 선택 후 스킬 정보를 편집할 수 있습니다.  
변경된 정보는 저장버튼을 눌러서 데이터를 보존할 수 있습니다.  
  
해당 기능은 엔진을 잘 다루지 못하는 사람도 손쉬운 스킬 정보 변경을 위해 추가하였습니다.

# 시스템 구조
![구조1](https://user-images.githubusercontent.com/48229283/104789265-d64eb680-57d7-11eb-9aec-72e012a26d4d.PNG)  
매니저가 모든 능력을 관리하고
능력마다 스킬정보를 가지고 있는 단순한 구조입니다.  
Json으로 스킬의 정보를 파싱해서 관리하며, 새로운 스킬을 추가할 때 Json정보만 업데이트 시켜주면 됩니다.  

# Code

## SkillTreeManager.cs

스킬트리 매니저 클래스입니다.  
스킬 정보를 관리하고 스킬 생성, 초기화, 변경 기능을 수행합니다.  

```cs
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
    public static SkillTreeManager instance;

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
    List<DataSkillInfo>[] DataListArray;
    List<DataSkillInfo> Battlerage;
    List<DataSkillInfo> Sorcery;
    List<DataSkillInfo> Archery;
    List<DataSkillInfo> Shadowplay;
    List<DataSkillInfo> Witchcraft;

    void Awake()
    {
        instance = this;
        
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

        File.WriteAllText(Application.streamingAssetsPath + "/BattlerageInfo.json", Battlerage_data);
        File.WriteAllText(Application.streamingAssetsPath + "/SorceryInfo.json", Sorcery_data);
        File.WriteAllText(Application.streamingAssetsPath + "/ArcheryInfo.json", Archery_data);
        File.WriteAllText(Application.streamingAssetsPath + "/ShadowplayInfo.json", Shadowplay_data);
        File.WriteAllText(Application.streamingAssetsPath + "/WitchcraftInfo.json", Witchcraft_data);

        print("저장 완료");
    }

    void LoadData()
    {
        string Battlerage_data = File.ReadAllText(Application.streamingAssetsPath + "/BattlerageInfo.json");
        string Sorcery_data = File.ReadAllText(Application.streamingAssetsPath + "/SorceryInfo.json");
        string Archery_data = File.ReadAllText(Application.streamingAssetsPath + "/ArcheryInfo.json");
        string Shadowplay_data = File.ReadAllText(Application.streamingAssetsPath + "/ShadowplayInfo.json");
        string Witchcraft_data = File.ReadAllText(Application.streamingAssetsPath + "/WitchcraftInfo.json");

        Battlerage = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Battlerage_data);
        Sorcery = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Sorcery_data);
        Archery = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Archery_data);
        Shadowplay = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Shadowplay_data);
        Witchcraft = JsonConvert.DeserializeObject<List<DataSkillInfo>>(Witchcraft_data);
    }
```

```cs
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
```

```cs
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
```

## SkillSetInfo.cs

스킬셋 클래스입니다.  
해당 스킬셋에 포함된 스킬정보를 가집니다.  

```cs
public class SkillSetInfo : MonoBehaviour
{
    public List<DataSkillInfo> CurrentSkillList;  // 스킬 정보
    public RectTransform rectTransform;
    public Sprite[] ActiveSkills = null;          // 액티브 스킬 이미지
    public Sprite[] PassiveSkills = null;         // 패시브 스킬 이미지
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
```

## SkillInfo.cs

스킬 정보 클래스입니다.  
스킬의 세부 정보를 가지고 있습니다.  

```cs
public class SkillInfo : MonoBehaviour
{
    [HideInInspector]
    public SkillType m_type;

    [Header("스킬 타입")]
    public string type;

    [Header("스킬 정보")]
    [Header("스킬 이름")]           public string m_name;
    [Header("소모 활력")]           public int m_mana;
    [Header("유효거리(m)")]         public float m_range;
    [Header("시전시간(초)")]        public float m_castTime;
    [Header("재사용 대기시간(초)")] public float m_delayTime;
    [Header("요구 레벨")]           public int m_requiredLevel;
    [Header("스킬 설명")]           public string m_descryption;
    [Header("스킬 개방")]           public bool m_isUnLocked;
    [Header("지속 스킬")]           public bool m_isPassive;
    [Header("강화 포인트")]         public int m_specificPoint;
    [Header("활성화한 스킬")]       public bool m_isEnabled;

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

    public void SetData()
    {
        m_name = SkillEditor.instance.m_name;
        m_mana = SkillEditor.instance.m_mana;
        m_range = SkillEditor.instance.m_range;
        m_castTime = SkillEditor.instance.m_castTime;
        m_delayTime = SkillEditor.instance.m_delayTime;
        m_requiredLevel = SkillEditor.instance.m_requiredLevel;
        m_descryption = SkillEditor.instance.m_descryption;
        m_specificPoint = SkillEditor.instance.m_specificPoint;
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

    void Awake()
    {
        type = GetTypeToString();
    }
}

```
  
## SimpleEditorInspector.cs
  
인스펙터창에서 스킬을 편집하기 위한 GUI 커스텀 에디터 클래스입니다.

```cs
[CustomEditor(typeof(SkillEditor))]

public class SkillEditorInspector : Editor
{
    SkillEditor _editor;

    void OnEnable()
    {
        // target은 위의 CustomEditor() 애트리뷰트에서 설정해 준 타입의 객체에 대한 레퍼런스
        // object형이므로 실제 사용할 타입으로 캐스팅 해 준다.
        _editor = target as SkillEditor;
    }

    public override void OnInspectorGUI()  //Editor상속, 커스텀에디터 구현 함수 재 정의.  
    {
        //base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck ();

        GUILayout.Label("현재 선택한 오브젝트 데이터 설정");
        EditorGUILayout.Space();
        var m_name = EditorGUILayout.TextField("스킬 이름", _editor.m_name);
        EditorGUILayout.Space();
        var m_mana = EditorGUILayout.IntSlider ("활력 수치", _editor.m_mana, 0, 500);
        EditorGUILayout.Space();
        var m_range = EditorGUILayout.Slider ("유효 거리(m)", _editor.m_range, 0, 500);
        EditorGUILayout.Space();
        var m_castTime = EditorGUILayout.Slider ("시전시간(초)", _editor.m_castTime, 0, 30);
        EditorGUILayout.Space();
        var m_delayTime = EditorGUILayout.Slider ("재사용 대기시간(초)", _editor.m_delayTime, 0, 300);
        EditorGUILayout.Space();
        var m_requiredLevel = EditorGUILayout.IntSlider ("요구 레벨", _editor.m_requiredLevel, 0, 55);
        EditorGUILayout.Space();
        var m_specificPoint = EditorGUILayout.IntSlider ("강화 포인트", _editor.m_specificPoint, 0, 8);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스킬 설명");
        var m_descryption = EditorGUILayout.TextArea(_editor.m_descryption, GUILayout.Height(50));
        
        if (EditorGUI.EndChangeCheck ()) {

            //변경전에 Undo 에 등록
            Undo.RecordObject (_editor, "Undo Action");

            _editor.m_name = m_name;
            _editor.m_mana = m_mana;
            _editor.m_range = m_range;
            _editor.m_castTime = m_castTime;
            _editor.m_delayTime = m_delayTime;
            _editor.m_requiredLevel = m_requiredLevel;
            _editor.m_specificPoint = m_specificPoint;
            _editor.m_descryption = m_descryption;
        }

        EditorGUILayout.Space();
        GUILayout.Label("변경된 내용을 적용합니다");
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("적용", GUILayout.Width(200), GUILayout.Height(30))) 
        {
            _editor.ApllyData();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GUILayout.Label("로컬파일로 데이터를 저장합니다");
        GUILayout.Label("(다음 실행때 저장한 데이터를 불러옵니다)");
        EditorGUILayout.BeginHorizontal();  
        GUILayout.FlexibleSpace(); 

        if (GUILayout.Button("저장(to Json)", GUILayout.Width(200), GUILayout.Height(30))) 
        {
            // 적용하고 저장
            _editor.ApllyData();
            SkillTreeManager.instance.SaveData();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)    //변경이 있을 시 적용된다. 이 코드가 없으면 인스펙터 창에서 변화는 있지만 적용은 되지 않는다.
            EditorUtility.SetDirty(target);
    }
}
```
  
## SkillEditor.cs
  
인스펙터에 표시되는 스킬의 정보를 표시해줍니다.
스킬의 정보를 변경하는 기능도 수행합니다.
  
```cs
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
```
