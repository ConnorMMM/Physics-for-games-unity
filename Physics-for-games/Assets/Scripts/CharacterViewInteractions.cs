using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewInteractions : MonoBehaviour
{
    public Camera camera;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 20f, layerMask))
            {
                if (Vector3.Distance(hit.point, transform.position) > 5f)
                    return;

                ElevatorButton elevator = hit.collider.GetComponent<ElevatorButton>();
                if (elevator)
                {
                    elevator.Pressed();
                }


            }
        }
    }
}
