using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float m_MaxLifeTime = 2f;
    public ParticleSystem m_ExplosionParticles;

    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);   
    }

    private void OnCollisionEnter(Collision other)
    {
        m_ExplosionParticles.transform.parent = null;  // Detaches the explosion from the shell, which is going to be destroyed
        m_ExplosionParticles.Play();
        
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
}
