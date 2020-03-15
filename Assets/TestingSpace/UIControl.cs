using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControl : VanillaManager
{
    private GameObject CharacterCanvas;
    private GameObject CharacterPanel;
    private GameObject InventoryPanel;
    private GameObject EquipmentPanel;
    private GameObject StatsPanel;
    private PhysicalInventory Inventory;
    
    private Image draggableItem; 
    private ItemSlot draggedSlot;

    Color pink = new Color(1f,0f,1f,1f);
    Color purple = new Color(0.25f,0f,0.5f,1f);
    public int menuX = -512;
    public void Init()
    {
        
        CreateCharacterCanvas();
        
        // AddGridLayoutGroup(EquipmentPanel);
        // AddGridLayoutGroup(InventoryPanel);
        Inventory = InventoryPanel.AddComponent<PhysicalInventory>();
        // EquipmentPanel.AddComponent<Equipment>();

        // physicalAttachmentPoints = new List<GameObject>();
        // equipmentSlots = new List<EquipmentSlot>();
        // attachmentPoints = bodyData.attachmentPoints;

        //Right Click
        Inventory.OnRightClickEvent += Equip;
        // equipment.OnRightClickEvent += Unequip;
        //Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        // equipment.OnBeginDragEvent += BeginDrag;
        //End Drag
        Inventory.OnEndDragEvent += EndDrag;
        // equipment.OnEndDragEvent += EndDrag;
        //Drag
        Inventory.OnDragEvent += Drag;
        // equipment.OnDragEvent += Drag;
        //Drop
        Inventory.OnDropEvent += Drop;
        // equipment.OnDropEvent += Drop;

        // inventory.ManualStart(bodyData);
        // equipment.ManualStart(bodyData.attachmentPoints);
        

        // Create Dragable Object for drag and drop functionality;
        GameObject draggableObject = CreatePanel("Draggable Item", CharacterPanel.transform);
        ScalePanel(draggableObject, 100, 100);
        draggableItem = draggableObject.GetComponent<Image>();
        draggableItem.enabled = false;
        draggableItem.raycastTarget = false;
    }

    private void CreateCharacterCanvas()
    {
        CharacterCanvas = new GameObject("Character Canvas");
        CharacterCanvas.transform.SetParent(transform);
        CharacterCanvas.AddComponent<RectTransform>();
        Canvas _canvas = CharacterCanvas.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CharacterCanvas.AddComponent<CanvasScaler>();        
        CharacterCanvas.AddComponent<GraphicRaycaster>();

        CharacterPanel = CreatePanel("Character Panel", CharacterCanvas.transform);
        EquipmentPanel = CreatePanel("Equipment Panel", CharacterPanel.transform);
        StatsPanel = CreatePanel("Stats Panel", CharacterPanel.transform);
        InventoryPanel = CreatePanel("Inventory Panel", CharacterPanel.transform);

        //Arranging Panel Pieces
        ColorPanel(CharacterPanel, purple);
        ScalePanel(CharacterPanel, 512, 1024);
        MovePanel(CharacterPanel, menuX, 0);
        ColorPanel(EquipmentPanel, pink);
        ScalePanel(EquipmentPanel, 256, 512);
        MovePanel(EquipmentPanel, 128, 256);
        ColorPanel(StatsPanel, Color.yellow);
        ScalePanel(StatsPanel, 256, 512);
        MovePanel(StatsPanel, -128, 256);
        ColorPanel(InventoryPanel, Color.red);
        ScalePanel(InventoryPanel, 512, 512);
        MovePanel(InventoryPanel, 0, -256);

    }

    public void TestInventory(Body bodyItem)
    {
        Inventory.CreateItemSlot(InventoryPanel);
        Inventory.ManualStart(bodyItem);
    }

    private void Equip(ItemSlot itemSlot)
    {
        Debug.Log("Equip 1");
    }
    public void Equip(EquippableItem item, ItemSlot itemSlot)
    {
        Debug.Log("Equip 2");
    }
    private void Unequip(ItemSlot itemSlot)
    {
        Debug.Log("Unequip 1");
    }
    public void Unequip(EquippableItem item, ItemSlot itemSlot)
    {
        Debug.Log("Unequip 2");
    }
    private void BeginDrag(ItemSlot itemSlot)
    {
        Debug.Log("BeginDrag");
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        Debug.Log("EndDrag");
    }
    private void Drag(ItemSlot itemSlot)
    {
        Debug.Log("Drag");
    }
    private void Drop(ItemSlot dropitemSlot)
    {
        Debug.Log("Drop");
    }
}
