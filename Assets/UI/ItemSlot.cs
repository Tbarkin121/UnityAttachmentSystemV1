using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[System.Serializable]
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    public event Action<Item> OnRightClickEvent;
    private Item _item;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("How bout Here?");
            print(item);
            print(OnRightClickEvent);
            if (item != null && OnRightClickEvent != null)
            {
                Debug.Log("Probably not here...");
                OnRightClickEvent(item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
