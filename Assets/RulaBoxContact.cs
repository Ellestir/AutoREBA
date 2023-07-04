using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulaBoxContact : MonoBehaviour
{

    public GameObject RulaBox;
    public GameObject RulaBoxGameObject;
    public GameObject FakeBox;
    public Rigidbody FakeBoxRigidbody;

    public bool canFinish;
    float pointOfSwitch;

    Material mat;
    
    // Wir haben zwei triggerzonen - setTrigger sagt an das die fakebox gleich da ist und stellt triggerzone um.
    // Wenn triggerzone berührt wird dann warten wir nur noch darauf dass die fakebox an die selbe z position kommt wie die rulabox zuletzt war
    // können wir die boxen an der selben stelle zurücktauschen.


    // Start is called before the first frame update
    void Start()
    {
        canFinish = false;
        // Fakebox soll nicht runterfallen
        FakeBoxRigidbody.useGravity = false;
        changeTransparency();
    }

     void changeTransparency()
    {
        // Hier wird die fakebox unsichtbar gemacht
        Material mat = FakeBox.GetComponent<Renderer>().material;
        Color alpha = mat.color;
        alpha.a = 0f;
        mat.color = alpha;
    }
    // Update is called once per frame
    void Update()
    {
        // Wenn beide Trigger aktiviert worden sind resetten wir und aktivieren die rulabox wieder
        if (canFinish)        
            if (FakeBox.GetComponent<Transform>().position.z == pointOfSwitch)
            {
                resetConveyer();
                RulaBoxGameObject.SetActive(true);
            }        
    }
    
    public void startConveyer()
    {
        // Postition der RulaBox nehmen damit die fakebox an genau diese stelle kommen kann
        float x = RulaBox.GetComponent<Transform>().position.x;
        float y = RulaBox.GetComponent<Transform>().position.y + 0.25f;
        float z = RulaBox.GetComponent<Transform>().position.z;

        pointOfSwitch = z;

        FakeBox.GetComponent<Transform>().position = new Vector3 (x, y, z);
        FakeBoxRigidbody.useGravity = true;

        //Körper der Rulabox ausschalten
        RulaBoxGameObject.SetActive(false);        
    }
    public void resetConveyer()
    {
        FakeBox.transform.position = new Vector3(-1.5f, 2f, 0.5f);
        changeTransparency();
        FakeBoxRigidbody.useGravity = false;
        FakeBoxRigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DEBUG: Trigger Active");
        if (other.tag == "RulaBox")
        {
            Debug.Log("DEBUG: Box");
        }
    }

    public void switchState()
    {
        canFinish = true;
    }
}
