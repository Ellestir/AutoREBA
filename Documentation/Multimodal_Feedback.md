# AutoREBA - Multimodal Feedback Module

## Overview:
The AutoREBA project signifies a significant advancement in the realm of human-computer interaction, 
particularly within the mixed reality domain. The project's main objective is to enhance posture ergonomics. 
To achieve this, a Multimodal Biofeedback system is employed that provides users with immersive feedback about 
their posture based on their REBA score.

In this README, we introduce the key components of the AutoREBA system, all of which revolve around the REBA score:

- **Vibration Feedback**: Using Arduino, a vibration motor is controlled which vibrates based on the user's REBA score, offering haptic feedback about the current posture.
- **Visual Feedback**: Dynamic visual indicators inform the user in real-time about their REBA score, providing cues about the current posture.
- **Auditory Feedback**: Auditory signals, matched to the user's REBA score, inform about the posture.
- **Arduino Communication**: The interface between software and hardware. The Arduino controls the vibration motor based on the REBA score, providing tactile feedback.
## Visual Feedback
### Overview
The Multimodal Feedback module is at the heart of our approach. It facilitates seamless communication between the Arduino Nano IoT and Unity, delivering comprehensive feedback to the user through a VR headset based on their REBA score.

The visual feedback utilizes various visualizations, like the REBA-Bar and SAM faces, all of which dynamically adapt to the user's REBA score. The primary goal of these mechanisms is to provide users with an intuitive means to understand their REBA score and adjust their posture in real-time accordingly.

### Components:
#### 1- REBA-Bar:
- Represents the user's current REBA score, ranging from values 1 to 15.
- **Invertible Direction**: Within Unity, there's an option to invert the direction of the REBA-Bar. This means the bar can fill in the opposite direction, offering the user a different visual representation based on their preference.
- **Color Indication**: The bar's color varies with the score: 1 (Green), 2-3 (Yellow), 4-7 (Orange), 8-10 (Red), and 11-15 (Dark Red).
#### 2- REBA-Number & REBA-Score Text:
- **REBA-Text**: Depending on the REBA score, a pertinent message is showcased:
  - 1: "Negligible risk, no action required"
  - 2-3: "Low risk, change may be needed"
  - 4-7: "Medium risk, further investigation, change soon"
  - 8-10: "High risk, investigate and implement change"
  - 11-15: "Very high risk, implement change"
- **REBA-Number**: Visual cues that modulate in size and hue based on the posture's calibre, furnishing deeper posture insights.
#### 3- SELF-ASSESSMENT MANIKIN (SAM):
- SAM, a non-verbal rating system, plays a crucial role in our feedback mechanism.
- **Why SAM?**: SAM presents an intuitive mode to pictorially portray posture quality. By leveraging universally understood facial illustrations, SAM's feedback transcends
linguistic confines, rendering it accessible to a broader spectrum of users.
- **Why SVGs with SAM?**: SAM employs SVG images to ensure pristine image quality, irrespective of scaling, offering users sharp and clear feedback visuals.
his accentuates the feedback's effectiveness and user comprehension.
- **Visual Representation**: SAM adopts images of figures (manikins) over words to convey emotions.
- **Faces Spectrum**: SAM spans five distinct faces delineating emotions from very negative to very positive. These faces signify levels 1 through 5, elucidating varied emotional feedback.
- **Application in AutoREBA**: AM endows users with intuitive feedback concerning their posture, thus enhancing their posture cognizance.
#### 4- Extra-Image:
- Users can seamlessly embed their personal images, infusing a bespoke touch to the feedback process.

### Configuration & Usage:
After adding the "RebaBar" and "VisualFeedback" scripts to a GameObject in Unity, it's crucial to provide the necessary references in the Unity Editor.
Figure (XXXX) illustrates the designated section within the Unity Inspector where these assignments can be made.
It's paramount to ensure that the required sprites and text references are correctly mapped as depicted in this figure.

### Dependencies:
#### 1- **Vector Graphics Version 2.0.0**:
- Vital for the impeccable rendering of SAM SVGs and the ExtraImage feature. This package is currently in its experimental phase.
- Integrate using the identifier com.unity.vectorgraphics.
#### 2- **TextMeshPro 3.0.6**:
- A sophisticated text solution tailored for Unity. It seamlessly replaces Unity's UI Text and the legacy Text Mesh.
- Integral in our module for the RebaText and RebaNumber elements.
