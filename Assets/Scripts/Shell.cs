using UnityEngine;

public class Shell : MonoBehaviour
{
    public float mMaxLifeTime = 2f;
    public ParticleSystem mExplosionParticles;
    public float mMaxDamage = 34f;
    public float mExplosionRadius = 5;
    public float mExplosionForce = 100f;
    
    /// <summary>
    /// Schedules the destruction of the shell after its lifetime is over.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, mMaxLifeTime);   
    }

    /// <summary>
    /// Explodes and applies damage to nearby objects when the shell hits one.
    /// </summary>
    private void OnCollisionEnter()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, mExplosionRadius);
        foreach (var t in objectsInRange)
        {
            Rigidbody targetRb = t.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                Damage(targetRb);
            }
        }
        mExplosionParticles.transform.parent = null;  // Detaches the explosion from the shell, which is going to be destroyed
        mExplosionParticles.Play();
        
        Destroy(mExplosionParticles.gameObject, mExplosionParticles.main.duration);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Applies damage and force to the given Rigidbody.
    /// </summary>
    /// <param name="targetRigidbody">The target's Rigidbody.</param>
    void Damage(Rigidbody targetRigidbody)
    {
        targetRigidbody.AddExplosionForce(mExplosionForce, transform.position, mExplosionRadius);
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
        float relativeDistance = (mExplosionRadius - explosionDistance) / mExplosionForce;
        float damage = relativeDistance * mMaxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}