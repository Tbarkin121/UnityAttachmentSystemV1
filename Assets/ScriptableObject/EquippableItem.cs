using System.Collections;
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
    private int hp; //current hit points
    
    

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