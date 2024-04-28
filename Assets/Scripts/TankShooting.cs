using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;

    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }
}
