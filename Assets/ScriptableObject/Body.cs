using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body", menuName = "Body/Small")]
public class Body : Item
{
    public int ArmorLevel;
    // public int NumThrusterSlots;
    // public int NumWeaponSlots;
    public int InventoryWidth;
    public int InventoryHeight;
    public List<AttachmentPoint> attachmentPoints;
    public float drag;
    public float angularDrag;
    public Vector2 colliderSize;
    // public List<Item> inventory;

}
