using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShipCoreController : VanillaManager
{
    public Body bodyData;
    public List<AttachmentPoint> attachmentPoints;
    public List<EquipmentSlotFlex> equipmentSlots;
    
    public Rigidbody2D rb;
    private List<GameObject> physicalAttachmentPoints;
    public InventoryFlex inventory;
    public EquipmentFlex equipment;
    private Image draggableItem; //Need to create this still.. should just be a panel like the rest of them
    private ItemSlotFlex draggedSlot;

    // private void Awake ()
    // {
        
        
    // }   

    // public void Equip(EquippableItem item, out int _slotNum)
    // {

    //     if (inventory.RemoveItem(item))
    //     {
    //         EquippableItem previousItem;
    //         if (equipment.AddItem(item, out previousItem, out _slotNum))
    //         {
    //             if (previousItem != null)
    //             {
    //                 inventory.AddItem(previousItem);
    //             }
    //         }
    //         else
    //         {
    //             inventory.AddItem(item);
    //         }
    //     }
    //     else
    //     {
    //         _slotNum = -1;
    //     }
        
    // }

    // public void Unequip(EquippableItem item, int _slotNum)
    // {
    //     Debug.Log("Slot Num : " + _slotNum);
    //     if (!inventory.IsFull() && equipment.RemoveItem(item, _slotNum))
    //     {
    //         inventory.AddItem(item);
    //     }
    // }
    private void Equip(ItemSlotFlex itemSlot)
    {
        EquippableItem equippableItem = itemSlot.item as EquippableItem;
        if (equippableItem != null)
        {
            // int _slotNum;
            Equip(equippableItem);
        }
    }
    public void Equip(EquippableItem item)
    {

        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipment.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    private void Unequip(ItemSlotFlex itemSlot)
    {
        EquippableItem equippableItem = itemSlot.item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }
    public void Unequip(EquippableItem item)
    {
        if (!inventory.IsFull() && equipment.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }

    
    
    private void BeginDrag(ItemSlotFlex itemSlot)
    {
        Debug.Log("SSC : BeginDrag");
        if (itemSlot.item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.artwork;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlotFlex itemSlot)
    {
        Debug.Log("SSC : EndDrag");
        draggedSlot = null;
        draggableItem.enabled = false;

    }
    private void Drag(ItemSlotFlex itemSlot)
    {
        Debug.Log("SSC : Drag");
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
        
    }
    private void Drop(ItemSlotFlex dropitemSlot)
    {
        Debug.Log("DropItem " + dropitemSlot.CanReceiveItem(draggedSlot.item));
        Debug.Log("DragItem " + draggedSlot.CanReceiveItem(dropitemSlot.item));
        if (dropitemSlot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropitemSlot.item))
        {
            EquippableItem dragItem = draggedSlot.item as EquippableItem;
            EquippableItem dropItem = dropitemSlot.item as EquippableItem;
            
            if (draggedSlot is EquipmentSlotFlex)
            {
                if (dragItem != null) Unequip(dragItem);
                if (dropItem != null) Equip(dropItem);
            }
            if (dropitemSlot is EquipmentSlotFlex)
            {
                if (dragItem != null) Equip(dragItem);
                if (dropItem != null) Unequip(dropItem);
            }

            Item draggedItem = draggedSlot.item;
            draggedSlot.item = dropitemSlot.item;
            dropitemSlot.item = draggedItem;
        }
        

    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach(AttachmentPoint x in attachmentPoints)
        {
            physicalAttachmentPoints.Add(CreateAttachmentPoints(x));
        }
        
    }

    
    
    public void StartUI(Canvas _canvas)
    {
        physicalAttachmentPoints = new List<GameObject>();
        
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = bodyData.artwork;
        attachmentPoints = bodyData.attachmentPoints;
        equipmentSlots = new List<EquipmentSlotFlex>();
        Transform _characterPanel = _canvas.transform.Find("Character Panel");
        Transform _inventoryParent = _characterPanel.Find("Inventory Panel");
        Transform _equipmentParent = _characterPanel.Find("Equipment Panel");
        if(_canvas != null)
        {
            equipment = _equipmentParent.GetComponent<EquipmentFlex>();
            inventory = _inventoryParent.GetComponent<InventoryFlex>();
            //Right Click
            inventory.OnRightClickEvent += Equip;
            equipment.OnRightClickEvent += Unequip;
            //Begin Drag
            inventory.OnBeginDragEvent += BeginDrag;
            equipment.OnBeginDragEvent += BeginDrag;
            //End Drag
            inventory.OnEndDragEvent += EndDrag;
            equipment.OnEndDragEvent += EndDrag;
            //Drag
            inventory.OnDragEvent += Drag;
            equipment.OnDragEvent += Drag;
            //Drop
            inventory.OnDropEvent += Drop;
            equipment.OnDropEvent += Drop;
            
            equipment.ManualStart(bodyData.attachmentPoints);
            inventory.ManualStart(bodyData);
        }
        // Create Dragable Object for drag and drop functionality;
        GameObject draggableObject = CreatePanel("Draggable Item", _characterPanel);
        ScalePanel(draggableObject, 100, 100);
        draggableItem = draggableObject.GetComponent<Image>();
        draggableItem.enabled = false;
        draggableItem.raycastTarget = false;

        print("Ship Core Online!");
    }

    GameObject CreateAttachmentPoints (AttachmentPoint _attachmentPoint)
    {
        GameObject attachmentPoint = new GameObject(_attachmentPoint.name);
        attachmentPoint.transform.SetParent(transform);
        attachmentPoint.transform.localPosition = _attachmentPoint.location;
        attachmentPoint.transform.rotation = Quaternion.identity;
        AttachmentManager attachmentManager = attachmentPoint.AddComponent<AttachmentManager>();
        attachmentManager.equipmentType = _attachmentPoint.equipmentType;
        attachmentManager.parent = gameObject;
        attachmentManager.ship_rb = rb;
        BoxCollider2D bc = attachmentPoint.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(0.05f, 0.1f); //This type of thing should be stored on the body object
        return attachmentPoint;
    }

    public void CPU (float targetForce, float targetTorque)
    {
        foreach(GameObject x in physicalAttachmentPoints)
        {
            AttachmentManager AM = x.GetComponent<AttachmentManager>();
            AM.RequestForce(targetForce);
            AM.RequestTurn(targetTorque);
        }
    }

    public void TestDamage(float damage)
    {
        physicalAttachmentPoints[0].GetComponent<AttachmentManager>().TakeDamage(damage);
    }

    public void DeathReport(GameObject _gameObject)
    {
        physicalAttachmentPoints.Remove(_gameObject);
    }
}
