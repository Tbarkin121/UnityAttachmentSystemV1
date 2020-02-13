using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlots : ItemSlot
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }
}
