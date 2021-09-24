using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tower : MapObject
{

}

public partial class Tower : MapObject
{
    public Tower() { }

    public Tower(Vector2Int rc) : base(rc)
    {
        gameObject.name = "Tower";
    }
}