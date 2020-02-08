using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterStats : EquipmentStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onThrusterChanged += OnThrusterChanged;
    }

    void OnThrusterChanged (Thruster thruster_)
    {
        Debug.Log("Thruster Stat Change!");
    }
}
