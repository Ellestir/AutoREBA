using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Kalibrierung))]
public class VibrationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Zeige den Standard-Inspector
        DrawDefaultInspector();

        // Hole eine Referenz auf das Vibration-Script
        Kalibrierung kalibrierung = (Kalibrierung)target;

        // Wenn der Button geklickt wird, führe die Funktion "OnButtonPress" aus
        if (GUILayout.Button("Vibration testen"))
        {
            kalibrierung.OnButtonPress();
        }
    }
}
