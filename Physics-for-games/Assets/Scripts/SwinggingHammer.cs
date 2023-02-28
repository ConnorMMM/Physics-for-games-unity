using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint)), RequireComponent(typeof(Rigidbody))]
public class SwinggingHammer : MonoBehaviour
{
    private HingeJoint _joint;
    private Rigidbody _rb;

    private float targetVelocity = 0;
    private float holdTime;

    // Start is called before the first frame update
    void Start()
    {
        _joint = GetComponent<HingeJoint>();
        _rb = GetComponent<Rigidbody>();

        _rb.maxAngularVelocity = Mathf.Infinity;

        holdTime = Random.value * 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdTime > 0)
        {
            holdTime--;
            return;
        }

        if ((transform.rotation.x >= 0.6f || transform.rotation.x <= -0.6f) && _rb.velocity.y > 0)
        {
            _rb.velocity = _rb.velocity * 0.8f;
        }

        if (transform.rotation.x >= 0.01f)
            targetVelocity = -7000;
        else if (transform.rotation.x <= -0.01f)
            targetVelocity = 7000;

        var temp = _joint.motor;
        temp.targetVelocity = targetVelocity;
        _joint.motor = temp;
    }
}
