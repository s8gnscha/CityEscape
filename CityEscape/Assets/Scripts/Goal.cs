using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Trigerbox, ob das ziel erreicht wurde
public class Goal : MonoBehaviour
{

	public GameObject ui;
	public GameObject target;

    //Aktivierung der notwendigen aktionen, um den win darzustellen. sowohl in der ui als auch beim player
    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageGoal();
		target.GetComponent<PlayerMove>().SetCheckpoint(transform.position);
    }
}
