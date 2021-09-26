using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Minion
}

public partial class Enemy : MapObject
{
    // [public]

    // [private]
    private EnemyType type;
}

public partial class Enemy : MapObject
{
    public Enemy() { }

    public Enemy(Vector2Int rc, EnemyType type) : base(rc)
    {
        this.type = type;
        this.spriteWH = Map.Instance.tileWH * 0.6f;

        SetUpLifeText(new Color32(255, 0, 0, 255));
        this.maxLife = 100;
        this.life = 100;

        switch (this.type)
        {
            case EnemyType.Minion:
                gameObject.name = "Minion";
                spritePath = SpritePath.Object.Enemy.minion;
                break;
            default:
                break;
        }
    }
}