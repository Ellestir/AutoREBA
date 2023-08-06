using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSupport : MonoBehaviour
{
    public static bool BackSupported = false;

    private void OnCollisonEnter(Collision collision)
    {
        BackSupported = true;
    }

    private void OnCollisonExit(Collision collision)
    {
        BackSupported = false;
    }
}