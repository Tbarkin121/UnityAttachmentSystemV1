using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnThrusterChanged (Thruster newThruster);
    public OnThrusterChanged onThrusterChanged;
    public GameObject Thruster1;
    public GameObject Thruster2;
    public Thruster hall_thruster;
    public Thruster rocket_thruster;
    private ThrusterDisplay thruster_display_1;
    private ThrusterDisplay thruster_display_2;
    private bool which_im = false;
    private bool first = true;
    


    void Start()
    {
        thruster_display_1 = Thruster1.GetComponent<ThrusterDisplay>();
        thruster_display_2 = Thruster2.GetComponent<ThrusterDisplay>();
        thruster_display_1.ThrusterOrientaiton(true);
        thruster_display_2.ThrusterOrientaiton(false);
        onThrusterChanged.Invoke(null);
    }
    void Update()
    {
        if(first)
        {
            onThrusterChanged.Invoke(hall_thruster);
            first = false;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            which_im = !which_im;
            if(which_im)
            {
                onThrusterChanged.Invoke(hall_thruster);
                // thruster_display_1.UpdateThruster(hall_thruster,true);
                // thruster_display_2.UpdateThruster(hall_thruster,false);
            }
            else
            {
                onThrusterChanged.Invoke(rocket_thruster);
                // thruster_display_1.UpdateThruster(rocket_thruster,true);
                // thruster_display_2.UpdateThruster(rocket_thruster,false);
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {

            onThrusterChanged.Invoke(null);
        }
            
        // if(Input.GetKeyDown(KeyCode.F))

    }

    public void RemoveEquipment(GameObject engine_)
    {
        
    }
}
