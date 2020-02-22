using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject parent;
    public ColliderType colliderType;
    public List<Collider2D> hits = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        switch (colliderType)
        {
            case ColliderType.BlastRadius:
                hits.Add(hitInfo);
                break;
            case ColliderType.Body:
                parent.GetComponent<Missile>().BodyCollisionReport(hitInfo);
                break;
            default:
                break;
        }
           
    }void OnTriggerExit2D(Collider2D hitInfo)
    {
        switch (colliderType)
        {
            case ColliderType.BlastRadius:
                hits.Remove(hitInfo);
                break;
            default:
                break;
        }
           
    }
}
public enum ColliderType
{
    Body,
    BlastRadius
}