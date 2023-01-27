using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Trigger für die Runde. soll die Triggerbox am ende des levels aktiviert werden, soll im triggermanager die runde hochgezählt werden.
/// </summary>
public class RoundCounter : MonoBehaviour
{


	public GameObject triggerManager;

// Aktivierung der erhöhung der rundenanzahl im triggermananger
	void OnTriggerEnter(Collider other)
    {
		triggerManager.GetComponent<TriggerManager>().NewRound();
	}
}
