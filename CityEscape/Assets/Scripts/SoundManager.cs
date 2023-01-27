using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Ist für die Sounds zuständig. Hiebei wird die Musik als auch das Musiktempo im Spiel übernommen.
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
    //Initialisierung der Variablen und Starten der Musik über die Wwise Engine
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
     * Überprüfung, wann der Song zu Ende ist. wenn die gezählten Sekunden (variierend durch die BpM Anzahl) größer als die länge der Musik ist, wird die Musik gestoppt und nochmal von vorne gespielt.
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
     * Variiert das Tempo in der Wwise Engine. Wird in der x Achse um die anzahl add erhöht in der Wwise Engine. Zudem wird die BpM Anzahl für die Berechnung der Wiederholung der Musik erhöht.
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
