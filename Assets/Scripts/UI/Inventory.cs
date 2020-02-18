using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventoryFlex : VanillaManager
{
    public List<Item> startingItems;
    public List<ItemSlotFlex> itemSlots;
    public event Action<ItemSlotFlex> OnRightClickEvent;
    public event Action<ItemSlotFlex> OnBeginDragEvent;
    public event Action<ItemSlotFlex> OnEndDragEvent;
    public event Action<ItemSlotFlex> OnDragEvent;
    public event Action<ItemSlotFlex> OnDropEvent;
    private void Awake()
    {
        startingItems = new List<Item>();
        itemSlots = new List<ItemSlotFlex>();
    }

    public void ManualStart (Body _bodyData)
    {
        startingItems = _bodyData.items;
        for (int i = 0; i < _bodyData.numInventorySlots; i++)
        {
            GameObject AttachmentTest = CreatePanel("Inventory Slot " + i, transform);
            ItemSlotFlex itemSlot = AttachmentTest.AddComponent<ItemSlotFlex>();
            itemSlot.slotNum = i;
            itemSlot.OnRightClickEvent += OnRightClickEvent;
            itemSlot.OnBeginDragEvent += OnBeginDragEvent;
            itemSlot.OnEndDragEvent += OnEndDragEvent;
            itemSlot.OnDragEvent += OnDragEvent;
            itemSlot.OnDropEvent += OnDropEvent;
            itemSlots.Add(itemSlot);
            ScalePanel(AttachmentTest, 128, 128);
            MovePanel(AttachmentTest, 0, 0);
            Sprite sp = Resources.Load<Sprite>("UI/InventoryButton");
            SetPanelSprite(AttachmentTest, sp);
        }
        SetStartingItems();
    }
    public virtual void SetStartingItems ()
    {
        int i = 0;
        for (; i < startingItems.Count && i<itemSlots.Count; i++)
        {
            itemSlots[i].item = startingItems[i];
        }
        for (; i < itemSlots.Count; i++)
        {
            itemSlots[i].item = null;
        }
    
    }

    public bool AddItem (Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            // Debug.Log(i + " : " + itemSlots[i].item + " : " + item + " : " + itemSlots.Count + " : " + (itemSlots[i].item == item) );
            if(itemSlots[i].item == null)
            {
                itemSlots[i].item = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            // Debug.Log(i + " : " + itemSlots[i].item + " : " + item + " : " + itemSlots.Count + " : " + (itemSlots[i].item == item) );
            if(itemSlots[i].item == item)
            {
                itemSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(ItemSlotFlex itemSlot)
    {
        if (itemSlot.item != null)
        {
            itemSlot.item = null;
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].item == null)
            {
                return false;
            }
        }
        return true;
    }
}
