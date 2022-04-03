using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{

	public GameObject[] tilePrefabs;
	private float zspawn=0;
	public float tileLength=30;
	private int numberOfTiles=7;

	public Transform player;
	private int counter=0;
	private float startposition;
	private float laufcounter;
	private List<GameObject> activeTiles=new List<GameObject>();

	private float countdown;
	private bool goCountdown;

	private int deleteCounter;
	private float weg;

    void Start()
    {
	    deleteCounter = 0;
	    weg = -40;
	    goCountdown = false;
	    countdown = 3f;
	startposition=player.position.z;	
 	

for(int i=0;i<numberOfTiles;i++){
		SpawnTile(i);
		counter=i+1;
		}
	



    }
    void Update()
    {
		//Debug.Log(startposition);
		//Debug.Log(player.position.z);

		
		
      if((player.position.z-startposition)<=weg){
		if(counter>13){
		counter=0;
		}
		startposition=player.position.z;
		
		
		SpawnTile(counter);
		
		counter++;

		deleteCounter++;


      }


      if(deleteCounter>1)
	      DeleteTile();

      
            
    }

	public void SpawnTile(int tileIndex){
	GameObject go=Instantiate(tilePrefabs[tileIndex],transform.forward*zspawn*-1,transform.rotation);
	
	activeTiles.Add(go);
	zspawn=zspawn+tileLength;
}

	void DeleteTile(){
		countdown-=1 * Time.deltaTime;

		if (countdown <= 0)
		{
			
			Destroy(activeTiles[0]);
			activeTiles.RemoveAt(0);
			Destroy(activeTiles[0]);
			activeTiles.RemoveAt(0);
			countdown = 3f;
			deleteCounter = 0;
		}
	}
   
}
