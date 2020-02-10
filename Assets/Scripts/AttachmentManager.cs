﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    public Thruster boosterData;
    public Item weaponData;
    public EquipmentType equipmentType;
    public Rigidbody2D ship_rb;
    public float maxHitpoints;
    public float currentHitpoints;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        {
            if (equipmentType == EquipmentType.Thruster)
            {
                sr.sprite = boosterData.artwork;
            }
            else
            {
                sr.sprite = weaponData.artwork;
            }
        }
        maxHitpoints = boosterData.hitpoints;
        currentHitpoints = maxHitpoints;
        
        
    }

    // Update is called once per frame
    public void RequestForce(float _force)
    {
        if(equipmentType == EquipmentType.Thruster)
        {
            float appliedForce = Mathf.Clamp(_force, -boosterData.force, boosterData.force); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+boosterData.thrustOffset.x)*appliedForce);
        }
        
    }
    public void RequestTurn(float _force)
    {
        if(equipmentType == EquipmentType.Thruster)
        {
            _force *= Mathf.Sign(transform.localPosition.x);
            float appliedForce = Mathf.Clamp(_force, -boosterData.force, boosterData.force); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+boosterData.thrustOffset.x)*appliedForce);
        }
        
    }
    public void TakeDamage(float _damage)
    {
        Debug.Log("Take Damage");
        currentHitpoints -= _damage;
        if(currentHitpoints <= 0)
            Die();
    }

    private void Die()
    {
        parent.GetComponent<ShipCoreController>().DeathReport(gameObject);
        Destroy(gameObject);
    }

}
