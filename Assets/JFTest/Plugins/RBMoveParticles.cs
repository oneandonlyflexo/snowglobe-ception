using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMoveParticles : MonoBehaviour
{


    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;        

        while (i < numCollisionEvents)
        {
            if (rb)
            {
               // Debug.LogError("Foudn RB");
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }
    }



    /*
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < m_Particles.Length; i++)
        {
            if (m_Particles[i].lifetime <= 0.1f)
            {
                m_Particles[i].lifetime = m_Particles[i].startLifetime;
            }
        }

        // Apply the particle changes to the particle system
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        // Grab the Particle System component of the object if not already done so
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        // Update the m_Particles with currently existing particles from the system
        // Use 'Max Particles' in Particle System to control particle count in scene! - Otherwise endless number of particles
        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }
    */

}
