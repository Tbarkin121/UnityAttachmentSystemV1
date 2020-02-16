using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    public EquippableItem item;
    public EquipmentType equipmentType;
    public Rigidbody2D ship_rb;
    public float maxHitpoints;
    public float currentHitpoints;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        UpdateEquipment(item);
        
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
            currentHitpoints = item.hitpoints;
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
        if(item != null && item.equipmentType == EquipmentType.Thruster)
        {
            float appliedForce = Mathf.Clamp(_force, -item.maxForce, item.maxForce); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+item.forceOffset.x)*appliedForce);
        }
        
    }
    public void RequestTurn(float _force)
    {
        if(item != null && item.equipmentType == EquipmentType.Thruster)
        {
            _force *= Mathf.Sign(transform.localPosition.x);
            float appliedForce = Mathf.Clamp(_force, -item.maxForce, item.maxForce); 
            ship_rb.AddForce(transform.up*appliedForce);
            ship_rb.AddTorque((transform.localPosition.x+item.forceOffset.x)*appliedForce);
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
