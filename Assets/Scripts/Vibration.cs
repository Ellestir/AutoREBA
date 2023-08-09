using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Vibration : MonoBehaviour
{

    private float lastUpdateTime;

    // Enumeration for controlling motor sliders
    public enum MotorSlider { one, two }
    public MotorSlider motorSlider;

    // Enumeration for controlling steps sliders
    public enum StepsSlider { five, fifteen }
    public StepsSlider stepsSlider;

    // Enumeration for controlling intensity
    public enum Intensity { Low, Medium, High }
    public Intensity intensitySlider;

    private int rebaScore;

    private UdpClient udpClient;
    private IPEndPoint endPoint;
    private string ip = "192.168.2.182";  // Replace this with the IP of your Arduino
    private int port = 8888;  // The port your Arduino is listening to

    //Vibration Mapping for two motors and fifteen Steps and Intesity for High, Medium, Low
    private int[] motorStrength1_high = new int[] {   0, 115, 125, 135, 155, 160, 170, 180, 190, 200, 210, 220, 230, 240, 255 };
    private int[] motorStrength2_high = new int[] {   0,   0,   0,   0,   0, 120, 135, 150, 160, 175, 190, 205, 225, 235, 255 };

    private int[] motorStrength1_medium = new int[] { 0, 104, 118, 130, 145, 150, 160, 170, 180, 190, 200, 210, 220, 230, 245 };
    private int[] motorStrength2_medium = new int[] { 0,   0,   0,   0,   0, 115, 130, 145, 155, 170, 185, 200, 210, 225, 245 };

    private int[] motorStrength1_low = new int[] {    0,  93, 106, 119, 135, 150, 160, 170, 175, 185, 195, 205, 215, 230, 235 };
    private int[] motorStrength2_low = new int[] {    0,   0,   0,   0,   0, 100, 115, 130, 145, 160, 175, 190, 200, 215, 235 };
    

    //Vibration Mapping for two motors and five Steps and Intesity for High, Medium, Low
    private int[] motorStrength1_high_5 = new int[] { 0, 135, 170, 210, 255 };
    private int[] motorStrength2_high_5 = new int[] { 0, 120, 155, 195, 255 };
    
    private int[] motorStrength1_medium_5 = new int[] { 0, 110, 160, 190, 245 };
    private int[] motorStrength2_medium_5 = new int[] { 0, 100, 130, 185, 245 };
                                        
    private int[] motorStrength1_low_5 = new int[] { 0, 100, 130, 180, 235 };
    private int[] motorStrength2_low_5 = new int[] { 0,   0, 130, 165, 235 };
                                       

    //Vibration Mapping for one motor and fifteen Steps and Intesity for High, Medium, Low
    private int[] motorStrength1_high_one = new int[] { 0  , 105, 115, 125, 135, 155, 170, 180, 190, 200, 210, 220, 230, 240, 255};

    private int[] motorStrength1_medium_one = new int[] { 0,   100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 245 };
                                                      
    private int[] motorStrength1_low_one = new int[] { 0,   91, 102, 113, 124, 145, 156, 167, 178, 189, 200, 210, 220, 230, 235 };


    //Vibration Mapping for one motor and five Steps and Intesity for High, Medium, Low                     
    private int[] motorStrength1_high_5_one = new int[] { 0, 120, 160, 200, 255 };
                                                        
    private int[] motorStrength1_medium_5_one = new int[] { 0, 105, 140, 190, 245 };
                                                        
    private int[] motorStrength1_low_5_one = new int[] { 0, 90, 130, 180, 235 };
                                                     

    private void Start()
    {
        // UDP connection
        endPoint = new IPEndPoint(IPAddress.Parse(ip), port); 
        udpClient = new UdpClient();
        lastUpdateTime = Time.time;

    }

    private void Update()
    {

        rebaScore = REBA_Score.Score; //calculated REBA_Score
        if (Time.time - lastUpdateTime >= 1) // Check if 1 second has passed
        {
            OnButtonPress();                // Send Vibration again 
            lastUpdateTime = Time.time;
        }
    }

    private void OnButtonPress()
    {
        // Select appropriate strength arrays based on the sliders and score
        int[] motor1StrengthArray = SelectStrengthArray(motorSlider, stepsSlider, intensitySlider, true);
        int[] motor2StrengthArray = SelectStrengthArray(motorSlider, stepsSlider, intensitySlider, false);
        int mappedRebaScore = MapRebaScore(rebaScore, stepsSlider);

        // Determine the motor strength based on the REBA score
        int motor1Strength = motor1StrengthArray[Mathf.Min(mappedRebaScore - 1, motor1StrengthArray.Length - 1)];
        int motor2Strength = motor2StrengthArray[Mathf.Min(mappedRebaScore - 1, motor2StrengthArray.Length - 1)];

        // Send the computed motor strengths as a message
        SendData("start vibration," + motor1Strength.ToString() + "," + motor2Strength.ToString());
    }

    // Mapping the REBA score based on the number of steps
    private int MapRebaScore(int rebaScore, StepsSlider stepsSlider)
    {
        if (stepsSlider == StepsSlider.five)
        {
            if (rebaScore == 1)
                return 1;
            if (rebaScore >= 2 && rebaScore <= 3)
                return 2;
            if (rebaScore >= 4 && rebaScore <= 7)
                return 3;
            if (rebaScore >= 8 && rebaScore <= 10)
                return 4;
            if (rebaScore >= 11 && rebaScore <= 15)
                return 5;
        }
        else
        {
        return rebaScore;
        }

        return 0; 
    }


    // Select the appropriate motor strength array based on sliders and motor number
    private int[] SelectStrengthArray(MotorSlider motorSlider, StepsSlider stepsSlider, Intensity intensitySlider, bool isMotor1)
    {
        // Determine which array to use based on the values of the sliders
        if (isMotor1 == true)
        {
            if (motorSlider == MotorSlider.one)
            {
                if (stepsSlider == StepsSlider.five)
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength1_low_5_one;
                        case Intensity.Medium: return motorStrength1_medium_5_one;
                        default: return motorStrength1_high_5_one;
                    }
                }
                else
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength1_low_one;
                        case Intensity.Medium: return motorStrength1_medium_one;
                        default: return motorStrength1_high_one;
                    }
                }
            }
            else
            {
                if (stepsSlider == StepsSlider.five)
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength1_low_5;
                        case Intensity.Medium: return motorStrength1_medium_5;
                        default: return motorStrength1_high_5;
                    }
                }
                else
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength1_low;
                        case Intensity.Medium: return motorStrength1_medium;
                        default: return motorStrength1_high;
                    }
                }
            }
        }
        else
        {
            if (motorSlider == MotorSlider.two)
            {
                if (stepsSlider == StepsSlider.five)
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength2_low_5;
                        case Intensity.Medium: return motorStrength2_medium_5;
                        default: return motorStrength2_high_5;
                    }
                }
                else
                {
                    switch (intensitySlider)
                    {
                        case Intensity.Low: return motorStrength2_low;
                        case Intensity.Medium: return motorStrength2_medium;
                        default: return motorStrength2_high;
                    }
                }
            }
            else
            {
                return new int[] { 0, 0 };
            }
        }
    }

    // Send a message to the connected device (Arduino)
    private void SendData(string message)
    {
        try
        {
            Debug.Log("Sending message: " + message);  // Log the message
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, endPoint);
        }
        catch (Exception e)
        {
            Debug.Log("Could not send data: " + e.Message);
        }
    }

    /*
    private void OnDestroy()
    {
        udpClient.Close();
    }
    */
}