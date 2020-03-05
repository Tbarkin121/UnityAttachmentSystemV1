using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPlayerManager : MonoBehaviour
{

    void Start ()
    {
        EqFunctions eq = gameObject.AddComponent<EqFunctions>();
        eq.GrandestParent = true;
        eq.partType = PartType.Body;
        
    }


}
