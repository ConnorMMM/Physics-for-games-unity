using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPocket : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject ball;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            uiManager.StopTimer();
        }
    }
}
