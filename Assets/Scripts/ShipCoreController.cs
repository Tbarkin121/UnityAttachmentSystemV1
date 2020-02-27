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
    public List<EquipmentSlot> equipmentSlots;
    public Rigidbody2D rb;
    private List<GameObject> physicalAttachmentPoints;
    public Inventory inventory;
    public Equipment equipment;
    private Image draggableItem; //Need to create this still.. should just be a panel like the rest of them
    private ItemSlot draggedSlot;

    
    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.item as EquippableItem;
        if (equippableItem != null)
        {
            Equip(equippableItem, itemSlot);
        }
    }
    public void Equip(EquippableItem item, ItemSlot itemSlot)
    {
        if (inventory.RemoveItem(itemSlot))
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
    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem, itemSlot);
        }
    }
    public void Unequip(EquippableItem item, ItemSlot itemSlot)
    {
        
        if (!inventory.IsFull() && equipment.RemoveItem(itemSlot))
        {
            inventory.AddItem(item);
        }
    }

    
    
    private void BeginDrag(ItemSlot itemSlot)
    {
        // Debug.Log("SSC : BeginDrag");
        if (itemSlot.item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.artwork;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        // Debug.Log("SSC : EndDrag");
        draggedSlot = null;
        draggableItem.enabled = false;

    }
    private void Drag(ItemSlot itemSlot)
    {
        // Debug.Log("SSC : Drag");
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
        
    }
    private void Drop(ItemSlot dropitemSlot)
    {
        if (dropitemSlot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropitemSlot.item))
        {
            EquippableItem dragItem = draggedSlot.item as EquippableItem;
            EquippableItem dropItem = dropitemSlot.item as EquippableItem;
            
            // The below if statements were for stat related functions I've yet to impliment
            // if (draggedSlot is EquipmentSlot)
            // {
                // if (dragItem != null) dragItem.Unequip(this);
                // if (dropItem != null) dropItem.Equip(this);
            // }
            // if (dropitemSlot is EquipmentSlot)
            // {
                // if (dragItem != null) dragItem.Equip(this);
                // if (dropItem != null) dropItem.Unequip(this);
            // }

            Item draggedItem = draggedSlot.item;
            draggedSlot.item = dropitemSlot.item;
            dropitemSlot.item = draggedItem;
        }
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(rb);
        int i = 0;
        foreach(AttachmentPoint x in attachmentPoints)
        {
            physicalAttachmentPoints.Add(CreateAttachmentPoints(x, i));
            i++;
        }
        gameObject.GetComponent<HealthMonitor>().health = bodyData.hpMax; 
        print("Ship Core Online!");
    }

    
    
    public void StartUI(Canvas _canvas)
    {
        physicalAttachmentPoints = new List<GameObject>();
        
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = bodyData.artwork;
        attachmentPoints = bodyData.attachmentPoints;
        equipmentSlots = new List<EquipmentSlot>();
        Transform _characterPanel = _canvas.transform.Find("Character Panel");
        Transform _inventoryParent = _characterPanel.Find("Inventory Panel");
        Transform _equipmentParent = _characterPanel.Find("Equipment Panel");
        if(_canvas != null)
        {
            equipment = _equipmentParent.GetComponent<Equipment>();
            inventory = _inventoryParent.GetComponent<Inventory>();
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

        

        
    }

    GameObject CreateAttachmentPoints (AttachmentPoint _attachmentPoint, int idx)
    {
        GameObject attachmentPoint = new GameObject(_attachmentPoint.name);
        attachmentPoint.transform.SetParent(transform);
        attachmentPoint.transform.localPosition = _attachmentPoint.location;
        attachmentPoint.transform.rotation = Quaternion.identity;
        attachmentPoint.tag = "VehiclePart";
        AttachmentManager attachmentManager = attachmentPoint.AddComponent<AttachmentManager>();
        attachmentManager.equipmentType = _attachmentPoint.equipmentType;
        attachmentManager.parent = gameObject;
        attachmentManager.ship_rb = rb;
        attachmentManager.ParentSlot = equipment.equipmentSlots[idx];
        equipment.equipmentSlots[idx].ChangeEquipmentEvent += attachmentManager.ChangeEquipment;
        HealthMonitor healthMonitor = attachmentPoint.AddComponent<HealthMonitor>();
        healthMonitor.health = 0;
        BoxCollider2D bc = attachmentPoint.AddComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(bc, gameObject.GetComponent<BoxCollider2D>());
        Rigidbody2D _rb = attachmentPoint.AddComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
        
        bc.size = new Vector2(0.05f, 0.1f); //This type of thing should be stored on the body object
        if (attachmentManager.equipmentType == EquipmentType.Weapon)
        {
            WeaponManager wm = attachmentPoint.AddComponent<WeaponManager>();
        }
            
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

    public void TestWeapon()
    {
        foreach(GameObject x in physicalAttachmentPoints)
        {
            EquippableItem ei = x.GetComponent<AttachmentManager>().item as EquippableItem;
            if (ei != null && ei.equipmentType == EquipmentType.Weapon)
            {
                x.GetComponent<AttachmentManager>().Fire();
            }
        }
    }

    public void DeathReport(GameObject _gameObject)
    {
        
    }
}
