using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private Item _item;
    public Item item 
    {
        get { return _item; }
        set 
        {
            _item = value;
            if(_item == null) 
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.artwork;
                image.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
