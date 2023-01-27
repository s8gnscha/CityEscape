using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�berpr�fung, ob die hindernisse getriggered werden (Da sie triggerboxen sind). Sollten sie aktiviert werden, dann soll das als hit gez�hlt werden
public class Obstacle : MonoBehaviour
{

	public GameObject ui;

	public GameObject hero;

    //Aktivierung der Hits. sowohl bei playermove als auch in der ui.
    void OnTriggerEnter(Collider other)
    {
        ui.GetComponent<UI>().MessageHit();
		hero.GetComponent<PlayerMove>().ShowHit();
    }
}
