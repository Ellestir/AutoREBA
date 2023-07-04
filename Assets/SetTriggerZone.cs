using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerZone : MonoBehaviour
{
    private bool switchTriggerZone;
    public TriggerZone tz_object;
    // Start is called before the first frame update
    void Start()
    {
        switchTriggerZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTriggerZone)
        {
            switchTriggerZone = false;
            tz_object.switchTrigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DEBUG: Trigger Active");
        if (other.tag == "RulaBox")
        {
            switchTriggerZone = true;
            Debug.Log("DEBUG: Box");
        }
    }

}
