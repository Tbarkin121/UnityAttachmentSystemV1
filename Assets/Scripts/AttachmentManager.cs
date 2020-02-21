using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    private bool whichBay = false;
    public EquippableItem item;
    public EquipmentType equipmentType;
    public Rigidbody2D ship_rb;
    public float maxHitpoints;
    public float currentHitpoints;
    public GameObject parent;
    private EquipmentSlotFlex parentSlot;
    public EquipmentSlotFlex ParentSlot
    {
        get{return parentSlot;}
        set{parentSlot = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        UpdateEquipment(item);
        
    }
    public void ChangeEquipment (EquipmentSlotFlex _equipmentSlot)
    {
        
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        EquippableItem equippableItem = _equipmentSlot.item as EquippableItem;
        Debug.Log(_equipmentSlot.item);
        if(equippableItem != null)
        {
            Debug.Log(item);
            item = equippableItem;
            Debug.Log(item);
            sr.sprite = item.artwork;
            sr.enabled = true;
            sr.sortingOrder = 1;
            currentHitpoints = item.hpMax;
        }
        else
        {
            item = null;
            sr.enabled = false;
        }
    }
    public void UpdateEquipment (EquippableItem _item)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if(_item != null)
        {
            item = _item;
            sr.sprite = item.artwork;
            sr.enabled = true;
            sr.sortingOrder = 1;
            currentHitpoints = item.hpMax;
        }
        else
        {
            item = null;
            sr.enabled = false;
        }
    }

    // Update is called once per frame
    public void RequestForce(float _force)
    {
        Thruster _thruster = item as Thruster;
        // Debug.Log("test 1   " + item);
        // Debug.Log(item);
        // Debug.Log("test 2   " + _thruster);
        // Debug.Log(_thruster);
        if(_thruster != null)
        {
            float appliedForce = Mathf.Clamp(_force, -_thruster.maxForce, _thruster.maxForce); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+_thruster.forceOffset.x)*appliedForce);
        }
        
    }
    public void RequestTurn(float _force)
    {
        Thruster _thruster = item as Thruster;
        if(_thruster != null)
        {
            _force *= Mathf.Sign(transform.localPosition.x);
            float appliedForce = Mathf.Clamp(_force, -_thruster.maxForce, _thruster.maxForce); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+_thruster.forceOffset.x)*appliedForce);
        }
        
    }
    public void TakeDamage(float _damage)
    {
        Debug.Log("Take Damage");
        currentHitpoints -= _damage;
        if(currentHitpoints <= 0)
            Die();
    }
    public void Fire ()
    {
        Weapon _weapon = item as Weapon;
        if(_weapon != null)
        {
            if(whichBay)
            {
                whichBay = false;
                GameObject _missile = Instantiate(_weapon.mainEffect, transform.position, transform.rotation);
                Rigidbody2D _rb = _missile.GetComponent<Rigidbody2D>();
                Rigidbody2D _rbp = parent.GetComponent<Rigidbody2D>();
                _rb.velocity = _rbp.velocity;
                _rb.angularVelocity = _rbp.angularVelocity;
                _rb.AddForce(transform.right*20f);
                
            }else{
                whichBay = true;
                GameObject _missile = Instantiate(_weapon.mainEffect, transform.position, transform.rotation);
                Rigidbody2D _rb = _missile.GetComponent<Rigidbody2D>();
                Rigidbody2D _rbp = parent.GetComponent<Rigidbody2D>();
                _rb.velocity = _rbp.velocity;
                _rb.angularVelocity = _rbp.angularVelocity;
                _rb.AddForce(-1.0f*transform.right*20f);
            }
        }
        
    }

    private void Die()
    {
        parent.GetComponent<ShipCoreController>().DeathReport(gameObject);
        parentSlot.item = null;
        // Destroy(gameObject);
    }

}
