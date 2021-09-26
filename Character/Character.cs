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
    public List<Skill> skills = new List<Skill>();


    // [private]
    private CharacterType type;

    private string originalSpritePath;
    private string selectedSpritePath;

    private int moveRange = 3;
    private HashSet<Vector2Int> movableRCs = new HashSet<Vector2Int>();
    private bool hasMoved = false;
    private bool hasUsedSkill = false;

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
        this.attackable = true;

        SetUpLifeText(new Color32(0, 255, 0, 255));
        this.maxLife = 100;
        this.life = 100;

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

        // Skills
        SkillAttack skill0 = new SkillAttack();
        SkillAttackAllSameColor skill1 = new SkillAttackAllSameColor();
        SkillAttackConnected skill2 = new SkillAttackConnected();
        skills.Add(skill0);
        skills.Add(skill1);
        skills.Add(skill2);

        spritePath = originalSpritePath;
    }

    public void ShowMovingAvailableRCs()
    {
        if (hasMoved) return;

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
                    e1.spritePath = SpritePath.Object.Effect.slashesGreen;
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
                    e2.spritePath = SpritePath.Object.Effect.slashesGreen;
                    movableRCs.Add(rc + new Vector2Int(r, c));
                }
            }
        }
    }

    public void CleanMovingAvailableRCs()
    {
        foreach (Vector2Int tileRc in movableRCs)
        {
            Assert.IsNotNull(Map.Instance.GetTile(tileRc));
            Map.Instance.GetTile(tileRc).DestroyObject<Effect>();
        }
        movableRCs.Clear();
    }

    public void ShowSkillAvailableRCs()
    {
        if (hasUsedSkill) return;

        SkillManager.Instance.selectedSkill.ShowAvailableRCs();
    }

    public void CleanSkillAvailableRCs()
    {
        SkillManager.Instance.selectedSkill.CleanAvailableRCs();
    }

    public void MoveTo(Vector2Int dstRc)
    {
        if (!hasMoved && movableRCs.Contains(dstRc))
        {
            this.rc = dstRc;
            CleanMovingAvailableRCs();
            hasMoved = true;
        }
    }
}
