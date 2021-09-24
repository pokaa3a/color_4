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
                CharacterManager.Instance.selectedCharacter = null;
                return stateIdle;
            }
            else
            {
                CharacterManager.Instance.selectedCharacter = maybeCharacter;
                return stateCharacter;
            }
        }

        CharacterManager.Instance.selectedCharacter.MoveTo(rc);


        return null;
    }

    public override void Enter()
    {

    }
}