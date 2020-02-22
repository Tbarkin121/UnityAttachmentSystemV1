using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Inventory : VanillaManager
{
    public List<Item> startingItems;
    public List<ItemSlot> itemSlots;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    private void Awake()
    {
        startingItems = new List<Item>();
        itemSlots = new List<ItemSlot>();
    }

    public void ManualStart (Body _bodyData)
    {
        
        startingItems = _bodyData.items;
        for (int i = 0; i < _bodyData.numInventorySlots; i++)
        {
            GameObject AttachmentTest = CreatePanel("Inventory Slot " + i, transform);
            ItemSlot itemSlot = AttachmentTest.AddComponent<ItemSlot>();
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
    public bool RemoveItem(ItemSlot itemSlot)
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
