using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonShot : MonoBehaviour
{
    public float fireForce = 400;

    private bool _canFire = true;

    private Rigidbody _rb = null;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && _canFire)
        {
            _rb.isKinematic = false;
            _rb.AddForce(transform.forward * fireForce);
            _canFire = false;
        }
    }
}
