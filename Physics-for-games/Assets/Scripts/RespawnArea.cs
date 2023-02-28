using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterMover character = other.GetComponentInParent<CharacterMover>();
        if (character != null)
            character.Respawn();
    }
}
