using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	public GameObject ui;

    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageHit();
    }
}
