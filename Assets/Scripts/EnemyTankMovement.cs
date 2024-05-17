using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    private bool m_Follow;  // Whether the enemy tank should follow the player
    public float m_CloseDistance = 8f;  // The minimum distance from the tank to the player before stopping movement
    private NavMeshAgent m_NavAgent;  // Reference to the tank's NavMeshAgent, an AI navigator
    private Transform m_Target;  // Reference to the tank's transform
    public Transform m_Turret;  // Reference to the tank turret's transform
    private Rigidbody m_Rigidbody;  // Reference to the tank's Rigidbody

    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;  // Enables physics
    }
    
    void OnDisable()
    {
        m_Rigidbody.isKinematic = true;  // Disables physics
    }

    void Awake()
    {
        // Sets the reference variables to components found on the object
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If the tank shouldn't follow the player, don't update movement or turret rotation
        if (m_Follow == false)
        {
            return;
        }
        MoveTowards();  // Move towards player
        TurretLook();  // Turn turret towards player
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // If the tank's collider (detection radius) intersects with the player, follow it...
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // ...otherwise, if the player leaves the tank collider, stop following them
        if (other.tag == "Player")
        {
            m_Follow = false; 
        }
    }

    void MoveTowards()
    {
        // Calculates the distance to the player
        float distance = (m_Target.position - transform.position).magnitude;
        
        // If the player is further than the minimum distance, move towards them
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

    void TurretLook()
    {
        // Rotates the tank turret to look at the player
        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Target);
        }
    }
}
