using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body", menuName = "Body/Small")]
public class Body : EquippableItem
{

    public int numInventorySlots;
    public List<AttachmentPoint> attachmentPoints;
    public List<Item> items;

}
