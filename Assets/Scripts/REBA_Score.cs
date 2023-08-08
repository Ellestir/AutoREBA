using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class REBA_Score : MonoBehaviour
{
    [Tooltip("Logs the angles of the dictionary to the console")]
    public bool LogAnglesConsole;
    [Tooltip("Logs the limb and table scores to a CSV-File")]
    public bool LogScoresCSV;
    [Tooltip("Logs the scores of limbs and tables to the console")]
    public bool LogScoresToConsole;
    //"What is sided or bend" determines at which angle the condition is met 
    [Tooltip("Determines the threshold at which angle condittions like sided, twisted or bend are met")]
    public int threshold;
    [Tooltip("The size of the rolling window for averaging")]
    public int windowSize = 5; // Default size, but can be adjusted in Unity's inspector
    private Queue<float> lastValues = new Queue<float>();
    float smooth = 0;
    //Score Attributes
    public static int Score;
    //Bones from Skeleton
    public Transform neck;
    public Transform head;
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
    public Transform Spine2;
    public Transform Spine4;
    // REBA-Tables from PDF
    public int[,,] tableA;
    public int[,,] tableB;
    public int[,] tableC;
    // Scores 
    public Dictionary<string, float> body;
    public Dictionary<string, float> arms;
    Quaternion averageTrunkRotation;
    Vector3 TrunkEulerRotation;
    Quaternion averageNeckRotation;
    Vector3 NeckEulerRotation;    
    private float previousSmoothedScore = 0f; // To store the previously smoothed REBA score.

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
        { "upper_right_arm_angle", 0},
        { "upper_left_arm_angle", 0},
        { "shoulder_raised", 0},
        { "right_arm_abducted", 0},
        { "left_arm_abducted", 0},
        { "leaning", 0},
        { "lower_right_arm_angle", 0},
        { "lower_left_arm_angle", 0},
        { "right_wrist_angle", 0},
        { "left_wrist_angle", 0},
        { "right_wrist_twisted", 0},
        { "right_wrist_bent", 0},
        { "left_wrist_twisted", 0},
        { "left_wrist_bent", 0},
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
        // Calculate the angles for table A
        // angle of neck
        averageNeckRotation = Quaternion.Slerp(Quaternion.Euler(neck.rotation.eulerAngles), Quaternion.Euler(head.rotation.eulerAngles), 0.5f);
        NeckEulerRotation = averageNeckRotation.eulerAngles;
        body["neck_angle"] = NeckEulerRotation.x;
        // if neck is side bending
        if (NeckEulerRotation.z > threshold || NeckEulerRotation.z < (360 - threshold))       
        {
            body["neck_side"] = 0; // Neck is inside the threshold            
        }
        else
        {
            body["neck_side"] = 1; // Neck is outside the threshold
        }

        // if neck is twisted
        if (NeckEulerRotation.y <= threshold || NeckEulerRotation.y >= (330 - threshold)) 
        {
            body["neck_twisted"] = 0; // Neck is inside the threshold
            
        }
        else
        {
            body["neck_twisted"] = 1; // Neck is outside the threshold
        }
        Debug.Log("Neck Z-Rotation: "+ NeckEulerRotation.z);
        Debug.Log("Neck Y-Rotation: "+ NeckEulerRotation.y);
        //Debug.Log("Neck Angle: " + NeckEulerRotation.x);
        //Debug.Log("Neck Side Angle: " + NeckEulerRotation.y);
        //Debug.Log("Neck Twist Angle: " + NeckEulerRotation.z);
        // angle of trunk
        // get average angle of all spines
        averageTrunkRotation = Quaternion.Slerp(Quaternion.Euler(hip.rotation.eulerAngles), Quaternion.Euler(Spine2.rotation.eulerAngles), 0.5f);
        TrunkEulerRotation = averageTrunkRotation.eulerAngles;
        //float SpineAverageX = (hip.localEulerAngles.x  + Spine2.localEulerAngles.x ) / 2;
        body["trunk_angle"] = TrunkEulerRotation.x;
        //Debug.Log("Spine Average: " + body["trunk_angle"]);
        //Debug.Log("Spine Sided Average: " + TrunkEulerRotation.z);
        //Debug.Log("Spine twisted Average: " + TrunkEulerRotation.y);
        // if trunk is side bending
        if(TrunkEulerRotation.z > threshold || TrunkEulerRotation.z < (360 - threshold)){
            body["trunk_side"] = 1;
        }else{
            body["trunk_side"] = 0;
        }
        if(TrunkEulerRotation.z < threshold || TrunkEulerRotation.z > (360 - threshold)){
            body["trunk_side"] = 0;
            //angle within threshold
        }else{
            body["trunk_side"] = 1;
        }
        Debug.Log("Trunk Z-Angle: "+ TrunkEulerRotation.z);
        // if trunk is twisted
        //250 because the initial angle is 275 when standing straight
        if(TrunkEulerRotation.y < (275 + threshold) || TrunkEulerRotation.y < (275 - threshold)){
            body["trunk_twisted"] = 0;
        }else{
            body["trunk_twisted"] = 1;
        }
        Debug.Log("Trunk Y-Angle: "+ TrunkEulerRotation.y);

        // angle of legs
        float leftLegAngle = Vector3.Angle(upperLeftLeg.up, lowerLeftLeg.up);
        float rightLegAngle = Vector3.Angle(upperRightLeg.up, lowerRightLeg.up);
        Debug.Log("left Leg Angle: " + leftLegAngle);
        Debug.Log("right Leg Angle: " + rightLegAngle);

        if(leftLegAngle > rightLegAngle) {
             body["legs_angle"] = leftLegAngle;
        }
        else {
             body["legs_angle"] = rightLegAngle;
        }


        if (GroundCheckLeft.LeftFootGrounded && GroundCheckRight.RightFootGrounded)
        {
            body["legs_walking"] = 0;
        }
        else
        {
            body["legs_walking"] = 1;
        }
        // Calculate the angles for table B

        // angle of upper arm
        arms["upper_right_arm_angle"] = upperRightArm.localEulerAngles.y;     
        arms["upper_left_arm_angle"] = upperLeftArm.localRotation.y;
        
        // if upper arm is abducted
        // right and left shoulder 
        float leftArmAbduction = Vector3.Angle(Spine4.up, upperLeftArm.up);
        Debug.Log("XXXLeft Arm Abduction: " + leftArmAbduction);
        float rightArmAbduction = Vector3.Angle(Spine4.up, upperRightArm.up);
        Debug.Log("XXXRight Arm Abduction: " + rightArmAbduction);

        if(rightArmAbduction < (160 - threshold)) {
            arms["right_arm_abducted"] = 1;
        }
        else
        {
            arms["right_arm_abducted"] = 0;
        }
        if(leftArmAbduction < (160 - threshold)) {
            arms["left_arm_abducted"] = 1;
        }
        else
        {
            arms["left_arm_abducted"] = 0;
        }
        // if shoulder is raised
        // difficult to compute, because the shoulder does not really change its possition in avatar
        if(rightShoulder.localPosition.y > 1 || leftShoulder.localPosition.y > 1) {
            arms["shoulder_raised"] = 1;
        }
        else {
             arms["shoulder_raised"] = 0;
        }

        // angle of lower arm
        arms["lower_right_arm_angle"] = lowerRightArm.localEulerAngles.x;     
        arms["lower_left_arm_angle"] = lowerLeftArm.localRotation.x;

        // angle of both wrists
        arms["right_wrist_angel"] = rightHand.localEulerAngles.x;
        arms["right_wrist_angel"] = rightHand.localEulerAngles.x;
        //check both wrist if twisted or bend 
        // if wrist is twisted
        if(rightHand.localEulerAngles.y != (270 + threshold) || rightHand.localEulerAngles.y != (270 - threshold)) {
             arms["right_wrist_twisted"] = 1;
        }
        else {
             arms["right_wrist_twisted"] = 0;
        }
        if(leftHand.localEulerAngles.y != (90 + threshold) || leftHand.localEulerAngles.y != (90 - threshold)){
            arms["left_wrist_twisted"] = 0;
        }else{
            arms["left_wrist_twisted"] = 1;
        }

        // if wrist is bent
        if((rightHand.localEulerAngles.z != (0 + threshold) ) || rightHand.localEulerAngles.z != (360 - threshold)) {
             arms["right_wrist_bent"] = 1;
        }
        else {
             arms["right_wrist_bent"] = 0;
        }

        if(leftHand.localEulerAngles.z != (0+ threshold) ||leftHand.localEulerAngles.z != (360 - threshold)){
            arms["left_wrist_bent"] = 1;
        }else{
            arms["left_wrist_bent"] = 0;
        }

        if (LeftForeArmCollider.LeftForeArmCollision || RightForearmCollider.RightForearmCollison || BackSupport.BackSupported)
        {
            arms["leaning"] = 1;
        }
        else
        {
            arms["leaning"] = 0;
        }

        // score calculations
        var result_sore_a = ComputeScoreA();
        var result_sore_b = ComputeScoreB();
        var result_sore_c = ComputeScoreC(result_sore_a.Item1, result_sore_b.Item1);
        int reba_score = ScoreCTo5Classes(result_sore_c.Item1);
        Score = AverageScore(result_sore_c.Item1);
        Debug.Log("Score A: " + result_sore_a.Item1);
        Debug.Log("REBA-Score: " + result_sore_c.Item1);
        Debug.Log("Smooth REBA-Score: " + Score);

        if (LogAnglesConsole) {
            Debug.Log("Neck position: " + body["neck_angle"]);
            Debug.Log("Neck sided: " + body["neck_side"]);
            Debug.Log("Neck twisted: " + body["neck_twisted"]);

            Debug.Log("Trunk position: " + body["trunk_angle"]);
            Debug.Log("Trunk sided: " + body["trunk_side"]);
            Debug.Log("Trunk twisted: " + body["trunk_twisted"]);
            
            Debug.Log("Upper right arm position: " + upperRightArm.localEulerAngles.y);
            Debug.Log("Upper left arm position: " + upperLeftArm.localEulerAngles.y);
            Debug.Log("Upper right arm abduction: " + upperRightArm.localEulerAngles.x);
            Debug.Log("Upper left arm abduction: " + upperLeftArm.localEulerAngles.x);

            Debug.Log("lower right arm position: " + lowerRightArm.localEulerAngles.x);
            Debug.Log("lower left arm position: " + lowerLeftArm.localEulerAngles.x);
            Debug.Log("Leaning: " + arms["leaning"]);
            Debug.Log("right wrist position: " + rightHand.localEulerAngles.x);
            Debug.Log("left wrist position: " + leftHand.localEulerAngles.x);
            Debug.Log("right wrist twist: " + rightHand.localEulerAngles.y);
            Debug.Log("left wrist twist: " + leftHand.localEulerAngles.y);
            Debug.Log("right wrist bend: " + rightHand.localEulerAngles.z);
            Debug.Log("left wrist bend: " + leftHand.localEulerAngles.z);

            Debug.Log("Right leg position: " + upperLeftLeg.localEulerAngles.x);
            Debug.Log("Left leg position): " + upperRightLeg.localEulerAngles.x);
            if(body["legs_walking"] == 1)
            {
                Debug.Log("Is not Grounded");
            }
            else
            {
                Debug.Log("Is Grounded");                
            }           
        }
       
        if (LogScoresCSV)
        {
            LogToCSV(result_sore_a.Item2[0],result_sore_a.Item2[1], result_sore_a.Item2[2], result_sore_a.Item1, result_sore_b.Item2[0], result_sore_b.Item2[1], result_sore_b.Item2[2],result_sore_b.Item1,result_sore_c.Item1);
        }

        if(LogScoresToConsole){
            //Log the table Scores to console
            Debug.Log("Neck Score: " + result_sore_a.Item2[0]);
            Debug.Log("Trunk Score: " + result_sore_a.Item2[1]);
            Debug.Log("Leg Score: " + result_sore_a.Item2[2]);

            Debug.Log("Upper Arm Score: " + result_sore_b.Item2[0]);
            Debug.Log("Lower Arm Score: " + result_sore_b.Item2[1]);
            Debug.Log("Wrist Score: " + result_sore_b.Item2[2]);

        }


    }

    public (int, int[]) ComputeScoreA()
    {
        int neckScore = 0, trunkScore = 0, legScore = 0, loadScore = 0;

        // Neck position
        if (body["neck_angle"] <= 20 || 340 <= body["neck_angle"])
            neckScore += 1;
        else
            neckScore += 2;

        // Neck adjust
        neckScore += body["neck_side"] != 0 ? 1 : 0;
        neckScore += body["neck_twisted"] != 0 ? 1 : 0;

        // Trunk position
        if ((0 <= body["trunk_angle"] && body["trunk_angle"] < 1)|| body["trunk_angle"] >= 359)
        {
            trunkScore += 1;
            Debug.Log("Trunk +1");
        }            
        else if ((body["trunk_angle"] <= 20)|| body["trunk_angle"] >= 339)
        {
            trunkScore += 2;
            Debug.Log("Trunk +2");
        }           
        else if ((body["trunk_angle"] <= 60)|| body["trunk_angle"] >= 300)
        {
            trunkScore += 3;
            Debug.Log("Trunk +3");
        }            
        else if ((body["trunk_angle"] > 60) && body["trunk_angle"] < 300){
            trunkScore += 4;
            Debug.Log("Trunk +4");
        }
            

        // Trunk adjust
        if(body["trunk_side"] == 1){
            trunkScore++;
            Debug.Log("Trunk Sided ++");
        }
        if(body["trunk_twisted"] == 1){
            trunkScore++;
            Debug.Log("Trunk Twisted ++");
        }
        // Legs position
        legScore += body["legs_walking"] == 1 ? 2 : 1;

        // Legs adjust
        if (((30 <= body["legs_angle"] && body["legs_angle"] <= 60)) ){
            legScore += 1; Debug.Log("Leg ++");
        }else if( body["legs_angle"] < 30){

        }
        else if (60 < body["legs_angle"] || body["legs_angle"] > 300 ){
            legScore += 2;Debug.Log("Leg +2");
        }

        // Load
        if (5 <= body["load"] && body["load"] <= 10)
            loadScore += 1;
        else if (body["load"] > 10)
            loadScore += 2;

        if (neckScore <= 0 || trunkScore <= 0 || legScore <= 0) { 
            throw new InvalidOperationException("Neck score, trunk score and leg score must all be greater than zero");
        }
        //if neck is 4 -> Index out of bounds, because REBA-PDF Table A shows max neck-score = 3 
        if(neckScore > 3)
        {
            Debug.Log("Reset neck Score from: " + neckScore);
            neckScore =  3;
        }
        if(trunkScore > 5)
        {
            Debug.Log("Reset trunk Score from: " + trunkScore);
            trunkScore = 5;
        }
        if(legScore > 4)
        {
            Debug.Log("Reset leg Score from: " + legScore);
            legScore = 4;
        }
        Debug.Log("NeckScore at End: " + neckScore);
        Debug.Log("TrunkScore at End: " + trunkScore);
        Debug.Log("LegScore at End: " + legScore);
        int scoreA = tableA[(neckScore - 1), (trunkScore - 1), (legScore - 1)];
        return (scoreA, new int[] { neckScore, trunkScore, legScore });
    }
    public (int, int[]) ComputeScoreB()
    {
        int upperArmScore = 0, lowerArmScore = 0, wristScore = 0, lowerLeftArmScore = 0, lowerRightArmScore = 0, rightWristScore = 0, leftWristScore = 0;

        int upperLeftArm = 0;
        //calculate left Arm
        if((340 <= arms["upper_left_arm_angle"] && arms["upper_left_arm_angle"] <360) || (0 <= arms["upper_left_arm_angle"] && arms["upper_left_arm_angle"] <= 20))
        {   
            upperLeftArm++;
        }else if((arms["upper_left_arm_angle"] < 340) || (20 < arms["upper_left_arm_angle"] && arms["upper_left_arm_angle"] <= 45))
        {
            upperLeftArm += 2;
        }else if (45 < arms["upper_left_arm_angle"] && arms["upper_left_arm_angle"] <= 90)
        {
            upperLeftArm += 3;
        }else if (90 < arms["upper_left_arm_angle"] )
        {
            upperLeftArm += 4;
        }
        if (arms["left_arm_abducted"]==1)
        upperLeftArm++;

        int upperRightArm = 0;
        //calculate right Arm
        if ((340 <= arms["upper_right_arm_angle"] && arms["upper_right_arm_angle"] <= 360) || (0 <= arms["upper_right_arm_angle"] && arms["upper_right_arm_angle"] <= 20))
        {
            upperRightArm++;
        }
        else if ((20 < arms["upper_right_arm_angle"]) || (315 < arms["upper_right_arm_angle"] && arms["upper_right_arm_angle"] <= 340))
        {
            upperRightArm += 2;
        }
        else if (270 <= arms["upper_right_arm_angle"] && arms["upper_right_arm_angle"] <= 315)
        {
            upperRightArm += 3;
        }else if (270 > arms["upper_right_arm_angle"])
        {
            upperRightArm += 4;
        }
        if (arms["right_arm_abducted"]==1)
            upperRightArm++;

        // use higher score
        if(upperLeftArm <= upperRightArm)
        {
            upperArmScore = upperRightArm;
        }
        else
        {
            upperArmScore = upperLeftArm;
        }

        // Upper arm adjust
        if (arms["shoulder_raised"] > 0)
            upperArmScore++;
        
        if (arms["leaning"]==1)
            upperArmScore--;

        //Calculate lower right arm 
        if ((60 <= arms["lower_right_arm_angle"] && arms["lower_right_arm_angle"] <= 100) || (260 <= arms["lower_right_arm_angle"] && arms["lower_right_arm_angle"] <= 300))
            lowerRightArmScore++;
        else
            lowerRightArmScore += 2;
        //Calculate lower left arm
        if ((60 <= arms["lower_left_arm_angle"] && arms["lower_left_arm_angle"] <= 100) || (260 <= arms["lower_left_arm_angle"] && arms["lower_left_arm_angle"] <= 300))
            lowerLeftArmScore++;
        else
            lowerLeftArmScore += 2;

         if(lowerRightArmScore <= lowerLeftArmScore)
         {
            lowerArmScore = lowerLeftArmScore;
         }else{
            lowerArmScore = lowerRightArmScore;
         }    

    
        // Right Wrist position
        if ((0 <= arms["right_wrist_angle"] && arms["right_wrist_angle"] <= 15) || (345 <= arms["right_wrist_angle"]  && arms["right_wrist_angle"] <= 360))
            rightWristScore++;
        else
            rightWristScore += 2;
       
        // right Wrist adjust
        if ((arms["right_wrist_twisted"]==1) || (arms["right_wrist_bent"] == 1))
            rightWristScore++;

        // left Wrist position
        if ((0 <= arms["left_wrist_angle"] && arms["left_wrist_angle"] <= 15) || (345 <= arms["left_wrist_angle"]  && arms["left_wrist_angle"] <= 360))
            leftWristScore++;
        else
            leftWristScore += 2;
       
        // left Wrist adjust
        if ((arms["left_wrist_twisted"]==1) || (arms["left_wrist_bent"] == 1))
            leftWristScore++;
        //use worse score
    	if(rightWristScore > leftWristScore){
            wristScore = rightWristScore;
        }else{
            wristScore = leftWristScore;
        }
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
    public void LogToCSV(int neck, int trunk, int leg, int tableA, int upperArm, int lowerArm, int wrist, int tabeleB, int reba)
    {
        string filePath = Application.dataPath + "/../Logs/AutoREBALogFile.csv";

        if (!File.Exists(filePath))
        {
            string header = "Timestamp, Neck-Score, Trunk-Score, Leg-Score, Table A, Upper-Arm-Score, Lower-Arm-Score, Wrist-Score, Table B, REBA-Score";
            File.WriteAllText(filePath, header + "\n");
        }

        string time = System.DateTime.Now.Ticks.ToString();
        string result = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\n", time, neck, trunk, leg, tableA, upperArm, lowerArm, wrist, tabeleB, reba);
        File.AppendAllText(filePath, result);
    }

    public int AverageScore(float currentREBAScore)
    {
        lastValues.Enqueue(currentREBAScore);        
        smooth += currentREBAScore; 
        // If the window size is exceeded, remove the oldest value
        if (lastValues.Count > windowSize)
        {
            smooth -= lastValues.Dequeue();
        }   
    
        return (int)Math.Ceiling(smooth/lastValues.Count);
    }
}
