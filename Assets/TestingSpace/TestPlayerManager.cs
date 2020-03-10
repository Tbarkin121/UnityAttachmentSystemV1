using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPlayerManager : MonoBehaviour
{
    public EquippableItem bodyItem;
    public EquippableItem thrusterItem;

    void Start ()
    {
        EqFunctions eq = gameObject.AddComponent<EqFunctions>();
        // eq.Initalizer(new Vector3(-1.0f, 0, 0), Quaternion.identity);
        eq.GrandestParent = true;
        GameObject core = eq.CreateAttachmentPoint("Core", Vector3.zero, Quaternion.identity);
        core.GetComponent<EqFunctions>().EquipItem(bodyItem);
        GameObject AP1 = core.GetComponent<EqFunctions>().CreateAttachmentPoint("AP1", new Vector3(0.2f, 0, 0), Quaternion.identity);
        AP1.GetComponent<EqFunctions>().EquipItem(thrusterItem);
        // eq.CreatePart(bodyItem, );
        // eq.CreatePart(bodyItem, "Core", new Vector3(-0.15f, 0 ,0), Quaternion.identity);
        
    }


}
