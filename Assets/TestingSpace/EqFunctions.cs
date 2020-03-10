using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqFunctions : MonoBehaviour
{
    public bool isGrandestParent = false;
    public EqFunctions GrandestParentObj;
    public EquippableItem equippableItem{get;set;}
    public int currentDepth = 0;
    public int CurrentDepth
    {
        get {return currentDepth;}
        set {currentDepth = value;}
    }
    public bool GrandestParent
    {
        get { return isGrandestParent; }
        set 
        {
            isGrandestParent = value;
            currentDepth = 0;
            GrandestParentObj = gameObject.GetComponent<EqFunctions>();
        }
    }
    public void Initalizer(Vector3 _pos, Quaternion _rot)
    {
        transform.localPosition = _pos;
        transform.localRotation = _rot;
    }
    public void CreatePart(EquippableItem _part, string _name, Vector3 _pos, Quaternion _rot)
    {
        
        
        if(currentDepth < 3)
        {
            
            GameObject child = new GameObject(_name);
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.identity;
            EqFunctions eq = child.AddComponent<EqFunctions>();
            eq.Initalizer(_pos, _rot);
            eq.CurrentDepth = currentDepth + 1;
            eq.GrandestParentObj = GrandestParentObj;

            eq.EquipItem(_part);
        }
    }
    public GameObject CreateAttachmentPoint(string _name, Vector3 _pos, Quaternion _rot)
    {
        GameObject child = new GameObject(_name);
        child.transform.SetParent(transform);
        child.transform.localPosition = Vector3.zero;
        child.transform.rotation = Quaternion.identity;
        EqFunctions eq = child.AddComponent<EqFunctions>();
        eq.Initalizer(_pos, _rot);
        eq.CurrentDepth = currentDepth + 1;
        eq.GrandestParentObj = GrandestParentObj;
        return child;
    }
    public void EquipItem(EquippableItem _part)
    {
        equippableItem = _part;
        DrawArtwork(equippableItem);
        CreatePolygonCollider(null);
        CreateHealthMonitor(_part.hpMax);
        Rigidbody2D rb;
        switch(_part.equipmentType)
        {
            case EquipmentType.Body:
                Debug.Log("Detected Body Type");
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.drag = _part.drag;
                rb.angularDrag = _part.angularDrag;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                break;
            case EquipmentType.Thruster:
                Debug.Log("Detected Thruster Type");
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                break;
            default:
                break;
        }
    }
    private void DrawArtwork(EquippableItem _item)
    {
        SpriteRenderer _sr = gameObject.GetComponent<SpriteRenderer>();
        if (_sr != null)
        {
            Debug.Log("Sprite Renderer already exists!");
        }
        else
        {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = _item.artwork;
            sr.enabled = true;
            sr.sortingOrder = _item.drawLayer;
        }
    }
    private void CreatePolygonCollider(PolygonCollider2D _parent_pc)
    {
        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
        if(_parent_pc != null)
            Physics2D.IgnoreCollision(pc, _parent_pc);
    }
    private void CreateHealthMonitor(int _healthPoints)
    {
        HealthMonitor healthMonitor = gameObject.AddComponent<HealthMonitor>();
        healthMonitor.health = _healthPoints;
    }
    void Start()
    {
        // CreatePart(PartType.Thruster);
    }

}