using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Body bodyData;
    public Thruster boosterData;
    public Item weaponData;
    private GameObject shipCore;
    private List<GameObject> children;

    void Start ()
    {
        children = new List<GameObject>();
        children.Add(CreateShipCore ());
        
    }

    GameObject CreateShipCore ()
    {
        if(shipCore == null)
        {
            shipCore = new GameObject();
            shipCore.name = "Ship Core";
            shipCore.transform.SetParent(transform);
            shipCore.transform.localPosition = Vector3.zero;
            shipCore.transform.rotation = Quaternion.identity;
            ShipCoreController scController = shipCore.AddComponent<ShipCoreController>();
            scController.bodyData = bodyData;
            scController.boosterData = boosterData;
            scController.weaponData = weaponData;
            Rigidbody2D rb = shipCore.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = bodyData.drag;
            rb.angularDrag = bodyData.angularDrag;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            BoxCollider2D bc = shipCore.AddComponent<BoxCollider2D>();
            bc.size = bodyData.colliderSize;
            return shipCore;
        }
        return null; 
    }

    void DestroyShipCore ()
    {
        //Destroy core and eject attachments if they aren't destroyed as well.
        //This might mean disabled or exploded. Not sure yet. 
    }

    void Update ()
    {
        // These control commands should go to a CPU game element that does control mixing later
        if(Input.GetKey(KeyCode.W))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(1.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(0.0f, 1.0f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(-1.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(0.0f, -1.0f);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            shipCore.GetComponent<ShipCoreController>().TestDamage(25);
            
        }
        
    }
}
