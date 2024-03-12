using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyTankMovement : MonoBehaviour
{
    private bool m_Follow;
    public float m_CloseDistance = 8f;
    private NavMeshAgent m_NavAgent;
    private Transform m_Target;
    public Transform m_Turret;

    void Awake()
    {
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (m_Follow == false)
        {
            return;
        }
        MoveTowards();
        TurretLook();
    }

    void MoveTowards()
    {
        float distance = (m_Target.position - transform.position).magnitude;
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Target.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            m_NavAgent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false; 
        }
    }

    void TurretLook()
    {
        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Target);
        }
    }
}
