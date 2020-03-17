using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicalInventory : VanillaManager
{
    
    public List<Item> startingItems;
    public List<InventoryItem> InventoryItems;
    public event Action<InventoryItem> OnRightClickEvent;
    public event Action<InventoryItem> OnBeginDragEvent;
    public event Action<InventoryItem> OnEndDragEvent;
    public event Action<InventoryItem> OnDragEvent;
    public event Action<InventoryItem> OnDropEvent;
    private void Awake()
    {
        startingItems = new List<Item>();
        InventoryItems = new List<InventoryItem>();
    }

    public void ManualStart (Body _bodyData)
    {
        
        // startingItems = _bodyData.items;
        // for (int i = 0; i < _bodyData.numInventorySlots; i++)
        // {
        //     GameObject AttachmentTest = CreatePanel("Inventory Slot " + i, transform);
        //     ItemSlot itemSlot = AttachmentTest.AddComponent<ItemSlot>();
        //     itemSlot.slotNum = i;
        //     itemSlot.OnRightClickEvent += OnRightClickEvent;
        //     itemSlot.OnBeginDragEvent += OnBeginDragEvent;
        //     itemSlot.OnEndDragEvent += OnEndDragEvent;
        //     itemSlot.OnDragEvent += OnDragEvent;
        //     itemSlot.OnDropEvent += OnDropEvent;
        //     itemSlots.Add(itemSlot);
        //     ScalePanel(AttachmentTest, 128, 128);
        //     MovePanel(AttachmentTest, 0, 0);
        //     Sprite sp = Resources.Load<Sprite>("UI/InventoryButton");
        //     SetPanelSprite(AttachmentTest, sp);
        // }
        // SetStartingItems();
    }
    public void CreateInventoryItem(EquippableItem item, GameObject inventoryPanel)
    {
        GameObject _inventoryItemHolder = CreatePanel(item.name, inventoryPanel.transform);
        InventoryItem _inventoryItem = _inventoryItemHolder.AddComponent<InventoryItem>();
        _inventoryItem.OnRightClickEvent += OnRightClickEvent;
        _inventoryItem.OnBeginDragEvent += OnBeginDragEvent;
        _inventoryItem.OnEndDragEvent += OnEndDragEvent;
        _inventoryItem.OnDragEvent += OnDragEvent;
        _inventoryItem.OnDropEvent += OnDropEvent;
        InventoryItems.Add(_inventoryItem);
        
        // ScalePanel(_inventoryItemHolder, 256, 128);
        MovePanel(_inventoryItemHolder, -128, -128);
        Sprite sp = item.artwork;
        SetPanelSprite(_inventoryItemHolder, sp);
    }
    public void CreateItemSlot(GameObject Panel)
    {
        // GameObject _attachmentPoint = CreatePanel("Inventory Slot", Panel.transform);
        // InventoryItem inventoryItem = _attachmentPoint.AddComponent<InventoryItem>();
        // // itemSlot.slotNum = i;
        // itemSlot.OnRightClickEvent += OnRightClickEvent;
        // itemSlot.OnBeginDragEvent += OnBeginDragEvent;
        // itemSlot.OnEndDragEvent += OnEndDragEvent;
        // itemSlot.OnDragEvent += OnDragEvent;
        // itemSlot.OnDropEvent += OnDropEvent;
        // itemSlots.Add(itemSlot);
        // ScalePanel(_attachmentPoint, 128, 128);
        // MovePanel(_attachmentPoint, 0, 0);
        // Sprite sp = Resources.Load<Sprite>("UI/InventoryButton");
        // SetPanelSprite(_attachmentPoint, sp);
    }
    public void DestroyItemSlot()
    {

    }
    public virtual void SetStartingItems ()
    {
        // int i = 0;
        // for (; i < startingItems.Count && i<itemSlots.Count; i++)
        // {
        //     itemSlots[i].item = startingItems[i];
        // }
        // for (; i < itemSlots.Count; i++)
        // {
        //     itemSlots[i].item = null;
        // }
    
    }

    public bool AddItem (Item item)
    {
        // for (int i = 0; i < itemSlots.Count; i++)
        // {
        //     // Debug.Log(i + " : " + itemSlots[i].item + " : " + item + " : " + itemSlots.Count + " : " + (itemSlots[i].item == item) );
        //     if(itemSlots[i].item == null)
        //     {
        //         itemSlots[i].item = item;
        //         return true;
        //     }
        // }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        // for (int i = 0; i < itemSlots.Count; i++)
        // {
        //     // Debug.Log(i + " : " + itemSlots[i].item + " : " + item + " : " + itemSlots.Count + " : " + (itemSlots[i].item == item) );
        //     if(itemSlots[i].item == item)
        //     {
        //         itemSlots[i].item = null;
        //         return true;
        //     }
        // }
        return false;
    }
    public bool RemoveItem(ItemSlot itemSlot)
    {
        // if (itemSlot.item != null)
        // {
        //     itemSlot.item = null;
        //     return true;
        // }
        return false;
    }

    public bool IsFull()
    {
        // for (int i = 0; i < itemSlots.Count; i++)
        // {
        //     if(itemSlots[i].item == null)
        //     {
        //         return false;
        //     }
        // }
        return true;
    }
}
