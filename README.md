# AutoREBA

## Project Description:
People in virtual and mixed reality spaces tend to worsen their posture, likely because of decreased awareness of their bodies inside of these simulations.
With the upcoming shift to implementing these technologies at workspaces, humans will spend an increasing amount of time using virtual reality applications.
In order to minimise the damage caused by suboptimal posture in these situations, we started: AutoREBA (Automatic Rapid Entire Body Assessment Score) a significant project in the field of human-computer interaction. 
The aim of this project is to address a research question related to posture ergonomics in Mixed Reality. The overarching theme of the project is "Multimodal Biofeedback for Enhancing Posture Ergonomics in Mixed Reality."

The project involves approximately 13 students working collaboratively to answer the research question. The team comprises four groups, each consisting of three members and a project manager. The groups are divided as follows:
- [Computation and Visualization](Documentation/Computation&Visualization.md)
- [Multi Device Communication](Documentation/Multi_Device_Communication.md)
- [Multimodal Feedback](Documentation/Multimodal_Feedback.md)
- [Ground Truth](Documentation/Ground_Truth.md)

## Requirements:

## Requirements:
To run the project, the following components are required:
Unity (Version 2021.3.26f1) 
VR Device and software
Optitrack
Further dependencies are specified in the respective directory of the component.

## Installation and Use:
Set up your virtual reality device
Clone the repository to your local computer: 
Go to the desired folders location
Open the command line and type:
git clone https://github.com/Ellestir/AutoREBA
Open the project in Unity 


## Contributors:
- [Patricia Maria Bombik](http://github.com/PatPatDango)
- [Albin Hoti](http://github.com/albinh55)
- [Pouya Nikbakhsh](http://github.com/pouya-nik)
- [Tom Roman Rosenberger](http://github.com/Ellestir)
- Jonas Scheffner
- [Luke Werle](https://github.com/Luke-Werle-99)
- [Frederik Wiemer](http://github.com/FreddyOs)
- [Nassim Laaraj](https://github.com/Nassim795)
- [Nabil Akir](https://github.com/nabil-ak)
- [Ibtehal Al-Omari](https://github.com/ib1907)
- [Jessica Brandl](https://github.com/JessBrandl)
- [Vito Costa](https://github.com/VitoCostaaa)
- [Cem Dogan](https://github.com/DoganCem)

## Implementation Details

## Der REBA Score:
The Rapid Entire Body Assessment (REBA) was developed to “rapidly” evaluate risk of musculoskeletal disorders (MSD) associated with certain job tasks. 




It consists of a scoring system from 1-15 and splits these into 5 subgroups as seen in the image above with 1 being negligible risk and 11+ being very high risk, implement change. 

In order to calculate the score one must have a video or a picture of a person, ideally one that is doing a task or job. Following this one will look at six different body parts to evaluate; Neck, trunk, legs, upper arm, lower arm and the wrists. 
For each body part there are a number of different evaluations to do to determine its subscore. For example, the trunk varies in score from 1 - 5, The angle of the upper body in comparison to the upright position can be a score from 1-4, +1 for a perfect straight trunk, and +4 for a trunk bending forward 60 degrees or more. Additionally 1 is added if the upper body is twisted or side bending, leading to a possible maximum subscore of 5.

After all 6 body parts have been assessed, it is also necessary to note down the “force load”, if a good grip was found via the “coupling score” and which activity the person in question had potentially been doing for the past minute or more for the “activity score”.
The Force Load is implemented if the person was holding something of a certain weight, 0-10 lbs is a score of +0, 11-22 lbs is +1 and 23+ is +2. And additionally +1 if there is shock or a rapid buildup of force
Coupling defines the hand hold or grip the person has on the item they are holding, ranging from good over fair to poor. 
The Activity Score will need more knowledge on the person's movement over time than a screenshot offers. It adds score if one or more body parts were held for longer than one minute, if there were repeated actions or there was a large range change in posture. 

To calculate the final Score using the subscores Table A, Table B and Table C will be necessary. 

From Table A one can get a score from 1 - 9. Onto this one adds the Force Load Score of max 3, leading to Score A having the range of 1-12.


From Table B one can get a score from 1-9 also. Onto this one adds the Coupling Score of max 3, leading to Score B having the range of 1-12. 



Score A and Score B is used in Table C to determine Score C, which ranges from 1-12.

Onto Score C one must add the Activity Score which ranges from 1-3 to end up at the final REBA Score. 


## Project Conclusion:

Wrapping up our journey with the REBA-Score and the Optitrack system, it's clear that this project was both a challenge and a fantastic learning experience. With 13 of us, divided into four focused groups, the organisation was a tremendous task.
We each had our own set of hurdles to overcome. Whether it was setting up Optitrack with the VR environment or fixing the code until the REBA-Score feedback was spot on, there were moments that truly tested our mettle.

But here's the thing: amidst these challenges, we learned a ton about teamwork and organization. The diverse skills in our team not only made problem-solving more dynamic but also made the process genuinely enjoyable. We shared knowledge, supported each other, and celebrated our small successes along the way.

We're genuinely excited about the potential of our work. Implementing posture feedback in virtual and mixed reality could be a game-changer for many. We're hopeful that our efforts will pave the way for real-world applications that can make a positive impact on people's lives.

In retrospect, while we faced our fair share of challenges. We've grown as a team, enjoyed the process, and are proud of our collective output. Here's to our work serving as a foundation for future innovations and truly making a difference!
