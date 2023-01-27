using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Kümmert sich um die veränderung der UI Und handeln der jeweiligen szenen
/// </summary>
public class UI : MonoBehaviour
{

    public Text goal;
	
    public Text hit;
	public Text wallText;
	public Text counterText;
	private SceneManager sceneManager;
	private int hitcounter;
    private int runde;
	private float currentTime;

    //Initialisierung der Werte
    void Start()
    {
        runde = 2;
		currentTime=2f;
		hitcounter=0;
        goal.text = "";
        hit.text = "";
		wallText.text="3";
		counterText.text="HitCounter:"+hitcounter;
    }

    //Zählt den timer runter
	void Update(){
	currentTime -= 1 * Time.deltaTime;
	}

    //Beendet das Spiel. Debuggen notwendig
    public void Quit()
    {
        Application.Quit();
    }


    //Neustart des Spiels. Debuggen notwendig
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Geht zur hauptmenü szene
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Geht zum studienlevel
    public void GoToStudyScene()
    {
        SceneManager.LoadScene("StudyScene");
    }


    //Geht zum zweiten studienlevel
    public void GoToStudyScene2()
    {
        SceneManager.LoadScene("StudyScene2");
    }


    /// <summary>
    /// Erhöhung hitcoutner und anzeigen eines hits
    /// </summary>
    public void MessageHit()
    {
		if(currentTime<=0f){
        StartCoroutine(Hit());
		hitcounter++;
		counterText.text="HitCounter:"+hitcounter;
}
    }
    
    //Anzeigen, dass das ziel erreicht wurde. notwendig für das debuggen
    public void MessageGoal()
    {
        StartCoroutine(Goal());
    }

    //Anzeigen, wie lange das wandlaufen geht. notwendig zum debuggen
    public void MessageWallText(string countdown)
    {
        wallText.text=countdown;
    }

    //Gibt anzahl hits zurück
    public int GetHitCounter()
    {
        return hitcounter;
    }
    

    //Anzeigen, dass man getroffen wurde
    IEnumerator Hit()
    {

        hit.text = "HIT";
        yield return new WaitForSeconds(0.5f);
        hit.text = "";
    }
    
    //Anzeigen, dass das ziel erreicht wurde. notwendig zum debuggen
    IEnumerator Goal()
    {
        goal.text = "Runde "+runde;
        yield return new WaitForSeconds(2f);
        goal.text = "";
        runde++;
    }


    //Erhält die aktuelle runde
    public int GetRound()
    {
        return runde;
    }
}
