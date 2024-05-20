using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;
    private bool m_CanShoot;
    public float m_ShootDelay = 1f;
    private float m_ShootTimer;
    public AudioClip m_ShellFiredSfx;
    public GameObject m_AudioSource;
    public AudioClip m_ExplosionSfx;
    
    /// <summary>
    /// Decreases the shooting cooldown by the time passed since the last frame. If the timer is less than 0, the enemy tank shoots.
    /// </summary>
    void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDelay;
                AudioSource.PlayClipAtPoint(m_ShellFiredSfx, m_AudioSource.transform.position);
                Fire();
                AudioSource.PlayClipAtPoint(m_ExplosionSfx, m_AudioSource.transform.position);
            }
        }
    }
    
    /// <summary>
    /// Creates a new shell at the tank's turret and sets its velocity, sending it forward.
    /// </summary>
    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }
    
    /// <summary>
    /// Prevents the tank from shooting when the game starts.
    /// </summary>
    private void Awake()
    {
        m_CanShoot = false;
    }
    
    /// <summary>
    /// Lets the tank shoot if the player enters its detection radius.
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = true;
        }
    }
    
    /// <summary>
    /// Deny the enemy tank the ability to shoot if the player isn't in its collider.
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = false;
        }
    }
}
