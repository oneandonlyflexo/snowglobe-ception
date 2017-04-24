using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SnowParticleEmitterShaker : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    private void Awake()
    {
        EventManager.Listen("ShakeSnowGlobe", ToggleIndoorOutDoor);
        m_Drift = 1000f;
    }

    private void OnDisable()
    {
        EventManager.StopListen("ShakeSnowGlobe", ToggleIndoorOutDoor);
    }

    private void ToggleIndoorOutDoor()
    {
        Debug.LogError(">> JIGGLE SNOW PARTICLE");
        jiggleMe = true;
    }

    bool jiggleMe = false;
    private void LateUpdate()
    {
        if (jiggleMe)
        {
            InitializeIfNeeded();

            int numParticlesAlive = m_System.GetParticles(m_Particles);

            for (int i = 0; i < numParticlesAlive; i++)
                m_Particles[i].velocity += Vector3.up * m_Drift;

            m_System.SetParticles(m_Particles, numParticlesAlive);

            jiggleMe = false;
        }
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }
}