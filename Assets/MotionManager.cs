using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    public GameObject Engine1;
    public GameObject Engine2;
    private ThrusterForce thruster1;
    private ThrusterForce thruster2;
    private bool leftVright = false;

    void Start ()
    {
        thruster1 = Engine1.gameObject.GetComponent<ThrusterForce>();
        thruster2 = Engine2.gameObject.GetComponent<ThrusterForce>();
    }
    void Update ()
    {
        thruster1.UpdateTargetForce(0.0f);
        thruster2.UpdateTargetForce(0.0f);
        if(Input.GetKey(KeyCode.W))
        {
            thruster1.UpdateTargetForce(1.0f);
            thruster2.UpdateTargetForce(1.0f);
            
        }
        if(Input.GetKey(KeyCode.A))
        {
            thruster1.UpdateTargetForce(-1.0f);
            thruster2.UpdateTargetForce(1.0f);
            
        }
        if(Input.GetKey(KeyCode.S))
        {
            thruster1.UpdateTargetForce(-1.0f);
            thruster2.UpdateTargetForce(-1.0f);
            
        }
        if(Input.GetKey(KeyCode.D))
        {
            thruster1.UpdateTargetForce(1.0f);
            thruster2.UpdateTargetForce(-1.0f);
            
        }
            
    }

}
