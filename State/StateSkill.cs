using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateSkill : State
{

}

public partial class StateSkill : State
{

}

public partial class StateSkill : State
{
    public override State Click(Vector2Int rc)
    {
        Character maybeCharacter = Map.Instance.GetTile(rc).GetObject<Character>();
        if (maybeCharacter != null)
        {
            int selectedSkillId = SkillManager.Instance.selectedSkillId;
            UIManager.Instance.uiSkillHolder.holders[selectedSkillId].selected = false;

            UIManager.Instance.uiSkillHolder.enabled = false;
            SkillManager.Instance.selectedSkillId = -1;

            if (maybeCharacter == CharacterManager.Instance.selectedCharacter)
            {
                // Click selected character -> change to idle state
                CharacterManager.Instance.selectedCharacter = null;
                return stateIdle;
            }
            else
            {
                // Click another character -> change to chracter state
                CharacterManager.Instance.selectedCharacter = maybeCharacter;
                return stateCharacter;
            }
        }

        return null;
    }

    public override State UIClick(Type type)
    {
        if (type == typeof(UISkillHolder))
        {
            // Get new selected skill ID
            int selectedSkillId = -1;
            for (int i = 0; i < UIManager.Instance.uiSkillHolder.holders.Count; ++i)
            {
                if (UIManager.Instance.uiSkillHolder.holders[i].selected)
                {
                    selectedSkillId = i;
                    break;
                }
            }
            SkillManager.Instance.selectedSkillId = selectedSkillId;

            // Maybe change state
            if (selectedSkillId == -1)
            {
                return stateCharacter;
            }
            if (selectedSkillId != SkillManager.Instance.selectedSkillId)
            {
                return stateSkill;
            }
        }

        return null;
    }

    public override void Enter()
    {
        UIManager.Instance.uiSkillHolder.enabled = true;

        for (int i = 0; i < UIManager.Instance.uiSkillHolder.holders.Count; ++i)
        {
            if (UIManager.Instance.uiSkillHolder.holders[i].selected)
            {
                SkillManager.Instance.selectedSkillId = i;
                break;
            }
        }
    }
}