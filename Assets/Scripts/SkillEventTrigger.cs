using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillEventTrigger : MonoBehaviour
{
    RectTransform SkillInfoTab;
    RectTransform SkillInfoTabPassive;
    [SerializeField] public SkillInfo _SkillInfo;
    SkillTreeManager _SkillTreeManager;
    PlayerStateInfo ExPlayer;

    void Start () {
        SkillInfoTab = GameObject.Find("SkillInfo").GetComponent<RectTransform>();
        SkillInfoTabPassive = GameObject.Find("SkillInfoPassive").GetComponent<RectTransform>();
        _SkillInfo = GetComponent<SkillInfo>();
        _SkillTreeManager = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
        entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
        entry_PointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerEnter);

        EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
        entry_PointerExit.eventID = EventTriggerType.PointerExit;
        entry_PointerExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerExit);
    }
	
    void OnPointerDown(PointerEventData data)
    {
        _SkillTreeManager.ClickSkill(_SkillInfo,GetComponent<RectTransform>());
    }

    void OnPointerEnter(PointerEventData data)
    {
        Vector2 pos = GetComponent<RectTransform>().position;
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        Vector2 tabsize;
        Vector2 tabPos = Vector2.zero;

        
        //정보창 위치 지정
        if(_SkillInfo.m_isPassive)
        {
            tabsize = SkillInfoTabPassive.sizeDelta;
            tabPos = new Vector2(pos.x + size.x/2 + tabsize.x/2 + 10, pos.y);

            if(Camera.main.pixelWidth < pos.x + size.x/2 + tabsize.x + 10)
                tabPos.x = pos.x - size.x/2 - tabsize.x/2 - 10;
                
            SkillInfoTabPassive.position = tabPos;
        }
        else
        {
            tabsize = SkillInfoTab.sizeDelta;

            tabPos = new Vector2(pos.x + size.x/2 + tabsize.x/2 + 10, pos.y);

            // height 넘어갈때 y좌표 보정
            if(Camera.main.pixelHeight < pos.y + tabsize.y/2)
                tabPos.y = Camera.main.pixelHeight - tabsize.y/2;
            else if(0 > pos.y - tabsize.y/2)
                tabPos.y = Camera.main.pixelHeight + tabsize.y/2;
            
            // width 넘어갈때 x좌표 보정
            if(Camera.main.pixelWidth < pos.x + size.x/2 + tabsize.x + 10)
                tabPos.x = pos.x - size.x/2 - tabsize.x/2 - 10;

            SkillInfoTab.position = tabPos;
        }

        ShowInfo();
    }

    void OnPointerExit(PointerEventData data)
    {
        SkillInfoTab.anchoredPosition = new Vector2(-1300, 160);
        SkillInfoTabPassive.anchoredPosition = new Vector2(-1973, 160);
    }

    void ShowInfo()
    {  
        if(_SkillInfo.m_isPassive)
        {
            //이름
            SkillInfoTabPassive.Find("Name").GetComponent<Text>().text = _SkillInfo.m_name;
            // 스킬타입
            SkillInfoTabPassive.Find("SkillType").GetComponent<Text>().text = _SkillInfo.GetTypeToString();
            //설명
            SkillInfoTabPassive.Find("Descryption").GetComponent<Text>().text = _SkillInfo.m_descryption;
            //요구레벨
            SkillInfoTabPassive.Find("RequiredLevel").GetComponent<Text>().text = 
                "<b><color=#ff0000>" + "배우기 요구 조건 " + "</color>" + "<color=#00ff00ff>" +"[" + _SkillInfo.GetTypeToString() + "] </color>" + 
                "<color=#ff0000> 강화 포인트 </color>" + "<color=#00ff00ff>" + _SkillInfo.m_requiredLevel.ToString() + "</color> <color=#ff0000>이상" + "</color></b>";
        }
        else
        {
            // 이름
            SkillInfoTab.Find("Name").GetComponent<Text>().text = _SkillInfo.m_name;
            // 스킬타입
            SkillInfoTab.Find("SkillType").GetComponent<Text>().text = _SkillInfo.GetTypeToString();
            // 활력
            SkillInfoTab.Find("Mana").GetComponent<Text>().text = "활력 " + _SkillInfo.m_mana.ToString();
            // 유효거리
            if(_SkillInfo.m_range == 0)
                SkillInfoTab.Find("Range").GetComponent<Text>().text = "유효거리 (자신)";
            else
                SkillInfoTab.Find("Range").GetComponent<Text>().text = "유효 거리 0~" + _SkillInfo.m_range.ToString()+"m";
            
            //시전시간
            if(_SkillInfo.m_castTime == 0)
                SkillInfoTab.Find("CastTime").GetComponent<Text>().text = "즉시 시전";
            else
                SkillInfoTab.Find("CastTime").GetComponent<Text>().text = "시전 시간 " + _SkillInfo.m_castTime.ToString() +"초";

            //재사용 대기시간
            SkillInfoTab.Find("DelayTime").GetComponent<Text>().text = _SkillInfo.m_delayTime.ToString()+"초 후 재사용 가능";
            //설명
            SkillInfoTab.Find("Descryption").GetComponent<Text>().text = _SkillInfo.m_descryption;

            //요구레벨
            if(_SkillInfo.m_specificPoint != 0)
            {
                SkillInfoTab.Find("RequiredLevel").GetComponent<Text>().text = 
                    "배우기 요구 조건 " + "[" + _SkillInfo.GetTypeToString() + "] 능력 " + _SkillInfo.m_requiredLevel.ToString() + "레벨\n" +
                    "<b><color=#ff0000>" + "강화 포인트 </color> <color=#00ff00ff>" + _SkillInfo.m_specificPoint + "</color> <color=#ff0000>이상" + "</color></b>";
            }
            else
            {
                SkillInfoTab.Find("RequiredLevel").GetComponent<Text>().text = 
                    "배우기 요구 조건 " + "[" + _SkillInfo.GetTypeToString() + "] 능력 " + _SkillInfo.m_requiredLevel.ToString() + "레벨";
            }
        }
    }
}
