using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    float m_MovementInputValue;
    float m_TurnInputValue;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public Transform m_TurretAsset;
    private LayerMask m_LayerMask;
    public int scrollSpeed;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LayerMask = LayerMask.GetMask("Ground");
    }

    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }
    
    void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");  // Position on the up and down positions
        m_TurnInputValue = Input.GetAxis("Horizontal");  // Position on the left and right positions
        TurnTurret();
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float cameraZoom = scroll * scrollSpeed;
        if (scroll != 0 && (Camera.main.orthographicSize + cameraZoom > 0))
        {
            Camera.main.orthographicSize += cameraZoom;
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    /// <summary>
    /// Applies velocity on the tank's Vector.
    /// Todo: Write a proper comment
    /// </summary>
    void Move()
    {
        Vector3 wantedVelocity = transform.forward * m_MovementInputValue * m_Speed;
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Todo: Comment this
    /// </summary>
    void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    
    void TurnTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            m_TurretAsset.LookAt(hit.point);
            m_TurretAsset.eulerAngles = new Vector3(0, m_TurretAsset.eulerAngles.y, m_TurretAsset.eulerAngles.z);
            // Vector3.Distance(-m_TurretAsset.eulerAngles.x, Mathf.Clamp()); Todo
        }
    }
}
