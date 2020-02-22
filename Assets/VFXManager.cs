using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject Effect;
    private List<GameObject> activeEffects;
    void Awake ()
    {
        activeEffects = new List<GameObject>();
    }
    public void EnableEffect(List<GameObject> _effects)
    {
        if(activeEffects.Count>0)
        {
            foreach (GameObject x in activeEffects)
            {
                Destroy(x);
            }
            activeEffects.Clear();
        }
        foreach(GameObject x in _effects)
        {
            GameObject _ps = Instantiate(x,transform.position,transform.rotation);
            _ps.transform.SetParent(transform);
            activeEffects.Add(_ps);
        }
    }
    public void DisableEffect()
    {
        if(activeEffects.Count>0)
            {
                foreach (GameObject x in activeEffects)
                {
                    x.GetComponent<ParticleSystem>().Stop();
                }
            }
    }

}
