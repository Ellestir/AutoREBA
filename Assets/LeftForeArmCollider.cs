using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftForeArmCollider : MonoBehaviour
{
    public static bool LeftForeArmCollision = false;

    private void OnCollisonEnter(Collision collision)
    {
        LeftForeArmCollision = true;
    }
    private void OnCollisonExit(Collision collision)
    {
        LeftForeArmCollision = false;
    }
}
