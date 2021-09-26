using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action
{
    public static void Attack(Vector2Int rc, int amount)
    {
        if (!Map.Instance.InsideMap(rc)) return;

        Character maybeCharacter = Map.Instance.GetTile(rc).GetObject<Character>();
        if (maybeCharacter != null)
        {
            maybeCharacter.life -= amount;
        }

        // TODO: attack other types of objects

        CoroutineRunner.RunCoroutine(AttackCoroutine(rc));
    }
}

public partial class Action
{
    private static IEnumerator AttackCoroutine(Vector2Int rc)
    {
        Effect e = new Effect(rc);
        e.spritePath = SpritePath.Object.Effect.attack;

        yield return new WaitForSeconds(0.5f);
        Map.Instance.GetTile(rc).DestroyObject(e);
    }
}