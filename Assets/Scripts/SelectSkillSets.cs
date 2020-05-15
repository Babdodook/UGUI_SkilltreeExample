using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillSets : MonoBehaviour
{
    SkillTreeManager manager;
    void Awake()
    {
        manager = GameObject.Find("SkillTreeManager").GetComponent<SkillTreeManager>();
    }

	public void ClickButton()
    {
        manager.ClickSelectSkillSetButton(this.gameObject.transform);
    }
}
