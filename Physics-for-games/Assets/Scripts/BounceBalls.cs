using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBalls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterMover character = other.GetComponent<CharacterMover>();
        if(character)
        {
            Vector3 force = Vector3.Normalize(other.transform.position - transform.position);
            character.velocity = -character.velocity + (force * character.velocity.magnitude * 0.05f);
        }
    }
}
