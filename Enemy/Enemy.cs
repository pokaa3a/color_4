using System;
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

    public IEnumerator Act()
    {
        yield return MoveCoroutine();
    }

    protected IEnumerator MoveCoroutine()
    {
        List<Vector2Int> moveRCs = PlanMoves();

        // Draw path
        List<Effect> movingPathTiles = new List<Effect>();
        foreach (Vector2Int rc in moveRCs)
        {
            Effect e = new Effect(rc);
            e.spritePath = SpritePath.Object.Effect.slashesOrange;
            movingPathTiles.Add(e);
        }
        yield return new WaitForSeconds(1f);

        // Move
        foreach (Vector2Int rc in moveRCs)
        {
            this.rc = rc;
            yield return new WaitForSeconds(0.2f);
        }

        // Erase moving path tiles
        for (int i = 0; i < moveRCs.Count; ++i)
        {
            Map.Instance.GetTile(moveRCs[i]).DestroyObject(movingPathTiles[i]);
        }
        yield return new WaitForSeconds(1f);
    }

    protected List<Vector2Int> PlanMoves()
    {
        // Decide destination
        // TODO: Meaningful destination
        Vector2Int dstRc = new Vector2Int(
            UnityEngine.Random.Range(0, Map.rows),
            UnityEngine.Random.Range(0, Map.rows));
        for (int i = 0; i < 10; ++i)
        {
            if (Map.Instance.GetTile(dstRc).IsEmpty()) break;
            dstRc = new Vector2Int(
                UnityEngine.Random.Range(0, Map.rows),
                UnityEngine.Random.Range(0, Map.rows));
        }

        // Path planning
        const int maxSteps = 3;
        List<Vector2Int> rcMoves = new List<Vector2Int>();
        Vector2Int lastRc = this.rc;
        Vector2Int distToDst = new Vector2Int(
            Math.Abs(dstRc.x - lastRc.x),
            Math.Abs(dstRc.y - lastRc.y));
        while (rcMoves.Count < maxSteps && distToDst.x + distToDst.y > 0)
        {
            if (distToDst.x > 0)
            {
                int r = lastRc.x + (dstRc.x > lastRc.x ? 1 : -1);
                rcMoves.Add(new Vector2Int(r, lastRc.y));
            }
            else
            {
                int c = lastRc.y + (dstRc.y > lastRc.y ? 1 : -1);
                rcMoves.Add(new Vector2Int(lastRc.x, c));
            }
            lastRc = rcMoves.Count > 0 ? rcMoves[rcMoves.Count - 1] : this.rc;
            distToDst = new Vector2Int(
                Math.Abs(dstRc.x - lastRc.x),
                Math.Abs(dstRc.y - lastRc.y));
        }

        return rcMoves;
    }
}