using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterDisplay : MonoBehaviour
{
    public Thruster thruster;
    public SpriteRenderer spriteRenderer;
    public bool flipped = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = thruster.artwork;
        // spriteRenderer.flipX = flipped;
        
        transform.localPosition.Set(thruster.attachment_point.x, thruster.attachment_point.y, thruster.attachment_point.z);
        EquipmentManager.instance.onThrusterChanged += OnThrusterChanged;
    }
    void Update()
    {
        // Debug.Log(transform.localPosition);
        if (thruster != null)
        {
            if (!flipped)
            {
                transform.localPosition = thruster.attachment_point;
            }
            else
            {
                Vector3 attachment_pointF = thruster.attachment_point;
                attachment_pointF.x *= -1;
                transform.localPosition = attachment_pointF;
            }
        }
        
    }

    public void ThrusterOrientaiton(bool flipped_)
    {
        flipped = flipped_;
    }
    void OnThrusterChanged (Thruster newThruster)
    {
        thruster = newThruster;
        Debug.Log("Change Thruster");
        // Render Thruster
        if (thruster != null)
        {
            spriteRenderer.sprite = thruster.artwork;
            spriteRenderer.flipX = flipped;
            
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }
}
