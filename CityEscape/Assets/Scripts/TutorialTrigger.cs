using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * �berpr�fung im Tutorial Level, ob eine Triggerbox f�r die notwendige Tutorialsequenz aktiviert wurde. Sollte das der Fall sein, soll die jeweilige Art der Erkl�rung angezeigt werden
 */
public class TutorialTrigger : MonoBehaviour
{
    public int tutorialKind;
    public string move;
    public GameObject tutorial;


    // Aktivierung der entsprechenden Erkl�rung im Tutorial
    void OnTriggerEnter(Collider other)
    {
        tutorial.GetComponent<Tutorial>().TriggerTutorial(tutorialKind,move);
    }
}
