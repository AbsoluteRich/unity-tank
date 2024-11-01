using UnityEngine;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody mShellPrefab;
    public Transform mCannonTransform;
    public float mLaunchForce = 30f;
    private bool _mCanShoot;
    public float mShootDelay = 1f;
    private float _mShootTimer;
    public AudioClip mShellFiredSfx;
    public GameObject mAudioSource;
    public AudioClip mExplosionSfx;
    
    /// <summary>
    /// Decreases the shooting cooldown by the time passed since the last frame. If the timer is less than 0, the enemy tank shoots.
    /// </summary>
    void Update()
    {
        if (_mCanShoot)
        {
            _mShootTimer -= Time.deltaTime;
            
            if (_mShootTimer <= 0)
            {
                _mShootTimer = mShootDelay;
                AudioSource.PlayClipAtPoint(mShellFiredSfx, mAudioSource.transform.position);
                Fire();
                AudioSource.PlayClipAtPoint(mExplosionSfx, mAudioSource.transform.position);
            }
        }
    }
    
    /// <summary>
    /// Creates a new shell at the tank's turret and sets its velocity, sending it forward.
    /// </summary>
    void Fire()
    {
        Rigidbody shellInstance = Instantiate(mShellPrefab, mCannonTransform.position, mCannonTransform.rotation);
        shellInstance.velocity = mLaunchForce * mCannonTransform.forward;
    }
    
    /// <summary>
    /// Prevents the tank from shooting when the game starts.
    /// </summary>
    private void Awake()
    {
        _mCanShoot = false;
    }
    
    /// <summary>
    /// Lets the tank shoot if the player enters its detection radius.
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mCanShoot = true;
        }
    }
    
    /// <summary>
    /// Deny the enemy tank the ability to shoot if the player isn't in its collider.
    /// </summary>
    /// <param name="other">The collider that enters the enemy tank's detection radius.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mCanShoot = false;
        }
    }
}
