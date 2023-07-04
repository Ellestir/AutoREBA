using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputingAngles : MonoBehaviour
{
    public GameObject ObjectA;
    public GameObject ObjectB;
    public GameObject ObjectC;
    public GameObject ObjectD;

    private Vector3 ObjectA_calibrated;
    private Vector3 ObjectB_calibrated;
    private Vector3 ObjectC_calibrated;
    private Vector3 ObjectD_calibrated;

    // Start is called before the first frame update
    void Start()
    {
        ObjectA_calibrated = new Vector3();
        ObjectB_calibrated = new Vector3();
        ObjectC_calibrated = new Vector3();
        ObjectD_calibrated = new Vector3();
    }

    public void Calibrate()
    {
        ObjectA_calibrated = ObjectA.transform.position;
        ObjectB_calibrated = ObjectB.transform.position;
        ObjectC_calibrated = ObjectC.transform.position;
        ObjectD_calibrated = ObjectD.transform.position;
    }

    private float angleBetweenTwoPositions(GameObject A, GameObject B, GameObject C)
    {
        Vector3 directionBC = Vector3.Normalize(B.transform.position - C.transform.position);
        Vector3 directionBA = Vector3.Normalize(B.transform.position - A.transform.position);

        Debug.DrawLine(B.transform.position, A.transform.position, Color.red, 0.001f, false);
        Debug.DrawLine(B.transform.position, C.transform.position, Color.red, 0.001f, false);

        return Vector3.Angle(directionBC, directionBA);
    }

    private float angleBetweenTwoPositions(GameObject A, GameObject B, GameObject C, Vector3 calibrateA, Vector3 calibrateB, Vector3 calibrateC)
    {
        Vector3 directionBC = Vector3.Normalize(B.transform.position - C.transform.position);
        Vector3 directionBA = Vector3.Normalize(B.transform.position - A.transform.position);
        Vector3 directionBC_calibrated = Vector3.Normalize(calibrateB - calibrateC);
        Vector3 directionBA_calibrated = Vector3.Normalize(calibrateB - calibrateA);
        return Vector3.Angle(directionBC, directionBA) - Vector3.Angle(directionBC_calibrated, directionBA_calibrated);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Angle at Object B (red): " + angleBetweenTwoPositions(ObjectA, ObjectB, ObjectC));
        Debug.Log("Angle at Object C (blue): " + angleBetweenTwoPositions(ObjectB, ObjectC, ObjectD));
        Debug.Log("Calibrated Angle at Object B (red): " + angleBetweenTwoPositions(ObjectA, ObjectB, ObjectC, ObjectA_calibrated, ObjectB_calibrated, ObjectC_calibrated));
        Debug.Log("Calibrated Angle at Object C (blue): " + angleBetweenTwoPositions(ObjectB, ObjectC, ObjectD, ObjectB_calibrated, ObjectC_calibrated, ObjectD_calibrated));
    }
}