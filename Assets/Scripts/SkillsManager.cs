using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance;
    public List<SkillsInventory> SkillsInventory = new List<SkillsInventory>();
    public List<SkillsInventory> SkillsLoadout = new List<SkillsInventory>();
    public SkillsInventory[] skillsInventoryArray;
    public SkillsLoadout skillsLoadoutClass;
    public SkillsInventory skillsInventory_Script;
    public SkillsInventory skillsLoadout_Script;
    public string skillName;
    public string skillSlot1;
    public string skillSlot2;
    public string skillSlot3;
    public string skillSlot4;
    public string skillSlot5;

    [Header("Skill Sidebar")]
    public TextMeshProUGUI skillCostText;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillScalingText;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillPropertiesText;
    public TextMeshProUGUI skillButtonText;
    public bool isEquipped;



    public Transform inventorySkillContent;
    public GameObject inventorySkill;
    public Transform loadoutSkillContent;
    public GameObject loadoutSkill;

    private void Awake()
    {
        Instance = this;
    }

    public void AddInventorySkill(SkillsInventory skill)
    {
        SkillsInventory.Add(skill);
    }

    public void RemoveInventorySkill(SkillsInventory skill)
    {
        SkillsInventory.Remove(skill);
    }
    public void AddLoadoutSkill(SkillsInventory skill)
    {
        SkillsLoadout.Add(skill);
    }

    public void RemoveLoadoutSkill(SkillsInventory skill)
    {
        SkillsLoadout.Remove(skill);
    }
    public void ListInventorySkills()
    {
        foreach(Transform skill in inventorySkillContent)
        {
            Destroy(skill.gameObject);
        }
        foreach(var skill in SkillsInventory)
        {
  
            GameObject obj = Instantiate(inventorySkill, inventorySkillContent);
            var skillIcon = obj.transform.Find("Skill_Icon").GetComponent<Image>();
            Button btn = skillIcon.GetComponent<Button>();
            btn.onClick.AddListener(delegate { SetSkillSideBarText(skill);});
            obj.name = skill.skillName;
            skillIcon.sprite = skill.skillIcon;           
        }
    }
    public void ListLoadoutSkills()
    {
        foreach (Transform skill in loadoutSkillContent)
        {
            Destroy(skill.gameObject);
        }
        foreach (var skill in SkillsLoadout)
        {

            GameObject obj = Instantiate(loadoutSkill, loadoutSkillContent);
            var skillIcon = obj.transform.Find("Skill_Icon").GetComponent<Image>();
            Button btn = skillIcon.GetComponent<Button>();
            btn.onClick.AddListener(delegate { SetSkillSideBarText(skill); });
            obj.name = skill.skillName;
            skillIcon.sprite = skill.skillIcon;
            skillsLoadoutClass.SkillName = skill.name;
        }

    }
    public void CleanSkills()
    {
        foreach (Transform skill in inventorySkillContent)
        {
            Destroy(skill.gameObject);
        }
    }

    public void SetSkillSideBarText(SkillsInventory skill)
    {
        
        skillCostText.text = "";
        skillScalingText.text = "";
        skillNameText.text = "";
        skillDescriptionText.text = "";
        skillPropertiesText.text = "";
        skillScalingText.horizontalAlignment = HorizontalAlignmentOptions.Left;
        skillScalingText.verticalAlignment = VerticalAlignmentOptions.Top;
        skillScalingText.fontSize = 13;

        skillNameText.text = skill.skillName;
        skillName = skill.skillName;
        skillCostText.text = "Stamina:" + skill.stamCost + "<br>AP:" + skill.apCost;
        if (skill.hpCost > 0)
        {
            skillCostText.text += "<br>HP:" + skill.hpCost;
        }
        if (skill.strengthScaling > 0)
        {
            skillScalingText.text += "Strength:" + skill.strengthScaling + "x<br>";
        }
        if (skill.enduranceScaling > 0)
        {
            skillScalingText.text += "Endurance:" + skill.enduranceScaling + "x<br>";
        }
        if (skill.vitalityScaling > 0)
        {
            skillScalingText.text += "Vitality:" + skill.vitalityScaling + "x<br>";
        }
        if (skill.speedScaling > 0)
        {
            skillScalingText.text += "Speed:" + skill.speedScaling + "x<br>";
        }
        if (skill.spiritScaling > 0)
        {
            skillScalingText.text += "Spirit:" + skill.spiritScaling + "x<br>";
        }
        if (skill.presenceScaling > 0)
        {
            skillScalingText.text += "Presence:" + skill.presenceScaling + "x<br>";
        }
        if (skill.strengthScaling == 0 && skill.enduranceScaling == 0 && skill.vitalityScaling 
        == 0 && skill.speedScaling == 0 && skill.spiritScaling == 0 && skill.presenceScaling == 0)
        {
            skillScalingText.fontSize = 30;
            skillScalingText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            skillScalingText.verticalAlignment = VerticalAlignmentOptions.Middle;
            skillScalingText.text = "N/A";
        }
        skillDescriptionText.text = skill.skillDescription;
        if(skill.properties == "")
        {
            skillPropertiesText.text = "N/A";
        }
        else
        {
            skillPropertiesText.text = skill.properties;
        }
        ChangeEquipState();
        skillsLoadout_Script = Resources.Load<SkillsInventory>("Skills/" + skillName);
    }

    public void Equip()
    {
        if (skillName != skillSlot1 && skillName != skillSlot2 && skillName != skillSlot1
        && skillName != skillSlot3 && skillName != skillSlot4 && skillName != skillSlot5)
        {


            if (skillButtonText.text == "Equip" && skillSlot5 == "")
            {
                AddLoadoutSkillFromScript();
                if (skillSlot5 == "" && skillSlot4 != "")
                {
                    skillSlot5 = skillName;
                    skillsLoadoutClass.SkillSlot5 = skillSlot5;
                }
                if (skillSlot4 == "" && skillSlot3 != "")
                {
                    skillSlot4 = skillName;
                    skillsLoadoutClass.SkillSlot4 = skillSlot4;
                }
                if (skillSlot3 == "" && skillSlot2 != "")
                {
                    skillSlot3 = skillName;
                    skillsLoadoutClass.SkillSlot3 = skillSlot3;
                }
                if (skillSlot2 == "" && skillSlot1 != "")
                {
                    skillSlot2 = skillName;
                    skillsLoadoutClass.SkillSlot2 = skillSlot2;
                }
                if (skillSlot1 == "")
                {
                    skillSlot1 = skillName;
                    skillsLoadoutClass.SkillSlot1 = skillSlot1;
                }
            }
           
        }
        if (skillButtonText.text == "Unequip" && skillSlot1 == skillName)
        {
            RemoveLoadoutSkill(skillsLoadout_Script);
            skillsLoadoutClass.SkillSlot1 = "";
            skillSlot1 = "";

            Debug.Log("Remove");
        }
        if (skillButtonText.text == "Unequip" && skillSlot2 == skillName)
        {
            RemoveLoadoutSkill(skillsLoadout_Script);
            skillsLoadoutClass.SkillSlot2 = "";
            skillSlot2 = "";
            Debug.Log("Remove");
        }
        if (skillButtonText.text == "Unequip" && skillSlot3 == skillName)
        {
            RemoveLoadoutSkill(skillsLoadout_Script);
            skillsLoadoutClass.SkillSlot3 = "";
            skillSlot3 = "";
            Debug.Log("Remove");
        }
        if (skillButtonText.text == "Unequip" && skillSlot4 == skillName)
        {
            RemoveLoadoutSkill(skillsLoadout_Script);
            skillsLoadoutClass.SkillSlot4 = "";
            skillSlot4 = "";
            Debug.Log("Remove");
        }
        if (skillButtonText.text == "Unequip" && skillSlot5 == skillName)
        {
            RemoveLoadoutSkill(skillsLoadout_Script);
            skillsLoadoutClass.SkillSlot5 = "";
            skillSlot5 = "";
            Debug.Log("Remove");
        }
        if(skillSlot1 == "")
        {
            skillSlot1 = skillSlot2;
            skillsLoadoutClass.SkillSlot1 = skillSlot2;
            skillSlot2 = "";
            skillsLoadoutClass.SkillSlot2 = "";
        }
        if (skillSlot2 == "")
        {
            skillSlot2 = skillSlot3;
            skillsLoadoutClass.SkillSlot2 = skillSlot3;
            skillSlot3 = "";
            skillsLoadoutClass.SkillSlot3 = "";
        }
        if (skillSlot3 == "")
        {
            skillSlot3 = skillSlot4;
            skillsLoadoutClass.SkillSlot3 = skillSlot4;
            skillSlot4 = "";
            skillsLoadoutClass.SkillSlot4 = "";
        }
        if (skillSlot4 == "")
        {
            skillSlot4 = skillSlot5;
            skillsLoadoutClass.SkillSlot4 = skillSlot5;
            skillSlot5 = "";
            skillsLoadoutClass.SkillSlot5 = "";
        }
        ChangeEquipState();
    }

    public void ChangeEquipState()
    {
        if (skillName == skillSlot1 || skillName == skillSlot2 || skillName == skillSlot3
      || skillName == skillSlot4 || skillName == skillSlot5)
        {
            isEquipped = true;
        }
        else
        {
            isEquipped = false;
        }

        if (isEquipped == false)
        {
            skillButtonText.text = "Equip";
        }
        else
        {
            skillButtonText.text = "Unequip";
        }
    }
    public void LoadInventorySkills(string skill)
    {
        
        skillsInventory_Script = Resources.Load<SkillsInventory>("Skills/" + skill);
        AddInventorySkillFromScript();
    }
    public void AddInventorySkillFromScript()
    {
        SkillsInventory.Add(skillsInventory_Script);
    }

    public void LoadLoadoutSkills(string skill)
    {
        skillsLoadout_Script = Resources.Load<SkillsInventory>("Skills/" + skill);
        Debug.Log(skill);
        AddLoadoutSkillFromScript();
    }
    public void AddLoadoutSkillFromScript()
    {
        SkillsLoadout.Add(skillsLoadout_Script);
    }

}
