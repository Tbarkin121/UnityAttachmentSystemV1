using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EquipmentFlex : InventoryFlex
{
    public List<AttachmentPoint> attachmentPoints;
    public List<EquipmentSlotFlex> equipmentSlots;
    new public event Action<Item, int> OnItemRightClickedEvent;
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
            equipmentSlot.slotNum = i;
            equipmentSlots.Add(equipmentSlot);
            equipmentSlot.OnRightClickEvent += OnItemRightClickedEvent;
            ScalePanel(AttachmentTest, 128, 128);
            MovePanel(AttachmentTest, 0, 0);
            Sprite sp = Resources.Load<Sprite>("UI/EquipmentButton");
            if(x.equipmentType == EquipmentType.Thruster)
            {
                sp = Resources.Load<Sprite>("UI/ThrusterButton");
            }
            else if(x.equipmentType == EquipmentType.Weapon)
            {
                sp = Resources.Load<Sprite>("UI/WeaponButton");
            }
            
            SetPanelSprite(AttachmentTest, sp);
            i++;
        }
        RefreshUI();
    }
    public bool AddItem (EquippableItem item, out EquippableItem previousItem, out int _slotNum)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(equipmentSlots[i].equipmentType == item.equipmentType && equipmentSlots[i].item == null)
            {
                previousItem = (EquippableItem)equipmentSlots[i].item;
                _slotNum = i;
                equipmentSlots[i].item = item;
                return true;
            }
        }
        previousItem = null;
        _slotNum = -1;
        return false;
    }
    public bool RemoveItem(EquippableItem item, int _slotNum)
    {
        if(equipmentSlots[_slotNum].item != null)
        {
            equipmentSlots[_slotNum].item = null;
            return true;
        }
        return false;
    }
    public override void RefreshUI ()
    {

    }
}
