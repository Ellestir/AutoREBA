using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    [Range(1, 2)]
    public int motorSlider;

    public enum StepsSlider { Fuenf, Fuenfzehn }
    public StepsSlider stepsSlider;

    public enum Intensity { Low, Medium, High }
    public Intensity intensitySlider;

    public string ip = "192.168.2.92"; 
    public int port = 8888;

    //public Button vibrateButton;  // Button to start vibration
    // Vibrationsintensität, die an das Arduino-Gerät gesendet werden soll


    private UdpClient udpClient;
    private IPEndPoint endPoint;
    

    // Define all your motorStrength arrays here as in your code.
    //two motors
    private int[] motorStrength1_high = new int[] { 105, 115, 125, 135, 155, 160, 170, 180, 190, 200, 210, 220, 230, 240, 255 };
    private int[] motorStrength2_high = new int[] { 0, 0, 0, 0, 0, 115, 130, 145, 160, 175, 190, 205, 220, 235, 255 };

    private int[] motorStrength1_medium = new int[] { 100, 110, 120, 130, 150, 150, 160, 170, 180, 190, 200, 210, 220, 230, 245 };
    private int[] motorStrength2_medium = new int[] { 0, 0, 0, 0, 0, 115, 130, 145, 155, 170, 190, 205, 220, 235, 245 };

    private int[] motorStrength1_low = new int[] { 90, 100, 110, 120, 130, 150, 160, 170, 175, 185, 195, 200, 220, 230, 235 };
    private int[] motorStrength2_low = new int[] { 0, 0, 0, 0, 0, 100, 115, 130, 145, 160, 175, 190, 205, 220, 235 };

    private int[] motorStrength1_high_5 = new int[] { 105, 155, 180, 210, 255 };
    private int[] motorStrength2_high_5 = new int[] { 0, 100, 145, 190, 255 };

    private int[] motorStrength1_medium_5 = new int[] { 100, 135, 160, 190, 245 };
    private int[] motorStrength2_medium_5 = new int[] { 0, 0, 130, 175, 245 };

    private int[] motorStrength1_low_5 = new int[] { 90, 120, 150, 200, 235 };
    private int[] motorStrength2_low_5 = new int[] { 0, 0, 120, 165, 235 };



    //one motor
    private int[] motorStrength1_high_one = new int[] { 105, 115, 125, 135, 155, 160, 170, 180, 190, 200, 210, 220, 230, 240, 255 };

    private int[] motorStrength1_medium_one = new int[] { 100, 110, 120, 130, 150, 150, 160, 170, 180, 190, 200, 210, 220, 230, 245 };

    private int[] motorStrength1_low_one = new int[] { 90, 100, 110, 120, 130, 150, 160, 170, 180, 190, 200, 210, 220, 230, 235 };

    private int[] motorStrength1_high_5_one = new int[] { 105, 155, 180, 210, 255 };

    private int[] motorStrength1_medium_5_one = new int[] { 100, 130, 160, 200, 245 };

    private int[] motorStrength1_low_5_one = new int[] { 90, 120, 150, 190, 235 };

    private void Start()
    {
        endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        udpClient = new UdpClient();

        //vibrateButton.onClick.AddListener(OnButtonPress);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            OnButtonPress();
        }
    }


    private void OnButtonPress()
    {

        int[] motor1StrengthArray = SelectStrengthArray(motorSlider, stepsSlider, intensitySlider, true);
        int[] motor2StrengthArray = SelectStrengthArray(motorSlider, stepsSlider, intensitySlider, false);

        // Send the maximum value from the selected arrays.
        int motor1Strength = motor1StrengthArray[motor1StrengthArray.Length - 1]; // Correction here
        int motor2Strength = motor2StrengthArray[motor2StrengthArray.Length - 1]; // Correction here

        SendData("start vibration," + motor1Strength.ToString() + "," + motor2Strength.ToString());
    }


    private int[] SelectStrengthArray(int motorSlider, StepsSlider stepsSlider, Intensity intensitySlider, bool isMotor1)
    {
        // Add your logic here to select the correct array based on the parameters.
        // This is a placeholder and needs to be replaced with your own logic.
        // For example, if motorSlider == 0, you might return motorStrength1_high, and so on.
        if (isMotor1 == true)
        {
            if (motorSlider == 1)
            {
                if (stepsSlider == StepsSlider.Fuenf)
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
                if (stepsSlider == StepsSlider.Fuenf)
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
            if (motorSlider == 2)
            {
                if (stepsSlider == StepsSlider.Fuenf)
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


    private void OnDestroy()
    {
        udpClient.Close();
    }
}