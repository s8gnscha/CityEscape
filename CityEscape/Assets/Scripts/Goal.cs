using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

	public GameObject ui;
    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageGoal();
    }
}
