using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : VanillaManager
{
    public Body bodyData;
    private GameObject shipCore;
    GameObject shipControlCore;
    public GameObject mCanvas;
    Color pink = new Color(1f,0f,1f,1f);
    Color purple = new Color(0.25f,0f,0.5f,1f);
    public int menuX = -512;
    private GameObject CharacterPanel;
    private GameObject EquipmentPanel;
    private GameObject StatsPanel;
    private GameObject InventoryPanel;

    void Start ()
    {
        
        // GameObject mCanvas = GameObject.Find("Canvas");
        GameObject mCanvas = new GameObject("Character Canvas");
        mCanvas.transform.SetParent(transform);
        mCanvas.AddComponent<RectTransform>();
        Canvas canvas = mCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        mCanvas.AddComponent<CanvasScaler>();        
        mCanvas.AddComponent<GraphicRaycaster>();

        CharacterPanel = CreatePanel("Character Panel", mCanvas.transform);
        EquipmentPanel = CreatePanel("Equipment Panel", CharacterPanel.transform);
        StatsPanel = CreatePanel("Stats Panel", CharacterPanel.transform);
        InventoryPanel = CreatePanel("Inventory Panel", CharacterPanel.transform);

        AddGridLayoutGroup(EquipmentPanel);
        AddGridLayoutGroup(InventoryPanel);
        InventoryPanel.AddComponent<Inventory>();
        EquipmentPanel.AddComponent<Equipment>();

        //Arranging Panel Pieces
        ColorPanel(CharacterPanel, purple);
        ScalePanel(CharacterPanel, 512, 1024);
        MovePanel(CharacterPanel, menuX, 0);
        ColorPanel(EquipmentPanel, Color.black);
        ScalePanel(EquipmentPanel, 256, 512);
        MovePanel(EquipmentPanel, 128, 256);
        ColorPanel(StatsPanel, Color.yellow);
        ScalePanel(StatsPanel, 256, 512);
        MovePanel(StatsPanel, -128, 256);
        ColorPanel(InventoryPanel, Color.black);
        ScalePanel(InventoryPanel, 512, 512);
        MovePanel(InventoryPanel, 0, -256);

        CreateShipCore(canvas);

    }
    
    GameObject CreateShipCore (Canvas _canvas)
    {
        if(shipCore == null)
        {
            shipCore = new GameObject("Ship Core");
            shipCore.transform.SetParent(transform);
            shipCore.transform.localPosition = Vector3.zero;
            shipCore.transform.rotation = Quaternion.identity;
            shipCore.tag = "VehiclePart";
            ShipCoreController scController = shipCore.AddComponent<ShipCoreController>();
            scController.bodyData = bodyData;
            scController.StartUI(_canvas);
            Rigidbody2D rb = shipCore.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = bodyData.drag;
            rb.angularDrag = bodyData.angularDrag;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            // BoxCollider2D bc = shipCore.AddComponent<BoxCollider2D>();
            // bc.size = bodyData.colliderSize;
            HealthMonitor healthMonitor = shipCore.AddComponent<HealthMonitor>();
            healthMonitor.health = 0;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shipCore.GetComponent<ShipCoreController>().TestWeapon();   
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            CharacterPanel.SetActive(!CharacterPanel.activeSelf);  
            if ( CharacterPanel.activeSelf )
            {
                ShowMouseCursor();
            }
            else
            {
                HideMouseCursor();
            }
        }
        
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
