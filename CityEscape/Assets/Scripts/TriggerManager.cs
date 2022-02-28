using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
   private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength = 30;
    public int numberOfTiles = 3;
    public int totalNumOfTiles = 8;

    public float zSpawn = 0;

    private Transform playerTransform;

    private int previousIndex;

    private int nextIndex;

    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(i);
            nextIndex = i+1;
        }

        playerTransform = GameObject.FindGameObjectWithTag("Hero").transform;

    }
    void Update()
    {
        if(playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
        {
            previousIndex = nextIndex - 1;
            DeleteTile();
            SpawnTile(nextIndex);
            nextIndex = nextIndex + 1;

        }
            
    }

    public void SpawnTile(int index = 0)
    {
        GameObject tile = tilePrefabs[index];
        if (tile.activeInHierarchy)
            tile = tilePrefabs[index + 8];

        if(tile.activeInHierarchy)
            tile = tilePrefabs[index + 16];

        tile.transform.position = Vector3.forward * zSpawn;
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
    }

    private void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
        //PlayerManager.score += 3;
    }
}
