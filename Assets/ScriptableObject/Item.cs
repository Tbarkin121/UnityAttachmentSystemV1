using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject{
    new public string name = "New Item";
    public Sprite artwork = null;
    public int mass;
    public int hitpoints;
    public int armor;

    public virtual void Use ()
    {
        // Use the item
        // Something might happen

        Debug.Log ("Using  " + name);
    }
    
    protected virtual void Die ()
    {

    }
}
