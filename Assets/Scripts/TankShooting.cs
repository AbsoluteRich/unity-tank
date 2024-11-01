using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Rigidbody mShellPrefab;
    public Transform mCannonTransform;
    public float mLaunchForce = 30f;
    public AudioClip mShellFiredSfx;
    public GameObject mAudioSource;
    public AudioClip mExplosionSfx;
    
    /// <summary>
    /// Fires a shell and plays the appropriate sound effects when the left mouse button is clicked.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            AudioSource.PlayClipAtPoint(mShellFiredSfx, mAudioSource.transform.position);
            Fire();
            AudioSource.PlayClipAtPoint(mExplosionSfx, mAudioSource.transform.position);
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
}
