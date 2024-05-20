using UnityEngine;

public class Shell : MonoBehaviour
{
    public float m_MaxLifeTime = 2f;
    public ParticleSystem m_ExplosionParticles;
    public float m_MaxDamage = 34f;
    public float m_ExplosionRadius = 5;
    public float m_ExplosionForce = 100f;
    
    /// <summary>
    /// Schedules the destruction of the shell after its lifetime is over.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);   
    }
    
    /// <summary>
    /// Explodes and applies damage to nearby objects when the shell hits one.
    /// </summary>
    /// <param name="other">The collider that enters the shell's collider.</param>
    private void OnCollisionEnter(Collision other)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            Rigidbody targetRb = objectsInRange[i].GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Damage(targetRb);
            }
        }
        m_ExplosionParticles.transform.parent = null;  // Detaches the explosion from the shell, which is going to be destroyed
        m_ExplosionParticles.Play();
        
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Applies damage and force to the given Rigidbody.
    /// </summary>
    /// <param name="targetRigidbody">The target's Rigidbody.</param>
    void Damage(Rigidbody targetRigidbody)
    {
        targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
        TankHealth targetHealth = targetRigidbody.transform.GetComponent<TankHealth>();
        if (targetHealth != null)
        {
            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage);
        }
    }
    
    /// <summary>
    /// Calculates damage based on the shell's position from a target, adding splash damage.
    /// </summary>
    /// <param name="targetPosition">The target's position.</param>
    /// <returns>The calculated damage amount.</returns>
    float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionForce;
        float damage = relativeDistance * m_MaxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}