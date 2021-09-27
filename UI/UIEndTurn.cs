using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIEndTurn : UIObject
{

}

public partial class UIEndTurn : UIObject
{
    public UIEndTurn() : base(ObjectPath.endTurn)
    {
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(-980f, -450f);
        rectTransform.sizeDelta = new Vector2(280f, 120f);
    }

    protected override void Click()
    {
        if (!(Administrator.Instance.state is StateEnemy))
            Administrator.Instance.UIClick(typeof(UIEndTurn));
    }
}