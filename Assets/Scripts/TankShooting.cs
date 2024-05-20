using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;
    public AudioClip m_ShellFiredSfx;
    public GameObject m_AudioSource;
    public AudioClip m_ExplosionSfx;
    
    /// <summary>
    /// Fires a shell and plays the appropriate sound effects when the left mouse button is clicked.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            AudioSource.PlayClipAtPoint(m_ShellFiredSfx, m_AudioSource.transform.position);
            Fire();
            AudioSource.PlayClipAtPoint(m_ExplosionSfx, m_AudioSource.transform.position);
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
}
