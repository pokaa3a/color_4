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
            }
            else
            {
                this.spritePath = originalSpritePath;
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
}
