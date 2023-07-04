using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoREBA : MonoBehaviour
{
    public ethancontroler ethancontrol;

    public int SubjectID;
    public Boolean scoreOn;
    public Boolean avatarOn;
    public Text userScore;
    public TextMeshProUGUI tmp_userScore;
    public Button Btn_show_score;
    public Button btn_hide_score;
    private String score;

    public GameObject collarR;
    public GameObject upperArmR;
    public GameObject lowerArmR;
    public GameObject wristR;
    public GameObject midfingerR;
    public GameObject midfinger1R;

    private Vector3 lowerArmR_calibrated;
    private Vector3 wristR_calibrated;
    private Vector3 midfingerR_calibrated;

    public GameObject collarL;
    public GameObject upperArmL;
    public GameObject lowerArmL;
    public GameObject wristL;
    public GameObject midfingerL;

    private Vector3 lowerArmL_calibrated;
    private Vector3 wristL_calibrated;
    private Vector3 midfingerL_calibrated;

    public GameObject neck;
    public GameObject head;
    public GameObject spine1;
    public GameObject spine;
    public GameObject spine2;

    private Vector3 spine_calibrated;
    private Vector3 spine1_calibrated;
    private Vector3 spine2_calibrated;

    public GameObject hipR;
    public GameObject kneeR;
    public GameObject footR;

    public GameObject hipL;
    public GameObject kneeL;
    public GameObject footL;

    public Transform rotCalUpperArmR;
    public Transform rotCalCollarR;
    public Transform rotCalLowerArmR;
    public Transform rotCalWristR;
    public Transform rotCalMidfingerR;
    public Transform rotCalMidfinger1R;

    public Transform rotCalHipR;
    public Transform rotCalKneeR;
    public Transform rotCalFootR;

    public Transform rotCalHipL;
    public Transform rotCalKneeL;
    public Transform rotCalFootL;

    private Quaternion rotCalUpperArmQuadR;
    private Quaternion rotCalCollarQuadR;
    private Quaternion rotCalLowerArmQuadR;
    private Quaternion rotCalWristQuadR;
    private Quaternion rotCalMidfingerQuadR;
    private Quaternion rotCalMidfinger1QuadR;

    private Quaternion rotCalHipQuadR;
    private Quaternion rotCalKneeQuadR;
    private Quaternion rotCalFootQuadR;

    private Quaternion rotCalHipQuadL;
    private Quaternion rotCalKneeQuadL;
    private Quaternion rotCalFootQuadL;

    public Transform rotCalUpperArmL;
    public Transform rotCalCollarL;
    public Transform rotCalLowerArmL;
    public Transform rotCalWristL;
    public Transform rotCalMidfingerL;

    public Transform rotCalNeck;
    public Transform rotCalHead;
    public Transform rotCalSpine1;
    public Transform rotCalSpine;

    private Quaternion rotCalUpperArmQuadL;
    private Quaternion rotCalCollarQuadL;
    private Quaternion rotCalLowerArmQuadL;
    private Quaternion rotCalWristQuadL;
    private Quaternion rotCalMidfingerQuadL;

    private Quaternion rotCalNeckQuad;
    private Quaternion rotCalHeadQuad;
    private Quaternion rotCalSpine1Quad;
    private Quaternion rotCalSpineQuad;

    private ArrayList exportDaten = new ArrayList();
    bool calibrated = false;

    public void Calibration()
    {
        rotCalUpperArmQuadR = rotCalUpperArmR.transform.rotation;
        rotCalCollarQuadR = rotCalCollarR.transform.rotation;

        rotCalLowerArmQuadR = rotCalLowerArmR.transform.rotation;
        rotCalWristQuadR = rotCalWristR.transform.rotation * rotCalLowerArmR.transform.rotation; // old
        lowerArmR_calibrated = lowerArmR.transform.position;
        wristR_calibrated = wristR.transform.position;
        midfingerR_calibrated = midfingerR.transform.position;
        rotCalMidfingerQuadR = rotCalMidfingerR.transform.rotation; //old
        //rotCalMidfinger1QuadR = rotCalMidfinger1R.transform.rotation;

        rotCalUpperArmQuadL = rotCalUpperArmL.transform.rotation;
        rotCalCollarQuadL = rotCalCollarL.transform.rotation;
        rotCalLowerArmQuadL = rotCalLowerArmL.transform.rotation;
        rotCalWristQuadL = rotCalWristL.transform.rotation; // old
        rotCalMidfingerQuadL = rotCalMidfingerL.transform.rotation; //old

        lowerArmL_calibrated = lowerArmL.transform.position;
        wristL_calibrated = wristL.transform.position;
        midfingerL_calibrated = midfingerL.transform.position;
        
        rotCalNeckQuad = rotCalNeck.transform.rotation;
        rotCalHeadQuad = rotCalHead.transform.rotation;
        rotCalSpineQuad = rotCalSpine.transform.rotation;
        rotCalSpine1Quad = rotCalSpine1.transform.rotation;

        spine_calibrated = spine.transform.position;
        spine1_calibrated = spine1.transform.position;
        spine2_calibrated = spine2.transform.position;

        lowerArmR_calibrated = lowerArmR.transform.position;
        wristR_calibrated = wristR.transform.position;
        midfingerR_calibrated = midfingerR.transform.position;

        rotCalHipQuadR = rotCalHipR.transform.rotation;
        rotCalKneeQuadR = rotCalKneeR.transform.rotation;
        rotCalFootQuadR = rotCalFootR.transform.rotation;

        rotCalHipQuadL = rotCalHipL.transform.rotation;
        rotCalKneeQuadL = rotCalKneeL.transform.rotation;
        rotCalFootQuadL = rotCalFootL.transform.rotation;
        calibrated = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        lowerArmR_calibrated = new Vector3();
        wristR_calibrated = new Vector3();
        midfingerR_calibrated = new Vector3();
        lowerArmL_calibrated = new Vector3();
        wristL_calibrated = new Vector3();
        midfingerL_calibrated = new Vector3();
        spine_calibrated = new Vector3();
        spine1_calibrated = new Vector3();
        spine2_calibrated = new Vector3();
        tmp_userScore.enabled = false;
        userScore.enabled = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion qUpperArmR = Quaternion.Inverse(rotCalUpperArmQuadR) * upperArmR.transform.rotation;
        Quaternion qCollarR = Quaternion.Inverse(rotCalCollarQuadR) * collarR.transform.rotation;
        Quaternion qLowerArmR = Quaternion.Inverse(rotCalLowerArmQuadR) * lowerArmR.transform.rotation;
        Quaternion qWristR = Quaternion.Inverse(rotCalWristQuadR) * wristR.transform.rotation;
        Quaternion qMidfingerR = Quaternion.Inverse(rotCalMidfingerQuadR) * midfingerR.transform.rotation;
        //Quaternion qMidfinger1R = Quaternion.Inverse(rotCalMidfinger1QuadR) * midfinger1R.transform.rotation;

        Quaternion qUpperArmL = Quaternion.Inverse(rotCalUpperArmQuadL) * upperArmL.transform.rotation;
        Quaternion qCollarL = Quaternion.Inverse(rotCalCollarQuadL) * collarL.transform.rotation;
        Quaternion qLowerArmL = Quaternion.Inverse(rotCalLowerArmQuadL) * lowerArmL.transform.rotation;
        Quaternion qWristL = Quaternion.Inverse(rotCalWristQuadL) * wristL.transform.rotation;
        Quaternion qMidfingerL = Quaternion.Inverse(rotCalMidfingerQuadL) * midfingerL.transform.rotation;

        Quaternion qNeck = Quaternion.Inverse(rotCalNeckQuad) * neck.transform.rotation;
        Quaternion qHead = Quaternion.Inverse(rotCalHeadQuad) * head.transform.rotation;

        Quaternion qSpine1 = Quaternion.Inverse(rotCalSpine1Quad) * spine1.transform.rotation;
        Quaternion qSpine = Quaternion.Inverse(rotCalSpineQuad) * spine.transform.rotation;

        Quaternion qHipR = Quaternion.Inverse(rotCalHipQuadR) * hipR.transform.rotation;
        Quaternion qKneeR = Quaternion.Inverse(rotCalKneeQuadR) * kneeR.transform.rotation;
        Quaternion qFootR = Quaternion.Inverse(rotCalFootQuadR) * footR.transform.rotation;

        Quaternion qHipL = Quaternion.Inverse(rotCalHipQuadL) * hipL.transform.rotation;
        Quaternion qKneeL = Quaternion.Inverse(rotCalKneeQuadL) * kneeL.transform.rotation;
        Quaternion qFootL = Quaternion.Inverse(rotCalFootQuadL) * footL.transform.rotation;


        //angle upper arm right(1)
        float angleUpperArmR = 0f;
        angleUpperArmR = Quaternion.Angle(qUpperArmR, qCollarR); 
        Debug.Log("REBA Right Upper Arm Angle:" + angleUpperArmR);
        //right schoulder up right(1a)
        float shoulderRotationR = 0f;
        shoulderRotationR = collarR.transform.localRotation.eulerAngles.y;
        //shoulder abduced right(1a)
        float angleHeadShoulderR = 0f;
        angleHeadShoulderR = Quaternion.Angle(qNeck, qUpperArmR);
        
        //angle upper arm left(1)
        float angleUpperArmL = 0f;
        angleUpperArmL= Quaternion.Angle(qUpperArmL, qCollarL);
        //left schoulder up (1a)
        float shoulderRotationL = 0f;
        shoulderRotationL = collarL.transform.localRotation.eulerAngles.y;
        //shoulder abduced left(1a)
        float angleHeadShoulderL = 0f;
        angleHeadShoulderL = Quaternion.Angle(qNeck, qUpperArmL);

        //Angle right lower arm(2)
        float angleLowerArmR = 0f;
        angleLowerArmR = Quaternion.Angle(qUpperArmR, qLowerArmR); //angleLower arm
        //Debug.Log("REBA Right Lower Arm:" + angleLowerArmR);

        //Angle left lower arm(2)
        float angleLowerArmL = 0f;
        angleLowerArmL = Quaternion.Angle(qUpperArmL, qLowerArmL); //angleLower arm
        //Debug.Log("REBA Right Lower Arm:" + angleLowerArmR);

        //angle right wrist (3)
        float angleWristR = 0f;
        angleWristR = angleBetweenTwoPositions(lowerArmR, wristR, midfingerR, lowerArmR_calibrated, wristR_calibrated, midfingerR_calibrated);
        //right wrist deviates sideways from the midline (3a)
        float wristSidewayDeviationR = 0f;
        wristSidewayDeviationR = Quaternion.Angle(qWristR, qMidfingerR);  
        //wrist twist
        float wristRotationR = 0f;
        wristRotationR = wristR.transform.localRotation.eulerAngles.y;

        //angle left wrist (3)
        float angleWristL = 0f;
        angleWristL= angleBetweenTwoPositions(lowerArmL, wristL, midfingerL, lowerArmL_calibrated, wristL_calibrated, midfingerL_calibrated);

        //left wrist deviates sideways from the midline (3a)
        float wristSidewayDeviationL = 0f;
        wristSidewayDeviationL = Quaternion.Angle(qWristL, qMidfingerL);  //wristR.transform.rotation, midfingerR.transform.rotation);
        Debug.Log("REBA Left Wrist sideways Deviation: " + wristSidewayDeviationL);
        //left wrist twist
        float wristRotationL = 0f;
        wristRotationL = wristL.transform.localRotation.eulerAngles.y;


        //angle trunk
        float angleTrunk = 0f;
        angleTrunk = angleBetweenTwoPositions(spine2, spine1, spine, spine2_calibrated, spine1_calibrated, spine_calibrated);  //Quaternion.Angle(qSpine1, qSpine);
        //sideway lean
        float trunkRotationY = spine1.transform.localRotation.eulerAngles.y;
        //sideway trunk turn
        float trunkRotationX = spine1.transform.localRotation.eulerAngles.x;

        // Neck angle and rotations for score calculation
        float angleNeck = 0f;
        angleNeck = Quaternion.Angle(qNeck, qHead);
        float headRotationZ = 0f;
        headRotationZ = head.transform.localRotation.eulerAngles.z;
        //left or right neck turn
        float headRotationX = head.transform.localRotation.eulerAngles.x;
        //left or right neck lean
        float headRotationY = head.transform.localRotation.eulerAngles.y;


        //rotation of the right hip: towards/backwards
        float hipRotationR = 0f;
        hipRotationR = hipR.transform.localRotation.eulerAngles.z;
        //angle right knee 
        float angleKneeR = 0f;
        angleKneeR = Quaternion.Angle(qHipR, qKneeR);

        //rotation of the left hip: towards/backwards
        float hipRotationL = 0f;
        hipRotationL = hipL.transform.localRotation.eulerAngles.z;
        //angle left knee
        float angleKneeL = 0f;
        angleKneeL = Quaternion.Angle(qHipL, qKneeL);

        int neckS = neckRebaScore(headRotationZ, headRotationX, headRotationY);
        int trunk = trunkRebaScore(angleTrunk, trunkRotationX, trunkRotationY);
        int legs = legRebaScore(hipRotationR, hipRotationL, angleKneeR, angleKneeL);
        int tableA =REBA_Score_Neck_Trunk_Legs(neckS, trunk, legs);

        int upperArmRS = upperArmRebaScore(angleUpperArmR, shoulderRotationR, angleHeadShoulderR, angleTrunk);
        int upperArmLS = upperArmRebaScore(angleUpperArmL, shoulderRotationL, angleHeadShoulderL, angleTrunk);
        int lowerArmRS = unterArmRebaScore(angleLowerArmR);
        int lowerArmLS = unterArmRebaScore(angleLowerArmR);
        int wristRS = wristRebaScore(angleWristR, wristSidewayDeviationR, wristRotationR);
        int wristLS = wristRebaScore(angleWristL, wristSidewayDeviationL, wristRotationL);
        int tableBR= REBA_Score_Upper_Lower_Arm_Wrist(upperArmRS, lowerArmRS, wristRS);
        int tableBL= REBA_Score_Upper_Lower_Arm_Wrist(upperArmLS, lowerArmLS, wristLS);
        
        int sR = displayS(tableA, tableBR);
        int sL = displayS(tableA, tableBR);

        score = REBA_Score_Feedback(sR);
        
        userScore.text = score;
        tmp_userScore.text = score;
        Btn_show_score.onClick.AddListener(showScore);
        btn_hide_score.onClick.AddListener(hideScore);

        if (calibrated== true)
        {
            DatenExport datenExport = new DatenExport(SubjectID, scoreOn, avatarOn, neckS, trunk, legs, tableA, upperArmRS, upperArmLS, lowerArmRS, lowerArmLS, wristRS, wristLS, tableBR, tableBL, sR, sL);
            exportDaten.Add(datenExport);
            writecsv();
        }
    }

    public void showScore()
    {
        userScore.enabled= true;
    }
    public void hideScore()
    {
        userScore.enabled = false;
    }

    public int displayS(int tableA, int tableB)
    {
        int score = REBA_Score(tableA, tableB, 1, 0);
        return score;
    }       

    private void writecsv()
    {
        string filename= @"E:\AutoRULA Gruppe 11 v1\Results\logData.csv";
        string delimiter = ";";
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("UserId; Avatar; Score; NeckScore; TrunkScore; LegsScore; TableAScore; UpperArmRS; UpperArmLS; LowerArmRS; LowerArmLS; WristRS; WristLS; TableBR; TableBL; ScoreOverallR; ScoreOverallL; Date; Time");
                foreach (DatenExport data in exportDaten)
                {
                    writer.Write(data.GetUserId() + delimiter);
                    writer.Write(data.GetAvatar() + delimiter);
                    writer.Write(data.GetScore() + delimiter);
                    writer.Write(data.GetNeckScore() + delimiter);
                    writer.Write(data.GetTrunkScore() + delimiter);
                    writer.Write(data.GetLegsScore() + delimiter);
                    writer.Write(data.GetTableAScore() + delimiter);
                    writer.Write(data.GetUpperArmRS() + delimiter);
                    writer.Write(data.GetUpperArmLS() + delimiter);
                    writer.Write(data.GetLowerArmRS() + delimiter);
                    writer.Write(data.GetLowerArmLS() + delimiter);
                    writer.Write(data.GetWristRS() + delimiter);
                    writer.Write(data.GetWristLS() + delimiter);
                    writer.Write(data.GetTableBR() + delimiter);
                    writer.Write(data.GetTableBL() + delimiter);
                    writer.Write(data.GetScoreOverallR() + delimiter);
                    writer.Write(data.GetScoreOverallL() + delimiter);
                    writer.Write(DateTime.Now.ToString("dd-MM-yyyy") + delimiter);
                     writer.Write(DateTime.Now.ToString("h:mm:ss") + "\n");

                }
               
            }
        }
        catch (Exception exp)
        {
            Console.Write(exp.Message);
        }
    }
    private float angleBetweenTwoPositions(GameObject A, GameObject B, GameObject C)
    {
        Vector3 directionBC = Vector3.Normalize(B.transform.position - C.transform.position);
        Vector3 directionBA = Vector3.Normalize(B.transform.position - A.transform.position);
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

    private Boolean shoulderUp(float angleShoulderRotation)
    {
        if (angleShoulderRotation < 59)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean shoulderAbduced(float angleHeadShoulder)
    {
        if (angleHeadShoulder > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean personLeansForward(float angleTrunk)
    {
        if (angleTrunk != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Calculate the score for shoulder
    private int upperArmRebaScore(float angleUpperArm, float shoulderRotation, float angleHeadShoulder, float angleTrunk)
    {
        int scoreUpperArm = 0;
        if (shoulderUp(shoulderRotation))
        {
            scoreUpperArm = scoreUpperArm + 1;
        }
        if (shoulderAbduced(angleHeadShoulder))
        {
            scoreUpperArm = scoreUpperArm + 1;
        }
        if (personLeansForward(angleTrunk))
        {
            scoreUpperArm = scoreUpperArm - 1;
        }

        if (angleUpperArm >= 0 && angleUpperArm <= 20)
        {
            scoreUpperArm = scoreUpperArm + 1;
        }
        else if (angleUpperArm > 20 && angleUpperArm <= 45)
        {
            scoreUpperArm = scoreUpperArm + 2;
        }
        else if (angleUpperArm > 45 && angleUpperArm <= 90)
        {
            scoreUpperArm = scoreUpperArm + 3;
        }
        else if (angleUpperArm > 90)
        {
            scoreUpperArm = scoreUpperArm + 4;
        }
        return scoreUpperArm;
    }

    //Calculate the score for the forearm
    private int unterArmRebaScore(float angleUnterArm)
    {
        int scoreLowerArm = 0;

        if (angleUnterArm < 60)
        {
            scoreLowerArm = scoreLowerArm + 2;
        }
        else if (angleUnterArm >= 60 && angleUnterArm <= 100)
        {
            scoreLowerArm = scoreLowerArm + 1;
        }

        else if (angleUnterArm > 100)
        {
            scoreLowerArm = scoreLowerArm + 2;
        }
        return scoreLowerArm;
    }

    private int wristRebaScore(float angleWrist, float wristSidewayDeviation, float wristRotation)
    {
        int scoreWrist = 0;
        if (wristTwisted(wristSidewayDeviation) || wristRotated(wristRotation))
        {
            scoreWrist = scoreWrist + 1;
        }

        if (angleWrist > 0 && angleWrist <= 15)
        {
            scoreWrist = scoreWrist + 1;
        }
        else if (angleWrist > 15)
        {
            scoreWrist = scoreWrist + 2;
        }
        return scoreWrist;
    }
    //Check the wrist sideways deviation
    private Boolean wristTwisted(float wristSidewayDeviation)
    {
        if (wristSidewayDeviation != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private Boolean wristRotated(float wristRotation)
    {
        if (wristRotation > 265 && wristRotation < 280)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private int neckRebaScore(float headRotationZ, float headRotationX, float headRotationY)
    {
        int scoreNeck = 0;
        if (headRotationX != 0 || headRotationY != 0) //(headRotationX > 3 && headRotationX < 357) || (headRotationY > 3 && headRotationY < 357))
        {
            scoreNeck = scoreNeck + 1;
        }

        if (headRotationZ >= 0 && headRotationZ <= 20)
        {
            scoreNeck = scoreNeck + 1;
        }

        else if (headRotationZ > 20)
        {
            scoreNeck = scoreNeck + 2;
        }

        return scoreNeck;
    }

    private int trunkRebaScore(float angleTrunk, float trunkRotationX, float trunkRotationY)
    {
        int scoreTrunk = 0;

        if (trunkRotationX != 0 || trunkRotationY != 0) //(trunkRotationX > 3 && trunkRotationX < 357) || (trunkRotationY > 3 && trunkRotationY < 357))
        {
            scoreTrunk = scoreTrunk + 1;
        }

        if (angleTrunk == 0)
        {
            scoreTrunk = scoreTrunk + 1;
        }
        else if (angleTrunk > 0 && angleTrunk <= 20)
        {
            scoreTrunk = scoreTrunk + 2;
        }
        else if (angleTrunk > 20 && angleTrunk <= 60)
        {
            scoreTrunk = scoreTrunk + 3;
        }
        else if (angleTrunk > 60)
        {
            scoreTrunk = scoreTrunk + 4;
        }
        return scoreTrunk;
    }

    private int legRebaScore(float hipRotationR, float hipRotationL, float angleKneeR, float angleKneeL)
    {
        int scoreLeg = 0;
        if (legRotated(hipRotationR, hipRotationL))
        {
            scoreLeg = scoreLeg + 2;
        }
        else
        {
            scoreLeg = scoreLeg + 1;
        }

        if (legsBended(angleKneeR, angleKneeL))
        {
            if ((angleKneeR > 30 && angleKneeR <= 60) || (angleKneeL > 30 && angleKneeL <= 60))
            {
                scoreLeg = scoreLeg + 1;
            }
            else if (angleKneeR > 60 || angleKneeL > 60)
            {
                scoreLeg = scoreLeg + 2;
            }
        }
        return scoreLeg;
    }

    private Boolean legsBended(float angleKneeR, float angleKneeL)
    {
        if (angleKneeR == 180 && angleKneeL == 180)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private Boolean legRotated(float hipRotationR, float hipRotationL)
    {
        if (hipRotationR != 0 || hipRotationL != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int REBA_Score_Neck_Trunk_Legs(int neck, int trunk, int legs)
    {
        int[,] REBA_Score_Neck_Trunk_Legs_1 = new int[5, 4] { { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 2, 4, 5, 6 }, { 3, 5, 6, 7 }, { 4, 6, 7, 8 } };
        int[,] REBA_Score_Neck_Trunk_Legs_2 = new int[5, 4] { { 1, 2, 3, 4 }, { 3, 4, 5, 6 }, { 4, 5, 6, 7 }, { 5, 6, 7, 8 }, { 6, 7, 8, 9 } };
        int[,] REBA_Score_Neck_Trunk_Legs_3 = new int[5, 4] { { 1, 2, 3, 4 }, { 3, 3, 5, 6 }, { 5, 6, 7, 8 }, { 6, 7, 8, 9 }, { 7, 8, 9, 9 } };

        if (neck == 1)
        {
            if (trunk == 1)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[0, 0]; // answer: 1
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[0, 1]; // answer: 2
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[0, 2]; // answer: 3
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[0, 3]; // answer: 4
                }
            }
            else if (trunk == 2)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[1, 0]; // answer: 2
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[1, 1]; // answer: 3
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[1, 2]; // answer: 4
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[1, 3]; // answer: 5
                }
            }
            else if (trunk == 3)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[2, 0]; // answer: 2
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[2, 1]; // answer: 4
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[2, 2]; // answer: 5
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[2, 3]; // answer: 6
                }
            }
            else if (trunk == 4)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[3, 0]; // answer: 3
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[3, 1]; // answer: 5
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[3, 2]; // answer: 6
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[3, 3]; // answer: 7
                }
            }
            else if (trunk == 5)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[4, 0]; // answer: 4
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[4, 1]; // answer: 6
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[4, 2]; // answer: 7
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_1[4, 3]; // answer: 8
                }
            }

        }
        else if (neck == 2)
        {
            if (trunk == 1)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 0]; // answer: 1
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 1]; // answer: 2
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 2]; // answer: 3
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 3]; // answer: 4
                }
            }
            else if (trunk == 2)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[1, 0]; // answer: 3
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[1, 1]; // answer: 4
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[1, 2]; // answer: 5
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[1, 3]; // answer: 6
                }
            }
            else if (trunk == 3)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[2, 0]; // answer: 4
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[2, 1]; // answer: 5
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[2, 2]; // answer: 6
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[2, 3]; // answer: 7
                }
            }
            else if (trunk == 4)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[3, 0]; // answer: 5
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[3, 1]; // answer: 6
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[3, 2]; // answer: 7
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[3, 3]; // answer: 8
                }
            }
            else if (trunk == 5)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[4, 0]; // answer: 6
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[4, 1]; // answer: 7
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[4, 2]; // answer: 8
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[4, 3]; // answer: 9
                }
            }
        }
        else if (neck == 3)
        {
            if (trunk == 1)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 0]; // answer: 3
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 1]; // answer: 3
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 2]; // answer: 5
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_2[0, 3]; // answer: 6
                }
            }
            else if (trunk == 2)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[1, 0]; // answer: 4
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[1, 1]; // answer: 5
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[1, 2]; // answer: 6
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[1, 3]; // answer: 7
                }
            }
            else if (trunk == 3)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[2, 0]; // answer: 5
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[2, 1]; // answer: 6
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[2, 2]; // answer: 7
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[2, 3]; // answer: 8
                }
            }
            else if (trunk == 4)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[3, 0]; // answer: 6
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[3, 1]; // answer: 7
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[3, 2]; // answer: 8
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[3, 3]; // answer: 9
                }
            }
            else if (trunk == 5)
            {
                if (legs == 1)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[4, 0]; // answer: 7
                }
                else if (legs == 2)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[4, 1]; // answer: 8
                }
                else if (legs == 3)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[4, 2]; // answer: 9
                }
                else if (legs == 4)
                {
                    return REBA_Score_Neck_Trunk_Legs_3[4, 3]; // answer: 9
                }
            }
        }

        return 0;
    }

    public static int REBA_Score_Upper_Lower_Arm_Wrist(int upperArm, int lowerArm, int wrist)
    {
        int[,] REBA_Score_Upper_Lower_Arm_1 = new int[2, 3] { { 1, 2, 2 }, { 1, 2, 3 } };
        int[,] REBA_Score_Upper_Lower_Arm_2 = new int[2, 3] { { 1, 2, 3 }, { 2, 3, 4 } };
        int[,] REBA_Score_Upper_Lower_Arm_3 = new int[2, 3] { { 3, 4, 5 }, { 4, 5, 5 } };
        int[,] REBA_Score_Upper_Lower_Arm_4 = new int[2, 3] { { 4, 5, 5 }, { 5, 6, 7 } };
        int[,] REBA_Score_Upper_Lower_Arm_5 = new int[2, 3] { { 6, 7, 8 }, { 7, 8, 8 } };
        int[,] REBA_Score_Upper_Lower_Arm_6 = new int[2, 3] { { 7, 8, 8 }, { 8, 9, 9 } };

        if (upperArm == 1)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_1[0, 0]; // answer: 1
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_1[0, 1]; // answer: 2
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_1[0, 2]; // answer: 2
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_1[1, 0]; // answer: 1
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_1[1, 1]; // answer: 2
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_1[1, 2]; // answer: 3
                }
            }
        }
        else if (upperArm == 2)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_2[0, 0]; // answer: 1
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_2[0, 1]; // answer: 2
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_2[0, 2]; // answer: 3
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_2[1, 0]; // answer: 2
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_2[1, 1]; // answer: 3
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_2[1, 2]; // answer: 4
                }
            }
        }
        else if (upperArm == 3)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_3[0, 0]; // answer: 3
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_3[0, 1]; // answer: 4
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_3[0, 2]; // answer: 5
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_3[1, 0]; // answer: 4
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_3[1, 1]; // answer: 5
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_3[1, 2]; // answer: 5
                }
            }
        }
        else if (upperArm == 4)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_4[0, 0]; // answer: 4
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_4[0, 1]; // answer: 5
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_4[0, 2]; // answer: 5
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_4[1, 0]; // answer: 5
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_4[1, 1]; // answer: 6
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_4[1, 2]; // answer: 7
                }
            }
        }
        else if (upperArm == 5)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_5[0, 0]; // answer: 6
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_5[0, 1]; // answer: 7
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_5[0, 2]; // answer: 8
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_5[1, 0]; // answer: 7
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_5[1, 1]; // answer: 8
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_5[1, 2]; // answer: 8
                }
            }
        }
        else if (upperArm == 6)
        {
            if (lowerArm == 1)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_6[0, 0]; // answer: 7
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_6[0, 1]; // answer: 8
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_6[0, 2]; // answer: 8
                }
            }
            else if (lowerArm == 2)
            {
                if (wrist == 1)
                {
                    return REBA_Score_Upper_Lower_Arm_6[1, 0]; // answer: 8
                }
                else if (wrist == 2)
                {
                    return REBA_Score_Upper_Lower_Arm_6[1, 1]; // answer: 9
                }
                else if (wrist == 3)
                {
                    return REBA_Score_Upper_Lower_Arm_6[1, 2]; // answer: 9
                }
            }
        }
        return 0;
    }

    public static int REBA_Score(int postureScoreA, int postureScoreB, int load, int couplingSource)
    {
        int[] scoreA_1 = new int[12] { 1, 1, 1, 2, 3, 3, 4, 5, 6, 7, 7, 7 };
        int[] scoreA_2 = new int[12] { 1, 2, 2, 3, 4, 4, 5, 6, 6, 7, 7, 8 };
        int[] scoreA_3 = new int[12] { 2, 3, 3, 3, 4, 5, 6, 7, 7, 8, 8, 8 };
        int[] scoreA_4 = new int[12] { 3, 4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9 };
        int[] scoreA_5 = new int[12] { 4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9, 9 };
        int[] scoreA_6 = new int[12] { 6, 6, 6, 7, 8, 8, 9, 9, 10, 10, 10, 10 };
        int[] scoreA_7 = new int[12] { 7, 7, 7, 8, 9, 9, 9, 10, 10, 11, 11, 11 };
        int[] scoreA_8 = new int[12] { 8, 8, 8, 9, 10, 10, 10, 10, 10, 11, 11, 11 };
        int[] scoreA_9 = new int[12] { 9, 9, 9, 10, 10, 10, 11, 11, 11, 12, 12, 12 };
        int[] scoreA_10 = new int[12] { 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 12 };
        int[] scoreA_11 = new int[12] { 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12 };
        int[] scoreA_12 = new int[12] { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 };

        if ((postureScoreA + load) == 1)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_1[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_1[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_1[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_1[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_1[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_1[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_1[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_1[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_1[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_1[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_1[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_1[11];
            }
        }
        else if ((postureScoreA + load) == 2)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_2[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_2[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_2[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_2[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_2[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_2[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_2[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_2[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_2[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_2[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_2[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_2[11];
            }
        }
        else if ((postureScoreA + load) == 3)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_3[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_3[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_3[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_3[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_3[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_3[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_3[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_3[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_3[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_3[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_3[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_3[11];
            }
        }
        else if ((postureScoreA + load) == 4)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_4[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_4[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_4[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_4[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_4[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_4[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_4[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_4[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_4[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_4[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_4[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_4[11];
            }
        }
        else if ((postureScoreA + load) == 5)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_5[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_5[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_5[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_5[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_5[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_5[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_5[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_5[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_5[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_5[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_5[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_5[11];
            }
        }
        else if ((postureScoreA + load) == 6)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_6[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_6[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_6[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_6[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_6[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_6[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_6[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_6[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_6[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_6[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_6[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_6[11];
            }
        }

        else if ((postureScoreA + load) == 7)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_7[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_7[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_7[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_7[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_7[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_7[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_7[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_7[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_7[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_7[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_7[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_7[11];
            }
        }
        else if ((postureScoreA + load) == 8)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_8[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_8[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_8[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_8[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_8[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_8[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_8[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_8[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_8[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_8[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_8[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_8[11];
            }
        }
        else if ((postureScoreA + load) == 9)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_9[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_9[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_9[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_9[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_9[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_9[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_9[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_9[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_9[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_9[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_9[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_9[11];
            }
        }
        else if ((postureScoreA + load) == 10)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_10[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_10[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_10[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_10[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_10[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_10[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_10[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_10[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_10[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_10[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_10[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_10[11];
            }
        }
        else if ((postureScoreA + load) == 11)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_11[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_11[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_11[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_11[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_11[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_11[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_11[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_11[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_11[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_11[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_11[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_10[11];
            }
        }
        else if ((postureScoreA + load) == 12)
        {
            if ((postureScoreB + couplingSource == 1))
            {
                return scoreA_12[0];
            }
            else if ((postureScoreB + couplingSource == 2))
            {
                return scoreA_12[1];
            }
            else if ((postureScoreB + couplingSource == 3))
            {
                return scoreA_12[2];
            }
            else if ((postureScoreB + couplingSource == 4))
            {
                return scoreA_12[3];
            }
            else if ((postureScoreB + couplingSource == 5))
            {
                return scoreA_12[4];
            }
            else if ((postureScoreB + couplingSource == 6))
            {
                return scoreA_12[5];
            }
            else if ((postureScoreB + couplingSource == 7))
            {
                return scoreA_12[6];
            }
            else if ((postureScoreB + couplingSource == 8))
            {
                return scoreA_12[7];
            }
            else if ((postureScoreB + couplingSource == 9))
            {
                return scoreA_12[8];
            }
            else if ((postureScoreB + couplingSource == 10))
            {
                return scoreA_12[9];
            }
            else if ((postureScoreB + couplingSource == 11))
            {
                return scoreA_12[10];
            }
            else if ((postureScoreB + couplingSource == 12))
            {
                return scoreA_12[11];
            }
        }
        return 0;
    }

    public static String REBA_Score_Feedback(int rebaScore)
    {
        String feedback;
        if (rebaScore == 1)
        {
           feedback= "Result: " + rebaScore + "\n" +  "// negligible risk, no action required";
            return feedback;
        }
        else if (rebaScore == 2 || rebaScore == 3)
        {
            feedback = "Result: " + rebaScore + "\n" + " low risk, change may be needed";
            return feedback;

        }
        else if (rebaScore == 4 || rebaScore == 5 || rebaScore == 6 || rebaScore == 7)
        {
            feedback= "Result: " + rebaScore + "\n" + " // medium risk, futher investigation, change soon";
            return feedback;

        }
        else if (rebaScore == 8 || rebaScore == 9 || rebaScore == 10)
        {
            feedback= "Result: " + rebaScore + "\n" + " // high risk, investigate and implement change";
            return feedback;

        }
        else if (rebaScore >= 11)
        {
            feedback= "Result: " + rebaScore + "\n" + "// very high risk, implement change";
            return feedback;

        }
        else
        {
            return "";
        }
    }
}

