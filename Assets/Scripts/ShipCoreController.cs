using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
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
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach(AttachmentPoint x in attachmentPoints)
        {
            physicalAttachmentPoints.Add(CreateAttachmentPoints(x));
        }
        
    }

    private void EquipFromInventory(Item item, int _slotNum)
    {
        Debug.Log("Test Fun Equip!");
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item, out _slotNum);
            Debug.Log("Slot Num : " + _slotNum);
            if(_slotNum>=0 && _slotNum<=2)
            {
                physicalAttachmentPoints[_slotNum].GetComponent<AttachmentManager>().UpdateEquipment((EquippableItem)item);
            }
            
        }
        
    }
    private void UnequipFromEquipPanel(Item item, int _slotNum)
    {
        Debug.Log("Test Fun Unequip!");
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item, _slotNum);
            physicalAttachmentPoints[_slotNum].GetComponent<AttachmentManager>().UpdateEquipment(null);
        }
    }
    public void Equip(EquippableItem item, out int _slotNum)
    {

        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipment.AddItem(item, out previousItem, out _slotNum))
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
        else
        {
            _slotNum = -1;
        }
        
    }

    public void Unequip(EquippableItem item, int _slotNum)
    {
        Debug.Log("Slot Num : " + _slotNum);
        if (!inventory.IsFull() && equipment.RemoveItem(item, _slotNum))
        {
            inventory.AddItem(item);
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
            equipment.OnItemRightClickedEvent += UnequipFromEquipPanel;
            equipment.ManualStart(bodyData.attachmentPoints);

            inventory = _inventoryParent.GetComponent<InventoryFlex>();
            inventory.OnItemRightClickedEvent += EquipFromInventory;
            inventory.ManualStart(bodyData);

        }
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
