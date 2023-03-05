using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ConfigurableJoint elevatorJoint;

    public Vector3 startPos = new Vector3();
    public Vector3 endPos = new Vector3();

    void Start()
    {
        elevatorJoint.targetPosition = startPos;
    }

    public void Pressed()
    {
        if (elevatorJoint.targetPosition != startPos)
            elevatorJoint.targetPosition = startPos;
        else
            elevatorJoint.targetPosition = endPos;
    }
}
