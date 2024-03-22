using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_ShellPrefab;
    public Transform m_CannonTransform;
    public float m_LaunchForce = 30f;
    private bool m_CanShoot;
    public float m_ShootDelay = 1f;
    private float m_ShootTimer;

    void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDelay;
                Fire();
            }
        }
    }

    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_ShellPrefab, m_CannonTransform.position, m_CannonTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_CannonTransform.forward;
    }
    
    private void Awake()
    {
        m_CanShoot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = false;
        }
    }
}
