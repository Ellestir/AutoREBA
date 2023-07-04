using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckLeft : MonoBehaviour
{
    public static bool LeftFootGrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        LeftFootGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        LeftFootGrounded = false;
    }
}
