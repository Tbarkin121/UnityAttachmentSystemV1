using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBullet : MonoBehaviour
{
    public ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start ()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Do we ever see this happen?");
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;
        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 1;
                rb.AddForce(force);
            }
            i++;
        }
    }

    // these lists are used to contain the particles which match
    // // the trigger conditions each frame.
    // List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    // List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    // List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    // void OnEnable()
    // {
    //     ps = GetComponent<ParticleSystem>();
    // }

    // void OnParticleTrigger()
    // {
    //     // get the particles which matched the trigger conditions this frame
    //     int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //     int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //     // iterate through the particles which entered the trigger and make them red
    //     for (int i = 0; i < numEnter; i++)
    //     {
    //         ParticleSystem.Particle p = enter[i];
    //         p.startColor = new Color32(255, 0, 0, 255);
    //         enter[i] = p;
    //     }

    //     // iterate through the particles which exited the trigger and make them green
    //     for (int i = 0; i < numExit; i++)
    //     {
    //         ParticleSystem.Particle p = exit[i];
    //         p.startColor = new Color32(0, 255, 0, 255);
    //         exit[i] = p;
    //     }

    //     // re-assign the modified particles back into the particle system
    //     ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //     ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    // }
}
