using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillManager
{
    // [public]
    public int selectedSkillId = -1;

    // [private]
}

public partial class SkillManager
{
    private Skill _selectedSkill = null;
    public Skill selectedSkill
    {
        get
        {
            if (selectedSkillId < 0 ||
                CharacterManager.Instance.selectedCharacter == null)
            {
                _selectedSkill = null;
            }
            else
            {
                _selectedSkill =
                    CharacterManager.Instance.selectedCharacter.skills[selectedSkillId];
            }
            return _selectedSkill;
        }
    }

    // Singleton
    private static SkillManager _instance;
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null) { _instance = new SkillManager(); }
            return _instance;
        }
    }
}

public partial class SkillManager
{
    private SkillManager() { }
}