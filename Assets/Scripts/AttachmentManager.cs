using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttachmentManager : MonoBehaviour
{
    
    private bool whichBay = false;
    private GameObject VFXs;
    private GameObject ImageHolder;
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
        ImageHolder = new GameObject("Image Holder");
        ImageHolder.transform.SetParent(transform);
        ImageHolder.transform.localPosition = Vector3.zero;
        SpriteRenderer sr = ImageHolder.AddComponent<SpriteRenderer>();
        UpdateEquipment(item);
        VFXs = new GameObject("VFXs");
        VFXs.transform.SetParent(transform);
        VFXs.transform.localPosition =  Vector3.zero;
        VFXs.AddComponent<VFXManager>();
        
    }
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("BAH");
            Weapon _weapon = item as Weapon;
            if (_weapon != null)
                gameObject.GetComponent<WeaponManager>().Fire();
        }
            
        if(Input.GetKeyDown(KeyCode.P))
        {
        }
            
    }
    
    public void ChangeEquipment (EquipmentSlot _equipmentSlot)
    {
   
        SpriteRenderer sr = ImageHolder.GetComponent<SpriteRenderer>();
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
            ImageHolder.transform.localPosition = item.attachmentPointLocation;
            sr.sortingOrder=item.drawLayer;
            sr.enabled = true;
            gameObject.GetComponent<HealthMonitor>().health = item.hpMax;
            VFXs.GetComponent<VFXManager>().DisableEffect();
            if (equippableItem.equipmentType == EquipmentType.Weapon)
            {
                Debug.Log("TP1");
                WeaponManager _wm = gameObject.GetComponent<WeaponManager>();
                if (_wm != null)
                    _wm.Item = equippableItem;
            }
        }
        else
        {
            isDamaged = true;
            isDestroyed = true;
            isEquiped = false;
            item = null;
            sr.enabled = false;
            Debug.Log("TP2");
            WeaponManager _wm = gameObject.GetComponent<WeaponManager>();
            if (_wm != null)
                _wm.Item=null;
        }
    }
    public void UpdateEquipment (EquippableItem _item)
    {
        SpriteRenderer sr = ImageHolder.GetComponent<SpriteRenderer>();
        if(_item != null)
        {
            item = _item;
            sr.sprite = item.artwork;
            ImageHolder.transform.localPosition = item.attachmentPointLocation;
            sr.enabled = true;
            sr.sortingOrder = item.drawLayer;
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

            gameObject.GetComponent<WeaponManager>().Fire();
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
