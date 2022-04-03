using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    public int checkpoint;

    void OnTriggerEnter(Collider other)
    {
        
        player.GetComponent<PlayerMove>().SetCheckpoint(transform.position);
    }
}
