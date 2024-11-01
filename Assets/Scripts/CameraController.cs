using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mTarget;
    private Vector3 _mDesiredPosition;
    public float mDampTime;
    private Vector3 _mMoveVelocity;
    
    /// <summary>
    /// Moves the camera every frame.
    /// </summary>
    void FixedUpdate()
    {
        Move();
    }
    
    /// <summary>
    /// Smoothly moves the camera from its current position to the desired position.
    /// </summary>
    private void Move()
    {
        _mDesiredPosition = mTarget.position;
        Vector3.SmoothDamp(
            transform.position,
            _mDesiredPosition,
            ref _mMoveVelocity,
            mDampTime
        );
        transform.position = _mDesiredPosition;
    }
}
