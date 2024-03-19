using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }
}
