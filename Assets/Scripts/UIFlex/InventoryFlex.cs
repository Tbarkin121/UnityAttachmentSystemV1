using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventoryFlex : VanillaManager
{
    public List<Item> items;
    public List<ItemSlotFlex> itemSlots;
    public event Action<Item, int> OnItemRightClickedEvent;
    private void Awake()
    {
        items = new List<Item>();
        itemSlots = new List<ItemSlotFlex>();
    }

    public void ManualStart (Body _bodyData)
    {
        items = _bodyData.items;
        for (int i = 0; i < _bodyData.numInventorySlots; i++)
        {
            GameObject AttachmentTest = CreatePanel("Inventory Slot " + i, transform);
            ItemSlotFlex itemSlot = AttachmentTest.AddComponent<ItemSlotFlex>();
            itemSlot.slotNum = i;
            itemSlots.Add(itemSlot);
            itemSlot.OnRightClickEvent += OnItemRightClickedEvent;
            ScalePanel(AttachmentTest, 128, 128);
            MovePanel(AttachmentTest, 0, 0);
            Sprite sp = Resources.Load<Sprite>("UI/InventoryButton");
            SetPanelSprite(AttachmentTest, sp);
        }
        RefreshUI();
    }
    public virtual void RefreshUI ()
    {
        int i = 0;
        for (; i < items.Count && i<itemSlots.Count; i++)
        {
            itemSlots[i].item = items[i];
        }
        for (; i < itemSlots.Count; i++)
        {
            itemSlots[i].item = null;
        }
    
    }
    public bool AddItem (Item item)
    {
        if (IsFull())
            return false;
        items.Add(item);
        RefreshUI();
        return true; 
    }
    public bool RemoveItem(Item item)
    {
        if(items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }
    public bool IsFull()
    {
        return items.Count >= itemSlots.Count;
    }
}
