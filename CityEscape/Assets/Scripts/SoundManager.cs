using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Ist f�r die Sounds zust�ndig. Hiebei wird die Musik als auch das Musiktempo im Spiel �bernommen.
 */
public class SoundManager : MonoBehaviour
{
    private float tempo;
	public float add;
    public float songTime;
    private float songTimeChecker;
    private float bpM;
    private float totalBpM;
    private float wasteBpM;
    private float round;
    private uint id;
    private float factor;
   
   

    // Start is called before the first frame update
    //Initialisierung der Variablen und Starten der Musik �ber die Wwise Engine
    void Start()
    {
        factor = 0.44f;
        wasteBpM = 0f;
        totalBpM = 270f;
        tempo = 0f;
        AkSoundEngine.SetRTPCValue("Tempo",tempo);
        id=AkSoundEngine.PostEvent("medmus1", gameObject);
        songTimeChecker = songTime;
        bpM = 190f;
        round = 0;
    }

    // Update is called once per frame
    /**
     * �berpr�fung, wann der Song zu Ende ist. wenn die gez�hlten Sekunden (variierend durch die BpM Anzahl) gr��er als die l�nge der Musik ist, wird die Musik gestoppt und nochmal von vorne gespielt.
     */
    void Update()
    {
        Debug.Log(wasteBpM);

        wasteBpM = wasteBpM + Time.deltaTime *(bpM / 90);

        //Debug.Log(songTimeChecker*(90f/180f));
       if (wasteBpM>songTime)
        {
            wasteBpM = 0;
            AkSoundEngine.StopPlayingID(id);
            id = AkSoundEngine.PostEvent("medmus1", gameObject);
        }

    }


    /**
     * Variiert das Tempo in der Wwise Engine. Wird in der x Achse um die anzahl add erh�ht in der Wwise Engine. Zudem wird die BpM Anzahl f�r die Berechnung der Wiederholung der Musik erh�ht.
     */
	public void AddTempo(){

        
        tempo = tempo + add;

        if (tempo > 99f)
        {

            tempo = 99f;
            bpM = 40f;
            AkSoundEngine.SetRTPCValue("Tempo", tempo);
        }
		else{
        bpM = bpM - 0.2f;
            
        AkSoundEngine.SetRTPCValue("Tempo",tempo);
        
		}
	}

    //Die Musik im Spiel wird gestoppt
    public void StopMusic()
    {
        id = AkSoundEngine.PostEvent("medmus1", gameObject);
        AkSoundEngine.StopPlayingID(id);
    }

}
