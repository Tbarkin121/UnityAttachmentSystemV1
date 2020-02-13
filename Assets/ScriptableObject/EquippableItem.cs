using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquippableItem : Item
{
    public float maxForce;
    public Vector3 forceOffset;
    public Vector3 attachmentPoint;
    public Vector2 colliderSize;
    public EquipmentType equipmentType;

}

public enum EquipmentType
{
    Thruster,
    Weapon
}