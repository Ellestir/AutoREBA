using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRight : MonoBehaviour
{
    public static bool RightFootGrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        RightFootGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        RightFootGrounded = false;
    }
}
