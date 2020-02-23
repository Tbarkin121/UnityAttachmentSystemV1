using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Small")]
public class Weapon : EquippableItem
{
    // new public EquipmentType equipmentType = EquipmentType.Weapon;

    public WeaponType weaponType; 
    public int damage; //Total damage. This would be the sum of all particles from a shotgun for instance
    public int perferationPower;
    public Vector3 firePoint;

}
public enum WeaponType
{
    Missile,
    Laser,
    Particle
}