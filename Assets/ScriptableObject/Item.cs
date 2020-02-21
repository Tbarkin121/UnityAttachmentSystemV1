using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject{
    new public string name = "New Item";
    public Sprite artwork = null;
    public Vector2 colliderSize;
    //Stats : 
    public int mass;
    private int width;
    private int height;

    public virtual void Use ()
    {
        // Use the item
        // Something might happen

        Debug.Log ("Using  " + name);
    }
    
    public virtual void Die ()
    {
        // Kill the item
        // Something might happen
        Debug.Log ("Dieing  " + name);
    }
}
