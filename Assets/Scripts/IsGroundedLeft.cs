using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedLeft : MonoBehaviour
{
    public delegate void CollisionEvent(Collision collision);
    public static event CollisionEvent CollisionEnterLeft;
    public static event CollisionEvent CollisionExitLeft;
    public static bool isGroundedLeft = false;

    public void OnCollisionEnter(Collision collision)
    {
        CollisionEnterLeft?.Invoke(collision);
        isGroundedLeft = true;

    }
    public void OnCollisionExit(Collision collision)
    {
        CollisionExitLeft?.Invoke(collision);
        isGroundedLeft = false;
    }
}
