using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIManager
{
    // [public]
    public UISkillHolder uiSkillHolder;

    // [private]
}

public partial class UIManager
{
    // Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
}

public partial class UIManager
{
    public void InitiateUIObjects()
    {
        uiSkillHolder = new UISkillHolder();
    }

    private UIManager()
    {

    }
}