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

    void Start()
    {
	
	startposition=player.position.z;	
 	

for(int i=0;i<numberOfTiles;i++){
		SpawnTile(i);
		counter=i+1;
		}
	



    }
    void Update()
    {
		Debug.Log(startposition);
		//Debug.Log(player.position.z);
		
      if((player.position.z-startposition)<=-50){
		if(counter>12){
		counter=0;
		}
		startposition=player.position.z;
		
		
		SpawnTile(counter);
		DeleteTile();
		counter++;
		
} 
            
    }

	public void SpawnTile(int tileIndex){
	GameObject go=Instantiate(tilePrefabs[tileIndex],transform.forward*zspawn*-1,transform.rotation);
	activeTiles.Add(go);
	zspawn=zspawn+tileLength;
}

	void DeleteTile(){
Destroy(activeTiles[0]);
activeTiles.RemoveAt(0);
}
   
}
