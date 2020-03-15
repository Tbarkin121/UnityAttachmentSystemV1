using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPlayerManager : MonoBehaviour
{
    public EquippableItem bodyItem;
    public EquippableItem thrusterItem;
    public EquippableItem weaponItem;
    public GameObject core;

    void Start ()
    {
        EqFunctions eq = gameObject.AddComponent<EqFunctions>();
        // eq.Initalizer(new Vector3(-1.0f, 0, 0), Quaternion.identity);
        eq.GrandestParent = true;
        core = eq.CreateAttachmentPoint("Core", Vector3.zero, Quaternion.identity);
        core.GetComponent<EqFunctions>().EquipItem(bodyItem);
        GameObject AP1 = core.GetComponent<EqFunctions>().CreateAttachmentPoint("AP1", new Vector3(0.08f, -0.08f, 0), Quaternion.identity);
        AP1.GetComponent<EqFunctions>().EquipItem(thrusterItem);
        core.GetComponent<EqFunctions>().IgnoreCollisionsWith(AP1.GetComponent<PolygonCollider2D>());
        GameObject AP2 = core.GetComponent<EqFunctions>().CreateAttachmentPoint("AP2", new Vector3(-0.08f, -0.08f, 0), Quaternion.identity);
        AP2.GetComponent<EqFunctions>().EquipItem(thrusterItem);
        core.GetComponent<EqFunctions>().IgnoreCollisionsWith(AP2.GetComponent<PolygonCollider2D>());
        GameObject AP3 = core.GetComponent<EqFunctions>().CreateAttachmentPoint("AP3", new Vector3(0.005f, 0.11f, 0), Quaternion.identity);
        AP3.GetComponent<EqFunctions>().EquipItem(weaponItem);
        core.GetComponent<EqFunctions>().IgnoreCollisionsWith(AP3.GetComponent<PolygonCollider2D>());

        //Start Setting Up UI
        UIControl ui = gameObject.AddComponent<UIControl>();
        ui.Init();
        ui.TestInventory(bodyItem as Body);
    }


}
