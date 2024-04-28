using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 30f;
    public float m_CurrentHealth;
    private bool m_Dead;
    public ParticleSystem m_ExplosionPrefab;
    private ParticleSystem m_ExplosionParticles;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab);
        m_ExplosionParticles.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;

        if (m_CurrentHealth <= 0 && m_Dead == false)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        
        gameObject.SetActive(false);
    }
}
