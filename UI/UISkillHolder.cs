using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UISkillHolder
{
    // [public]
    public List<SkillHolder> holders = new List<SkillHolder>();

    // [private]
}

public partial class UISkillHolder
{
    public class SkillHolder : UIObject
    {
        public UISkillHolder uiSkillHolder;
        private int id = 0;
        private GameObject selectedObject;

        public SkillHolder(int id) : base($"SkillHolder{id}")
        {
            this.id = id;

            // Set position & size
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            if (id == 0)
            {
                rectTransform.localPosition = new Vector2(640f, -450f);
                rectTransform.sizeDelta = new Vector2(200f, 200f);
            }
            else if (id == 1)
            {
                rectTransform.localPosition = new Vector2(930f, -380f);
                rectTransform.sizeDelta = new Vector2(200f, 200f);
            }
            else if (id == 2)
            {
                rectTransform.localPosition = new Vector2(1140, -200f);
                rectTransform.sizeDelta = new Vector2(200f, 200f);
            }

            // New SelectedRing object"
            selectedObject = new GameObject("SelectedRing");
            selectedObject.transform.SetParent(this.gameObject.transform);

            Image img = selectedObject.AddComponent<Image>() as Image;
            img.sprite = Resources.Load<Sprite>(SpritePath.Skill.Icon.selected);
            img.enabled = false;

            selectedObject.transform.localPosition = Vector2.zero;
            selectedObject.GetComponent<RectTransform>().sizeDelta =
                this.gameObject.GetComponent<RectTransform>().sizeDelta * 1.2f;
        }

        private bool _selected = false;
        public bool selected
        {
            get => _selected;
            set
            {
                _selected = value;
                Image img = selectedObject.GetComponent<Image>() as Image;
                if (img == null) img = selectedObject.AddComponent<Image>() as Image;

                img.enabled = _selected;
            }
        }

        protected override void Click()
        {
            uiSkillHolder.ClickSkill(this.id);
        }
    }

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            for (int i = 0; i < holders.Count; ++i)
            {
                Character selectedCharacter =
                    CharacterManager.Instance.selectedCharacter;
                if (selectedCharacter != null)
                {
                    holders[i].spritePath = selectedCharacter.skills[i].iconSprite;
                }

                holders[i].enabled = _enabled;
            }
        }
    }
}

public partial class UISkillHolder
{
    public SkillHolder this[int i]
    {
        get => holders[i];
        protected set { holders[i] = value; }
    }

    public UISkillHolder()
    {
        SkillHolder holder0 = new SkillHolder(0);
        SkillHolder holder1 = new SkillHolder(1);
        SkillHolder holder2 = new SkillHolder(2);

        holder0.uiSkillHolder = this;
        holder1.uiSkillHolder = this;
        holder2.uiSkillHolder = this;

        holder0.enabled = false;
        holder1.enabled = false;
        holder2.enabled = false;

        holders.Add(holder0);
        holders.Add(holder1);
        holders.Add(holder2);
    }

    private void ClickSkill(int id)
    {
        for (int i = 0; i < holders.Count; ++i)
        {
            if (i == id) holders[i].selected = !holders[i].selected;
            else holders[i].selected = false;
        }
        Administrator.Instance.UIClick(this.GetType());
    }
}