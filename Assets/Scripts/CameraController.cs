using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform m_Target;
    private Vector3 m_DesiredPosition;
    public float m_DampTime;
    private Vector3 m_MoveVelocity;
    
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
        m_DesiredPosition = m_Target.position;
        Vector3.SmoothDamp(
            transform.position,
            m_DesiredPosition,
            ref m_MoveVelocity,
            m_DampTime
        );
        transform.position = m_DesiredPosition;
    }
}
