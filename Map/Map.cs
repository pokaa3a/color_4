using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// uv: Screen space (1170 x 2532)
// xy: World space (2.31*2 x 5*2)
// rc: Row Column

public partial class Map
{
    // [public]
    public const int rows = 8;
    public Vector2 screenWH { get; private set; }
    public Vector2 tileWH { get; private set; }

    // [private]
    private List<Tile> tiles = new List<Tile>();
    private const float marginN = 0f;
    private const float marginS = 0f;
    private const float marginW = 0f;
    private const float marginE = 0f;

    private Vector2 xyN;
    private Vector2 xyS;
    private Vector2 xyW;
    private Vector2 xyE;
}

public partial class Map
{
    // Singleton
    private static Map _instance;
    public static Map Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Map();
            }
            return _instance;
        }
    }
}

public partial class Map
{
    public Tile GetTile(Vector2Int rc)
    {
        return tiles[rc.x * rows + rc.y];
    }

    public void InitializeMap()
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < rows; ++c)
            {
                tiles.Add(new Tile(new Vector2Int(r, c)));
            }
        }
        RefillTiles();
    }

    public void RefillTiles()
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < rows; ++c)
            {
                Vector2Int rc = new Vector2Int(r, c);
                Tile tile = GetTile(rc);
                if (tile.color == GameColor.Empty)
                {
                    int nextColor = UnityEngine.Random.Range(0, GameConst.numColors);
                    switch (nextColor)
                    {
                        case 0:
                            tile.color = GameColor.Red;
                            break;
                        case 1:
                            tile.color = GameColor.Blue;
                            break;
                        case 2:
                            tile.color = GameColor.Yellow;
                            break;
                        case 3:
                            tile.color = GameColor.Green;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public int ConsumeTiles(Vector2Int rc)
    {
        // BFS
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        GameColor selectedColor = GetTile(rc).color;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> consumed = new HashSet<Vector2Int>();
        queue.Enqueue(rc);
        consumed.Add(rc);
        while (queue.Count > 0)
        {
            int qCount = queue.Count;
            for (int i = 0; i < qCount; ++i)
            {
                Vector2Int curRc = queue.Dequeue();
                foreach (Vector2Int d in directions)
                {
                    Vector2Int nextRc = curRc + d;
                    if (InsideMap(nextRc) &&
                        GetTile(nextRc).color == selectedColor &&
                        !consumed.Contains(nextRc))
                    {
                        queue.Enqueue(nextRc);
                        consumed.Add(nextRc);
                    }
                }
            }
        }

        // Play animation of consuming tiles
        CoroutineRunner.RunCoroutine(ConsumeTilesCoroutine(consumed));
        return consumed.Count;
    }

    public bool InsideMap(Vector2 xy)
    {
        Func<Vector2, Vector2, Vector2, bool> lineSign = (xy, xy1, xy2) =>
            (xy2.y - xy1.y) * xy.x - (xy2.x - xy1.x) * xy.y +
            (xy2.x - xy1.x) * xy1.y - (xy2.y - xy1.y) * xy1.x > 0f;
        bool sign1 = lineSign(xy, xyW, xyN);
        bool sign2 = lineSign(xy, xyS, xyE);
        bool sign3 = lineSign(xy, xyS, xyW);
        bool sign4 = lineSign(xy, xyE, xyN);
        return sign1 != sign2 && sign3 != sign4;
    }

    public Vector2Int XYtoRC(Vector2 xy)
    {
        if (!InsideMap(xy)) return new Vector2Int(-1, -1);

        Func<Vector2, Vector2, Vector2, bool> lineSign = (xy, xy1, xy2) =>
            (xy2.y - xy1.y) * xy.x - (xy2.x - xy1.x) * xy.y +
            (xy2.x - xy1.x) * xy1.y - (xy2.y - xy1.y) * xy1.x > 0f;

        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < rows; ++c)
            {
                Vector2 xyL = xyW + new Vector2(                                // left
                    (r + c) * tileWH.x / 2f,
                    (c - r) * tileWH.y / 2f);
                Vector2 xyR = xyL + new Vector2(tileWH.x, 0f);                  // right
                Vector2 xyT = xyL + tileWH / 2f;                                // top
                Vector2 xyB = xyL + new Vector2(tileWH.x / 2f, -tileWH.y / 2f); // bottom

                bool sign1 = lineSign(xy, xyL, xyT);
                bool sign2 = lineSign(xy, xyB, xyR);

                bool sign3 = lineSign(xy, xyB, xyL);
                bool sign4 = lineSign(xy, xyR, xyT);

                if (sign1 != sign2 && sign3 != sign4)
                {
                    return new Vector2Int(r, c);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public Vector2 RCtoXY(Vector2Int rc)
    {
        Assert.IsTrue(InsideMap(rc), $"rc = {rc}");

        float x = (rc.x + rc.y) * tileWH.x / 2f;
        float y = (rc.y - rc.x) * tileWH.y / 2f;
        return xyW + new Vector2(x, y) + new Vector2(tileWH.x / 2f, 0f);
    }

    public bool InsideMap(Vector2Int rc)
    {
        return rc.x >= 0 &&
            rc.x < rows &&
            rc.y >= 0 &&
            rc.y < rows;
    }

    private Map()
    {
        tiles = new List<Tile>();

        // This gets the Main Camera from the Scene
        Camera mainCam = Camera.main;
        Assert.IsNotNull(mainCam);

        float screenHeight = mainCam.orthographicSize * 2f;
        float screenWidth = mainCam.aspect * screenHeight;
        this.screenWH = new Vector2(screenWidth, screenHeight);

        float tileHeight = screenHeight * (1f - marginN - marginS) / rows;
        float tileWidth = screenWidth * (1f - marginW - marginE) / rows;
        this.tileWH = new Vector2(tileWidth, tileHeight);

        float xW = -screenWH.x / 2f + screenWH.x * marginW;
        float xE = screenWH.x / 2f - screenWH.x * marginE;
        float xC = (xW + xE) / 2f;
        float yS = -screenWH.y / 2f + screenWH.y * marginS;
        float yN = screenWH.y / 2f - screenWH.y * marginN;
        float yC = (yS + yN) / 2f;
        xyN = new Vector2(xC, yN);
        xyS = new Vector2(xC, yS);
        xyW = new Vector2(xW, yC);
        xyE = new Vector2(xE, yC);
    }

    // yield can't be used in lambda function...
    private IEnumerator ConsumeTilesCoroutine(HashSet<Vector2Int> RCs)
    {
        foreach (Vector2Int rc in RCs)
        {
            yield return new WaitForSeconds(0.1f);
            GetTile(rc).color = GameColor.Empty;
        }
        yield return new WaitForSeconds(0.3f);
        RefillTiles();
    }
}