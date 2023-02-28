using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RagdollHit : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ragdoll ragdoll = collision.collider.GetComponentInParent<Ragdoll>();
        if (ragdoll != null)
        {
            ragdoll.ragdollOn = true;
            Debug.Log($"x: {collision.impulse.x}, y: {collision.impulse.y}, z: {collision.impulse.z}, mag: {collision.impulse.magnitude}");
            if (collision.impulse.magnitude > 120)
                collision.rigidbody.AddForceAtPosition((collision.impulse) * .3f, collision.GetContact(0).point, ForceMode.Impulse);
            else
                collision.rigidbody.AddForceAtPosition((collision.impulse) * .4f, collision.GetContact(0).point, ForceMode.Impulse);
            //collision.rigidbody.AddForceAtPosition(-collision.GetContact(0).normal * 100f, collision.GetContact(0).point, ForceMode.Impulse);
        }
    }
}
