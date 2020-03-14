using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControlUnit : MonoBehaviour
{
    WCU_LEVEL wcu_lvl;
    private EqFunctions GrandestParentObj;
    public void Init(EqFunctions _grandestParentObject)
    {
        GrandestParentObj = _grandestParentObject;
        wcu_lvl = WCU_LEVEL.LVL0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) // Left
        {
            WeaponCoordinator(GrandestParentObj.WeaponGroup);
        }
        if(Input.GetKey(KeyCode.Mouse1)) // Right
        {
        }
        if(Input.GetKeyDown(KeyCode.Mouse2)) // Midde
        {
        }
    }

    private void WeaponCoordinator(List<GameObject> _weaponList)
    {
        switch(wcu_lvl)
        {
            case WCU_LEVEL.LVL0: //Simple Weapon Control
                foreach(GameObject x in _weaponList)
                {
                        Weapon weapon = x.GetComponent<EqFunctions>().equippableItem as Weapon;
                        switch(weapon.weaponType)
                        {
                            case WeaponType.Laser:
                                FireLaser(x);
                                break;
                            case WeaponType.Missile:
                                FireMissile(x);
                                break;
                            case WeaponType.Particle:
                                FireParticle(x);
                                break;
                            default:
                                break;
                        }
                }
                break;
            case WCU_LEVEL.LVL1: //Add Centerline Convergance Point
                break;
            case WCU_LEVEL.LVL2: //Add Horrizontal Convergance Point
                break;
            case WCU_LEVEL.LVL3: //Tracking Close to mouse, 1st order + Faster Missile Lock Times
                break;
            case WCU_LEVEL.LVL4: //Tracking Close to mouse, 2st order + ???
                break;
            default:
                Debug.LogError("wcu level not set!");
                break;

        }
    }
    private void FireLaser(GameObject _weaponObj)
    {
        Debug.Log("Fire Laser");
        Weapon _weapon = _weaponObj.GetComponent<EqFunctions>().equippableItem as Weapon;
        LineRenderer lr = _weaponObj.GetComponentInChildren<LineRenderer>();
        RaycastHit2D hitInfo = Physics2D.Raycast(_weaponObj.transform.position + _weaponObj.transform.up * 0.25f, _weaponObj.transform.up);
        Debug.Log("Hit Info : " + hitInfo.collider.name);
        if (hitInfo)
        {
            Debug.Log(hitInfo.collider.transform.name);
            HealthMonitor hm = hitInfo.collider.transform.GetComponent<HealthMonitor>();
            if(hm != null)
            {
                hm.TakeDamage(_weapon.damage);
            }
            
            lr.SetPosition (0, _weaponObj.transform.position + _weaponObj.transform.rotation * _weapon.firePoint);
            lr.SetPosition(1, hitInfo.point);
        }
        else
        {
            lr.SetPosition (0, _weaponObj.transform.position + _weaponObj.transform.rotation * _weapon.firePoint);
            lr.SetPosition(1, _weaponObj.transform.position  + _weaponObj.transform.up);
        }
        StartCoroutine(ShowLaser(lr));
    }
    IEnumerator ShowLaser(LineRenderer _lr)
    {
        _lr.enabled = true;
        yield return 0;
        _lr.enabled = false;

    }
    private void FireMissile(GameObject _weapon)
    {
        Debug.Log("Fire Missile");
    }
    private void FireParticle(GameObject _weapon)
    {
        Debug.Log("Fire Particle");
    }

}

public enum WCU_LEVEL
{
    LVL0,
    LVL1,
    LVL2,
    LVL3,
    LVL4,
    LVL5
}