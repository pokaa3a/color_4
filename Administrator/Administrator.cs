using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor
{
    Empty,
    Red,
    Blue,
    Yellow
};

public partial class Administrator
{
    // [public]
    public State state = new StateIdle();

    // [private]

}

public partial class Administrator
{
    private static Administrator _instance;
    public static Administrator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Administrator();
            }
            return _instance;
        }
    }
}

public partial class Administrator
{
    public void Initialize()
    {
        state.Enter();
    }

    public void Click(Vector2 xy)
    {
        Vector2Int rc = Map.Instance.XYtoRC(xy);
        if (Map.Instance.InsideMap(rc))
        {
            State maybeState = state.Click(rc);
            if (maybeState != null)
            {
                maybeState.Enter();
                state = maybeState;
            }
        }
    }

    private Administrator() { }
}