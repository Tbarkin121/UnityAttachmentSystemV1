using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlUnit : MonoBehaviour
{
    private EqFunctions GrandestParentObj;
    public void Init(EqFunctions _grandestParentObject)
    {
        GrandestParentObj = _grandestParentObject;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)) // Left
        {
        }
        if(Input.GetKey(KeyCode.Mouse1)) // Right
        {
        }
        if(Input.GetKey(KeyCode.Mouse2)) // Midde
        {
        }
    }
}
