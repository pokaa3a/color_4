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
    private GameObject spriteObject;
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

        spriteObject = new GameObject("sprite-object");
        spriteObject.transform.SetParent(gameObject.transform);

        // Position
        Vector2 xy = Map.Instance.RCtoXY(rc);
        gameObject.transform.position = new Vector3(xy.x, xy.y, 0);

        // Sprite
        spriteObject.AddComponent<SpriteRenderer>();
        this.color = GameColor.Empty;
    }

    public T GetObject<T>() where T : MapObject, new()
    {
        foreach (MapObject obj in objects)
        {
            if (obj is T) return (T)obj;
        }
        return null;
    }

    // Create new object
    public T AddObject<T>() where T : MapObject, new()
    {
        T newObject = System.Activator.CreateInstance(typeof(T), rc) as T;
        InsertObject(newObject);
        return null;
    }

    // Insert an existing object
    public void InsertObject(MapObject obj)
    {
        objects.Add(obj);
        obj.gameObject.transform.parent = this.gameObject.transform;
        obj.gameObject.transform.localPosition = Vector2.zero;

        obj.SetSpriteSortingOrder(this.gameObject.transform.childCount);
    }

    // Destroy object
    public void DestroyObject<T>() where T : MapObject, new()
    {
        MapObject obj = (MapObject)GetObject<T>();
        if (obj == null) return;

        objects.Remove(obj);
        obj.Destroy();
    }

    // Remove an object from the tile
    public T RemoveObject<T>() where T : MapObject, new()
    {
        MapObject obj = (MapObject)GetObject<T>();
        if (obj == null) return null;

        objects.Remove(obj);
        return (T)obj;
    }

    private void SetColor()
    {
        SpriteRenderer sprRend = spriteObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
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