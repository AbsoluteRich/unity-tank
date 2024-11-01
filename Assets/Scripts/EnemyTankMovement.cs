using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    private bool _mFollow;
    public float mCloseDistance = 8f;
    private NavMeshAgent _mNavAgent;
    private Transform _mTarget;
    public Transform mTurret;
    private Rigidbody _mRigidbody;
    
    /// <summary>
    /// Enables physics.
    /// </summary>
    void OnEnable()
    {
        _mRigidbody.isKinematic = false;
    }
    
    /// <summary>
    /// Disables physics.
    /// </summary>
    void OnDisable()
    {
        _mRigidbody.isKinematic = true;
    }
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    void Awake()
    {
        _mNavAgent = GetComponent<NavMeshAgent>();
        _mTarget = GameObject.FindGameObjectWithTag("Player").transform;
        _mRigidbody = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Moves the tank towards the player and rotates turret, if the tank should be following the player.
    /// </summary>
    void Update()
    {
        if (_mFollow == false)
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
        if (other.CompareTag("Player"))
        {
            _mFollow = true;
        }
    }
    
    /// <summary>
    /// Stop following the player if the player leaves the enemy tank collider (detection radius).
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mFollow = false; 
        }
    }
    
    /// <summary>
    /// Calculates the distance to the player, and if the player is further than the minimum distance, move towards them.
    /// </summary>
    void MoveTowards()
    {
        float distance = (_mTarget.position - transform.position).magnitude;
        
        if (distance > mCloseDistance)
        {
            _mNavAgent.SetDestination(_mTarget.position);
            _mNavAgent.isStopped = false;
        }
        else
        {
            _mNavAgent.isStopped = true;
        }
    }
    
    /// <summary>
    /// Rotates the tank turret to look at the player.
    /// </summary>
    void TurretLook()
    {
        if (mTurret != null)
        {
            mTurret.LookAt(_mTarget);
        }
    }
}
