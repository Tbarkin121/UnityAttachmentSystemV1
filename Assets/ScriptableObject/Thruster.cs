using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thruster", menuName = "Thruster/Small")]
public class Thruster : EquippableItem
{
    public ThrusterType thrusterType;
    public float maxForce;
    public Vector3 forceOffset;

}
public enum ThrusterType
{
    Hall,
    Rocket
}