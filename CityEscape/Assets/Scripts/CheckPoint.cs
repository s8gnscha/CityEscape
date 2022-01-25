using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject target;
    public int checkpoint;

    void OnTriggerEnter(Collider other)
    {
        
        target.GetComponent<PlayerMove>().SetCheckpoint(checkpoint);
    }
}
