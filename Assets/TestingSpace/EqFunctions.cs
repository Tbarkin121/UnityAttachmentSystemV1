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
        
        
        if(currentDepth < 3)
        {
            Rigidbody2D rb;
            GameObject child = new GameObject(_name);
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.identity;
            EqFunctions eq = child.AddComponent<EqFunctions>();
            eq.equippableItem = _part;
            eq.CurrentDepth = currentDepth + 1;
            eq.GrandestParentObj = GrandestParentObj;
            eq.EquipItem(_part);
            
            switch(_part.equipmentType)
            {
                case EquipmentType.Body:
                    Debug.Log("Detected Body Type");
                    rb = child.AddComponent<Rigidbody2D>();
                    rb.gravityScale = 0;
                    rb.drag = _part.drag;
                    rb.angularDrag = _part.angularDrag;
                    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                    break;
                case EquipmentType.Thruster:
                    Debug.Log("Detected Thruster Type");
                    rb = child.AddComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
                    break;
                default:
                    break;
            }
        }
    }
    private void EquipItem(EquippableItem _item)
    {
        equippableItem = _item;
        DrawArtwork(equippableItem);
        CreatePolygonCollider();
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

    void Start()
    {
        // CreatePart(PartType.Thruster);
    }

}