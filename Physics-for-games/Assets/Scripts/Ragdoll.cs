using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    private Animator _animator = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public bool ragdollOn 
    { 
        get { return !_animator.enabled; }
        set { _animator.enabled = !value;
            foreach (Rigidbody rb in rigidbodies)
                rb.isKinematic = !value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        foreach (Rigidbody rb in rigidbodies)
            rb.isKinematic = true;
    }
}
