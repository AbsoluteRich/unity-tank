using UnityEngine;

public class TankMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_MovementInputValue;
    float m_TurnInputValue;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public Transform m_TurretAsset;
    private LayerMask m_LayerMask;
    public int scrollSpeed;
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LayerMask = LayerMask.GetMask("Ground");
    }
    
    /// <summary>
    /// Disables physics and resets all movement values.
    /// </summary>
    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }
    
    /// <summary>
    /// Enables physics.
    /// </summary>
    void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    
    /// <summary>
    /// Moves the tank when the player presses W and S or the up and down arrows.
    /// Rotates the tank treads when the player pressed A and D or the left and right arrows.
    /// Changes the zoom of the camera when the player scrolls their mouse wneel.
    /// </summary>
    void Update()
    {
        if (GameManager.m_isPaused)
        {
            return;
        }
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

    /// <summary>
    /// Moves the tank and its turret every frame.
    /// </summary>
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    /// <summary>
    /// Applies velocity on the tank's Vector, moving it in a direction.
    /// </summary>
    void Move()
    {
        Vector3 wantedVelocity = transform.forward * m_MovementInputValue * m_Speed;
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Rotates the tank treads.
    /// </summary>
    void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    
    /// <summary>
    /// Rotates the tank turret according to where the mouse cursor is.
    /// </summary>
    void TurnTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            m_TurretAsset.LookAt(hit.point);
            m_TurretAsset.eulerAngles = new Vector3(0, m_TurretAsset.eulerAngles.y, m_TurretAsset.eulerAngles.z);
        }
    }
}
