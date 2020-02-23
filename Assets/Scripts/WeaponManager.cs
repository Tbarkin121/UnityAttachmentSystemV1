using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    bool weaponEquipped = false;
    private ParticleSystem particleSystem;
    private List<GameObject> emitters;
    private EquippableItem item;
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
                            GameObject _go = Instantiate(weapon.mainEffect, transform.position, transform.rotation);
                            _go.transform.SetParent(transform);
                            _go.transform.localPosition = weapon.firePoint;
                            _go.transform.localRotation = Quaternion.identity;
                            emitters.Add(_go);
                            break;
                        case WeaponType.Laser:
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
    }

    public void Fire()
    {
        Weapon weapon = item as Weapon;
        if(weapon != null)
        {
            switch(weapon.weaponType)
            {
            case WeaponType.Missile:
                            break;
                        case WeaponType.Particle:
                            foreach(GameObject x in emitters)
                            {
                                x.GetComponentInChildren<ParticleSystem>().Play();
                            }
                            break;
                        case WeaponType.Laser:
                            break;
                        default:
                            break;
            }
        }
            
    }
}
