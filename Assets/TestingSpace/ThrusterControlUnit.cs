using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControlUnit : MonoBehaviour
{
    private EqFunctions GrandestParentObj;
    TCU_LEVEL tcu_lvl;
    public void Init(EqFunctions _grandestParentObject)
    {
        GrandestParentObj = _grandestParentObject;
        tcu_lvl = TCU_LEVEL.LVL0;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            ThrustCoordinator(1, 0, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.A))
        {
            ThrustCoordinator(0, 0.05f, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.S))
        {
            ThrustCoordinator(-1, 0, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.D))
        {
            ThrustCoordinator(0, -0.05f, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    private void ThrustCoordinator(float _force, float _torque, List<GameObject> _thrusterList)
    {
        switch(tcu_lvl)
        {
            case TCU_LEVEL.LVL0:
                foreach(GameObject x in _thrusterList)
                {
                    GameObject _core = x.GetComponent<EqFunctions>().GrandestParentObj.gameObject.GetComponent<TestPlayerManager>().core;
                    Rigidbody2D _rb = _core.GetComponent<Rigidbody2D>();
                    if(_rb != null)
                    {
                        _rb.AddForce(_core.transform.up*_force);
                        _rb.AddTorque(_torque);
                        // We need Center of gravity. 
                        // _rb.AddTorque((transform.localPosition.x+_thruster.forceOffset.x)*appliedForce);
                    }
                        
                }
                break;
            case TCU_LEVEL.LVL1:
                break;
            case TCU_LEVEL.LVL2:
                break;
            case TCU_LEVEL.LVL3:
                break;
            case TCU_LEVEL.LVL4:
                break;
            case TCU_LEVEL.LVL5:
                break;
            default:
                Debug.LogError("tcu level not set!");
                break;

        }
    }
}

public enum TCU_LEVEL
{
    LVL0,
    LVL1,
    LVL2,
    LVL3,
    LVL4,
    LVL5
}