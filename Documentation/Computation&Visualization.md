# AutoREBA - Computation & Visualization

## Overview:
In the Computation and Visualization group, we focus on calculating the so-called REBA score. The Rapid Entire Body Assessment (REBA) is a method for evaluating body posture and movement. It was developed to assess the risk of musculoskeletal disorders at work. The method is based on observing body posture, movement, force requirements, and muscle activity.

The REBA method consists of two parts: the upper and lower body. Each part is evaluated separately and then added together to determine the overall risk. The assessment scale ranges from 1 (low risk) to 15 (very high risk). Therefore, we use the angles between the limbs to determine whether users of a VR headset have a risky posture and report this to the users in real-time using the work of the Multimodal Feedback through visual, auditory, and vibration-based feedback.

Figure 1:
<p align="center">
  <br>
  <img src="./Images/Computation&Visualization/OptitrackController.png" alt="OptitrackController.png" width="800" />
  <br>
  Figure 1: Inspector of the Optitrack Controller
</p>
The Optitrack_Client, which holds the Optitrack Straming Client Script, is used for the communication between Unity and Motive. Before the Application is started the Server and Client credentials must be updated to the corresponding IP-Addresses, which can be seen in the Motive settings in Figure 2.

Figure 2:
<p align="center">
  <br>
  <img src="./Images/Computation&Visualization/MotiveStreaming.png" alt="MotiveStreaming.png" width="800" />
  <br>
  Figure 2: Settings in Motive 
</p>
The Optitrack Skeleton Animator Skript needs to be attached to the Avatar Game Object. The "Skeleton Asset Name" has to be the same name as it is in Motive. DAZ-Studio 3D Avatar is used, which needs to be configured as humanoid, thus the mapping needs to be draged into the "Destination Avatar" space. 

Figure 3:
<p align="center">
  <br>
  <img src="./Images/Computation&Visualization/Gen9AvatarInspector.png" alt="Gen9AvatarInspector.png" width="800" />
  <br>
  Figure 3: Avatar Game Object 
</p>

Figure 4:
<p align="center">
  <br>
  <img src="./Images/Computation&Visualization/InspectorRebaController.png" alt="InspectorRebaController.png" width="800" />
  <br>
  Figure 4: Reba-Controller
</p>

If you want to log the limb scores and the corresponding table scores into a csv-file you need to tick the "Log Scores CSV" box. The CSV Log File is saved in %PojectFile%/Logs/AutoREBALogFile.csv. Furthermore there are the options to print the angles and/or scores into the console. The "threshold" variable is used to determin at which angle a limb is considered twisted, sided or bend. The "Window Size" determines how large the rolling window is supposed to be, i.e. how many frames are onsidered in the averiging of the score.

## Addressing the Challenges:

As we delved into the project, we encountered a multitude of challenges, each requiring innovative solutions and adaptability. These challenges provided valuable insights and paved the way for refining our methodologies. In the following section, we will outline the primary challenges we faced and the solutions we implemented to overcome them.

### Challenge 1:
One of our initial challenges was the general calculation of angles. Our first strategy employed local rotations for all body parts. Ultimately, we integrated quaternions for a more refined approach.

### Challenge 2:
Determining whether a part is side-bent or twisted posed its own set of challenges. We initially used a single bone for reference. However, after some iterations, we averaged the euler angles of multiple bones. The final solution incorporated the Quaternion.Slerp() function to average rotations. The REBA-PDF lacked clear guidelines on this, so we added a customizable threshold variable to address this ambiguity.

### Challenge 3:
Another complication arose when determining side-bends and twists simultaneously. The solution we implemented resets the score if it goes out of a defined range.

### Challenge 4:
The positioning and movement of arms in mixed reality simulations brought up distinct challenges. Specifically, there was a lack of concrete guidelines for defining arm abduction. To address this, we used the 'spine4' as a reference point and calculated the angle between the upper arm and 'spine4'. Introducing a threshold allowed us to more accurately determine arm abduction. Another problem we faced was the inversion of y & x coordinates for the left and right arms. To rectify this, we employed both 0° and 360° as starting points for angle calculations.

### Challenge 5:
We sought to account for the position of raised shoulders using local positions. However, we observed that these positions did not register significant changes. Due to time constraints, a definitive solution could not be found. Additionally, there was an issue with the wrist scoring as the mapping led to high deviation. We introduced a threshold for determining when a wrist was twisted or side-bended to improve accuracy.

### Challenge 6:
Defining actions like walking and sitting in mixed reality environments posed more challenges. For walking, we resolved the issue by adding colliders to the feet and ground. However, we ran into another problem with the avatar appearing to float, caused by a mismatch between 'motive' and 'unity' mappings. Adjusting the ground level in Unity addressed this. When it came to defining a seated posture, the current scoring was not satisfactory due to the angle of the legs and, once again, a lack of clear guidelines in the REBA-PDF.

### Challenge 7:
User feedback indicated that there was an overloading of biofeedback, which could confuse users or potentially overload the Arduino. We recognized the necessity of filtering the feedback for better user comprehension. Our solution was to implement a rolling window combined with an exponential moving average, allowing for smoother and more understandable feedback to the user.

### Challenge 8:
The averaging of the REBA score sometimes resulted in missing out on extreme values. Our solution was the application of a peak detection algorithm using the first derivative test. Detected peaks are then multiplied by a constant, termed "peak sensitivity," to adjust the sensitivity of the detection.

## Contributors:

- [Jonas Scheffner](https://github.com/jonasscheffner)
- [Luke Werle](https://github.com/Luke-Werle-99)
- [Frederik Wiemer](http://github.com/FreddyOs)
