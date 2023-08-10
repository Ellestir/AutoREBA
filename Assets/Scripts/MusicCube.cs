using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCube : MonoBehaviour
{
    // This integer will be shown as a slider,
    // with the range of 1 to 5 in the Inspector
    private int REBA;


    //Volume 
    //Volume range as Slider in float characters

    //Volume Range 1
    //Volume Range setting REBA LEVEL 1 (1 Score)
    [Range(0.05f, 1f)]
    public float VOLUME1 = 0.05f;

    //Volume Range 2
    //Volume Range setting REBA LEVEL 2 (2-3 Score)
    [Range(0.05f, 1f)]
    public float VOLUME2 = 0.1f;

    //Volume Range 3
    //Volume Range setting REBA LEVEL 2 (4-7 Score)
    [Range(0.05f, 1f)]
    public float VOLUME3 = 0.15f;

    //Volume Range 4
    //Volume Range setting REBA LEVEL 2 (8-10 Score)
    [Range(0.05f, 1f)]
    public float VOLUME4 = 0.25f;

    //Volume Range 5
    //Volume Range setting REBA LEVEL 2 (11-15 Score)
    [Range(0.05f, 1f)]
    public float VOLUME5 = 0.35f;



    private int prev_REBA; //save previous REBA score 
    private int REBA_level; // save (previous) REBA level

    //Add AudioSources as a List to save multiple Audio Sources 
    private AudioSource[] sourcyList;


    //Save both AudioClips from 
    public AudioClip clippyA; //save first Audioclip ( elevator sound)
    private const float cliplengthA = 1f; //set length of the audioclip A in seconds
    public AudioClip clippyB; //save second Audioclip ( signal alert sound)
    private const float cliplengthB = 1f; //set length of the audioclip B in seconds


    //Intervall für Update
    private const float REBA_intervall = 1f; //Set time of the interval between the sounds in seconds 
    private float time_counter = 0; //count time passed


    //START PROGRAM
    void Start()
    {
        //da bis zu 3 mal widerholt --> pro Widerholung einer Audio Source
        sourcyList = GetComponents<AudioSource>(); //Audio Source übergeben <Generi> 
        //sourcy.volume = 0.2f; //f sonst denkt double
        for (int i = 0; i < 3; i++)
        {
            sourcyList[i].volume = 0.2f; //Volume setzen pro AudioSource

        }


    }

    // Update is called once per frame
    void Update()
    {
        // Reference on Class REBA_Score
        REBA = REBA_Score.Score;
        //Save Score in Levels (5 levels)
        if (REBA == 1) REBA_level = 1;
        if (REBA > 1 && REBA < 4) REBA_level = 2;
        if (REBA > 3 && REBA < 8) REBA_level = 3;
        if (REBA > 7 && REBA < 11) REBA_level = 4;
        if (REBA > 10 && REBA < 16) REBA_level = 5;
        //Update nur im bestimmten Intervall --> nach wie vielen sekunden immer updaten
        //alle 5 sekunden abfragen nach einem Upate 

        Debug.Log(" AUDIO SOURCE " + sourcyList[0].time); //print time of sourc list
        time_counter = time_counter + Time.deltaTime; //Difference Time from the last update is added to the counter


        //debug prints to set time right
        Debug.Log(time_counter);
        Debug.Log("REBA: " + REBA_level + " prev REBA: " + prev_REBA);

        //predefined queries
        bool change_REBA = prev_REBA != REBA_level; //look up if REBA Level is changing 
        bool any_audio_playing = sourcyList[0].isPlaying || sourcyList[1].isPlaying || sourcyList[2].isPlaying; //look up if any sound is still playing

        if ((time_counter > REBA_intervall && !any_audio_playing) || change_REBA)
        { //if time counter above the setted intervall and 
          //not any audio playing or if Reba changed (to set time between sounds and check up for changes)
            time_counter = 0; //reset counter (to start counting again)

            //stop loop of REBA 5 if Level is not 5 anymore
            if (REBA_level != 5) { sourcyList[0].loop = false; }

            //play sound REBA LEVEL 1
            if (REBA_level == 1)
            { //Check if REBA level == 1 
                //stop previous soundplaying 
                for (int i = 0; i < 3; i++) //for every Audio Source in SourcyList (3 Audiosources to allow 3 repetitions)
                {
                    sourcyList[i].Stop(); //stop previous sounds playing (for every 3 possible repetitions)
                    sourcyList[i].volume = VOLUME1; //set volume from Range VOLUME 1

                }

                sourcyList[0].clip = clippyA; //save Clip A in Audio source (only one because only one time playing)

                sourcyList[0].Stop(); // Stop sound after played
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5);  // set time until when it will play again 
            }


            //play sound REBA LEVEL 2
            if (REBA_level == 2)
            { //Check if REBA level == 2
                //stop previous soundplaying 
                for (int i = 0; i < 3; i++) //for every Audio Source in SourcyList (3 Audiosources to allow 3 repetitions)
                {
                    sourcyList[i].Stop(); //stop previous sounds playing (for every 3 possible repetitions)
                    sourcyList[i].volume = VOLUME2; //set volume from Range VOLUME 2
                }

                for (int i = 0; i < 2; i++) // 2 Audiosources used to make 2 repetition possible
                {
                    sourcyList[i].clip = clippyA; //save Clip A in Audio source List
                    sourcyList[i].Stop(); // Stop sound after played
                    sourcyList[i].PlayScheduled(AudioSettings.dspTime + i * cliplengthA + 0.5); // set time until when it will play again 
                    //i is Total time Past time 

                }
            }

            //play sound REBA LEVEL 3
            if (REBA_level == 3)
            { //Check if REBA level == 3
                //stop previous soundplaying 
                for (int i = 0; i < 3; i++) //for every Audio Source in SourcyList (3 Audiosources to allow 3 repetitions)
                {
                    sourcyList[i].Stop(); //stop previous sounds playing (for every 3 possible repetitions)
                    sourcyList[i].volume = VOLUME3; //set volume from Range VOLUME 3

                }

                for (int i = 0; i < 3; i++) // 3 Audiosources used to make 3 repetition possible
                {
                    sourcyList[i].clip = clippyA; //save Clip A in Audio source List
                    sourcyList[i].Stop(); // Stop sound after played
                    sourcyList[i].PlayScheduled(AudioSettings.dspTime + i * cliplengthA + 0.5); //set time until when it will play again 
                    //i is Total time Past time 

                }
            }

            //play sound REBA LEVEL 4
            if (REBA_level == 4)
            { //Check if REBA level == 4
                //stop previous soundplaying
                for (int i = 0; i < 3; i++) //for every Audio Source in SourcyList (3 Audiosources to allow 3 repetitions)
                {
                    sourcyList[i].Stop(); //stop previous sounds playing (for every 3 possible repetitions)
                    sourcyList[i].volume = VOLUME4; //set volume from Range VOLUME 4

                }

                sourcyList[0].clip = clippyB; //save Clip B in Audio source List
                sourcyList[0].Stop(); //Stop sound after played
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5); //set time until when it will play again 
            }


            //play sound REBA LEVEL 4
            if (REBA_level == 5)
            { //Check if REBA level == 5
                ///stop previous soundplaying
                for (int i = 0; i < 3; i++) //for every Audio Source in SourcyList (3 Audiosources to allow up to 3 possible repetitions)
                {
                    sourcyList[i].Stop(); //stop previous sounds playing (for every 3 possible repetitions)
                    sourcyList[i].volume = VOLUME5; //set volume from Range VOLUME 5

                }

                sourcyList[0].clip = clippyB; //save Clip B in Audio source List
                sourcyList[0].Stop(); //Stop sound after played

                //Set Loop
                sourcyList[0].loop = true;// Check the loop box in Unity
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5); //set time until when it will play again (buffer)
                Debug.Log("SPIELE 5: " + time_counter); //testing
            }

            // Save previous REBA Level to compare 
            prev_REBA = REBA_level;

        }

    }
}
//}
