# AutoREBA - Ground Truth

## Overview:
In order to assess the individual body parts and their respective scores we used the official ErgoPlus’ website and PDF document “A Step-by-Step Guide to the REBA Assessment Tool” 
This included explanations, pictures and the tables used to determine the scores that make up the final REBA score. 
<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: REBA Bild
</p>

As the website outlines, we took multiple pictures and videos to assess the posture accurately. 
Firstly we took videos from two different angles from which we took screenshots and noted down their timestamps within the video. 
Additionally we used Motive, a motion capture program with which we could track the target's movement precisely and from multiple angles at once. To achieve this the person to be measured needed to wear a full body suit with reflective knobs that the setup cameras around the room could recognise. The accurate position of the knobs on the person was of great importance due to the program needing this precision to better follow the person's movement. 

## The REBA Score

The Rapid Entire Body Assessment (REBA) was developed to “rapidly” evaluate risk of musculoskeletal disorders (MSD) associated with certain job tasks. 

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: MSD Risk
</p>

It consists of a scoring system from 1-15 and splits these into 5 subgroups as seen in the image above with 1 being negligible risk and 11+ being very high risk, implement change. 

In order to calculate the score one must have a video or a picture of a person, ideally one that is doing a task or job. Following this one will look at six different body parts to evaluate; Neck, trunk, legs, upper arm, lower arm and the wrists. 
For each body part there are a number of different evaluations to do to determine its subscore. For example, the trunk varies in score from 1 - 5, The angle of the upper body in comparison to the upright position can be a score from 1-4, +1 for a perfect straight trunk, and +4 for a trunk bending forward 60 degrees or more. Additionally 1 is added if the upper body is twisted or side bending, leading to a possible maximum subscore of 5.

After all 6 body parts have been assessed, it is also necessary to note down the “force load”, if a good grip was found via the “coupling score” and which activity the person in question had potentially been doing for the past minute or more for the “activity score”.
The Force Load is implemented if the person was holding something of a certain weight, 0-10 lbs is a score of +0, 11-22 lbs is +1 and 23+ is +2. And additionally +1 if there is shock or a rapid buildup of force
Coupling defines the hand hold or grip the person has on the item they are holding, ranging from good over fair to poor. 
The Activity Score will need more knowledge on the person's movement over time than a screenshot offers. It adds score if one or more body parts were held for longer than one minute, if there were repeated actions or there was a large range change in posture. 

To calculate the final Score using the subscores Table A, Table B and Table C will be necessary. 

From Table A one can get a score from 1 - 9. Onto this one adds the Force Load Score of max 3, leading to Score A having the range of 1-12.

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Table A
</p>
From Table B one can get a score from 1-9 also. Onto this one adds the Coupling Score of max 3, leading to Score B having the range of 1-12. 

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Table B
</p>
Score A and Score B is used in Table C to determine Score C, which ranges from 1-12.

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Table C
</p>

Onto Score C one must add the Activity Score which ranges from 1-3 to end up at the final REBA Score. 

## Motive
Motive is an optical motion capture program, capable of connecting to cameras which detect certain reflective sensors that form shapes and bodies. We used Motive to record our movements and evaluate them later using REBA. To achieve this we first had to place reflective markers on the person to be recorded. 

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Skeleton Body Front
</p>

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Skeleton Body Back
</p>

49 markers are required to be able to detect and record a person properly without issue. The white markers indicate which knobs need to be exact on the respective muscle/bone. The purple markers indicate which knobs can vary in position due to the person's height or other physical traits. After successfully attaching them the knobs are to be marked and can then be set as a Skeleton body.

<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: Motive Body with box and table
</p>

Also visible here are other objects we imported such as a table and a box. This follows the same procedure as before with attaching markers and then importing them into Motive, this time using the Rigid Body type.

### Word explanations:

## Process:

### 1. Planning:

### 2. Test Recording:

### 3. Refinement:

### 4. Final Recording:

### 5. Evaluation:

## Examples:

### Good Posture:

This posture we ended up giving a perfect score of 1. We evaluated the neck as having a subscore of 1 as it is barely bent forward if it all, it is not twisted or bent. Whether it is bent may be controversial but as the official page does not mention any angles we decided here that this is not bent enough to add +1. 
<p align="center">
  <br>
  <img src="./Images/Ground_Truth/leeresImage.png" alt="leeresImage.png" width="500" />
  <br>
  Figure 1: REBA Bild
</p>
Similarly, the trunk is straight, not twisted or bent. Leading to a score of +1. 

The legs are straight and he is standing on both legs equally, leading to a score of +1.

Force load is 0 due to the box he is holding being below 11 lbs. 

The upper arm is at a slight angle but not more than 20 degrees, so it is +1. The shoulders are not raised, the arm is not abducted and he is not leaning. 

The lower arm is at an angle of ~90 degrees leading to a score of +1. 

Wrists look to be straight and not bend or twisted, so +1. 

Coupling looks to be a little awkward, as no handles are present, but we deemed it possible so +2. 

Activity score wise the movement was picking up a light box once. So neither the repeated actions nor the held body parts for longer than a minute apply. 
Picking up a box also does not cause a rapid large range change in posture and the base was stable. Thus +0 for the activity score. 

Inputting the subscores into the given tables and adding the coupling score and force load gives a Score A of 1 and a Score B of 1. Inputting these into Table C gives a Score of 1, adding onto the +0 of the activity score leaves it at a 1, giving an end score of 1.

### Bad Posture:

### Further examples:
#### Twisted Trunk:
#### Abducted Arm:
#### Side bending Neck:
#### Raised Leg:
#### Coupling Score:
#### Activity Score:

## Problems:

## Conclusion

## Contributors:
- [Jessica Brandl](https://github.com/JessBrandl)
- [Vito Costa](https://github.com/VitoCostaaa)
- [Cem Dogan](https://github.com/DoganCem)
