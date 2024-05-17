using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;
    public AudioClip m_ShellFiredSfx;
    public GameObject m_AudioSource;
    
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            AudioSource.PlayClipAtPoint(m_ShellFiredSfx, m_AudioSource.transform.position);
            Fire();
        }
    }
    
    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }
}
