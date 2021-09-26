using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Skill
{
    // [public]
    public string iconSprite { get; protected set; }
    public string descriptionSprite { get; protected set; }
    public HashSet<Vector2Int> availableRCs = new HashSet<Vector2Int>();

    // [private]
}

public partial class Skill
{

}

public partial class Skill
{
    public Skill() { }

    public virtual bool Launch(Vector2Int rc) { return true; }

    public virtual void ShowAvailableRCs()
    {
        foreach (Vector2Int tileRc in availableRCs)
        {
            Effect e = new Effect(tileRc);
            e.spritePath = SpritePath.Object.Effect.slashesOrange;
        }
    }

    public virtual void CleanAvailableRCs()
    {
        foreach (Vector2Int tileRc in availableRCs)
        {
            Map.Instance.GetTile(tileRc).DestroyObject<Effect>();
        }
        availableRCs.Clear();
    }
}