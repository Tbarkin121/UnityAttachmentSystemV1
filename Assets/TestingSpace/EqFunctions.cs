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
    public void CreatePart(EquippableItem _part, string _name)
    {
        if( currentDepth < 3 )
        {
            GameObject child = CreateGameObject(_name);
            EqFunctions eq = CreateEqFunctions(_part, child);
            eq.EquipItem(_part);
            CreateRigidBody(_part, child);            
        }
    }
    private GameObject CreateGameObject(string _name)
    {
        GameObject child = new GameObject(_name);
        child.transform.SetParent(transform);
        child.transform.localPosition = Vector3.zero;
        child.transform.rotation = Quaternion.identity;
        child.tag = "VehiclePart";
        return child;
    }
    private EqFunctions CreateEqFunctions(EquippableItem _part, GameObject _child)
    {
        EqFunctions eq = _child.AddComponent<EqFunctions>();
        eq.CurrentDepth = currentDepth + 1;
        eq.GrandestParentObj = GrandestParentObj;
        return eq;
    }
    private void CreateRigidBody(EquippableItem _part, GameObject _child)
    {
        Rigidbody2D rb;
        switch(_part.equipmentType)
        {
            case EquipmentType.Body:
                Debug.Log("Detected Body Type");
                rb = _child.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.drag = _part.drag;
                rb.angularDrag = _part.angularDrag;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                break;
            case EquipmentType.Thruster:
                Debug.Log("Detected Thruster Type");
                rb = _child.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
                break;
            default:
                break;
        }
    }
    
    private void EquipItem(EquippableItem _item)
    {
        equippableItem = _item;
        DrawArtwork(equippableItem);
        CreatePolygonCollider();
        CreateHealthMonitor(_item.hpMax);
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
        //Create Sprite Renderer
        //Draw Part
    }
    private void CreatePolygonCollider()
    {
        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
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