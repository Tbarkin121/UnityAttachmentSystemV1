﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquippableItem : Item
{

    public Vector3 attachmentPointLocation;
    
    public EquipmentType equipmentType;
    public GameObject mainEffect;
    public GameObject damageEffect;
    public GameObject destroyedEffect;
    // Next 2 are for the Rigid Body dynamics of a system
    public float drag;
    public float angularDrag;
    // Stats :
    public int armor; //Damage Mitigation
    public int hpMax; //max hit points
    public int perferationResistance; // resistance to internal damage
    
    public void TakeDamage(int damage)
    {
    }
    public override void Die ()
    {
        base.Die();
    }

}

public enum EquipmentType
{
    Body,
    Thruster,
    Weapon,
    Sheild,
    Utility,
    Drone
}