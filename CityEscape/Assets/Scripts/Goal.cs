using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

	public GameObject ui;
	public GameObject target;
    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageGoal();
		target.GetComponent<PlayerMove>().SetCheckpoint(4);
    }
}
