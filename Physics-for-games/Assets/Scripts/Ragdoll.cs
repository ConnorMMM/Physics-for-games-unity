using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    private Animator _animator = null;
    private CharacterMover _characterMover = null;

    public bool ragdollOn 
    { 
        get { return !_animator.enabled; }
        set { _animator.enabled = !value;
            foreach (Rigidbody rb in rigidbodies)
                rb.isKinematic = !value;
            if(_characterMover)
                _characterMover._isRagdoll = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterMover = GetComponent<CharacterMover>();

        foreach (Rigidbody rb in rigidbodies)
            rb.isKinematic = true;
    }
}
