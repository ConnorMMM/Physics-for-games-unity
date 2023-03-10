using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    public UIManager uiManager;

    public float movementSpeed = 10;
    public float jumpHeight = 4;
    public float turnSpeed = 6;

    public float deathTriggerHeight = -19.5f;
    public LayerMask jumpSurfaces;

    private CharacterController _cc;
    private Rigidbody _rb;
    private Transform _camera;
    private Animator _animator;
    private Ragdoll ragdollScript = null;

    private Vector2 _moveInput = new Vector2();
    private bool _jumpInput = false;
    public bool _isGrounded = false;
    public bool _isRagdoll = false;

    public Vector3 velocity = new Vector3();

    private Vector3 _hitDirection = new Vector3();

    public Transform respawnPoint;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
        _animator = GetComponent<Animator>();
        ragdollScript = GetComponent<Ragdoll>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
        _jumpInput = Input.GetButton("Jump");

        //_animator.SetFloat("Forwards", _moveInput.y);
        //_animator.SetBool("Jump", !_isGrounded);

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (_isRagdoll)
            {
                Transform childTransform = transform.GetChild(1);
                transform.position = new Vector3(childTransform.position.x, transform.position.y, childTransform.position.z);
            }
            ragdollScript.ragdollOn = !ragdollScript.ragdollOn;
        }

        if (transform.GetChild(1).position.y <= deathTriggerHeight)
            Respawn();
    }

    private void FixedUpdate()
    {
        // Find the horizontal unit vector facing forward from the camera
        Vector3 camForward = _camera.forward;
        camForward.y = 0;
        camForward.Normalize();

        // Use our cmaera's right vector, which is always horizontal
        Vector3 camRight = _camera.right;

        // Player movement using WASD or arrow keys
        Vector3 delta = (_moveInput.x * camRight + _moveInput.y * camForward) * movementSpeed;
        if(_isGrounded || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            velocity.x = delta.x;
            velocity.z = delta.z;
        }

        // Check for jumping
        if (_jumpInput && OnGround())
            velocity.y = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);

        // Check if we've hit ground from falling. If so, remove our velocity
        if (_isGrounded && velocity.y < 0)
            velocity.y = 0;

        // Apply gravity
        velocity += Physics.gravity * Time.fixedDeltaTime;

        if (!_isGrounded)
            _hitDirection = Vector3.zero;

        // Slide objects off surfaces they're hanging on to
        if(_moveInput.x == 0 && _moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = _hitDirection;
            horizontalHitDirection.y = 0;
            float displacment = horizontalHitDirection.magnitude;
            if (displacment > 0.3f)
                velocity -= 0.2f * horizontalHitDirection / displacment;
        }

        CheckCheckpoint();

        Vector3 groundVel = new Vector3();
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * 50, Color.red, 10);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            if(hit.rigidbody)
                groundVel = hit.rigidbody.velocity;
            Debug.Log($"x:  {groundVel.x}, y: {groundVel.y}, z: {groundVel.z}");
        }

        _cc.Move((velocity + groundVel)  * Time.fixedDeltaTime);
        _isGrounded = _cc.isGrounded;

        if(!_isRagdoll)
        {
            Quaternion temp = Quaternion.Slerp(new Quaternion(transform.forward.x, transform.forward.y, transform.forward.z, 1),
            new Quaternion(camForward.x, camForward.y, camForward.z, 1), Time.fixedDeltaTime * turnSpeed);
            transform.forward = new Vector3(temp.x, temp.y, temp.z);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _hitDirection = hit.point - transform.position;
    }

    private bool OnGround()
    {
        return _cc.isGrounded || Physics.Raycast(transform.position, -transform.up, .4f);
    }

    private void CheckCheckpoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            CheckPoint checkpoint = hit.collider.GetComponent<CheckPoint>();
            if (checkpoint)
            {
                respawnPoint = checkpoint.GetRespawnPoint();
            }
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
        ragdollScript.ragdollOn = false;

        uiManager.AddDeath();
    }

    public void GoRagdoll(bool value)
    {
        _isRagdoll = value;
        _cc.enabled = !value;
    }

    public Vector3 GetVelocity()
    {
        return _cc.velocity;
    }
}
