using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UISkillHolder
{
    // [public]
    public List<SkillHolder> holders = new List<SkillHolder>();

    // [private]
}

public partial class UISkillHolder
{
    public class SkillHolder : UIObject
    {
        public SkillHolder(int id) : base($"SkillHolder{id}")
        {

        }
    }

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            for (int i = 0; i < holders.Count; ++i)
            {
                Character selectedCharacter =
                    CharacterManager.Instance.selectedCharacter;
                if (selectedCharacter != null)
                {
                    holders[i].spritePath = selectedCharacter.skills[i].iconSprite;
                }

                holders[i].enabled = _enabled;
            }
        }
    }
}

public partial class UISkillHolder
{
    public SkillHolder this[int i]
    {
        get => holders[i];
        protected set { holders[i] = value; }
    }

    public UISkillHolder()
    {
        SkillHolder holder0 = new SkillHolder(0);
        SkillHolder holder1 = new SkillHolder(1);
        SkillHolder holder2 = new SkillHolder(2);

        holder0.enabled = false;
        holder1.enabled = false;
        holder2.enabled = false;

        holders.Add(holder0);
        holders.Add(holder1);
        holders.Add(holder2);
    }
}