using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterForce : MonoBehaviour
{
    public Thruster thruster;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onThrusterChanged += OnThrusterChanged;
        rb.inertia = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            // Debug.Log("RB COM : " + rb.centerOfMass); //This is always 0,0,0
            Debug.Log("thruster COM : " + transform.localPosition);
            rb.AddForce(transform.up*thruster.force);
            //Can make the torque calc more flexible but this is fine for now
            rb.AddTorque((transform.localPosition.x+thruster.thrustOffset.x)*thruster.force);
        }
    }

    void OnThrusterChanged (Thruster newThruster)
    {
        thruster = newThruster;
        Debug.Log("Change Thruster Force");
    }
}
