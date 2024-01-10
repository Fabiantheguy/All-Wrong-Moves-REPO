using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tabs : MonoBehaviour
{
    public RectTransform treesScreen;
    public RectTransform skillsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TreesTabCycle()
    {
        treesScreen.SetAsLastSibling();
    }
    public void SkillsTabCycle()
    {
        skillsScreen.SetAsLastSibling();
    }
}
