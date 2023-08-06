using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(VisualFeedback))]
public class VisualFeedbackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        VisualFeedback visualFeedback = (VisualFeedback)target;

        // Draw the default inspector
        DrawDefaultInspector();
        // Draw rebaBarEnabled toggle
        visualFeedback.rebaBarEnabled = EditorGUILayout.Toggle("Reba Bar Enabled", visualFeedback.rebaBarEnabled);

        // Disable GUI for rebaBar if rebaBarEnabled is false
        GUI.enabled = visualFeedback.rebaBarEnabled;

        // Draw RebaBar field
        visualFeedback.rebaBar = (RebaBar)EditorGUILayout.ObjectField("Reba Bar", visualFeedback.rebaBar, typeof(RebaBar), true);

        // Ensure GUI is enabled before drawing RebaScoreTextEnabled toggle
        GUI.enabled = true;
        visualFeedback.RebaScoreTextEnabled = EditorGUILayout.Toggle("Reba Score Text Enabled", visualFeedback.RebaScoreTextEnabled);

        // Enable GUI for rebaScoreText if RebaScoreTextEnabled is true
        GUI.enabled = visualFeedback.RebaScoreTextEnabled;

        // Draw RebaScoreText field
        DrawRebaScoreTextField(visualFeedback);

        // Ensure GUI is enabled before drawing RebaScoreNumberEnabled toggle
        GUI.enabled = true;
        visualFeedback.RebaScoreNumberEnabled = EditorGUILayout.Toggle("Reba Score Number Enabled", visualFeedback.RebaScoreNumberEnabled);

        // Enable GUI for rebaScoreNumber if RebaScoreNumberEnabled is true
        GUI.enabled = visualFeedback.RebaScoreNumberEnabled;

        // Draw RebaScoreNumber field
        DrawRebaScoreNumberField(visualFeedback);

        // Disable GUI for SamFaceEnabled if ImageEnabled is true, otherwise enable it
        GUI.enabled = !visualFeedback.ImageEnabled;
        visualFeedback.SamFaceEnabled = EditorGUILayout.Toggle("Sam Face Enabled", visualFeedback.SamFaceEnabled);

        // Draw SVGImage fields
        DrawSVGImageFields(visualFeedback);

        // Disable GUI for ImageEnabled if SamFaceEnabled is true, otherwise enable it
        GUI.enabled = !visualFeedback.SamFaceEnabled;
        visualFeedback.ImageEnabled = EditorGUILayout.Toggle("Image Enabled", visualFeedback.ImageEnabled);

        // Draw ExtraImage field
        DrawExtraImageField(visualFeedback);

        // Re-enable the GUI
        GUI.enabled = true;
    }





    private void DrawSVGImageFields(VisualFeedback visualFeedback)
    {
        visualFeedback.rebaImage = (SVGImage)EditorGUILayout.ObjectField("Reba Image", visualFeedback.rebaImage, typeof(SVGImage), true);
        visualFeedback.level1Sprite = (Sprite)EditorGUILayout.ObjectField("Level 1 Sprite", visualFeedback.level1Sprite, typeof(Sprite), true);
        visualFeedback.level2Sprite = (Sprite)EditorGUILayout.ObjectField("Level 2 Sprite", visualFeedback.level2Sprite, typeof(Sprite), true);
        visualFeedback.level3Sprite = (Sprite)EditorGUILayout.ObjectField("Level 3 Sprite", visualFeedback.level3Sprite, typeof(Sprite), true);
        visualFeedback.level4Sprite = (Sprite)EditorGUILayout.ObjectField("Level 4 Sprite", visualFeedback.level4Sprite, typeof(Sprite), true);
        visualFeedback.level5Sprite = (Sprite)EditorGUILayout.ObjectField("Level 5 Sprite", visualFeedback.level5Sprite, typeof(Sprite), true);
    }

    private void DrawRebaScoreTextField(VisualFeedback visualFeedback)
    {
        visualFeedback.rebaScoreText = (TextMeshProUGUI)EditorGUILayout.ObjectField("Reba Score Text", visualFeedback.rebaScoreText, typeof(TextMeshProUGUI), true);
    }

    private void DrawRebaScoreNumberField(VisualFeedback visualFeedback)
    {
        visualFeedback.rebaScoreNumber = (TextMeshProUGUI)EditorGUILayout.ObjectField("Reba Score Number", visualFeedback.rebaScoreNumber, typeof(TextMeshProUGUI), true);
    }
    private void DrawExtraImageField(VisualFeedback visualFeedback)
    {
        visualFeedback.extraImage = (Image)EditorGUILayout.ObjectField("Extra Image", visualFeedback.extraImage, typeof(Image), true);
    }

}

