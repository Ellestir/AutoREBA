using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightForearmCollider : MonoBehaviour
{
    public static bool RightForearmCollison = false;

    private void OnCollisionEnter(Collision collision)
    {
        RightForearmCollison = true;
    }

    private void OnCollisonExit(Collision collision)
    {
        RightForearmCollison = false;
    }
}
