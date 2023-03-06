using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnPad : MonoBehaviour
{
    public GameObject ball;
    public Vector3 spawnPoint = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position + transform.up * 1.7f;
    }

    public void Respawn()
    {
        if(ball)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if(rb)
            {
                rb.velocity = new Vector3();
                rb.angularVelocity = new Vector3();
            }

            ball.transform.position = spawnPoint;
            ball.transform.rotation = new Quaternion();

        }
    }
}
