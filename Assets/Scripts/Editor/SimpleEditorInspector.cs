using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
