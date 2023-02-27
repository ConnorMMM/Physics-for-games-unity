using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public HingeJoint frontRight;
    public HingeJoint frontLeft;

    public float rightSpeed = 0;
    public float leftSpeed = 0;

    //public HingeJoint backRight;
    //public HingeJoint backLeft;

    //public bool fourWheelDrive = false;

    // Update is called once per frame
    void Update()
    {
        rightSpeed = 0;
        leftSpeed = 0;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rightSpeed = -400;
            leftSpeed = -400;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            rightSpeed = 400;
            leftSpeed = 400;
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (rightSpeed != 0)
            {
                rightSpeed *= 1.8f;
                leftSpeed *= 0.2f;
            }
            else
                rightSpeed = -600;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if (leftSpeed != 0)
            {
                leftSpeed *= 1.8f;
                rightSpeed *= 0.2f;
            }
            else
                leftSpeed = -600;
        }

        var temp1 = frontRight.motor;
        temp1.targetVelocity = rightSpeed;
        frontRight.motor = temp1;

        var temp2 = frontLeft.motor;
        temp2.targetVelocity = leftSpeed;
        frontLeft.motor = temp2;
    }
}
