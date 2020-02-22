using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttachmentManager : MonoBehaviour
{
    
    private bool whichBay = false;
    private GameObject VFXs;
    public EquippableItem item;
    public EquipmentType equipmentType;
    public Rigidbody2D ship_rb;
    public GameObject parent;
    private bool isEquiped;
    private bool isDamaged;
    private bool isDestroyed;
    private EquipmentSlot parentSlot;
    public EquipmentSlot ParentSlot
    {
        get{return parentSlot;}
        set{parentSlot = value;}
    }

    void Start()
    {
        isEquiped = false;
        isDamaged = true;
        isDestroyed = true;
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        UpdateEquipment(item);
        VFXs = new GameObject("VFXs");
        VFXs.transform.SetParent(transform);
        VFXs.transform.localPosition = new Vector3(0,0,0);
        VFXs.AddComponent<VFXManager>();
        
    }
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // damageEffect = Resources.Load<GameObject>("UI/ThrusterButton");
            List<GameObject> playEffects = new List<GameObject>();
            if(item != null)
            {
                playEffects.Add(item.damageEffect);
                playEffects.Add(item.destroyedEffect);
            }
            VFXs.GetComponent<VFXManager>().EnableEffect(playEffects);                
        }
            
        if(Input.GetKeyDown(KeyCode.P))
        {
            VFXs.GetComponent<VFXManager>().DisableEffect();   
        }
            
    }
    
    public void ChangeEquipment (EquipmentSlot _equipmentSlot)
    {
   
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        EquippableItem equippableItem = _equipmentSlot.item as EquippableItem;
        // Debug.Log(_equipmentSlot.item);
        if(equippableItem != null)
        {
            isDamaged = false;
            isDestroyed = false;
            isEquiped = true;
            // Debug.Log(item);
            item = equippableItem;
            // Debug.Log(item);
            sr.sprite = item.artwork;
            sr.enabled = true;
            sr.sortingOrder = 1;
            gameObject.GetComponent<HealthMonitor>().health = item.hpMax;
            VFXs.GetComponent<VFXManager>().DisableEffect();
        }
        else
        {
            isDamaged = true;
            isDestroyed = true;
            isEquiped = false;
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
            gameObject.GetComponent<HealthMonitor>().health = item.hpMax;
        }
        else
        {
            item = null;
            sr.enabled = false;
        }
    }

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

    public void ReportHealth(int _health)
    {
        if(isEquiped)
        {
            if (_health < item.hpMax/2)
            {
                Damaged();
            }
            if(_health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if(!isDestroyed)
        {
            isDestroyed = true;
            isEquiped = false;
            List<GameObject> playEffects = new List<GameObject>();
            if(item != null)
            {
                if(item.damageEffect != null)
                    playEffects.Add(item.damageEffect);
                if(item.destroyedEffect != null)
                    playEffects.Add(item.destroyedEffect);
            }
            VFXs.GetComponent<VFXManager>().EnableEffect(playEffects);    
            parent.GetComponent<ShipCoreController>().DeathReport(gameObject);
            parentSlot.item = null;
        }
    }
    public void Damaged()
    {
        if(!isDamaged)
        {   
            isDamaged = true;
            Debug.Log("Damage Effect?");
            List<GameObject> playEffects = new List<GameObject>();
            if(item != null)
            {
                Debug.Log("TP?");
                if(item.damageEffect != null)
                    playEffects.Add(item.damageEffect);
            }
            VFXs.GetComponent<VFXManager>().EnableEffect(playEffects);       
        }
    }

}
