using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public RulaBoxContact rbc_object;
    private bool canTriggerZone;

    // Start is called before the first frame update
    void Start()
    {
        canTriggerZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DEBUG: Trigger Active");
        if (other.tag == "RulaBox")
        {
            if (canTriggerZone) { 
                Debug.Log("DEBUG: Box");
                canTriggerZone = false;
                rbc_object.switchState();
            }
        }
    }

    public void switchTrigger()
    {
        canTriggerZone = true;
    }

}
