using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform m_Target;  // The target the camera will follow
    private Vector3 m_DesiredPosition;  // The position the camera is trying to reach
    public float m_DampTime;  // The time to takes for the camera to reach the desired position
    private Vector3 m_MoveVelocity;  // The camera's current velocity
    
    void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        // Smoothly moves the camera from its current position to the desired position
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
