using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * Kümmert sich um die Erläuterungen im Tutorial Level des Spiels
 */

public class Tutorial : MonoBehaviour
{
    public GameObject ui;
    public GameObject[] tutorials;

    private string tutMove;
    private string move;
    private int kind;
    // Start is called before the first frame update
    //Setzen der Erklärungen alle auf false
    void Start()
    {
        kind = 99;
        tutMove = "tutnothing";
        move = "nothing";
        for(int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        SetSwipe();
        SwipeChecker();
    }


    /**
     * Entsprechende Erläuterung soll aktiviert werden.
     * int kind: Welche art der erläuterung soll aktiviert werden
     * 
     *string move: Welche aktion ist notwendig um das resultierende Standbild und damit die erläuterung wegzumachen
     */
    public void TriggerTutorial(int kind,string move)
    {


        if (ui.GetComponent<UI>().GetRound() < 3)
        {
            tutorials[kind].SetActive(true);
            tutMove = move;
            Time.timeScale = 0f;
            this.kind = kind;

            if (kind == 8)
                StartCoroutine(Obstacle());
        }
                
    }

    //Setzen der Aktion, die durchgeführt wurde
    void SetSwipe()
    {
        if (SwipeManager.swipeLeft || Input.GetKeyDown("left"))
        {
            move = "left";
        } else if (SwipeManager.swipeUp || Input.GetKeyDown("up"))
        {
            move = "up";
        }
        else if (SwipeManager.swipeRight || Input.GetKeyDown("right"))
        {
            move = "right";
        }
        else if (SwipeManager.swipeDown || Input.GetKeyDown("down"))
        {
            move = "down";
        }
        else
        {

        }

    }

    //Überprüfung, welche aktion durchgeführt wurde. entspricht es der richtigen aktion, dann soll die Erläuterung verschwinden
    void SwipeChecker() {
  
         if(String.Equals(tutMove, move)&& kind!=8)
        {

            tutMove = "tutnothing";
            move = "nothing";
            Time.timeScale = 1f;

            for (int i = 0; i < tutorials.Length; i++)
            {
                tutorials[i].SetActive(false);
            }
        }
    }

    IEnumerator Obstacle()
    {
        yield return new WaitForSeconds(2f);
        tutorials[8].SetActive(false);
        Time.timeScale = 1f;
    }
}
