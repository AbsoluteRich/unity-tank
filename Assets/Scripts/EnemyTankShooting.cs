using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;  // Reference to a prefab of a tank shell
    public Transform m_CannonTransform;  // Reference to the tank turret's position and rotation
    public float m_LaunchForce = 30f;  // Force at which the shell will be launched
    private bool m_CanShoot;
    public float m_ShootDelay = 1f;  // Delay between each shot
    private float m_ShootTimer;  // Timer for the shooting delay
    public AudioClip m_ShellFiredSfx;  // Sound effect to be played when a shell is shot
    public GameObject m_AudioSource;  // Reference to the GameObject where sounds will be played
    public AudioClip m_ExplosionSfx;  // Sound effect to be played when a shell explodes
    
    void Update()
    {
        if (m_CanShoot == true)
        {
            // Decreases the shoot timer by the time passed since the last frame
            m_ShootTimer -= Time.deltaTime;
            
            // If the timer is less than 0, the tank is able to shoot
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDelay;
                AudioSource.PlayClipAtPoint(m_ShellFiredSfx, m_AudioSource.transform.position);
                Fire();
                AudioSource.PlayClipAtPoint(m_ExplosionSfx, m_AudioSource.transform.position);
            }
        }
    }

    void Fire()
    {
        // Creates a new shell at the turret
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        
        // Sets the velocity of the shell, sending it forward
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }
    
    private void Awake()
    {
        m_CanShoot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Lets the tank shoot if the player enters its detection radius...
        if (other.CompareTag("Player"))
        {
            m_CanShoot = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // ...otherwise, if the player isn't in its collider, it cannot shoot
        if (other.CompareTag("Player"))
        {
            m_CanShoot = false;
        }
    }
}
