using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateIdle : State
{

}

public partial class StateIdle : State
{
    public override State Click(Vector2Int rc)
    {
        Character maybeCharacter = Map.Instance.GetTile(rc).GetObject<Character>();
        if (maybeCharacter != null)
        {
            CharacterManager.Instance.selectedCharacter = maybeCharacter;
            return stateCharacter;
        }
        return null;
    }

    public override State UIClick(Type type)
    {
        if (type == typeof(UIEndTurn))
        {
            return stateEnemy;
        }
        return null;
    }

    public override void Enter()
    {

    }
}