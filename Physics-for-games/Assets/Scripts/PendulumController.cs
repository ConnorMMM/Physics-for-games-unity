using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    public GameObject endObject = null;

    // Start is called before the first frame update
    void Start()
    {
        if(endObject)
        {
            endObject.transform.position = transform.position + transform.right * 4.5f;
            endObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            endObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
