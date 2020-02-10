using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttachmentPoint
{
    [SerializeField]
    public string name = "New Item";
    public EquipmentType equipmentType;
    public Vector2 location;
    // public bool damaged = false;
    // public bool occupied = false;
}

public enum EquipmentType {Thruster, Weapon}