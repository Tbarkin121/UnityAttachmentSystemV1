using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Small")]
public class Weapon : EquippableItem
{

    public WeaponType weaponType;
    public int damage; //Total damage. This would be the sum of all particles from a shotgun for instance
    public int perferationPower;

}
public enum WeaponType
{
    Missile,
    Laser,
    Particle
}