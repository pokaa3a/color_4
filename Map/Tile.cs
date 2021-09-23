using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class Tile
{
    // [public]
    public Vector2Int rc { get; private set; }  // row-col

    // [private]
    private GameObject gameObject;
    private Vector2 spriteWH;
    private List<MapObject> objects = new List<MapObject>();
    private TileComponent component;
}

public partial class Tile
{
    private GameColor _color = GameColor.Empty;
    public GameColor color
    {
        get => _color;
        set
        {
            _color = value;
            SetColor();
        }
    }
}

public partial class Tile
{
    private class TileComponent : MonoBehaviour
    {
        public void CallStartCoroutine(IEnumerator iEnum)
        {
            StartCoroutine(iEnum);
        }

        public void CallDestroy()
        {
            Destroy(gameObject);
        }
    }
}

public partial class Tile
{
    public Tile(Vector2Int rc)
    {
        this.rc = rc;
        this.spriteWH = Map.Instance.tileWH;

        gameObject = new GameObject($"tile_{rc.x}_{rc.y}");
        component = gameObject.AddComponent<TileComponent>() as TileComponent;

        // Position
        Vector2 xy = Map.Instance.RCtoXY(rc);
        gameObject.transform.position = new Vector3(xy.x, xy.y, 0);

        // Sprite
        gameObject.AddComponent<SpriteRenderer>();
        this.color = GameColor.Empty;
    }

    private void SetColor()
    {
        SpriteRenderer sprRend = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        if (this.color == GameColor.Empty)
        {
            sprRend.sprite = Resources.Load<Sprite>(SpritePath.Tile.empty);
        }
        else if (this.color == GameColor.Red)
        {
            sprRend.sprite = Resources.Load<Sprite>(SpritePath.Tile.red);
        }
        else if (this.color == GameColor.Yellow)
        {
            sprRend.sprite = Resources.Load<Sprite>(SpritePath.Tile.yellow);
        }
        else if (this.color == GameColor.Blue)
        {
            sprRend.sprite = Resources.Load<Sprite>(SpritePath.Tile.blue);
        }
        else
        {
            Assert.IsTrue(false, "Invalid tile color");
        }

        // Adjust scale
        gameObject.transform.localScale = new Vector2(
            spriteWH.x / sprRend.size.x,
            spriteWH.y / sprRend.size.y);
    }
}