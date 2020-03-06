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
        eq.GrandestParent = true;
        eq.CreatePart(bodyItem, "Core");
        
    }


}
