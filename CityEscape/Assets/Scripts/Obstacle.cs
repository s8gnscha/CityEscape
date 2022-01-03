using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
    }
}
