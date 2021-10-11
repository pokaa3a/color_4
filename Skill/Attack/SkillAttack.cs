using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillAttack : Skill
{
    // [private]
    int attackAmount = 1;
}

public partial class SkillAttack : Skill
{
    public SkillAttack() : base()
    {
        this.iconSprite = SpritePath.Skill.Icon.attack;
        this.descriptionSprite = SpritePath.Skill.Description.attack;
    }

    public override bool Launch(Vector2Int rc)
    {
        if (!availableRCs.Contains(rc)) return false;

        Vector2Int characterRc = CharacterManager.Instance.selectedCharacter.rc;
        int power = Map.Instance.ConsumeTiles(characterRc);
        Action.Attack(rc, power);

        return true;
    }

    public override void ShowAvailableRCs()
    {
        Character maybeCharacter = CharacterManager.Instance.selectedCharacter;
        if (maybeCharacter == null) return;

        Vector2Int characterRc = maybeCharacter.rc;

        // up
        if (Map.Instance.InsideMap(characterRc + Vector2Int.up))
            availableRCs.Add(characterRc + Vector2Int.up);
        // down
        if (Map.Instance.InsideMap(characterRc + Vector2Int.down))
            availableRCs.Add(characterRc + Vector2Int.down);
        // left
        if (Map.Instance.InsideMap(characterRc + Vector2Int.left))
            availableRCs.Add(characterRc + Vector2Int.left);
        // right
        if (Map.Instance.InsideMap(characterRc + Vector2Int.right))
            availableRCs.Add(characterRc + Vector2Int.right);

        base.ShowAvailableRCs();
    }
}