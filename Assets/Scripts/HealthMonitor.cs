using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public int health;

    public void TakeDamage(int _damage)
    {
        AttachmentManager _am = gameObject.GetComponent<AttachmentManager>();
        if(_am != null)
        {
            health  -= _damage;
            Mathf.Clamp(health,0,1000);
            _am.ReportHealth(health);
        }
    }
    
}
