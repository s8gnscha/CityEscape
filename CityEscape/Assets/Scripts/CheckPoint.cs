using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Trigger für den checkpoint im spiel. soll die entsprechende triggerbox aktiviert werden, dann soll der spawn des spielers an die jeweilige position gestellt werden.
public class CheckPoint : MonoBehaviour
{
    public GameObject player;
    public int checkpoint;


    //Aktivierung des checkpoints setzen
    void OnTriggerEnter(Collider other)
    {
        
        player.GetComponent<PlayerMove>().SetCheckpoint(transform.position);
    }
}
