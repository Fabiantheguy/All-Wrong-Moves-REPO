using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Create New Skill")]
public class SkillsInventory : ScriptableObject
{
    public int id;
    public string skillName;
    public float stamCost;
    public float apCost;
    public float kiCost;
    public float hpCost;
    public float masteryLevel;
    public float masteryXP;
    public float masteryXPReq;
    public string skillDescription;
    public float strengthScaling;
    public float enduranceScaling;
    public float vitalityScaling;
    public float speedScaling;
    public float spiritScaling;
    public float presenceScaling;
    public string type;
    public string properties;
    public Sprite skillIcon;
}
