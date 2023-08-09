using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Vib_Kalibrierung))]
public class VibrationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Zeige den Standard-Inspector
        DrawDefaultInspector();

        // Hole eine Referenz auf das Vibration-Script
        Vib_Kalibrierung kalibrierung = (Vib_Kalibrierung)target;

        // Wenn der Button geklickt wird, führe die Funktion "OnButtonPress" aus
        if (GUILayout.Button("Vibration testen"))
        {
            kalibrierung.OnButtonPress();
        }
    }
}
