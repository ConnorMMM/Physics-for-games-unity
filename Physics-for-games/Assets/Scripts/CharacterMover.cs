using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpHeight = 4;
    public float turnSpeed = 6;
    public Vector3 hitDirection = new Vector3();

    private CharacterController _cc;
    private Transform _camera;
    private Animator _animator;

    private Vector2 _moveInput = new Vector2();
    private bool _jumpInput = false;
    private bool _isGrounded = false;

    private Vector3 _velocity = new Vector3();

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");
        _jumpInput = Input.GetButton("Jump");

        _animator.SetFloat("Forwards", _moveInput.y);
        _animator.SetBool("Jump", !_isGrounded);
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
        if(_isGrounded || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _velocity.x = delta.x;
            _velocity.z = delta.z;
        }

        // Check for jumping
        if (_jumpInput && _isGrounded)
            _velocity.y = Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);

        // Check if we've hit ground from falling. If so, remove our velocity
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = 0;

        // Apply gravity
        _velocity += Physics.gravity * Time.fixedDeltaTime;

        if (!_isGrounded)
            hitDirection = Vector3.zero;

        // Slide objects off surfaces they're hanging on to
        if(_moveInput.x == 0 && _moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;
            horizontalHitDirection.y = 0;
            float displacment = horizontalHitDirection.magnitude;
            if (displacment > 0.2f)
                _velocity -= 0.2f * horizontalHitDirection / displacment;
        }

        _cc.Move(_velocity * Time.fixedDeltaTime);
        _isGrounded = _cc.isGrounded;
        
        Quaternion temp = Quaternion.Slerp(new Quaternion(transform.forward.x, transform.forward.y, transform.forward.z, 1), 
            new Quaternion(camForward.x, camForward.y, camForward.z, 1), Time.fixedDeltaTime * turnSpeed);
        transform.forward = new Vector3(temp.x, temp.y, temp.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitDirection = hit.point - transform.position;
    }
}
