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
    public List<ItemSlotFlex> itemSlots;
    public List<EquipmentSlotFlex> equipmentSlots;
    // public List<> equipmentSlots;
    private List<Item> items;
    public Rigidbody2D rb;
    private List<GameObject> children;
    private int invtest = 0;
    private int eqtest = 0;
    private int timertest = 0;
    private bool eventbool = false;
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
            timertest = 0;
    }

    private void EquipFromInventory(Item item)
    {
        Debug.Log("Test Fun Equip!");

    }
    private void UnequipFromEquipPanel(Item item)
    {
        Debug.Log("Test Fun Unequip!");

    }

    private void InventoryTest()
    {
        if(timertest == 0)
        {
            for(int i = 0; i<=9; i++)
            {
                itemSlots[i].item = null;
            }
            itemSlots[invtest].item = items[0];
            invtest++;
            if(invtest>=10)
                invtest = 0;
        }
    }
    private void EquipmentTest()
    {
        if(timertest == 0)
        {
            for(int i = 0; i<=2; i++)
            {
                equipmentSlots[i].item = null;
            }
            equipmentSlots[eqtest].item = items[0];
            eqtest++;
            if(eqtest>=3)
                eqtest = 0;
        }
    }
    public void StartUI(Canvas _canvas)
    {
        children = new List<GameObject>();
        
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = bodyData.artwork;
        attachmentPoints = bodyData.attachmentPoints;
        items = bodyData.items;
        itemSlots = new List<ItemSlotFlex>();
        equipmentSlots = new List<EquipmentSlotFlex>();

        if(_canvas != null)
        {
            int i = 0;
            foreach(AttachmentPoint x in attachmentPoints)            
            {
                Transform _characterPanel = _canvas.transform.Find("Character Panel");
                Transform _parent = _characterPanel.Find("Equipment Panel");
                GameObject AttachmentTest = CreatePanel("Attachment Point " + i, _parent);
                EquipmentSlotFlex equipmentSlot = AttachmentTest.AddComponent<EquipmentSlotFlex>();
                equipmentSlots.Add(equipmentSlot);
                equipmentSlot.OnRightClickEvent += UnequipFromEquipPanel;
                ScalePanel(AttachmentTest, 128, 128);
                MovePanel(AttachmentTest, 0, 0);
                Sprite sp = Resources.Load<Sprite>("UI/EquipmentButton");
                SetPanelSprite(AttachmentTest, sp);
                i++;
                
            }

            i = 0;
            foreach(Item x in items)
            {
                Transform _characterPanel = _canvas.transform.Find("Character Panel");
                Transform _parent = _characterPanel.Find("Inventory Panel");
                GameObject AttachmentTest = CreatePanel("Inventory Slot " + i, _parent);
                ItemSlotFlex itemSlot = AttachmentTest.AddComponent<ItemSlotFlex>();
                itemSlots.Add(itemSlot);
                itemSlot.OnRightClickEvent += EquipFromInventory;
                ScalePanel(AttachmentTest, 128, 128);
                MovePanel(AttachmentTest, 0, 0);
                Sprite sp = Resources.Load<Sprite>("UI/InventoryButton");
                SetPanelSprite(AttachmentTest, sp);
                i++;
                
            }
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
