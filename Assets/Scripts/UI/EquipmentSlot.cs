using UnityEngine;
using System;
public class EquipmentSlotFlex : ItemSlotFlex
{
    public event Action<EquipmentSlotFlex> ChangeEquipmentEvent;  
    private Item _equipment;
    public EquipmentType equipmentType;
    public override Item item
    {
        get { return _equipment; }
        set 
        {
            Debug.Log("EquipmentSlotSet");
            _equipment = value;
            if(_equipment == null) 
            {
                image.color = disabledColor;
                if(ChangeEquipmentEvent != null)
                    ChangeEquipmentEvent(this);
            }
            else
            {
                image.sprite = _equipment.artwork;
                image.color = normalColor;
                if(ChangeEquipmentEvent != null)
                    ChangeEquipmentEvent(this);
            }
        }
    }
    

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;
        Debug.Log("Item Type : " + equippableItem.equipmentType + ". Slot Type : " + equipmentType);
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
