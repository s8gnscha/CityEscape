using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Soll die entsprechende triggerbox aktiviert werden, dann soll der Spieler spawnen
/// </summary>
public class SpawnTrigger : MonoBehaviour
{
    public GameObject ui;
    public GameObject target;


    //Setzen des Spawns
    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageHit();
        target.GetComponent<PlayerMove>().SetSpawn();
    }
}
