using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapObject
{
    // [public]
    public Vector2Int rc { get; protected set; }

    // [private]
    private GameObject gameObject;
    private MapObjectComponent component;
    private Vector2 spriteWH;

}

public partial class MapObject
{

    private string _spritePath;
    public string spritePath
    {
        get => _spritePath;
    }
}

public partial class MapObject
{
    private class MapObjectComponent : MonoBehaviour
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
    }
}

public partial class MapObject
{
    public void StartCoroutine(IEnumerator iEnum)
    {
        component.CallStartCoroutine(iEnum);
    }

    public void Destroy()
    {
        component.CallDestroy();
    }

    private void SetSprite()
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