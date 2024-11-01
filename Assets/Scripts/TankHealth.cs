using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float mStartingHealth = 30f;
    public float mCurrentHealth;
    private bool _mDead;
    public ParticleSystem mExplosionPrefab;
    private ParticleSystem _mExplosionParticles;
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    private void Awake()
    {
        _mExplosionParticles = Instantiate(mExplosionPrefab);
        _mExplosionParticles.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Resets health and death state.
    /// </summary>
    private void OnEnable()
    {
        mCurrentHealth = mStartingHealth;
        _mDead = false;
    }
    
    /// <summary>
    /// Reduces the tank health by a given amount and kills it when it goes below 0.
    /// </summary>
    /// <param name="amount">The amount of health to take away.</param>
    public void TakeDamage(float amount)
    {
        mCurrentHealth -= amount;

        if (mCurrentHealth <= 0 && _mDead == false)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Hides the tank and causes an explosion when the tank is killed.
    /// </summary>
    void OnDeath()
    {
        _mDead = true;

        _mExplosionParticles.transform.position = transform.position;
        _mExplosionParticles.gameObject.SetActive(true);
        _mExplosionParticles.Play();
        
        gameObject.SetActive(false);
    }
}
