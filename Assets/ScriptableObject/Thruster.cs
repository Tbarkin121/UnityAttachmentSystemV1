using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thruster", menuName = "Thruster/Small")]
public class Thruster : ScriptableObject
{
    public Sprite artwork;
    public int mass;
    public int hitpoints;
    public int force;
    public Vector3 attachment_point;
    public Vector3 thrustOffset;
}
