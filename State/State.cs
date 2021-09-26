using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class State
{
    // Static states
    protected static StateIdle stateIdle;
    protected static StateCharacter stateCharacter;
    protected static StateSkill stateSkill;
    protected static StateEnemy stateEnemy;

}

public partial class State
{
    public State()
    {
        // Idle
        if (this is StateIdle)
        {
            stateIdle = (StateIdle)this;
        }
        else if (stateIdle == null)
        {
            stateIdle = new StateIdle();
        }

        // Character
        if (this is StateCharacter)
        {
            stateCharacter = (StateCharacter)this;
        }
        else if (stateCharacter == null)
        {
            stateCharacter = new StateCharacter();
        }

        // Skill
        if (this is StateSkill)
        {
            stateSkill = (StateSkill)this;
        }
        else if (stateSkill == null)
        {
            stateSkill = new StateSkill();
        }

        // Enemy
        if (this is StateEnemy)
        {
            stateEnemy = (StateEnemy)this;
        }
        else if (stateEnemy == null)
        {
            stateEnemy = new StateEnemy();
        }
    }

    public virtual State Click(Vector2Int rc)
    {
        return null;    // return null means state does not change
    }

    public virtual State UIClick(System.Type type)
    {
        return null;    // return null means state does not change
    }

    public virtual void Enter() { }

    public virtual void Exit() { }
}