using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDisplay : MonoBehaviour
{
    public Body body;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = body.artwork;
    }

    void Update()
    {
        spriteRenderer.sprite = body.artwork;
    }



}
