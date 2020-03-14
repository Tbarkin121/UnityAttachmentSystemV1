using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterControlUnit : MonoBehaviour
{
    private EqFunctions GrandestParentObj;
    TCU_LEVEL tcu_lvl;
    GameObject _core;
    public void Init(EqFunctions _grandestParentObject)
    {
        GrandestParentObj = _grandestParentObject;
        tcu_lvl = TCU_LEVEL.LVL0;
    }
    void Start()
    {
        _core = GrandestParentObj.gameObject.GetComponent<TestPlayerManager>().core;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            ThrustCoordinator(0.5f, 0, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.A))
        {
            ThrustCoordinator(0, 0.5f, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.S))
        {
            ThrustCoordinator(-0.5f, 0, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKey(KeyCode.D))
        {
            ThrustCoordinator(0, -0.5f, GrandestParentObj.ThrusterGroup);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    private void ThrustCoordinator(float _force, float _torque, List<GameObject> _thrusterList)
    {
        switch(tcu_lvl)
        {
            case TCU_LEVEL.LVL0: //Simple Thruster Control
                foreach(GameObject x in _thrusterList)
                {
                    
                    Rigidbody2D _rb = _core.GetComponent<Rigidbody2D>();
                    if(_rb != null)
                    {
                        Thruster thruster = x.GetComponent<EqFunctions>().equippableItem as Thruster;
                        // Debug.Log(thruster.forceOffset);
                        _rb.AddForce(_core.transform.up*_force);
                        _rb.AddTorque((x.transform.localPosition.x + thruster.forceOffset.x)*_force);

                        float turnForce = Mathf.Sign(x.transform.localPosition.x + thruster.forceOffset.x)*_torque;
                        _rb.AddForce(_core.transform.up * turnForce);
                        _rb.AddTorque((x.transform.localPosition.x + thruster.forceOffset.x) * turnForce);
                    }
                        
                }
                break;
            case TCU_LEVEL.LVL1: //Limit Booster Forces to maintain linear forward and reverse motion
                break;
            case TCU_LEVEL.LVL2: //Add a Turn Power Variable to better turn
                break;
            case TCU_LEVEL.LVL3: //Waypoint Navigation
                break;
            case TCU_LEVEL.LVL4: //Auto Breaking for Terrain
                break;
            case TCU_LEVEL.LVL5: //Auto Projectile Dodgeing
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