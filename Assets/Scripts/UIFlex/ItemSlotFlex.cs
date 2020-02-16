using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[System.Serializable]
public class ItemSlotFlex : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    public event Action<Item> OnRightClickEvent;
    private Item _item;
    private GameObject itemDisplay;
    public Item item
    {
        get { return _item; }
        set 
        {
            _item = value;
            if(_item == null) 
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.artwork;
                image.enabled = true;
            }
        }
    }

    void Awake ()
    {   
        itemDisplay = new GameObject("Item Holder");
        itemDisplay.transform.SetParent(transform);
        itemDisplay.transform.localPosition = new Vector3(0, 0, 0);
        image = itemDisplay.AddComponent<Image>();
        image.sprite = null;
        image.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        // if (image == null)
        //     image = itemDisplay.GetComponent<Image>();
    }
}
