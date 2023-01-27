using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Überprüfung im Tutorial Level, ob eine Triggerbox für die notwendige Tutorialsequenz aktiviert wurde. Sollte das der Fall sein, soll die jeweilige Art der Erklärung angezeigt werden
 */
public class TutorialTrigger : MonoBehaviour
{
    public int tutorialKind;
    public string move;
    public GameObject tutorial;


    // Aktivierung der entsprechenden Erklärung im Tutorial
    void OnTriggerEnter(Collider other)
    {
        tutorial.GetComponent<Tutorial>().TriggerTutorial(tutorialKind,move);
    }
}
