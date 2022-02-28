using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

	public GameObject ui;

	public GameObject hero;

    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageHit();
		hero.GetComponent<PlayerMove>().ShowHit();
    }
}
