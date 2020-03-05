using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonColliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sr;

    void Start ()
    {
        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
        
        float[] points = new float[] {0.04f, -0.08f, -0.02f, 0.06f, -0.03f, 0.08f, 0.02f, 0.06f, 0.01f, -0.02f, 0.02f, -0.05f};
        Vector2[] path = new Vector2[points.Length];
        for (int i = 0; i < points.Length-1; i++)
        {
            if( (i % 2) == 0 )
            {
                path[i] = new Vector2(points[i], points[i+1]);
            }
            else
            {
                path[i] = new Vector2(points[i+1], points[i]);
            }
            
        }
        if( (points.Length % 2) == 0 )
        {
            path[points.Length-1] = new Vector2(points[0], points[points.Length-1]);
            
        }
        else
        {
            path[points.Length-1] = new Vector2(points[points.Length-1], points[1]);
        }
        Debug.Log(path);
        pc.SetPath(0, path);

    }
}
