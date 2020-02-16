using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentFlex : InventoryFlex
{
    public List<AttachmentPoint> attachmentPoints;
    public List<EquipmentSlotFlex> equipmentSlots;
    new public event Action<Item> OnItemRightClickedEvent; //Watch out for this if things dont work later
    void Awake()
    {
        attachmentPoints = new List<AttachmentPoint>();
        equipmentSlots = new List<EquipmentSlotFlex>();
    }
    public void ManualStart (List<AttachmentPoint> _attachmentPoints)
    {
        attachmentPoints = _attachmentPoints;
        int i = 0;
        foreach(AttachmentPoint x in attachmentPoints)
        {
            GameObject AttachmentTest = CreatePanel("Attachment Point " + i, transform);
            EquipmentSlotFlex equipmentSlot = AttachmentTest.AddComponent<EquipmentSlotFlex>();
            equipmentSlots.Add(equipmentSlot);
            equipmentSlot.OnRightClickEvent += OnItemRightClickedEvent;
            ScalePanel(AttachmentTest, 128, 128);
            MovePanel(AttachmentTest, 0, 0);
            Sprite sp = Resources.Load<Sprite>("UI/EquipmentButton");
            SetPanelSprite(AttachmentTest, sp);
            i++;
        }
        RefreshUI();
    }
    public bool AddItem (EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(equipmentSlots[i].equipmentType == item.equipmentType && equipmentSlots[i].item == null)
            {
                previousItem = (EquippableItem)equipmentSlots[i].item;
                equipmentSlots[i].item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(equipmentSlots[i].item == item)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
    public override void RefreshUI ()
    {
        // int i = 0;
        // for (; i < items.Count && i<equipmentSlots.Count; i++)
        // {
        //     equipmentSlots[i].item = items[i];
        // }
        // for (; i < equipmentSlots.Count; i++)
        // {
        //     equipmentSlots[i].item = null;
        // }
    
    }
}
