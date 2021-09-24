using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class MapObject
{
    // [public]
    public GameObject gameObject;


    // [protected]
    protected MapObjectComponent component;
    protected Vector2 spriteWH;

    protected bool hasLife = false;
    protected int maxLife;
    protected int life;
    protected TextMesh lifeText;
}

public partial class MapObject
{
    private Vector2Int _rc = Vector2Int.zero;
    public Vector2Int rc
    {
        get => _rc;
        set
        {
            // Generic method of Tile.RemoveObject
            var t = typeof(Tile);
            var m = t.GetMethod("RemoveObject");
            var g = m.MakeGenericMethod(this.GetType());
            g.Invoke(Map.Instance.GetTile(_rc), null);

            _rc = value;
            Map.Instance.GetTile(_rc).InsertObject(this);
        }
    }

    private string _spritePath;
    public string spritePath
    {
        get => _spritePath;
        set
        {
            _spritePath = value;
            SetSprite();
        }
    }
}

public partial class MapObject
{
    protected class MapObjectComponent : MonoBehaviour
    {
        MapObject mapObject;

        public void CallStartCoroutine(IEnumerator iEnum)
        {
            StartCoroutine(iEnum);
        }

        public void CallDestroy()
        {
            Destroy(gameObject);
        }

        public void Register(MapObject mapObject)
        {
            this.mapObject = mapObject;
        }
    }
}

public partial class MapObject
{
    public MapObject() { }

    public MapObject(Vector2Int rc)
    {
        gameObject = new GameObject();
        // Add component
        component = this.gameObject.AddComponent<MapObjectComponent>() as MapObjectComponent;
        component.Register(this);

        this.rc = rc;
        this.spriteWH = Map.Instance.tileWH;
    }

    public void StartCoroutine(IEnumerator iEnum)
    {
        component.CallStartCoroutine(iEnum);
    }

    public void Destroy()
    {
        component.CallDestroy();
    }

    public void SetSpriteSortingOrder(int idx)
    {
        SpriteRenderer sprRend = gameObject.GetComponent<SpriteRenderer>();
        if (sprRend == null)
        {
            sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        }
        sprRend.sortingOrder = idx;
    }

    protected void SetSprite()
    {
        SpriteRenderer sprRend = gameObject.GetComponent<SpriteRenderer>();
        if (sprRend == null)
        {
            sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        }
        sprRend.sprite = Resources.Load<Sprite>(spritePath);
        // Adjust scale
        gameObject.transform.localScale = new Vector2(
            spriteWH.x / sprRend.size.x,
            spriteWH.y / sprRend.size.y);
    }
}