using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCube : MonoBehaviour
{
        // This integer will be shown as a slider,
    // with the range of 1 to 5 in the Inspector
    private int REBA;


    //Volume 
    //Volume range alle Stufen 
    [Range(0.1f, 1f)]
    public float VOLUME1;

    //Volume Range 2
    [Range(0.1f, 1f)]
    public float VOLUME2;

    //Volume Range 3
    [Range(0.1f, 1f)]
    public float VOLUME3;

    //Volume Range 4
    [Range(0.1f, 1f)]
    public float VOLUME4;

     //Volume Range 5
    [Range(0.1f, 1f)]
    public float VOLUME5;



    private int prev_REBA; //previous Reba score zwischenspeichern
    private int REBA_level; //previous Reba score zwischenspeichern

    //Add AudioSources für das mehrfache abspielen
    private AudioSource[] sourcyList;
    

    //Beide Clips 
    public AudioClip clippyA; //neuer Audio clip hinzufügen (für den 1. sound - gaming)
    private const float cliplengthA = 1f; //länge des Clippes A definieren
    public AudioClip clippyB; //neuer Audio clip hinzufügen (für den 2. sound - elevator)
    private const float cliplengthB = 1f; //länge des Clippes B definieren


    //Intervall für Update
    private const float REBA_intervall = 1f; //sekunden - intervall zwischen den einzelnen REBAs
    private float time_counter = 0; //zählen wie viel Zeit schon vergangen ist
    

    void Start()
    {
        //da bis zu 3 mal widerholt --> pro Widerholung einer Audio Source
        sourcyList = GetComponents<AudioSource>() ; //Audio Source übergeben <Generi> 
        //sourcy.volume = 0.2f; //f sonst denkt double
        for (int i = 0; i < 3; i++)
        {
            sourcyList[i].volume = 0.2f; //Volume setzen pro AudioSource

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        REBA = REBA_Score.Score;
        //SCORE IN STUFEN ABWANDELN
        if (REBA ==1) REBA_level = 1; 
        if (REBA > 1 && REBA < 4) REBA_level = 2; 
        if (REBA > 3 && REBA < 8) REBA_level = 3;
        if (REBA > 7 && REBA < 11) REBA_level = 4;
        if (REBA > 10 && REBA < 16) REBA_level = 5;
        //Update nur im bestimmten Intervall --> nach wie vielen sekunden immer updaten
        //alle 5 sekunden abfragen nach einem Upate 
        
        Debug.Log(" AUDIO SOURCE " + sourcyList[0].time);
        time_counter = time_counter+Time.deltaTime; //diffenrenz Time vom letzten Update wird zum Counter aufaddiert


        Debug.Log(time_counter);
        Debug.Log("REBA: " +  REBA_level + " prev REBA: " + prev_REBA);

        //vordefinierte Abfragen
        bool change_REBA = prev_REBA!= REBA_level; //wenn REBA sich ändert --> TRUE 
        bool any_audio_playing = sourcyList[0].isPlaying || sourcyList[1].isPlaying || sourcyList[2].isPlaying; //schauen ob sound noch läuft
      
        if ((time_counter>REBA_intervall && !any_audio_playing) || change_REBA){
            time_counter = 0; //nach 5 sekunden ausführen und direkt auf 0 wieder setzen == Reset 

            //gucken ob sich was geändert hat 
                //geändert --> stoppen 
                //wenn es sich nicht geändert hat --> länge festlegen wie lange REBA gespielt wird 
            if (REBA_level != 5){sourcyList[0].loop = false;}

            //REBA LEVEL 1
            if(REBA_level == 1){
                //alte vorher stoppen
                for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].Stop();
                    sourcyList[i].volume = VOLUME1; //Volume setzen pro AudioSource

                }

                sourcyList[0].clip = clippyA;
                
                sourcyList[0].Stop();
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5);  //play --> sonst durch update die ganze zeit aufgerufen 
            }

            //REBA LEVEL 2
            if(REBA_level == 2 ){
                //alte vorher stoppen
                for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].Stop();
                    sourcyList[i].volume = VOLUME2; //Volume setzen pro AudioSource

                }

                 for (int i = 0; i < 2; i++)
                 {
                    sourcyList[i].clip = clippyA;
                    sourcyList[i].Stop();
                    sourcyList[i].PlayScheduled(AudioSettings.dspTime + i*cliplengthA + 0.5); 

                }
            }

            //REBA LEVEL 3
            if(REBA_level == 3){
                //alte vorher stoppen
                for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].Stop();
                    sourcyList[i].volume = VOLUME3; //Volume setzen pro AudioSource

                }

                 for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].clip = clippyA;
                    sourcyList[i].Stop();
                    sourcyList[i].PlayScheduled(AudioSettings.dspTime + i*cliplengthA + 0.5); 

                }
            }

            //REBA LEVEL 4
            if(REBA_level == 4){
                //alte vorher stoppen
                for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].Stop();
                    sourcyList[i].volume = VOLUME4; //Volume setzen pro AudioSource

                }

                sourcyList[0].clip = clippyB;
                sourcyList[0].Stop();
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5); 
            }
        
    
            //REBA LEVEL 5
            if(REBA_level == 5){
                //alte vorher stoppen
                for (int i = 0; i < 3; i++)
                 {
                    sourcyList[i].Stop();
                    sourcyList[i].volume = VOLUME5; //Volume setzen pro AudioSource

                }

                sourcyList[0].clip = clippyB;
                sourcyList[0].Stop();
                //Loop setzen --> In Unity Hacken
                sourcyList[0].loop = true;
                sourcyList[0].PlayScheduled(AudioSettings.dspTime + 0.5); 
                Debug.Log("SPIELE 5: " + time_counter);
            }

            //prev_Reba sichern nach einem Durchlauf
            prev_REBA = REBA_level;
        
        }
        
    }
}
//}
