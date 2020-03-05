using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqFunctions : MonoBehaviour
{
    public bool isGrandestParent = false;
    public EqFunctions GrandestParentObj;
    public int currentDepth = 0;
    public PartType partType;
    public int CurrentDepth
    {
        get {return currentDepth;}
        set {currentDepth = value;}
    }
    public bool GrandestParent
    {
        get { return isGrandestParent; }
        set 
        {
            isGrandestParent = value;
            currentDepth = 0;
            GrandestParentObj = gameObject.GetComponent<EqFunctions>();
        }
    }
    public void CreatePart(PartType _part)
    {
        if(currentDepth < 3)
        {
            GameObject child = new GameObject("child");
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.identity;
            EqFunctions eq = child.AddComponent<EqFunctions>();
            eq.CurrentDepth = currentDepth + 1;
            eq.partType = _part;
            eq.GrandestParentObj = GrandestParentObj;
        }
    }

    void Start()
    {
        CreatePart(PartType.Thruster);
    }

}
public enum PartType
{
    Body,
    Thruster,
    Weapon,
    Sheild,
    Utility,
    Drone
}