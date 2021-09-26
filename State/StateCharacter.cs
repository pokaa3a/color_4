using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateCharacter : State
{

}


public partial class StateCharacter : State
{
    public override State Click(Vector2Int rc)
    {
        Character maybeCharacter = Map.Instance.GetTile(rc).GetObject<Character>();
        if (maybeCharacter != null)
        {
            if (maybeCharacter == CharacterManager.Instance.selectedCharacter)
            {
                Exit();
                CharacterManager.Instance.selectedCharacter = null;
                // UIManager.Instance.uiSkillHolder.enabled = false;
                return stateIdle;
            }
            else
            {
                Exit();
                CharacterManager.Instance.selectedCharacter = maybeCharacter;
                // UIManager.Instance.uiSkillHolder.enabled = false;
                return stateCharacter;
            }
        }

        CharacterManager.Instance.selectedCharacter.MoveTo(rc);


        return null;
    }

    public override State UIClick(Type type)
    {
        if (type == typeof(UISkillHolder))
        {
            Exit();
            return stateSkill;
        }

        return null;
    }

    public override void Enter()
    {
        CharacterManager.Instance.selectedCharacter.ShowMovingAvailableRCs();
        UIManager.Instance.uiSkillHolder.enabled = true;
    }

    public override void Exit()
    {
        CharacterManager.Instance.selectedCharacter.CleanMovingAvailableRCs();
        UIManager.Instance.uiSkillHolder.enabled = false;
    }
}