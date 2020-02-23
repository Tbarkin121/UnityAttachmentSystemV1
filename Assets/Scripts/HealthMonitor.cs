using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public int health;

    public void TakeDamage(int _damage)
    {
        Debug.Log("Pellets Hit for " + _damage + " damage. Current Health : " + health);
        AttachmentManager _am = gameObject.GetComponent<AttachmentManager>();
        health  -= _damage;
        Mathf.Clamp(health,0,1000);
        if(_am != null)
        {
            _am.ReportHealth(health);
        }
    }
    
}
