using UnityEngine;
using System;
public class EquipmentSlotFlex : ItemSlotFlex
{
    public event Action<Item> ChangeEquipmentEvent;  
    private Item _equipment;
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
                    ChangeEquipmentEvent(null);
            }
            else
            {
                image.sprite = _equipment.artwork;
                image.color = normalColor;
                if(ChangeEquipmentEvent != null)
                    ChangeEquipmentEvent(_equipment);
            }
        }
    }
    public EquipmentType equipmentType;

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
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
