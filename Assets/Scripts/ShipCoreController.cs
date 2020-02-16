using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class ShipCoreController : VanillaManager
{
    public Body bodyData;
    public Thruster boosterData;
    public Item weaponData;
    public List<AttachmentPoint> attachmentPoints;
    public List<EquipmentSlotFlex> equipmentSlots;
    
    public Rigidbody2D rb;
    private List<GameObject> children;
    private int invtest = 0;
    private int eqtest = 0;
    private int timertest = 0;
    private bool eventbool = false;
    public InventoryFlex inventory;
    public EquipmentFlex equipment;
    // public event Action<Item> OnItemRightClickedEvent;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        foreach(AttachmentPoint x in attachmentPoints)
        {
            Debug.Log("Creating " + x.name);
            children.Add(CreateAttachmentPoints(x));
        }
        
    }
    void Update()
    {
        InventoryTest();
        EquipmentTest();
        timertest++;
        if(timertest>=100)
            timertest = 1;
    }

    private void EquipFromInventory(Item item)
    {
        Debug.Log("Test Fun Equip!");
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }
    private void UnequipFromEquipPanel(Item item)
    {
        Debug.Log("Test Fun Unequip!");
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item);
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

    public void Unequip(EquippableItem item)
    {
        if (!inventory.IsFull() && equipment.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
    
    
    
    // public bool AddItemEquipment (EquippableItem item, out EquippableItem previousItem)
    // {
    //     for (int i = 0; i < equipmentSlots.Count; i++)
    //     {
    //         if(equipmentSlots[i].equipmentType == item.equipmentType && equipmentSlots[i].item == null)
    //         {
    //             previousItem = (EquippableItem)equipmentSlots[i].item;
    //             equipmentSlots[i].item = item;
    //             return true;
    //         }
    //     }
    //     previousItem = null;
    //     return false;
    // }
    // public bool RemoveItemEquipment(EquippableItem item)
    // {
    //     for (int i = 0; i < equipmentSlots.Count; i++)
    //     {
    //         if(equipmentSlots[i].item == item)
    //         {
    //             equipmentSlots[i].item = null;
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    

    private void InventoryTest()
    {
        // if(timertest == 0)
        // {
        //     for(int i = 0; i<itemSlots.Count; i++)
        //     {
        //         itemSlots[i].item = null;
        //     }
        //     itemSlots[invtest].item = items[0];
        //     invtest++;
        //     if(invtest>=itemSlots.Count)
        //         invtest = 0;
        // }
    }
    private void EquipmentTest()
    {
        // if(timertest == 0)
        // {
        //     for(int i = 0; i<equipmentSlots.Count; i++)
        //     {
        //         equipmentSlots[i].item = null;
        //     }
        //     equipmentSlots[eqtest].item = inventory.items[0];
        //     eqtest++;
        //     if(eqtest>=equipmentSlots.Count)
        //         eqtest = 0;
        // }
    }
    public void StartUI(Canvas _canvas)
    {
        children = new List<GameObject>();
        
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
        GameObject attachmentPoint = new GameObject();
        attachmentPoint.name = _attachmentPoint.name;
        attachmentPoint.transform.SetParent(transform);
        attachmentPoint.transform.localPosition = _attachmentPoint.location;
        attachmentPoint.transform.rotation = Quaternion.identity;
        AttachmentManager attachmentManager = attachmentPoint.AddComponent<AttachmentManager>();
        attachmentManager.equipmentType = _attachmentPoint.equipmentType;
        attachmentManager.boosterData = boosterData;
        attachmentManager.weaponData = weaponData;
        attachmentManager.parent = gameObject;
        attachmentManager.ship_rb = rb;
        BoxCollider2D bc = attachmentPoint.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(0.05f, 0.1f); //This type of thing should be stored on the body object
        // ShipCoreController scController = thruster1.AddComponent<ShipCoreController>();
        // scController.bodyData = bodyData;
        return attachmentPoint;
    }

    public void CPU (float targetForce, float targetTorque)
    {
        foreach(GameObject x in children)
        {
            AttachmentManager AM = x.GetComponent<AttachmentManager>();
            if ( AM.equipmentType == EquipmentType.Thruster)
            {
                AM.RequestForce(targetForce);
                AM.RequestTurn(targetTorque);
            }
        }
    }

    public void TestDamage(float damage)
    {
        children[0].GetComponent<AttachmentManager>().TakeDamage(damage);
    }

    public void DeathReport(GameObject _gameObject)
    {
        children.Remove(_gameObject);
    }
}
