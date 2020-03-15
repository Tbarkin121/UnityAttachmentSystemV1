using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VanillaManager : MonoBehaviour
{
    public GameObject CreatePanel(string _name, Transform _parent)
    {
        GameObject Panel = new GameObject(_name);
        Panel.transform.SetParent(_parent, false);
        Panel.AddComponent<CanvasRenderer>();
        Panel.AddComponent<RectTransform>();
        Panel.AddComponent<Image>();
        SetAnchorPoints(Panel);
        return Panel;
    }
    public void MovePanel(GameObject _panel, int x, int y)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.anchoredPosition = new Vector3(x, y, 0);
        }
    }
    public void ScalePanel(GameObject _panel, int width, int height)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.sizeDelta  = new Vector3(width, height, 0);
        }
    }

    public void ColorPanel(GameObject _panel, Color _color)
    {  
        Image Im = _panel.GetComponent<Image>();
        if (Im != null)
        {
            Im.color = _color;
        }
    }
    public void SetPanelSprite(GameObject _panel, Sprite _sprite)
    {
        Image Im = _panel.GetComponent<Image>();
        if (Im != null)
        {
            Im.color = Color.white;
            Im.sprite = _sprite;
        }
    }
    public void SetAnchorPoints(GameObject _panel)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.anchorMax = new Vector2(0.5f, 0.5f);
            RectT.anchorMin = new Vector2(0.5f, 0.5f);
            RectT.pivot = new Vector2(0.5f, 0.5f);
        }
    }
    public void AddGridLayoutGroup(GameObject _panel)
    {
        GridLayoutGroup GLG = _panel.AddComponent<GridLayoutGroup>();
        GLG.padding.left = 0;
        GLG.padding.right = 0;
        GLG.padding.top = 0;
        GLG.padding.bottom = 0;
        GLG.cellSize = new Vector2(128,128);
        GLG.spacing = new Vector2(0, 0);
    }
    
}
