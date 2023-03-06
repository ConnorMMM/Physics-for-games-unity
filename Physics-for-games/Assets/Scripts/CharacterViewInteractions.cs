using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewInteractions : MonoBehaviour
{
    public Camera camera;
    public LayerMask layerMask;
    public float clickDistance = 6f;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 20f, layerMask))
            {
                if (Vector3.Distance(hit.point, transform.position) > clickDistance)
                    return;

                ElevatorButton elevatorButton = hit.collider.GetComponent<ElevatorButton>();
                if (elevatorButton)
                {
                    elevatorButton.Pressed();
                    return;
                }

                StartButton startButton = hit.collider.GetComponent<StartButton>();
                if (startButton)
                {
                    startButton.Pressed();
                    return;
                }

                ShortcutButton shortcutButton = hit.collider.GetComponent<ShortcutButton>();
                if (shortcutButton)
                {
                    shortcutButton.Pressed();
                    return;
                }

                BallSpawnPad spawnPad = hit.collider.GetComponent<BallSpawnPad>();
                if (spawnPad)
                {
                    spawnPad.Respawn();
                    return;
                }

                if(hit.collider.gameObject.tag == "Ball")
                {
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb)
                        rb.AddForce(Vector3.Normalize(rb.transform.position - hit.point) * 90f, ForceMode.Impulse);
                }
            }
        }
    }
}
