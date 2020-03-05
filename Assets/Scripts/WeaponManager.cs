using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    bool weaponEquipped = false;
    private List<GameObject> emitters;
    private bool whichBay = false;
    private EquippableItem item;
    private List<GameObject> lineRenderers;
    public EquippableItem Item
    {
        get{return item;}
        set
        {
            item = value;
            if(item != null)
            {
                weaponEquipped = true;
                Weapon weapon = item as Weapon;
                if (weapon != null)
                {
                    Debug.Log(weapon);
                    switch(weapon.weaponType)
                    {
                        case WeaponType.Missile:
                            break;
                        case WeaponType.Particle:
                            GameObject _blaster = Instantiate(weapon.mainEffect, transform.position, transform.rotation);
                            _blaster.transform.SetParent(transform);
                            _blaster.transform.localPosition = weapon.firePoint;
                            _blaster.transform.localRotation = Quaternion.identity;
                            emitters.Add(_blaster);
                            break;
                        case WeaponType.Laser:
                            GameObject _laser = Instantiate(weapon.mainEffect, transform.position, transform.rotation);
                            _laser.transform.SetParent(transform);
                            _laser.transform.localPosition = weapon.firePoint;
                            _laser.transform.localRotation = Quaternion.identity;
                            lineRenderers.Add(_laser);
                            break;
                        default:
                            Debug.LogWarning("What type of weapon is this anyways!?!?");
                            break;

                    }
                }
                else
                {
                    weaponEquipped = false;
                    foreach(GameObject x in emitters)
                    {
                        Destroy(x);
                    }
                    emitters.Clear();
                    foreach(GameObject x in lineRenderers)
                    {
                        Destroy(x);
                    }
                    lineRenderers.Clear();
                    Debug.LogWarning("Weapon is Null!");
                }
                
                
                Debug.Log("Setting Up Weapon Systems");
            }
            else
            {
                weaponEquipped = false;
                Debug.Log("Taking Down Weapon Systems");
            }
            
        }
    }
    void Start ()
    {
        emitters = new List<GameObject>();
        lineRenderers = new List<GameObject>();
    }

    public void Fire()
    {
        Weapon weapon = item as Weapon;
        if(weapon != null)
        {
            switch(weapon.weaponType)
            {
            case WeaponType.Missile:
                if(whichBay)
                {
                    whichBay = false;
                    GameObject _missile = Instantiate(weapon.mainEffect, transform.position, transform.rotation);
                    Rigidbody2D _rb = _missile.GetComponent<Rigidbody2D>();
                    Rigidbody2D _rbp = transform.parent.GetComponent<Rigidbody2D>();
                    _rb.velocity = _rbp.velocity;
                    _rb.angularVelocity = _rbp.angularVelocity;
                    _rb.AddForce(transform.right*20f);
                    
                }else{
                    whichBay = true;
                    GameObject _missile = Instantiate(weapon.mainEffect, transform.position, transform.rotation);
                    Rigidbody2D _rb = _missile.GetComponent<Rigidbody2D>();
                    Rigidbody2D _rbp = transform.parent.GetComponent<Rigidbody2D>();
                    _rb.velocity = _rbp.velocity;
                    _rb.angularVelocity = _rbp.angularVelocity;
                    _rb.AddForce(-1.0f*transform.right*20f);
                }
                break;

            case WeaponType.Particle:
                foreach(GameObject x in emitters)
                {
                    x.GetComponentInChildren<ParticleSystem>().Play();
                }
                break;

            case WeaponType.Laser:
                Debug.Log("Firing Laser");
                foreach(GameObject x in lineRenderers)
                {
                    LineRenderer lr = x.GetComponent<LineRenderer>();
                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position+transform.up * 0.25f, transform.up);
                    Debug.Log(hitInfo.point);
                    if (hitInfo)
                    {
                        HealthMonitor hm = hitInfo.collider.transform.GetComponent<HealthMonitor>();
                        if(hm != null)
                        {
                            hm.TakeDamage(weapon.damage);
                        }
                        Debug.Log(hitInfo.collider.transform.name);
                        lr.SetPosition (0, transform.position + transform.rotation * weapon.firePoint);
                        lr.SetPosition(1, hitInfo.point);
                    }
                    else
                    {
                        lr.SetPosition (0, transform.position + transform.rotation * weapon.firePoint);
                        lr.SetPosition(1, transform.position  + transform.up);
                    }
                    Debug.Log(lineRenderers);
                    StartCoroutine(ShowLaser(lr));
                }
                break;

            default:
                break;
            }
        }
            
    }

    IEnumerator ShowLaser(LineRenderer _lr)
    {
        _lr.enabled = true;
        yield return 0;
        _lr.enabled = false;

    }
}
