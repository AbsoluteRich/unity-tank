using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 30f;
    public float m_CurrentHealth;
    private bool m_Dead;
    public ParticleSystem m_ExplosionPrefab;
    private ParticleSystem m_ExplosionParticles;
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab);
        m_ExplosionParticles.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Resets health and death state.
    /// </summary>
    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }
    
    /// <summary>
    /// Reduces the tank health by a given amount and kills it when it goes below 0.
    /// </summary>
    /// <param name="amount">The amount of health to take away.</param>
    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;

        if (m_CurrentHealth <= 0 && m_Dead == false)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Hides the tank and causes an explosion when the tank is killed.
    /// </summary>
    void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        
        gameObject.SetActive(false);
    }
}
