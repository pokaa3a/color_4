using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillAttack : Skill
{

}

public partial class SkillAttack : Skill
{
    public SkillAttack() : base()
    {
        this.iconSprite = SpritePath.Skill.Icon.attack;
        this.descriptionSprite = SpritePath.Skill.Description.attack;
    }
}