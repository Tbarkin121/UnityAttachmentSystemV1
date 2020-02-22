using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public int health;

    public void TakeDamage(int _damage)
    {
        // Debug.Log("Damage Taken : " + _damage);
        health  -= _damage;
        if(health <= 0)
        {
            // Debug.Log("I should be dead");
            AttachmentManager _am = gameObject.GetComponent<AttachmentManager>();
            if(_am != null)
            {
                _am.Die();
            }
        }
    }
}
