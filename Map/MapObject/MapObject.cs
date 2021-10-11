using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class MapObject
{
    // [public]
    public GameObject gameObject;
    public bool attackable { get; protected set; } = false;

    // [protected]
    protected MapObjectComponent component;
    protected Vector2 spriteWH;
    protected bool hasLife = false;
    protected int maxLife;
    protected TextMesh lifeText;
}

public partial class MapObject
{
    private int _life;
    public int life
    {
        get => _life;
        set
        {
            _life = value;
            lifeText.text = $"{_life}/{maxLife}";

            if (_life <= 0) Die();
        }
    }

    private Vector2Int _rc = new Vector2Int(-1, -1);
    public Vector2Int rc
    {
        get => _rc;
        set
        {
            if (_rc.x >= 0 && _rc.y >= 0)
            {
                // Generic method of Tile.RemoveObject
                // var t = typeof(Tile);
                // var getObject = t.GetMethod("GetObject");
                // var genGetObject = getObject.MakeGenericMethod(this.GetType());
                // if (genGetObject.Invoke(Map.Instance.GetTile(_rc), null) == this)
                // {
                //     var removeObject = t.GetMethod("RemoveObject");
                //     var genRemoveObject = removeObject.MakeGenericMethod(this.GetType());
                //     genRemoveObject.Invoke(Map.Instance.GetTile(_rc), null);
                // }
                Map.Instance.GetTile(_rc).RemoveObject(this);
            }

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

    protected virtual void Die() { }

    protected void SetUpLifeText(Color32 color)
    {
        GameObject textObject = new GameObject("LifeText");
        textObject.transform.SetParent(this.gameObject.transform);
        lifeText = textObject.AddComponent<TextMesh>() as TextMesh;
        textObject.transform.localPosition = new Vector2(0f, 0.8f);
        textObject.transform.localScale = new Vector2(0.04f, 0.08f);
        lifeText.fontSize = 100;
        lifeText.text = $"{life}/{maxLife}";
        lifeText.anchor = TextAnchor.MiddleCenter;
        lifeText.color = color;
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
        gameObject.transform.localScale = new Vector2(1.0f, 1.0f);
    }
}