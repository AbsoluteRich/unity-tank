using UnityEngine;

public class Shell : MonoBehaviour
{
    public float m_MaxLifeTime = 2f;
    public ParticleSystem m_ExplosionParticles;
    public float m_MaxDamage = 34f;
    public float m_ExplosionRadius = 5;
    public float m_ExplosionForce = 100f;
    
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);   
    }

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