using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public partial class UIObject
{
    // [public]

    // [prptected]
    protected GameObject gameObject;
    protected UIObjectComponent component;
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
            gameObject.SetActive(_enabled);
            // if (_enabled) Enable();
            // else Disable();
        }
    }
}

public partial class UIObject
{
    public class UIObjectComponent : MonoBehaviour, IPointerClickHandler
    {
        public UIObject uiObject;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            uiObject.Click();
        }
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
        component.uiObject = this;
    }

    protected virtual void Click() { }

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