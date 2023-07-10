//#define LogAngles

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_Score : MonoBehaviour
{
    public Transform neck;
    public Transform torso;
    public Transform upperRightLeg;
    public Transform upperLeftLeg;
    public Transform lowerRightLeg;
    public Transform lowerLeftLeg;
    public Transform hip;
    public Transform upperRightArm;
    public Transform upperLeftArm; 
    public Transform lowerRightArm;
    public Transform lowerLeftArm; 
    public Transform rightHand; 
    public Transform leftHand; 
    public Transform rightShoulder; 
    public Transform leftShoulder; 
    public int[,,] tableA;
    public int[,,] tableB;
    public int[,] tableC;
    public Dictionary<string, float> body;
    public Dictionary<string, float> arms;
    public REBAScoreHUD rebaScoreHUD;

    void Start()
    {
        body = new Dictionary<string, float>(){
        { "neck_angle", 0},
        { "neck_side", 0},
        { "neck_twisted", 0},
        { "trunk_angle", 0},
        { "trunk_side", 0},
        { "trunk_twisted", 0},
        { "legs_walking", 0},
        { "legs_angle", 0},
        { "load", 0}
        };

        arms = new Dictionary<string, float>(){
        { "upper_arm_angle", 0},
        { "shoulder_raised", 0},
        { "arm_abducted", 0},
        { "leaning", 0},
        { "lower_arm_angle", 0},
        { "wrist_angle", 0},
        { "wrist_twisted", 0},
        { "wrist_bent", 0},
        };

        tableA = new int[,,] {
            { {1, 2, 3, 4}, {2, 3, 4, 5}, {2, 4, 5, 6}, {3, 5, 6, 7}, {4, 6, 7, 8} },
            { {1, 2, 3, 4}, {3, 4, 5, 6}, {4, 5, 6, 7}, {5, 6, 7, 8}, {6, 7, 8, 9} },
            { {3, 3, 5, 6}, {4, 5, 6, 7}, {5, 6, 7, 8}, {6, 7, 8, 9}, {7, 8, 9, 9} }
        };

        tableB = new int[,,] {
            {{1, 2, 2}, {1, 2, 3}},
            {{1, 2, 3}, {2, 3, 4}},
            {{3, 4, 5}, {4, 5, 5}},
            {{4, 5, 5}, {5, 6, 7}},
            {{6, 7, 8}, {7, 8, 8}},
            {{7, 8, 8}, {8, 9, 9}}
        };

        tableC = new int[,] {
        {1, 1, 1, 2, 3, 3, 4, 5, 6, 7, 7, 7},
        {1, 2, 2, 3, 4, 4, 5, 6, 6, 7, 7, 8},
        {2, 3, 3, 3, 4, 5, 6, 7, 7, 8, 8, 8},
        {3, 4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9},
        {4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9, 9},
        {6, 6, 6, 7, 8, 8, 9, 9, 10, 10, 10, 10},
        {7, 7, 7, 8, 9, 9, 9, 10, 10, 11, 11, 11},
        {8, 8, 8, 9, 10, 10, 10, 10, 10, 11, 11, 11},
        {9, 9, 9, 10, 10, 10, 11, 11, 11, 12, 12, 12},
        {10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 12},
        {11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12},
        {12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12},
        };
    }


    // Update is called once per frame
    void Update()
    {   
        // REBA description: https://hci-studies.org/methods-and-measures/downloads/REBA-Guide-v-5.0.pdf

        // Get the rotation quaternion between two objects for Table A
        Quaternion qNeckTorso = Quaternion.Inverse(neck.rotation) * torso.rotation;
        Quaternion qHipTorso = Quaternion.Inverse(hip.rotation) * torso.rotation;
        Quaternion qUpperRightLegLowerRightLeg = Quaternion.Inverse(upperRightLeg.rotation) * lowerRightLeg.rotation;
        Quaternion qUpperLeftLegLowerLeftLeg = Quaternion.Inverse(upperLeftLeg.rotation) * lowerLeftLeg.rotation;
        Quaternion qUpperRightArmTorso = Quaternion.Inverse(upperRightArm.rotation) * torso.rotation;
        Quaternion qUpperLeftArmTorso = Quaternion.Inverse(upperLeftArm.rotation) * torso.rotation;
        Quaternion qLowerRightArmUpperRightArm = Quaternion.Inverse(lowerRightArm.rotation) * upperRightArm.rotation;
        Quaternion qLowerLeftArmUpperLeftArm = Quaternion.Inverse(lowerLeftArm.rotation) * upperLeftArm.rotation;
        Quaternion qRightHandLowerRightArm = Quaternion.Inverse(rightHand.rotation) * lowerRightArm.rotation;
        Quaternion qLeftHandLowerLeftArm = Quaternion.Inverse(leftHand.rotation) * lowerLeftArm.rotation;

        // Calculate the angles for table A

        // angle of neck
        float angleNeck = Quaternion.Angle(qNeckTorso, Quaternion.identity);
        body["neck_angle"] = angleNeck;
        // if neck is side bending
        body["neck_side"] = neck.localRotation.eulerAngles.z;
        // if neck is twisted
        body["neck_twisted"] = neck.localRotation.eulerAngles.y;

        // angle of trunk
        float angleTrunk = Quaternion.Angle(qHipTorso, Quaternion.identity);
        body["trunk_angle"] = angleTrunk;
        // if trunk is side bending
        body["trunk_side"] = torso.localRotation.eulerAngles.z;
        // if trunk is twisted
        body["trunk_twisted"]  = torso.localRotation.eulerAngles.y;

        // angle of legs
        float angleRightLeg = Quaternion.Angle(qUpperRightLegLowerRightLeg, Quaternion.identity);
        float angleLeftLeg = Quaternion.Angle(qUpperLeftLegLowerLeftLeg, Quaternion.identity);
        if(Mathf.Abs(angleRightLeg) > Mathf.Abs(angleLeftLeg)) {
             body["legs_angle"] = angleRightLeg;
        }
        else {
             body["legs_angle"] = angleLeftLeg;
        }  

        // Calculate the angles for table B

        // angle of upper arm
        float angleUpperRightArm = Quaternion.Angle(qUpperRightArmTorso, Quaternion.identity);
        float angleUpperLeftArm = Quaternion.Angle(qUpperLeftArmTorso, Quaternion.identity);
        if(Mathf.Abs(angleUpperRightArm) > Mathf.Abs(angleUpperLeftArm)) {
             arms["upper_arm_angle"] = angleUpperRightArm;
        }
        else {
             arms["upper_arm_angle"] = angleUpperLeftArm;
        }
        // if upper arm is abducted
        float upperRightArmAbducted = upperRightArm.localRotation.eulerAngles.y;
        float upperLeftArmAbducted = upperLeftArm.localRotation.eulerAngles.y;
        if(upperRightArmAbducted > upperLeftArmAbducted) {
            arms["arm_abducted"] = upperRightArmAbducted;
        } else {
            arms["arm_abducted"] = upperLeftArmAbducted;
        }
        // if shoulder is raised
        float rightShoulderRaised = rightShoulder.localPosition.x;
        float leftShoulderRaised = leftShoulder.localPosition.x;
        if(rightShoulderRaised > leftShoulderRaised) {
            arms["shoulder_raised"] = rightShoulderRaised;
        }
        else {
             arms["shoulder_raised"] = leftShoulderRaised;
        }
        // TODO: If arm is supported or person is leaning: -1

        // angle of lower arm
        float angleLowerRightArm = Quaternion.Angle(qLowerRightArmUpperRightArm, Quaternion.identity);
        float angleLowerLeftArm = Quaternion.Angle(qLowerLeftArmUpperLeftArm, Quaternion.identity);
         if(Mathf.Abs(angleLowerRightArm) > Mathf.Abs(angleLowerLeftArm)) {
             arms["lower_arm_angle"] = angleLowerRightArm;
        }
        else {
             arms["lower_arm_angle"] = angleLowerLeftArm;
        }

        // angle of wrist
        float angleRightWrist = Quaternion.Angle(qRightHandLowerRightArm, Quaternion.identity);
        float angleLeftWrist = Quaternion.Angle(qLeftHandLowerLeftArm, Quaternion.identity);
        if(Mathf.Abs(angleRightWrist) > Mathf.Abs(angleLeftWrist)) {
             arms["wrist_angle"] = angleRightWrist;
        }
        else {
             arms["wrist_angle"] = angleLeftWrist;
        }

        // if wrist is twisted
        float rightWristTwisted = rightHand.localRotation.eulerAngles.y;
        float leftWristTwisted = leftHand.localRotation.eulerAngles.y;
        if(Mathf.Abs(rightWristTwisted) > Mathf.Abs(leftWristTwisted)) {
             arms["wrist_twisted"] = rightWristTwisted;
        }
        else {
             arms["wrist_twisted"] = leftWristTwisted;
        }
        // if wrist is bent
        float rightWristBent = rightHand.localRotation.eulerAngles.z;
        float leftWristBent = leftHand.localRotation.eulerAngles.z;
        if(Mathf.Abs(rightWristBent) > Mathf.Abs(leftWristBent)) {
             arms["wrist_bent"] = rightWristBent;
        }
        else {
             arms["wrist_bent"] = leftWristBent;
        }

        if (GroundCheckLeft.LeftFootGrounded == true && GroundCheckRight.RightFootGrounded == true)
        {
            body["legs_walking"] = 0;
            Debug.Log("Is Grounded");
        }
        else
        {
            body["legs_walking"] = 1;
            Debug.Log("Is not Grounded");
        }

        if (LeftForeArmCollider.LeftForeArmCollision || RightForearmCollider.RightForearmCollison || BackSupport.BackSupported)
        {
            Debug.Log("Avatar Supported");
            arms["leaning"] = 1;

        }
        else
        {
            arms["leaning"] = 0;
        }

        // TODO: calculation if wrists are twisted (if rotations x = 0 and y = 0, then wrists are not in normal position) 
        // TODO: calculation if upper arms are abducted (if rotations x = 0 and y = 0, then upper arms are not in normal position) 

        // score calculations
        var result_sore_a = ComputeScoreA();
        var result_sore_b = ComputeScoreB();
        var result_sore_c = ComputeScoreC(result_sore_a.Item1, result_sore_b.Item1);
        int reba_score = ScoreCTo5Classes(result_sore_c.Item1);
        //UpdateHUD(reba_score);
        Debug.Log("Leg grounded?: " + body["legs_walking"]);
        Debug.Log("Score A: " + result_sore_a.Item1);
        Debug.Log("REBA-Score: " + result_sore_c.Item1);

#if LogAngles
        // Print the angle
        Debug.Log("Angle between neck and torso (neck position): " + angleNeck);
        Debug.Log("Angle between hip and torso (trunk position): " + angleTrunk);
        Debug.Log("Angle between upper right arm and torso (upper right arm position): " + angleUpperRightArm);
        Debug.Log("Angle between upper left arm and torso (upper left arm position): " + angleUpperLeftArm);
        Debug.Log("Angle between lower right arm and upper right arm (lower right arm position): " + angleLowerRightArm);
        Debug.Log("Angle between lower left arm and upper left arm (lower left arm position): " + angleLowerLeftArm);
        Debug.Log("Angle between right hand and lower right arm (right wrist position): " + angleRightWrist);
        Debug.Log("Angle between left hand and lower left arm (left wrist position): " + angleLeftWrist);
        Debug.Log("Angle between right upper leg and right lower leg (right leg position): " + angleRightLeg);
        Debug.Log("Angle between left upper leg and left lower leg (left leg position): " + angleLeftLeg);
        Debug.Log("Neck sided: " + body["neck_side"]);
        Debug.Log("Neck twisted: " + body["neck_twisted"]);
        Debug.Log("Trunk sided: " + body["trunk_side"]);
        Debug.Log("Trunk twisted: " + body["trunk_twisted"]);
#endif

    }

    private void UpdateHUD(int rebaScore)
    {
        rebaScoreHUD.UpdateScore(rebaScore);
    }
    public (int, int[]) ComputeScoreA()
    {
        int neckScore = 0, trunkScore = 0, legScore = 0, loadScore = 0;

        // Neck position
        if (10 <= body["neck_angle"] && body["neck_angle"] <= 20)
            neckScore += 1;
        else
            neckScore += 2;

        // Neck adjust
        neckScore += body["neck_side"] != 0 ? 1 : 0;
        neckScore += body["neck_twisted"] != 0 ? 1 : 0;

        // Trunk position
        if (0 <= body["trunk_angle"] && body["trunk_angle"] <= 1)
            trunkScore += 1;
        else if (body["trunk_angle"] <= 20)
            trunkScore += 2;
        else if (20 <= body["trunk_angle"] && body["trunk_angle"] <= 60)
            trunkScore += 3;
        else if (body["trunk_angle"] > 60)
            trunkScore += 4;

        // Trunk adjust
        trunkScore += body["trunk_side"] != 180 ? 1 : 0;
        trunkScore += body["trunk_twisted"] != 180 ? 1 : 0;

        // Legs position
        legScore += body["legs_walking"] == 1 ? 2 : 1;

        // Legs adjust
        if (30 <= body["legs_angle"] && body["legs_angle"] <= 60)
            legScore += 1;
        else if (body["legs_angle"] > 60)
            legScore += 2;

        // Load
        if (5 <= body["load"] && body["load"] <= 10)
            loadScore += 1;
        else if (body["load"] > 10)
            loadScore += 2;

        if (neckScore <= 0 || trunkScore <= 0 || legScore <= 0) { 
            throw new InvalidOperationException("Neck score, trunk score and leg score must all be greater than zero");
        }
        else if(neckScore > 3 || trunkScore > 5 || legScore > 4)
        {
            neckScore =  3; trunkScore = 5; legScore = 4;
        }
        int scoreA = tableA[(neckScore - 1), (trunkScore - 1), (legScore - 1)];
        return (scoreA, new int[] { neckScore, trunkScore, legScore });
    }
    public (int, int[]) ComputeScoreB()
    {
        int upperArmScore = 0, lowerArmScore = 0, wristScore = 0;

        // Upper arm position
        if (-20 <= arms["upper_arm_angle"] && arms["upper_arm_angle"] <= 20)
            upperArmScore++;
        else if (arms["upper_arm_angle"] <= 45)
            upperArmScore += 2;
        else if (45 <= arms["upper_arm_angle"] && arms["upper_arm_angle"] <= 90)
            upperArmScore += 3;
        else if (arms["upper_arm_angle"] > 90)
            upperArmScore += 4;

        // Upper arm adjust
        if (arms["shoulder_raised"] > 0)
            upperArmScore++;
        if (arms["arm_abducted"]==1)
            upperArmScore++;
        if (arms["leaning"]==1)
            upperArmScore--;

        // Lower arm position
        if (60 <= arms["lower_arm_angle"] && arms["lower_arm_angle"] <= 100)
            lowerArmScore++;
        else
            lowerArmScore += 2;

        // Wrist position
        if (-15 <= arms["wrist_angle"] && arms["wrist_angle"] <= 15)
            wristScore++;
        else
            wristScore += 2;

        // Wrist adjust
        if (arms["wrist_twisted"]==1)
            wristScore++;

        // Make sure lower arm score and wrist score are both positive
        if (lowerArmScore <= 0 || wristScore <= 0)
            throw new Exception("lowerArmScore and wristScore must be greater than 0");

        int scoreB = tableB[(upperArmScore - 1),(lowerArmScore - 1),(wristScore - 1)];
        return (scoreB, new int[] { upperArmScore, lowerArmScore, wristScore });
    }
    public (int, string) ComputeScoreC(int scoreA, int scoreB)
    {
        string[] rebaScoring = {
        "Negligible Risk",
        "Low Risk. Change may be needed",
        "Medium Risk. Further Investigate. Change Soon",
        "High Risk. Investigate and Implement Change",
        "Very High Risk. Implement Change"
        };

        int scoreC = tableC[(scoreA - 1), (scoreB - 1)];
        int index = ScoreCTo5Classes(scoreC);
        string caption = rebaScoring[index];

        return (scoreC, caption);
    }
    public static int ScoreCTo5Classes(int scoreC)
    {
        int ret;
        if (scoreC == 1)
        {
            ret = 0;
        }
        else if (scoreC >= 2 && scoreC <= 3)
        {
            ret = 1;
        }
        else if (scoreC >= 4 && scoreC <= 7)
        {
            ret = 2;
        }
        else if (scoreC >= 8 && scoreC <= 10)
        {
            ret = 3;
        }
        else
        {
            ret = 4;
        }

        return ret;
    }
}
    
    

/*
public class RebaCalculator
{
    private int[,] tableC; // This needs to be initialized with the same values as the Python version

    public Tuple<int, string> ComputeScoreC(int scoreA, int scoreB)
    {
        string[] rebaScoring = new string[] {
            "Negligible Risk",
            "Low Risk. Change may be needed",
            "Medium Risk. Further Investigate. Change Soon",
            "High Risk. Investigate and Implement Change",
            "Very High Risk. Implement Change"
        };

        int scoreC = this.tableC[scoreA - 1, scoreB - 1];
        int ix = ScoreCTo5Classes(scoreC);
        string caption = rebaScoring[ix];

        return new Tuple<int, string>(scoreC, caption);
    }

    
}
*/