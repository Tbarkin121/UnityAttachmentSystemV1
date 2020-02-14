using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCoreController : MonoBehaviour
{
    public Body bodyData;
    public Thruster boosterData;
    public Item weaponData;
    public List<AttachmentPoint> attachmentPoints;
    public Rigidbody2D rb;
    private List<GameObject> children;
    
    void Start()
    {
        children = new List<GameObject>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = bodyData.artwork;
        attachmentPoints = bodyData.attachmentPoints;
        foreach(AttachmentPoint x in attachmentPoints)
        {
            Debug.Log("Creating " + x.name);
            children.Add(CreateAttachmentPoints(x));
        }
        EquipmentPanelFlex EPF = gameObject.AddComponent<EquipmentPanelFlex>();
        
        print("Ship Core Online!");
    }

    GameObject CreateAttachmentPoints (AttachmentPoint _attachmentPoint)
    {

        GameObject attachmentPoint = new GameObject();
        attachmentPoint.name = _attachmentPoint.name;
        attachmentPoint.transform.SetParent(transform);
        attachmentPoint.transform.localPosition = _attachmentPoint.location;
        attachmentPoint.transform.rotation = Quaternion.identity;
        AttachmentManager attachmentManager = attachmentPoint.AddComponent<AttachmentManager>();
        attachmentManager.equipmentType = _attachmentPoint.equipmentType;
        attachmentManager.boosterData = boosterData;
        attachmentManager.weaponData = weaponData;
        attachmentManager.parent = gameObject;
        attachmentManager.ship_rb = rb;
        BoxCollider2D bc = attachmentPoint.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(0.05f, 0.1f); //This type of thing should be stored on the body object
        // ShipCoreController scController = thruster1.AddComponent<ShipCoreController>();
        // scController.bodyData = bodyData;
        return attachmentPoint;
    }

    public void CPU (float targetForce, float targetTorque)
    {
        foreach(GameObject x in children)
        {
            AttachmentManager AM = x.GetComponent<AttachmentManager>();
            if ( AM.equipmentType == EquipmentType.Thruster)
            {
                AM.RequestForce(targetForce);
                AM.RequestTurn(targetTorque);
            }
        }
    }

    public void TestDamage(float damage)
    {
        children[0].GetComponent<AttachmentManager>().TakeDamage(damage);
    }

    public void DeathReport(GameObject _gameObject)
    {
        children.Remove(_gameObject);
    }

}
