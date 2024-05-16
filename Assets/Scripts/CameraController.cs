using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform m_Target;
    private Vector3 m_DesiredPosition;
    public float m_DampTime;
    private Vector3 m_MoveVelocity;
    
    void FixedUpdate()
    {
        Move();
    }
    
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
