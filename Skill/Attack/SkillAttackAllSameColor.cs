using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillAttackAllSameColor : Skill
{

}

public partial class SkillAttackAllSameColor : Skill
{
    public SkillAttackAllSameColor() : base()
    {
        this.iconSprite = SpritePath.Skill.Icon.attackSameColor;
        this.descriptionSprite = SpritePath.Skill.Description.attackSameColor;
    }
}