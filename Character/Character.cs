using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum CharacterType
{
    Circle,
    Square,
    Triangle
};

public partial class Character : MapObject
{
    // [public]


    // [private]
    private CharacterType type;
    string originalSpritePath;
    string selectedSpritePath;
    private int moveRange = 3;
    private HashSet<Vector2Int> movableRCs = new HashSet<Vector2Int>();
    private bool hasMoved = false;
}

public partial class Character : MapObject
{
    private bool _selected = false;
    public bool selected
    {
        get => _selected;
        set
        {
            _selected = value;

            if (_selected)
            {
                this.spritePath = selectedSpritePath;
                if (!hasMoved) ShowReachableRange();
            }
            else
            {
                this.spritePath = originalSpritePath;
                CleanRechableRange();
            }
        }
    }
}

public partial class Character : MapObject
{
    public Character() { }

    public Character(Vector2Int rc, CharacterType type) : base(rc)
    {
        this.type = type;
        this.spriteWH = Map.Instance.tileWH * 0.6f;

        switch (this.type)
        {
            case CharacterType.Circle:
                gameObject.name = "character-circle";
                originalSpritePath = SpritePath.Object.Character.circle;
                selectedSpritePath = SpritePath.Object.Character.circleSelected;
                break;
            case CharacterType.Square:
                gameObject.name = "character-square";
                originalSpritePath = SpritePath.Object.Character.square;
                selectedSpritePath = SpritePath.Object.Character.squareSelected;
                break;
            case CharacterType.Triangle:
                gameObject.name = "chracter-triangle";
                originalSpritePath = SpritePath.Object.Character.triangle;
                selectedSpritePath = SpritePath.Object.Character.triangleSelected;
                break;
            default:
                Assert.IsTrue(false, "[Character]: Invalid CharacterType");
                break;
        }
        spritePath = originalSpritePath;
    }

    public void ShowReachableRange()
    {
        for (int dist = 0; dist <= moveRange; ++dist)
        {
            for (int r = -dist; r <= dist; ++r)
            {
                int c = dist - Mathf.Abs(r);
                if (Map.Instance.InsideMap(rc + new Vector2Int(r, c)) &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Character>() == null &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Tower>() == null &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Enemy>() == null)
                {
                    Effect e1 = new Effect(rc + new Vector2Int(r, c));
                    e1.spritePath = SpritePath.Object.Effect.reachable;
                    movableRCs.Add(rc + new Vector2Int(r, c));
                }
                if (c == 0) continue;

                c *= -1;
                if (Map.Instance.InsideMap(rc + new Vector2Int(r, c)) &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Character>() == null &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Tower>() == null &&
                    Map.Instance.GetTile(rc + new Vector2Int(r, c)).GetObject<Enemy>() == null)
                {
                    Effect e2 = new Effect(rc + new Vector2Int(r, c));
                    e2.spritePath = SpritePath.Object.Effect.reachable;
                    movableRCs.Add(rc + new Vector2Int(r, c));
                }
            }
        }
    }

    public void CleanRechableRange()
    {
        foreach (Vector2Int tileRc in movableRCs)
        {
            Map.Instance.GetTile(tileRc).DestroyObject<Effect>();
        }
        movableRCs.Clear();
    }

    public void MoveTo(Vector2Int dstRc)
    {
        if (!hasMoved && movableRCs.Contains(dstRc))
        {
            this.rc = dstRc;
            CleanRechableRange();
            hasMoved = true;
        }
    }
}
