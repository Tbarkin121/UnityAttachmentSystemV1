using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[System.Serializable]
public class ItemSlotFlex : MonoBehaviour, IPointerClickHandler, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    private Image image;
    public event Action<ItemSlotFlex> OnRightClickEvent;
    public event Action<ItemSlotFlex> OnBeginDragEvent;
    public event Action<ItemSlotFlex> OnEndDragEvent;
    public event Action<ItemSlotFlex> OnDragEvent;
    public event Action<ItemSlotFlex> OnDropEvent;
    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1,1,1,0);
    private Item _item;
    private GameObject itemDisplay;
    public int slotNum;
    public Item item
    {
        get { return _item; }
        set 
        {
            _item = value;
            if(_item == null) 
            {
                image.color = disabledColor;
                image.enabled = true;
            }
            else
            {
                image.sprite = _item.artwork;
                image.color = normalColor;
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

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }
    Vector2 origionalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
        // origionalPosition = image.transform.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
        // image.transform.position = origionalPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
        // image.transform.position = Input.mousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }

    protected virtual void OnValidate()
    {
        // if (image == null)
        //     image = itemDisplay.GetComponent<Image>();
    }
}
