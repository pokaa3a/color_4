using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIObject
{
    // [public]

    // [private]
    GameObject gameObject;
    UIObjectComponent component;
}

public partial class UIObject
{
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

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            if (_enabled) Enable();
            else Disable();
        }
    }
}

public partial class UIObject
{
    public class UIObjectComponent : MonoBehaviour
    {

    }
}

public partial class UIObject
{
    public UIObject(string name)
    {
        gameObject = GameObject.Find(name);
        if (gameObject == null)
        {
            gameObject = new GameObject();
            gameObject.name = name;
        }
        var Canvas = GameObject.Find(ObjectPath.canvas);
        gameObject.transform.SetParent(Canvas.transform);
        component = gameObject.AddComponent<UIObjectComponent>() as UIObjectComponent;
    }

    protected void SetSprite()
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        if (img == null)
        {
            img = gameObject.AddComponent<Image>() as Image;
        }
        img.sprite = Resources.Load<Sprite>(spritePath);
    }

    protected void Enable()
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        if (img == null)
        {
            img = gameObject.AddComponent<Image>() as Image;
        }
        img.enabled = true;
    }

    protected void Disable()
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        if (img == null)
        {
            img = gameObject.AddComponent<Image>() as Image;
        }
        img.enabled = false;
    }
}