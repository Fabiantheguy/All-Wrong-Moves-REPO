using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLoadoutStats
{
    public string skillName { get; set; }
    public string skillSlot1 { get; set; }
    public string skillSlot2 { get; set; }
    public string skillSlot3 { get; set; }
    public string skillSlot4 { get; set; }
    public string skillSlot5 { get; set; }

    public float masteryLevel { get; set; }
    public float masteryXP { get; set; }
    public float masteryXPReq { get; set; }
    public SkillsLoadoutStats(string skillName, string skillSlot1, string skillSlot2, string skillSlot3, string skillSlot4,
    string skillSlot5, float masteryLevel, float masteryXP,float masteryXPReq)
    {
        this.skillName = skillName;
        this.skillSlot1 = skillSlot1;
        this.skillSlot2 = skillSlot2;
        this.skillSlot3 = skillSlot3;
        this.skillSlot4 = skillSlot4;
        this.skillSlot5 = skillSlot5;
        this.masteryLevel = masteryLevel;
        this.masteryXP = masteryXP;
        this.masteryXPReq = masteryXPReq;
    }
}

public class SkillsLoadout : MonoBehaviour
{
    [SerializeField] SkillsManager skillsManager;
    [SerializeField] string skillName;
    [SerializeField] string skillSlot1;
    [SerializeField] string skillSlot2;
    [SerializeField] string skillSlot3;
    [SerializeField] string skillSlot4;
    [SerializeField] string skillSlot5;
    [SerializeField] float masteryLevel;
    [SerializeField] float masteryXP;
    [SerializeField] float masteryXPReq;
    public ScriptableObject skill;

    public SkillsLoadoutStats ReturnClass()
    {       
        return new SkillsLoadoutStats(skillName, skillSlot1, skillSlot2, skillSlot3, skillSlot4, skillSlot5, masteryLevel, masteryXP, masteryXPReq);
    }
    private void Start()
    {
        skillName = "";
        skillSlot1 = "";
        skillSlot2 = "";
        skillSlot3 = "";
        skillSlot4 = "";
        skillSlot5 = "";
        masteryLevel = 0;
        masteryXP = 0;
        masteryXP = 0;
    }

    public string SkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }
    public string SkillSlot1
    {
        get { return skillSlot1; }
        set { skillSlot1 = value; }
    }
    public string SkillSlot2
    {
        get { return skillSlot2; }
        set { skillSlot2 = value; }
    }
    public string SkillSlot3
    {
        get { return skillSlot3; }
        set { skillSlot3 = value; }
    }
    public string SkillSlot4
    {
        get { return skillSlot4; }
        set { skillSlot4 = value; }
    }
    public string SkillSlot5
    {
        get { return skillSlot5; }
        set { skillSlot5 = value; }
    }
    public float MasteryLevel
    {
        get { return masteryLevel; }
        set { masteryLevel = value; }
    }
    public float MasteryXP
    {
        get { return masteryXP; }
        set { masteryXP = value; }
    } public float MasteryXPReq
    {
        get { return masteryXPReq; }
        set { masteryXPReq = value; }
    }
    public SkillsLoadout(string curname, string curskillslot1, string curskillslot2, 
    string curskillslot3, string curskillslot4, string curskillslot5, 
    float curmasterylevel, float curmasteryxp, float curmasteryxpreq)
    {

        curname = name;
        curskillslot1 = skillSlot1;
        curskillslot2 = skillSlot2;
        curskillslot3 = skillSlot3;
        curskillslot4 = skillSlot4;
        curskillslot5 = skillSlot5;
        curmasterylevel = masteryLevel;
        curmasteryxp = masteryXP;
        curmasteryxpreq = masteryXPReq;
    }
    public void SetSkillSlots(SkillsLoadoutStats skillsloadoutstats)
    { 
        SkillSlot1 = skillsloadoutstats.skillSlot1;
        SkillSlot2 = skillsloadoutstats.skillSlot2;
        SkillSlot3 = skillsloadoutstats.skillSlot3;
        SkillSlot4 = skillsloadoutstats.skillSlot4;
        SkillSlot5 = skillsloadoutstats.skillSlot5;
        if(skillSlot1 != "")
        {
            skillsManager.LoadLoadoutSkills(skillSlot1);
            skillsManager.skillSlot1 = skillSlot1;
        }
        if (skillSlot2 != "")
        {
            skillsManager.LoadLoadoutSkills(skillSlot2);
            skillsManager.skillSlot2 = skillSlot2;
        }
        if (skillSlot3 != "")
        {
            skillsManager.LoadLoadoutSkills(skillSlot3);
            skillsManager.skillSlot3 = skillSlot3;
        }
        if (skillSlot4 != "")
        {
            skillsManager.LoadLoadoutSkills(skillSlot4);
            skillsManager.skillSlot4 = skillSlot4;
        }
        if (skillSlot5 != "")
        {
            skillsManager.LoadLoadoutSkills(skillSlot5);
            skillsManager.skillSlot5 = skillSlot5;
        }
    }
    
}