using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutButton : MonoBehaviour
{
    public GameObject shortcut;

    public void Pressed()
    {
        shortcut.SetActive(!shortcut.active);
    }
}
