using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Serializable]
    public class References
    {
        public Rigidbody RigidBody;
        public CapsuleCollider CapsuleCollider;
    }

    [Serializable]
    public class Settings
    {
        [Tooltip("Maximum slope the character can jump on")]
        [Range(5f, 60f)]
        public float SlopeLimit = 45f;
        [Tooltip("Move speed in meters/second")]
        public float MoveSpeed = 2f;
        [Space]
        [Tooltip("Whether the character can jump")]
        public bool AllowJump = false;
        [Tooltip("Force of a jump")]
        public float JumpForce = 4f;
        [Tooltip("Type of force added to rigidbody while try jump")]
        public ForceMode JumpForceMode = ForceMode.Impulse;

        [Space]
        [Tooltip("Ground Layer")]
        public LayerMask GroundLayer;
        [Tooltip("Ground raycast distance")]
        public float GroundCheckerDistance;
    }


    [SerializeField] References _references;
    [SerializeField] Settings _settings;

    private bool _isGrounded;
    private float _forwardInput;
    private float _turnInput;
    private bool _jumpPressed;


    private void FixedUpdate()
    {
        this.CheckGrounded();
        this.ProcessMotion();
    }


    private void Update()
    {
        this.UpdateInputState();
        this.CheckJump();
        this.CheckInteract();
    }

    private void UpdateInputState()
    {
        _forwardInput = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        _turnInput = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        _jumpPressed = Input.GetButtonDown("Jump");
    }

    private void CheckGrounded()
    {
        _isGrounded = false;
        float capsuleHeight = Mathf.Max(_references.CapsuleCollider.radius * 2f, _references.CapsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(_references.CapsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,_settings.GroundCheckerDistance,_settings.GroundLayer))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < _settings.SlopeLimit)
            {
                    _isGrounded = true;
            }
        }
    }

    private void RotateTowardsMotion(Vector3 velocity)
    {
        velocity.y = 0;
        transform.rotation =  Quaternion.LookRotation(velocity, transform.up);
    }
    private void ProcessMotion()
    {
        Vector3 velocity =  (Vector3.forward * Mathf.Clamp(_forwardInput, -1f, 1f) + Vector3.right * Mathf.Clamp(_turnInput, -1f, 1f)) *  _settings.MoveSpeed * Time.fixedDeltaTime;
        Vector3 targetPosition = transform.position + velocity;
        _references.RigidBody.MovePosition(targetPosition);
        this.RotateTowardsMotion(velocity);
    }

    private void CheckJump()
    {
        if (_jumpPressed && _settings.AllowJump && _isGrounded)
        {
            _references.RigidBody.AddForce(transform.up * _settings.JumpForce,_settings.JumpForceMode);
        }
    }


    public void OnCheckpointRespawn(Checkpoint checkpoint)
    {
        this.transform.position = checkpoint.RespawnPostitionTransform.position;
    }


/// <summary>
/// Pick up objects
/// </summary>

    private bool hasObjectToPickUp;
    private bool hasPickedUpObject;
    private GameObject pickableObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable") && !hasObjectToPickUp && !hasPickedUpObject)
        {
            Debug.Log("PICKABLE");
            hasObjectToPickUp = true;
            pickableObject = other.gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable") && hasObjectToPickUp && !hasPickedUpObject)
        {
            hasObjectToPickUp = false;
            pickableObject = null;
        }
    }

    private void CheckInteract()
    {
        if (Input.GetKeyDown(KeyCode.C) && pickableObject != null){
            if (!hasPickedUpObject)
            {
                pickableObject.transform.parent = transform;
                hasPickedUpObject = true;
                Debug.Log("PICK UP OBJECT");
            }
            else
            {
                Debug.Log("DROP OBJECT");                
                pickableObject.transform.parent = null;
                hasPickedUpObject = false;
            }
        }
    }

}
