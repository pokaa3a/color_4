using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateEnemy : State
{

}

public partial class StateEnemy : State
{
    public override State EnemyTrigger()
    {
        Exit();
        return stateIdle;
    }

    public override void Enter()
    {
        UIManager.Instance.uiEndTurn.spritePath =
            SpritePath.UI.Button.endTurnButtonEnemy;

        EnemyManager.Instance.StartTurn();
    }

    public override void Exit()
    {
        UIManager.Instance.uiEndTurn.spritePath =
            SpritePath.UI.Button.endTurnButtonUnpressed;
    }
}