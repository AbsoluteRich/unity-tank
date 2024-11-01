using UnityEngine;

public class TankMovement : MonoBehaviour
{
    Rigidbody _mRigidbody;
    float _mMovementInputValue;
    float _mTurnInputValue;
    public float mSpeed = 12f; 
    public float mTurnSpeed = 180f;
    public Transform mTurretAsset;
    private LayerMask _mLayerMask;
    public int scrollSpeed;
    
    /// <summary>
    /// Initialises variables to components found on the object/in the scene.
    /// </summary>
    void Awake()
    {
        _mRigidbody = GetComponent<Rigidbody>();
        _mLayerMask = LayerMask.GetMask("Ground");
    }
    
    /// <summary>
    /// Disables physics and resets all movement values.
    /// </summary>
    void OnEnable()
    {
        _mRigidbody.isKinematic = false;
        _mMovementInputValue = 0f;
        _mTurnInputValue = 0f;
    }
    
    /// <summary>
    /// Enables physics.
    /// </summary>
    void OnDisable()
    {
        _mRigidbody.isKinematic = true;
    }
    
    /// <summary>
    /// Moves the tank when the player presses W and S or the up and down arrows.
    /// Rotates the tank treads when the player pressed A and D or the left and right arrows.
    /// Changes the zoom of the camera when the player scrolls their mouse wheel.
    /// </summary>
    void Update()
    {
        if (GameManager.MIsPaused)
        {
            return;
        }
        _mMovementInputValue = Input.GetAxis("Vertical");  // Position on the up and down positions
        _mTurnInputValue = Input.GetAxis("Horizontal");  // Position on the left and right positions
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
        Vector3 wantedVelocity = transform.forward * (_mMovementInputValue * mSpeed);
        _mRigidbody.AddForce(wantedVelocity - _mRigidbody.velocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Rotates the tank treads.
    /// </summary>
    void Turn()
    {
        float turn = _mTurnInputValue * mTurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _mRigidbody.MoveRotation(_mRigidbody.rotation * turnRotation);
    }
    
    /// <summary>
    /// Rotates the tank turret according to where the mouse cursor is.
    /// </summary>
    void TurnTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _mLayerMask))
        {
            mTurretAsset.LookAt(hit.point);
            mTurretAsset.eulerAngles = new Vector3(0, mTurretAsset.eulerAngles.y, mTurretAsset.eulerAngles.z);
        }
    }
}
