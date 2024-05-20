using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    private bool m_Follow;
    public float m_CloseDistance = 8f;
    private NavMeshAgent m_NavAgent;
    private Transform m_Target;
    public Transform m_Turret;
    private Rigidbody m_Rigidbody;
    
    /// <summary>
    /// Enables physics.
    /// </summary>
    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }
    
    /// <summary>
    /// Disables physics.
    /// </summary>
    void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    void Awake()
    {
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Moves the tank towards the player and rotates turret, if the tank should be following the player.
    /// </summary>
    void Update()
    {
        if (m_Follow == false)
        {
            return;
        }
        MoveTowards();
        TurretLook();
    }
    
    /// <summary>
    /// Follows the player if the enemy tank's collider (detection radius) intersects with them.
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }
    
    /// <summary>
    /// Stop following the player if the player leaves the enemy tank collider (detection radius).
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false; 
        }
    }
    
    /// <summary>
    /// Calculates the distance to the player, and if the player is further than the minimum distance, move towards them.
    /// </summary>
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
    
    /// <summary>
    /// Rotates the tank turret to look at the player.
    /// </summary>
    void TurretLook()
    {
        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Target);
        }
    }
}
