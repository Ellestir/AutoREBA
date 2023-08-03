using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class REBA_Score : MonoBehaviour
{
    public bool LogAnglesConsole;
    public bool LogAnglesCSV;
    public static int Score;
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
    public Transform Spine2;
    public Transform Spine3;
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
        { "upper_right_arm_angle", 0},
        { "upper_left_arm_angle", 0},
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


        
        // Calculate the angles for table A
        // angle of neck

        body["neck_angle"] = neck.localEulerAngles.x;
        // if neck is side bending
        body["neck_side"] = neck.localEulerAngles.z;
        // if neck is twisted
        body["neck_twisted"] = neck.localEulerAngles.y;

        // angle of trunk
        // get average angle of all spines
        float SpineAverage = ((hip.localEulerAngles.x ) + Spine2.localEulerAngles.x + Spine3.localEulerAngles.x + torso.localEulerAngles.x) / 4;
        Debug.Log("Spine Average: " + SpineAverage);
        body["trunk_angle"] = hip.localEulerAngles.x;
        // if trunk is side bending
        body["trunk_side"] = hip.localEulerAngles.z;
        // if trunk is twisted
        body["trunk_twisted"]  = hip.localEulerAngles.y;

        // angle of legs
        if(Mathf.Abs(lowerRightLeg.localEulerAngles.x) > Mathf.Abs(lowerLeftLeg.localEulerAngles.x)) {
             body["legs_angle"] = lowerRightLeg.localEulerAngles.x;
        }
        else {
             body["legs_angle"] = lowerLeftLeg.localEulerAngles.x;
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
        if((upperRightArm.localEulerAngles.x >= 80) && (upperRightArm.localEulerAngles.x <= 90)) {
            arms["arm_abducted"] = 0;
        } else if ((upperLeftArm.localEulerAngles.x >= 270) && (upperLeftArm.localEulerAngles.x <= 290))
        {
            arms["arm_abducted"] = 0;
        }
        else
        {
            arms["arm_abducted"] = 1;
        }
        // if shoulder is raised
        if(rightShoulder.localPosition.y > 1 || leftShoulder.localPosition.y > 1) {
            arms["shoulder_raised"] = 1;
        }
        else {
             arms["shoulder_raised"] = 0;
        }

        // angle of lower arm
        float angleLowerRightArm = (lowerRightArm.localEulerAngles.x);
        float angleLowerLeftArm = lowerLeftArm.localEulerAngles.x;
        if ((angleLowerRightArm >= 240 && angleLowerRightArm <= 280) && (angleLowerLeftArm >= 60 && angleLowerLeftArm <= 100)) {
             //both armes good
            arms["lower_arm_angle"] = angleLowerRightArm;
        }
        else if ((angleLowerRightArm >= 180 && angleLowerRightArm <= 240) && (angleLowerLeftArm >= 0 && angleLowerLeftArm <= 60))
        {   
            //both arms bad
             arms["lower_arm_angle"] = angleLowerLeftArm;
        }
        else if ((angleLowerRightArm >= 240 && angleLowerRightArm <= 280) && (angleLowerLeftArm >= 0 && angleLowerLeftArm <= 60))
        {
            //left arm worse
            arms["lower_arm_angle"] = angleLowerLeftArm;
        }
        else if ((angleLowerRightArm >= 180 && angleLowerRightArm <= 240) && (angleLowerLeftArm >= 60 && angleLowerLeftArm <= 100))
        {
            //right arm worse
            arms["lower_arm_angle"] = angleLowerRightArm;
        }else if(angleLowerLeftArm > 100)
        {   
            //
            arms["lower_arm_angle"] = angleLowerLeftArm;
        }else if(angleLowerRightArm > 280)
        {
            arms["lower_arm_angle"] = angleLowerRightArm;
        }
        else
        {
            arms["lower_arm_angle"] = angleLowerLeftArm;
        }

        // angle of wrist
        if (rightHand.localEulerAngles.x >= leftHand.localEulerAngles.x) {
             arms["wrist_angle"] = rightHand.localEulerAngles.x;
        }
        else {
             arms["wrist_angle"] = leftHand.localEulerAngles.x;
        }

        // if wrist is twisted
        if((rightHand.localEulerAngles.y != 270) || (leftHand.localEulerAngles.y != 90)) {
             arms["wrist_twisted"] = 0;
        }
        else {
             arms["wrist_twisted"] = 1;
        }
        // if wrist is bent
        if((rightHand.localEulerAngles.z != 0) || (leftHand.localEulerAngles.z != 0)) {
             arms["wrist_bent"] = 1;
        }
        else {
             arms["wrist_bent"] = 0;
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
        Score = result_sore_c.Item1;
        UpdateHUD(result_sore_c.Item1);
        Debug.Log("Score A: " + result_sore_a.Item1);
        Debug.Log("REBA-Score: " + result_sore_c.Item1);

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

            Debug.Log("lower right arm position: " + angleLowerRightArm);
            Debug.Log("lower left arm position: " + lowerLeftArm.localEulerAngles.x);
            
            Debug.Log("right wrist position: " + rightHand.localEulerAngles.x);
            Debug.Log("left wrist position: " + leftHand.localEulerAngles.x);
            Debug.Log("right wrist twist: " + rightHand.localEulerAngles.y);
            Debug.Log("left wrist twist: " + leftHand.localEulerAngles.y);
            Debug.Log("right wrist bend: " + rightHand.localEulerAngles.z);
            Debug.Log("left wrist bend: " + leftHand.localEulerAngles.z);

            Debug.Log("Right leg position: " + lowerLeftLeg.localRotation.x);
            Debug.Log("Left leg position): " + lowerRightLeg.localRotation.x);
            if(body["legs_walking"] == 1)
            {
                Debug.Log("Is not Grounded");

            }
            else
            {
                Debug.Log("Is Grounded");                
            }           
        }
       
        if (LogAnglesCSV)
        {
            LogToCSV(Score);
        }


    }

    private void UpdateHUD(int rebaScore)
    {
        rebaScoreHUD.UpdateScoreText(rebaScore);
    }
    public (int, int[]) ComputeScoreA()
    {
        int neckScore = 0, trunkScore = 0, legScore = 0, loadScore = 0;

        // Neck position
        if (body["neck_angle"] <= 20)
            neckScore += 1;
        else
            neckScore += 2;

        // Neck adjust
        neckScore += body["neck_side"] != 0 ? 1 : 0;
        neckScore += body["neck_twisted"] != 0 ? 1 : 0;

        // Trunk position
        if ((0 <= body["trunk_angle"] && body["trunk_angle"] <= 1) || (360 <= body["trunk_angle"] && body["trunk_angle"] >= 359))
            trunkScore += 1;
        else if ((body["trunk_angle"] > 1 && body["trunk_angle"] <= 20) || (body["trunk_angle"] < 359 && body["trunk_angle"] >= 339))
            trunkScore += 2;
        else if ((20 < body["trunk_angle"] && body["trunk_angle"] <= 60) || (body["trunk_angle"] < 339 && body["trunk_angle"] >= 300))
            trunkScore += 3;
        else if ((body["trunk_angle"] > 60 && body["trunk_angle"] < 180) || (body["trunk_angle"] < 300 && body["trunk_angle"] > 180))
            trunkScore += 4;

        // Trunk adjust
        //180 because standard value is 180
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
        int scoreA = tableA[(neckScore - 1), (trunkScore - 1), (legScore - 1)];
        return (scoreA, new int[] { neckScore, trunkScore, legScore });
    }
    public (int, int[]) ComputeScoreB()
    {
        int upperArmScore = 0, lowerArmScore = 0, wristScore = 0;

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

        int upperRightArm = 0;
        //calculate left Arm
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
        // use higher score
        if(upperLeftArm < upperRightArm)
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
        if (arms["arm_abducted"]==1)
            upperArmScore++;
        if (arms["leaning"]==1)
            upperArmScore--;

        // Lower arm position
        if ((60 <= arms["lower_arm_angle"] && arms["lower_arm_angle"] <= 100) || (240 <= arms["lower_arm_angle"] && arms["lower_arm_angle"] <= 280))
            lowerArmScore++;
        else
            lowerArmScore += 2;

        // Wrist position
        if ((0 <= arms["wrist_angle"] && arms["wrist_angle"] <= 15) || (345 <= arms["wrist_angle"]  && arms["wrist_angle"] <= 360))
            wristScore++;
        else
            wristScore += 2;

        // Wrist adjust
        if ((arms["wrist_twisted"]==1) || (arms["wrist_bent"] == 1))
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
    public void LogToCSV(int data)
    {
        string filePath = Application.dataPath + "/../Logs/AutoREBALogFile.csv";

        if (!File.Exists(filePath))
        {
            string header = "Timestamp, Data";
            File.WriteAllText(filePath, header + "\n");
        }

        string time = System.DateTime.Now.ToString();
        string result = string.Format("{0},{1}\n", time, data);
        File.AppendAllText(filePath, result);
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