using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public Body bodyData;
    public Thruster boosterData;
    public Item weaponData;
    private GameObject shipCore;
    private List<GameObject> children;
    private GameObject mCanvas;
    public Sprite testsprite;

    void Start ()
    {
        children = new List<GameObject>();
        children.Add(CreateShipCore ());
        GameObject mCanvas = GameObject.Find("Canvas");

        GameObject CharacterPanel = CreatePanel("Character Panel", mCanvas.transform);
        GameObject EquipmentPanel = CreatePanel("Equipment Panel", CharacterPanel.transform);
        GameObject StatsPanel = CreatePanel("Stats Panel", CharacterPanel.transform);
        GameObject InventoryPanel = CreatePanel("Inventory Panel", CharacterPanel.transform);

        Color pink = new Color(1f,0f,1f,1f);
        Color purple = new Color(0.25f,0f,0.5f,1f);
        ColorPanel(CharacterPanel, purple);
        ScalePanel(CharacterPanel, 512, 1024);
        MovePanel(CharacterPanel, -512, 0);
        ColorPanel(EquipmentPanel, pink);
        ScalePanel(EquipmentPanel, 256, 512);
        MovePanel(EquipmentPanel, 128, 256);
        ColorPanel(StatsPanel, Color.yellow);
        ScalePanel(StatsPanel, 256, 512);
        MovePanel(StatsPanel, -128, 256);
        ColorPanel(InventoryPanel, Color.blue);
        ScalePanel(InventoryPanel, 512, 512);
        MovePanel(InventoryPanel, 0, -256);

        SetPanelSprite(InventoryPanel, testsprite);

    }
    GameObject CreatePanel(string _name, Transform _parent)
    {
        GameObject Panel = new GameObject(_name);
        Panel.transform.SetParent(_parent, false);
        Panel.AddComponent<CanvasRenderer>();
        Panel.AddComponent<RectTransform>();
        Panel.AddComponent<Image>();
        SetAnchorPoints(Panel);
        return Panel;
    }
    void MovePanel(GameObject _panel, int x, int y)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.anchoredPosition = new Vector3(x, y, 0);
        }
    }
    void ScalePanel(GameObject _panel, int width, int height)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.sizeDelta  = new Vector3(width, height, 0);
        }
    }

    void ColorPanel(GameObject _panel, Color _color)
    {  
        Image Im = _panel.GetComponent<Image>();
        if (Im != null)
        {
            Im.color = _color;
        }
    }
    void SetPanelSprite(GameObject _panel, Sprite _sprite)
    {
        Image Im = _panel.GetComponent<Image>();
        if (Im != null)
        {
            Im.color = Color.white;
            Im.sprite = _sprite;
        }
    }
    void SetAnchorPoints(GameObject _panel)
    {
        RectTransform RectT = _panel.GetComponent<RectTransform>();
        if (RectT != null)
        {
            RectT.anchorMax = new Vector2(0.5f, 0.5f);
            RectT.anchorMin = new Vector2(0.5f, 0.5f);
            RectT.pivot = new Vector2(0.5f, 0.5f);
        }
    }
    GameObject CreateShipCore ()
    {
        if(shipCore == null)
        {
            shipCore = new GameObject();
            shipCore.name = "Ship Core";
            shipCore.transform.SetParent(transform);
            shipCore.transform.localPosition = Vector3.zero;
            shipCore.transform.rotation = Quaternion.identity;
            ShipCoreController scController = shipCore.AddComponent<ShipCoreController>();
            scController.bodyData = bodyData;
            scController.boosterData = boosterData;
            scController.weaponData = weaponData;
            Rigidbody2D rb = shipCore.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = bodyData.drag;
            rb.angularDrag = bodyData.angularDrag;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            BoxCollider2D bc = shipCore.AddComponent<BoxCollider2D>();
            bc.size = bodyData.colliderSize;
            return shipCore;
        }
        return null; 
    }

    void DestroyShipCore ()
    {
        //Destroy core and eject attachments if they aren't destroyed as well.
        //This might mean disabled or exploded. Not sure yet. 
    }

    void Update ()
    {
        // These control commands should go to a CPU game element that does control mixing later
        if(Input.GetKey(KeyCode.W))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(1.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(0.0f, 1.0f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(-1.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            shipCore.GetComponent<ShipCoreController>().CPU(0.0f, -1.0f);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            shipCore.GetComponent<ShipCoreController>().TestDamage(25);
            
        }
        
    }
}
