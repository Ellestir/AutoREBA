// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AutoRULA : MonoBehaviour
// {
//     public GameObject collarR;
//     public GameObject upperArmR;
//     public GameObject lowerArmR;
//     public GameObject wristR;
//     public GameObject midfingerR;

//     public GameObject collarL;
//     public GameObject upperArmL;
//     public GameObject lowerArmL;
//     public GameObject wristL;
//     public GameObject midfingerL;

//     public GameObject neck;
//     public GameObject head;
//     public GameObject spine1;
//     public GameObject spine;

//     public GameObject hipR;
//     public GameObject kneeR;
//     public GameObject footR;

//     public GameObject hipL;
//     public GameObject kneeL;
//     public GameObject footL;

//     public int SubjectID;
//     public int withOrWithoutRULA;

//     public Transform rotCalUpperArmR;
//     public Transform rotCalCollarR;
//     public Transform rotCalLowerArmR;
//     public Transform rotCalWristR;
//     public Transform rotCalMidfingerR;

//     public Transform rotCalHipR;
//     public Transform rotCalKneeR;
//     public Transform rotCalFootR;

//     public Transform rotCalHipL;
//     public Transform rotCalKneeL;
//     public Transform rotCalFootL;


//     private Quaternion rotCalUpperArmQuadR;
//     private Quaternion rotCalCollarQuadR;
//     private Quaternion rotCalLowerArmQuadR;
//     private Quaternion rotCalWristQuadR;
//     private Quaternion rotCalMidfingerQuadR;

//     private Quaternion rotCalHipQuadR;
//     private Quaternion rotCalKneeQuadR;
//     private Quaternion rotCalFootQuadR;

//     private Quaternion rotCalHipQuadL;
//     private Quaternion rotCalKneeQuadL;
//     private Quaternion rotCalFootQuadL;

//     public Transform rotCalUpperArmL;
//     public Transform rotCalCollarL;
//     public Transform rotCalLowerArmL;
//     public Transform rotCalWristL;
//     public Transform rotCalMidfingerL;

//     public Transform rotCalNeck;
//     public Transform rotCalHead;
//     public Transform rotCalSpine1;
//     public Transform rotCalSpine;

//     private Quaternion rotCalUpperArmQuadL;
//     private Quaternion rotCalCollarQuadL;
//     private Quaternion rotCalLowerArmQuadL;
//     private Quaternion rotCalWristQuadL;
//     private Quaternion rotCalMidfingerQuadL;

//     private Quaternion rotCalNeckQuad;
//     private Quaternion rotCalHeadQuad;
//     private Quaternion rotCalSpine1Quad;
//     private Quaternion rotCalSpineQuad;


//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     public void Calibration()
//     {
//         rotCalUpperArmQuadR = rotCalUpperArmR.transform.rotation;
//         rotCalCollarQuadR = rotCalCollarR.transform.rotation;
//         rotCalLowerArmQuadR = rotCalLowerArmR.transform.rotation;
//         rotCalWristQuadR = rotCalWristR.transform.rotation;
//         rotCalMidfingerQuadR = rotCalMidfingerR.transform.rotation;

//         rotCalUpperArmQuadL = rotCalUpperArmL.transform.rotation;
//         rotCalCollarQuadL = rotCalCollarL.transform.rotation;
//         rotCalLowerArmQuadL = rotCalLowerArmL.transform.rotation;
//         rotCalWristQuadL = rotCalWristL.transform.rotation;
//         rotCalMidfingerQuadL = rotCalMidfingerL.transform.rotation;

//         rotCalNeckQuad = rotCalNeck.transform.rotation;
//         rotCalHeadQuad = rotCalHead.transform.rotation;
//         rotCalSpineQuad = rotCalSpine.transform.rotation;
//         rotCalSpine1Quad = rotCalSpine1.transform.rotation;

//         rotCalHipQuadR = rotCalHipR.transform.rotation;
//         rotCalKneeQuadR = rotCalKneeR.transform.rotation;
//         rotCalFootQuadR = rotCalFootR.transform.rotation;

//         rotCalHipQuadL = rotCalHipL.transform.rotation;
//         rotCalKneeQuadL = rotCalKneeL.transform.rotation;
//         rotCalFootQuadL = rotCalFootL.transform.rotation;
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         //right Arm
//         //Angle Upper Arm
//         Quaternion qUpperArmR = Quaternion.Inverse(rotCalUpperArmQuadR) * upperArmR.transform.rotation;
//         Quaternion qCollarR = Quaternion.Inverse(rotCalCollarQuadR) * collarR.transform.rotation;
//         Quaternion qLowerArmR = Quaternion.Inverse(rotCalLowerArmQuadR) * lowerArmR.transform.rotation;
//         Quaternion qWristR = Quaternion.Inverse(rotCalWristQuadR) * wristR.transform.rotation;
//         Quaternion qMidfingerR = Quaternion.Inverse(rotCalMidfingerQuadR) * midfingerR.transform.rotation;

//         Quaternion qUpperArmL = Quaternion.Inverse(rotCalUpperArmQuadL) * upperArmL.transform.rotation;
//         Quaternion qCollarL = Quaternion.Inverse(rotCalCollarQuadL) * collarL.transform.rotation;
//         Quaternion qLowerArmL = Quaternion.Inverse(rotCalLowerArmQuadL) * lowerArmL.transform.rotation;
//         Quaternion qWristL = Quaternion.Inverse(rotCalWristQuadL) * wristL.transform.rotation;
//         Quaternion qMidfingerL = Quaternion.Inverse(rotCalMidfingerQuadL) * midfingerL.transform.rotation;

//         Quaternion qNeck = Quaternion.Inverse(rotCalNeckQuad) * neck.transform.rotation;
//         Quaternion qHead = Quaternion.Inverse(rotCalHeadQuad) * head.transform.rotation;

//         Quaternion qSpine1 = Quaternion.Inverse(rotCalSpine1Quad) * spine1.transform.rotation;
//         Quaternion qSpine = Quaternion.Inverse(rotCalSpineQuad) * spine.transform.rotation;

//         Quaternion qHipR = Quaternion.Inverse(rotCalHipQuadR) * hipR.transform.rotation;
//         Quaternion qKneeR = Quaternion.Inverse(rotCalKneeQuadR) * kneeR.transform.rotation;
//         Quaternion qFootR = Quaternion.Inverse(rotCalFootQuadR) * footR.transform.rotation;

//         Quaternion qHipL = Quaternion.Inverse(rotCalHipQuadL) * hipL.transform.rotation;
//         Quaternion qKneeL = Quaternion.Inverse(rotCalKneeQuadL) * kneeL.transform.rotation;
//         Quaternion qFootL = Quaternion.Inverse(rotCalFootQuadL) * footL.transform.rotation;

//         //angle UPPER arm RIGHT(1)
//         float angleUpperArmR = 0f;
//         angleUpperArmR = Quaternion.Angle(qUpperArmR, qCollarR); //Debug.Log("RULA Right Upper Arm:" + angleUpperArmR);
//         //right schoulder up (1a)
//         float shoulderRotationR = 0f;
//         shoulderRotationR = collarR.transform.localRotation.eulerAngles.y; // Debug.Log("RULA Right Upper arm angle if up: " + shoulderRotationR);"RULA Right Upper arm up " + shoulderUp(shoulderRotationR));
//         //shoulder abduced(1a)
//         float angleHeadShoulderR = 0f;
//         angleHeadShoulderR = Quaternion.Angle(qNeck, qUpperArmR); // Debug.Log("RULA Right Upper arm angle to head: " + angleHeadShoulderR + " RULA Right Upper arm abducted: " + shoulderAbduced(angleHeadShoulderR));


//         //angle UPPER arm LEFT(1)
//         float angleUpperArmL = 0f;
//         angleUpperArmL = Quaternion.Angle(qUpperArmL, qCollarL);
//         //Left schoulder up (1a)
//         float shoulderRotationL = 0f;
//         shoulderRotationL = collarL.transform.localRotation.eulerAngles.y;
//         //shoulder abduced(1a)
//         float angleHeadShoulderL = 0f;
//         angleHeadShoulderL = Quaternion.Angle(qNeck, qUpperArmL);
        

//         //Angle RIGHT LOWER arm (2)
//         float angleLowerArmR = 0f;
//         angleLowerArmR = Quaternion.Angle(qUpperArmR, qLowerArmR); 
//         Debug.Log("RULA Right Lower Arm:" + angleLowerArmR);
//         //lower arm rotation (2a)
//         float lowerArmRotationR = 0f;
//         lowerArmRotationR= lowerArmR.transform.localRotation.eulerAngles.z; //
//         Debug.Log("RULA Right Lower Arm Z rotation:" + lowerArmRotationR);

//         //Angle LEFT LOWER arm (2)
//         float angleLowerArmL = 0f;
//         angleLowerArmL = Quaternion.Angle(qUpperArmL, qLowerArmL); //Debug.Log("RULA Left Lower Arm:" + angleLowerArmL);
//         //lower arm rotation (2a)
//         float lowerArmRotationL = 0f;
//         lowerArmRotationL = lowerArmL.transform.localRotation.eulerAngles.z; //Debug.Log("RULA Left Lower Arm Z rotation:" + lowerArmRotationL);

//         //angle right wrist (3)
//         float angleWristR = 0f;
//         angleWristR = Quaternion.Angle(qLowerArmR, qWristR);  //
//         Debug.Log("RULA Right Wrist:" + angleWristR);
//         //right wrist deviates sideways from the midline (3a)
//         float wristSidewayDeviationR = 0f;
//         wristSidewayDeviationR = Quaternion.Angle(qWristR, qMidfingerR);  //
//         Debug.Log("RULA Right Wrist sideways Deviation: " + wristSidewayDeviationR);
//         //wrist twist (4)
//         float wristRotationR = 0f;
//         wristRotationR = wristR.transform.localRotation.eulerAngles.y;
//         Debug.Log("RULA Right Wrist Rotation: " + wristRotationR);
//         Debug.Log("RULA Right Wrist Twist Score: " + wristTwistRulaScore(wristRotationR));

//         Debug.Log("RULA Right Lower Arm Score " + unterArmRulaScore(angleLowerArmR, lowerArmRotationR));
//         Debug.Log("RULA Right Wrist Score " + wristRulaScore(angleWristR, wristSidewayDeviationR));

//         //Left wrist
//         float angleWristL = 0f;
//         angleWristL = Quaternion.Angle(qLowerArmL, qWristL); //lowerArmL.transform.rotation, wristL.transform.rotation);
//         float wristSidewayDeviationL = 0f;
//         wristSidewayDeviationL = Quaternion.Angle(qWristL, qMidfingerL);  //wristL.transform.rotation, midfingerL.transform.rotation);
//         float wristRotationL = 0f;
//         wristRotationL = wristL.transform.localRotation.eulerAngles.y;

//         //Neck angle and rotations for score calculation
//         float angleNeck = 0f;
//         angleNeck = Quaternion.Angle(qNeck, qHead);
//         Debug.Log("RULA Angle Neck to head " + angleNeck);
//         //forward or backward 
//         float headRotationZ= head.transform.localRotation.eulerAngles.z;
//         Debug.Log("RULA Head rotation z  " + headRotationZ);
//         //left or right turn
//         float headRotationX = head.transform.localRotation.eulerAngles.x;
//         Debug.Log("RULA Head rotation x  " + headRotationX);
//         //left or right lean
//         float headRotationY = head.transform.localRotation.eulerAngles.y;
//         Debug.Log("RULA Head rotation y  " + headRotationY);
//         Debug.Log("RULA Neck-Head Score  " + neckRulaScore(angleNeck,headRotationZ,headRotationX, headRotationY));

//         float angleTrunk = 0f;
//         angleTrunk = Quaternion.Angle(qSpine1, qSpine); //
//                                                         Debug.Log("RULA Angle trunk" + angleTrunk);
//         //sideway trunk lean
//         float trunkRotationY = spine1.transform.localRotation.eulerAngles.y; //
//                                                                              Debug.Log("RULA Trunk rotation y  " + trunkRotationY);
//         //sideway trunk turn
//         float trunkRotationX = spine1.transform.localRotation.eulerAngles.x; //
//                                                                              Debug.Log("RULA Trunk rotation x  " + trunkRotationX);
//         Debug.Log("RULA Trunk RULA Score  " + trunkRulaScore(angleTrunk, trunkRotationX, trunkRotationY));

//         //angle right hip to spine
//         float angleHipR = 0f;
//         angleHipR = Quaternion.Angle(qHipR, qSpine);    //Debug.Log("RULA Angle Leg Hip R " + angleHipR);
//         //angle right knee 
//         float angleKneeR = 0f;
//         angleKneeR = Quaternion.Angle(qHipR, qFootR);   //Debug.Log("RULA Angle Leg Knee R " + angleKneeR);
//         //angle left hip to spine
//         float angleHipL = 0f;
//         angleHipL = Quaternion.Angle(qHipL, qSpine);    //Debug.Log("RULA Angle Leg Hip L " + angleHipL);
//         //angle left knee
//         float angleKneeL = 0f;
//         angleKneeL = Quaternion.Angle(qHipL, qFootL); // Debug.Log("RULA Angle Leg Knee L " + angleKneeL);
//         Debug.Log("RULA Leg RULA Score  " + legRulaScore(angleHipR, angleHipL, angleKneeR, angleKneeL));

//         Debug.Log("RULA Right Upper Arm Score " + upperArmRulaScore(angleUpperArmR, shoulderRotationR, angleHeadShoulderR, angleTrunk));

//         int upperArmSR = upperArmRulaScore(angleUpperArmR, shoulderRotationR, angleHeadShoulderR, angleTrunk);
//         int lowerArmSR = unterArmRulaScore(angleLowerArmR, lowerArmRotationR);
//         int wristSR = wristRulaScore(angleWristR, wristSidewayDeviationR);
//         int wristTwistSR = wristTwistRulaScore(wristRotationR);

//         int upperArmSL = upperArmRulaScore(angleUpperArmL, shoulderRotationL, angleHeadShoulderL, angleTrunk);
//         int lowerArmSL = unterArmRulaScore(angleLowerArmL, lowerArmRotationL);
//         int wristSL = wristRulaScore(angleWristL, wristSidewayDeviationL);
//         int wristTwistSL = wristTwistRulaScore(wristRotationL);

//         int neckS = neckRulaScore(angleNeck, headRotationZ, headRotationX, headRotationY);
//         int trunkS = trunkRulaScore(angleTrunk, trunkRotationX, trunkRotationY);
//         int legS = legRulaScore(angleHipR, angleHipL, angleKneeR, angleKneeL);

//         int tableAR= RULA_Score_ArmHandgelenkshaltung(upperArmSR, lowerArmSR, wristSR, wristTwistSR);
//         Debug.Log("Score table A:" + tableAR);
//         int tableAL=RULA_Score_ArmHandgelenkshaltung(upperArmSL, lowerArmSL, wristSL, wristTwistSL);
//         int tableB= RULA_Score_Oberk�rperBeinhaltung(neckS, trunkS, legS);
//         Debug.Log("Score table B:" + tableB);
//         int scoreOverall = RULA_Score_Gesamtpunktzahl(tableAR, tableB);
//         Debug.Log("Score overall:" + scoreOverall);
//         int scoreOverall2 = RULA_Score_Gesamtpunktzahl(tableAL, tableB);

//     }

//     Boolean shoulderUp(float angleShoulderRotation)
//     {
//         if (angleShoulderRotation < 59)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     Boolean shoulderAbduced(float angleHeadShoulder)
//     {
//         if (angleHeadShoulder > 0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     Boolean personLeansForward(float angleTrunk)
//     {
//         if(angleTrunk != 0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }
//     //Calculate the score for shoulder
//     int upperArmRulaScore(float angleUpperArm, float shoulderRotation,float angleHeadShoulder, float angleTrunk)
//     {
//         int scoreUpperArm = 0;
//         if (shoulderUp(shoulderRotation))
//         {
//             scoreUpperArm = scoreUpperArm + 1;
//         }
//         if (shoulderAbduced(angleHeadShoulder))
//         {
//             scoreUpperArm = scoreUpperArm + 1;
//         }
//         if (personLeansForward(angleTrunk))
//         {
//             scoreUpperArm = scoreUpperArm - 1;
//         }

//         if (angleUpperArm >= 0 && angleUpperArm <= 20)
//         {
//             scoreUpperArm = scoreUpperArm + 1;
//         }
//         else if(angleUpperArm > 20 && angleUpperArm <= 45)
//         {
//             scoreUpperArm = scoreUpperArm + 2;
//         }
//         else if (angleUpperArm > 45 && angleUpperArm <= 90)
//         {
//             scoreUpperArm = scoreUpperArm + 3;
//         }
//         else if (angleUpperArm> 90)
//         {
//             scoreUpperArm = scoreUpperArm + 4;
//         }
//         return scoreUpperArm;
//     }

//     //Calculate the score for the forearm
//     int unterArmRulaScore(float angleUnterArm, float lowerArmRotation)
//     {
//         int scoreLowerArm = 0;
//         if (lowerarmTwisted(lowerArmRotation))
//         {
//             scoreLowerArm = scoreLowerArm + 1;
//         }
       
//         if (angleUnterArm < 60)
//         {
//             scoreLowerArm = scoreLowerArm + 2;
//         }
//         else if (angleUnterArm >= 60 && angleUnterArm <= 100)
//         {
//             scoreLowerArm = scoreLowerArm + 1;
//         }
        
//         else if (angleUnterArm >100)
//         {
//             scoreLowerArm = scoreLowerArm + 2;
//         }
//         return scoreLowerArm;
//     }

//     //Calculate the score for the wrist
//     int wristRulaScore(float angleWrist, float wristSidewayDeviation)
//     {
//         int scoreWrist = 0;
//         if (wristTwisted(wristSidewayDeviation))
//         {
//             scoreWrist = scoreWrist + 1;
//         }

//         if (angleWrist == 0)
//         {
//             scoreWrist = scoreWrist + 1;
//         }
//         else if (angleWrist <= 15 && angleWrist > 0) 
//         {
//             scoreWrist = scoreWrist + 2;
//         }

//         else if (angleWrist > 15) 
//         {
//             scoreWrist = scoreWrist + 3;
//         }
//         return scoreWrist;
//     }

//     //Check the wrist sideways deviation
//     Boolean wristTwisted(float wristSidewayDeviation)
//     {
//         if(wristSidewayDeviation != 0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     int wristTwistRulaScore(float wristRotation)
//     {
//        int wristTwistScore = 0;
//        if (wristRotation > 263 && wristRotation < 283)
//         {
//             wristTwistScore = wristTwistScore + 1;
//         }
//        else
//         {
//             wristTwistScore = wristTwistScore + 2;
//         }
//         return wristTwistScore;
//     }

//     //check if the lower arm is rotated
//     Boolean lowerarmTwisted(float lowerArmRotation)
//     {
//         if (lowerArmRotation != 0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     int neckRulaScore(float angleNeck, float headRotationZ, float headRotationX, float headRotationY)
//     {
//         int scoreNeck = 0;
//         if (headRotationZ > 300)
//         {
//             scoreNeck = 4;
//             if (headRotationX !=0)
//             {
//                 scoreNeck = scoreNeck + 1;
//             }
//             if (headRotationY !=0)
//             {
//                 scoreNeck = scoreNeck + 1;
//             }
//         }
        
//         else
//         {
//             if (headRotationX !=0)
//             {
//                 scoreNeck = scoreNeck + 1;
//             }
//             if (headRotationY !=0)
//             {
//                 scoreNeck = scoreNeck + 1;
//             } 

//             if (angleNeck >= 0 && angleNeck < 10)
//             {
//                 scoreNeck = scoreNeck + 1;
//             }
//             else if (angleNeck >= 10 && angleNeck <= 20)
//             {
//                 scoreNeck = scoreNeck + 2;
//             }
//             else if (angleNeck > 20)
//             {
//                 scoreNeck = scoreNeck + 3;
//             }
//         }
//         return scoreNeck;
//     }

//     int trunkRulaScore(float angleTrunk, float trunkRotationX, float trunkRotationY)
//     {
//         int scoreTrunk = 0;
        
//         if(trunkRotationX != 0)
//         {
//             scoreTrunk = scoreTrunk + 1;
//         }
//         if(trunkRotationY !=0)
//         {
//             scoreTrunk = scoreTrunk + 1;
//         }

//         if (angleTrunk == 0)
//         {
//             scoreTrunk = scoreTrunk + 1;
//         }
//         else if(angleTrunk>0 && angleTrunk <= 20)
//         {
//             scoreTrunk = scoreTrunk + 2;
//         }
//         else if (angleTrunk > 20 && angleTrunk <= 60)
//         {
//             scoreTrunk = scoreTrunk + 3;
//         }
//         else if (angleTrunk > 60)
//         {
//             scoreTrunk = scoreTrunk + 4;
//         }
//         return scoreTrunk;
//     }

//     int legRulaScore(float angleHipR, float angleHipL, float angleKneeR, float angleKneeL)
//     {
//         int scoreLeg = 0;
//         if (Math.Abs(angleHipL- angleHipR)<7 && Math.Abs(angleKneeR - angleKneeL)<7)
//         {
//             scoreLeg = scoreLeg + 1;
//         }
//         else
//         {
//             scoreLeg = scoreLeg + 2;
//         }
//         return scoreLeg;
//     }

//     public static int RULA_Score_ArmHandgelenkshaltung(int oberarm, int unterarm, int handgelenk, int unterarmumwendung)
//     {
//         // Three-dimensional oberarm_RULAScore_1: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_1 = new int[3, 4, 2] { { { 1, 2 }, { 2, 2 }, { 2, 3 }, { 3, 3 }},
//                 { { 2, 2 }, { 2, 2 }, { 3, 3 }, { 3, 3 } }, {{ 2, 3 }, { 3, 3 }, { 3, 3 }, { 4, 4} } };

//         // Three-dimensional oberarm_RULAScore_2: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_2 = new int[3, 4, 2] { { { 2, 3 }, { 3, 3 }, { 3, 4 }, { 4, 4 }},
//                 { { 3, 3 }, { 3, 3 }, { 3, 4 }, { 4, 4 } }, {{ 3, 4 }, { 4, 4 }, { 4, 4 }, { 5, 5} } };

//         // Three-dimensional oberarm_RULAScore_3: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_3 = new int[3, 4, 2] { { { 3, 3 }, { 4, 4 }, { 4, 4 }, { 5, 5 }},
//                 { { 3, 4 }, { 4, 4 }, { 4, 4 }, { 5, 5 } }, {{ 4, 4 }, { 4, 4 }, { 4, 5 }, { 5, 5} } };

//         // Three-dimensional oberarm_RULAScore_4: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_4 = new int[3, 4, 2] { { { 4, 4 }, { 4, 4 }, { 4, 5 }, { 5, 5 }},
//                 { { 4, 4 }, { 4, 4 }, { 4, 5 }, { 5, 5 } }, {{ 4, 4 }, { 4, 5 }, { 5, 5 }, { 6, 6} } };

//         // Three-dimensional oberarm_RULAScore_5: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_5 = new int[3, 4, 2] { { { 5, 5 }, { 5, 5 }, { 5, 6 }, { 6, 7 }},
//                 { { 5, 6 }, { 6, 6 }, { 6, 7 }, { 7, 7 } }, {{ 6, 6 }, { 6, 7 }, { 7, 7 }, { 7, 8} } };

//         // Three-dimensional oberarm_RULAScore_6: 1D = Unterarm, 2D = Handgelenk, 3D = Unterarmumwendung
//         int[,,] oberarm_RULAScore_6 = new int[3, 4, 2] { { { 7, 7 }, { 7, 7 }, { 7, 8 }, { 8, 9 }},
//                 { { 8, 8 }, { 8, 8 }, { 8, 9 }, { 9, 9 } }, {{ 9, 9 }, { 9, 9 }, { 9, 9 }, { 9, 9} } };

//         if (oberarm == 1)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[0, 0, 0]; // answer: 1
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[0, 0, 1]; // answer: 2
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[0, 1, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[0, 1, 1]; // answer: 2 
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[0, 2, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[0, 2, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[0, 3, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[0, 3, 1]; // answer: 3
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[1, 0, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[1, 0, 1]; // answer: 2
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[1, 1, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[1, 1, 1]; // answer: 2
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[1, 2, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[1, 2, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[1, 3, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[1, 3, 1]; // answer: 3
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[2, 0, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[2, 0, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[2, 1, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[2, 1, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[2, 2, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[2, 2, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_1[2, 3, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_1[2, 3, 1]; // answer: 4
//                     }
//                 }
//             }

//         }
//         else if (oberarm == 2)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[0, 0, 0]; // answer: 2
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[0, 0, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[0, 1, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[0, 1, 1]; // answer: 3
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[0, 2, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[0, 2, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[0, 3, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[0, 3, 1]; // answer: 4
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[1, 0, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[1, 0, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[1, 1, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[1, 1, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[1, 2, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[1, 2, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[1, 3, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[1, 3, 1]; // answer: 4
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[2, 0, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[2, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[2, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[2, 1, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[2, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[2, 2, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_2[2, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_2[2, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//         }
//         else if (oberarm == 3)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[0, 0, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[0, 0, 1]; // answer: 3
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[0, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[0, 1, 1]; // answer: 4
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[0, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[0, 2, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[0, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[0, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[1, 0, 0]; // answer: 3
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[1, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[1, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[1, 1, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[1, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[1, 2, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[1, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[1, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[2, 0, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[2, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[2, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[2, 1, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[2, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[2, 2, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_3[2, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_3[2, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//         }
//         else if (oberarm == 4)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[0, 0, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[0, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[0, 1, 0]; // answer: 4 
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[0, 1, 1]; // answer: 4
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[0, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[0, 2, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[0, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[0, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[1, 0, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[1, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[1, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[1, 1, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[1, 2, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[1, 2, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[1, 3, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[1, 3, 1]; // answer: 5
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[2, 0, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[2, 0, 1]; // answer: 4
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[2, 1, 0]; // answer: 4
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[2, 1, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[2, 2, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[2, 2, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_4[2, 3, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_4[2, 3, 1]; // answer: 6
//                     }
//                 }
//             }
//         }
//         else if (oberarm == 5)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[0, 0, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[0, 0, 1]; // answer: 5
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[0, 1, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[0, 1, 1]; // answer: 5
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[0, 2, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[0, 2, 1]; // answer: 6
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[0, 3, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[0, 3, 1]; // answer: 7
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[1, 0, 0]; // answer: 5
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[1, 0, 1]; // answer: 6
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[1, 1, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[1, 1, 1]; // answer: 6
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[1, 2, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[1, 2, 1]; // answer: 7
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[1, 3, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[1, 3, 1]; // answer: 7
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[2, 0, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[2, 0, 1]; // answer: 6
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[2, 1, 0]; // answer: 6
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[2, 1, 1]; // answer: 7
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[2, 2, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[2, 1, 1]; // answer: 7
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_5[2, 3, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_5[2, 3, 1]; // answer: 8
//                     }
//                 }
//             }
//         }
//         else if (oberarm == 6)
//         {
//             if (unterarm == 1)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[0, 0, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[0, 0, 1]; // answer: 7
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[0, 1, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[0, 1, 1]; // answer: 7
//                     }

//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[0, 2, 0]; // answer: 7
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[0, 2, 1]; // answer: 8
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[0, 3, 0]; // answer: 8
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[0, 3, 1]; // answer: 9
//                     }
//                 }
//             }
//             else if (unterarm == 2)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[1, 0, 0]; // answer: 8
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[1, 0, 1]; // answer: 8
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[1, 1, 0]; // answer: 8
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[1, 1, 1]; // answer: 8
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[1, 2, 0]; // answer: 8
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[1, 2, 1]; // answer: 9
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[1, 3, 0]; // answer: 9
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[1, 3, 1]; // answer: 9
//                     }
//                 }
//             }
//             else if (unterarm == 3)
//             {
//                 if (handgelenk == 1)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[2, 0, 0]; // answer: 9
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[2, 0, 1]; // answer: 9
//                     }
//                 }
//                 else if (handgelenk == 2)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[2, 1, 0]; // answer: 9
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[2, 1, 1]; // answer: 9
//                     }
//                 }
//                 else if (handgelenk == 3)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[2, 2, 0]; // answer: 9
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[2, 2, 1]; // answer: 9
//                     }
//                 }
//                 else if (handgelenk == 4)
//                 {
//                     if (unterarmumwendung == 1)
//                     {
//                         return oberarm_RULAScore_6[2, 3, 0]; // answer: 9
//                     }
//                     else if (unterarmumwendung == 2)
//                     {
//                         return oberarm_RULAScore_6[2, 3, 1]; // answer: 9
//                     }
//                 }
//             }
//         }
//         return 0;

//     }

//     public static int RULA_Score_Oberk�rperBeinhaltung(int hals, int oberk�rper, int beine)
//     {
//         // Two-dimensional obek�rperBeinhaltung_RULAScore_1: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_1 = new int[6, 2] { { 1, 3 }, { 2, 3 }, { 3, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 } };

//         // Two-dimensional obek�rperBeinhaltung_RULAScore_2: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_2 = new int[6, 2] { { 2, 3 }, { 2, 3 }, { 3, 4 }, { 5, 5 }, { 6, 7 }, { 7, 7 } };

//         // Two-dimensional obek�rperBeinhaltung_RULAScore_3: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_3 = new int[6, 2] { { 3, 3 }, { 3, 4 }, { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 7 } };

//         // Two-dimensional obek�rperBeinhaltung_RULAScore_4: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_4 = new int[6, 2] { { 5, 5 }, { 5, 6 }, { 6, 7 }, { 7, 7 }, { 7, 7 }, { 8, 8 } };

//         // Two-dimensional obek�rperBeinhaltung_RULAScore_5: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_5 = new int[6, 2] { { 7, 7 }, { 7, 7 }, { 7, 8 }, { 8, 8 }, { 8, 8 }, { 8, 8 } };

//         // Two-dimensional obek�rperBeinhaltung_RULAScore_6: 1D = �berk�rper, 2D = Beine
//         int[,] obek�rperBeinhaltung_RULAScore_6 = new int[6, 2] { { 8, 8 }, { 8, 8 }, { 8, 8 }, { 8, 9 }, { 9, 9 }, { 9, 9 } };

//         if (hals == 1)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[0, 0]; // answer 1
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[0, 1]; // answer 3
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[1, 0]; // answer 2
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[1, 1]; // answer 3
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[2, 0]; // answer 3
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[2, 1]; // answer 4
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[3, 0]; // answer 5
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[3, 1]; // answer 5
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[4, 0]; // answer 6
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[4, 1]; // answer 6
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[5, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_1[5, 1]; // answer 7
//                 }
//             }
//         }
//         else if (hals == 2)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[0, 0]; // answer 2
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[0, 1]; // answer 3
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[1, 0]; // answer 2
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[1, 1]; // answer 3
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[2, 0]; // answer 4
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[2, 1]; // answer 5
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[3, 0]; // answer 5
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[3, 1]; // answer 5
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[4, 0]; // answer 6
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[4, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[5, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_2[5, 1]; // answer 7
//                 }
//             }
//         }
//         else if (hals == 3)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[0, 0]; // answer 3
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[0, 1]; // answer 3
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[1, 0]; // answer 3
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[1, 1]; // answer 4
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[2, 0]; // answer 4
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[2, 1]; // answer 5
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[3, 0]; // answer 5
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[3, 1]; // answer 6
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[4, 0]; // answer 6
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[4, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[5, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_3[5, 1]; // answer 7
//                 }
//             }
//         }
//         else if (hals == 4)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[0, 0]; // answer 5
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[0, 1]; // answer 5
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[1, 0]; // answer 5
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[1, 1]; // answer 6
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[2, 0]; // answer 6
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[2, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[3, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[3, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[4, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[4, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[5, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_4[5, 1]; // answer 8
//                 }
//             }
//         }
//         else if (hals == 5)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[0, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[0, 1]; // answer 7
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[1, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[1, 1]; // answer 7
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[2, 0]; // answer 7
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[2, 1]; // answer 8
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[3, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[3, 1]; // answer 8
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[4, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[4, 1]; // answer 8
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[5, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[5, 1]; // answer 8
//                 }
//             }
//         }
//         else if (hals == 6)
//         {
//             if (oberk�rper == 1)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[0, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_5[0, 1]; // answer 8
//                 }

//             }
//             else if (oberk�rper == 2)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[1, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[1, 1]; // answer 8
//                 }
//             }
//             else if (oberk�rper == 3)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[2, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[2, 1]; // answer 8
//                 }
//             }
//             else if (oberk�rper == 4)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[3, 0]; // answer 8
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[3, 1]; // answer 9
//                 }
//             }
//             else if (oberk�rper == 5)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[4, 0]; // answer 9
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[4, 1]; // answer 9
//                 }
//             }
//             else if (oberk�rper == 6)
//             {
//                 if (beine == 1)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[5, 0]; // answer 9
//                 }
//                 else if (beine == 2)
//                 {
//                     return obek�rperBeinhaltung_RULAScore_6[5, 1]; // answer 9
//                 }
//             }
//         }

//         return 0;
//     }

//     public static int RULA_Score_Gesamtpunktzahl(int gesamtwertArmHandgelenk, int gesamtwerkHalsOberk�rperBeine)
//     {
//         int[] gesamtwertArmHandgelenk_RULAScore_1 = new int[7] { 1, 2, 3, 3, 4, 5, 5 };
//         int[] gesamtwertArmHandgelenk_RULAScore_2 = new int[7] { 2, 2, 3, 4, 4, 5, 5 };

//         int[] gesamtwertArmHandgelenk_RULAScore_3 = new int[7] { 3, 3, 3, 4, 4, 5, 6 };
//         int[] gesamtwertArmHandgelenk_RULAScore_4 = new int[7] { 3, 3, 3, 4, 5, 6, 6 };

//         int[] gesamtwertArmHandgelenk_RULAScore_5 = new int[7] { 4, 4, 4, 5, 6, 7, 7 };
//         int[] gesamtwertArmHandgelenk_RULAScore_6 = new int[7] { 4, 4, 5, 6, 6, 7, 7 };

//         int[] gesamtwertArmHandgelenk_RULAScore_7 = new int[7] { 5, 5, 6, 6, 7, 7, 7 };
//         int[] gesamtwertArmHandgelenk_RULAScore_8 = new int[7] { 5, 5, 6, 7, 7, 7, 7 };

//         if (gesamtwertArmHandgelenk == 1)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 1 // Ergebnis: akzeptabel");
//                 return gesamtwertArmHandgelenk_RULAScore_1[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 2 // Ergebnis: akzeptabel");
//                 return gesamtwertArmHandgelenk_RULAScore_1[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_1[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_1[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_1[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_1[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_1[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk == 2)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 2 // Ergebnis: akzeptabel");
//                 return gesamtwertArmHandgelenk_RULAScore_2[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 2 // Ergebnis: akzeptabel");
//                 return gesamtwertArmHandgelenk_RULAScore_2[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_2[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_2[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_2[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_2[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_2[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk == 3)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_3[6];

//             }

//         }
//         else if (gesamtwertArmHandgelenk == 4)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 3 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_4[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk == 5)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_5[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk == 6)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 4 // Ergebnis: in naher Zukunft weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_6[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk == 7)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_7[6];

//             }
//         }
//         else if (gesamtwertArmHandgelenk >= 8)
//         {
//             if (gesamtwerkHalsOberk�rperBeine == 1)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[0];
//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 2)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 5 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[1];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 3)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 6 // Ergebnis: in K�rze weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[2];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 4)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[3];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 5)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[4];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine == 6)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[5];

//             }
//             else if (gesamtwerkHalsOberk�rperBeine >= 7)
//             {
//                 System.Console.WriteLine("Gesamtpunktzahl: 7 // Ergebnis: sofort weitere Ma�nahmen einleiten");
//                 return gesamtwertArmHandgelenk_RULAScore_8[6];

//             }
//         }

//         return 0;
//     }
// }

