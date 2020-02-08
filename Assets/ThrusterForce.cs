using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterForce : MonoBehaviour
{
    public Thruster thruster;
    public Rigidbody2D rb;
    private float targetForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onThrusterChanged += OnThrusterChanged;
        rb.inertia = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        float actualForce = Mathf.Clamp(targetForce, -thruster.force, thruster.force);
        Debug.Log("thruster COM : " + transform.localPosition);
        rb.AddForce(transform.up*actualForce);
        //Can make the torque calc more flexible but this is fine for now
        rb.AddTorque((transform.localPosition.x+thruster.thrustOffset.x)*actualForce);
    }
    public void UpdateTargetForce (float targetForce_)
    {
        Debug.Log("Updating Thruster Force");
        targetForce = targetForce_;
    }

    void OnThrusterChanged (Thruster thruster_)
    {
        thruster = thruster_;
        Debug.Log("Thurst Change");
    }
}
