using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/**
 * Kümmert sich um das Laden neuer Abschnitte im Spiel, dem Speichern in die .csv Datei sowie das variieren des Musiktempos
 */
public class TriggerManager : MonoBehaviour
{

	public GameObject ui;
	public GameObject GameOverUI;
	public GameObject soundManager;

	public GameObject csvWriter;
	private int round;
	private int[] hitcounter=new int[6];
	public int end;
	public bool tutorial;
	public GameObject[] tilePrefabs;
	private float zspawn=0;
	public float tileLength=30;
	private int numberOfTiles=7;

	public Transform player;
	private int pathcounter;
	private int counter=0;
	private float startposition;
	private float startsoundposition;
	private float laufcounter;
	private List<GameObject> activeTiles=new List<GameObject>();

	private float countdown;
	private bool goCountdown;

	private int deleteCounter;
	private float weg;
	private float soundweg;
	private float wasteTime;
	private string filename = "";
	private int addresult;


	//Initialisierung der Werte, erstellung der ersten Abschnitte und setzen der hits auf 0
    void Start()
    {
		Time.timeScale = 1;
		addresult = 0;
		filename = Application.persistentDataPath + "/test"+PlayerPrefs.GetString("id","9999")+ "C.csv";
		//filename = Application.dataPath + "/test.csv";
		wasteTime = 0f;
		pathcounter = 0;
	    GameOverUI.SetActive(false);
	    round = 1;
	    deleteCounter = 0;
	    weg = -40;
		soundweg=-4;
	    goCountdown = false;
	    countdown = 3f;
	startposition=player.position.z;
	startsoundposition=player.position.z;	
 	

for(int i=0;i<numberOfTiles;i++){
		SpawnTile(i);
		counter=i+1;
		}
	

for(int i = 0; i < hitcounter.Length; i++)
        {
			hitcounter[i] = -1;
        }

    }

	//Überprüfung der strecke. ab einer gewissenen gelaufenen Strecke sollen neuer abschnitte geladen, alte gelöscht werden und das musiktempo variiert werden.
    void Update()
    {
		
		

		
		
      if((player.position.z-startposition)<=weg){
		if(counter>tilePrefabs.Length-1){
		counter=0;
		}

          
			pathcounter++;
		startposition=player.position.z;
		
		
		SpawnTile(counter);
		
		counter++;

		deleteCounter++;



      }

		if (!tutorial) {
			if ((player.position.z - startsoundposition) <= soundweg) {
				soundManager.GetComponent<SoundManager>().AddTempo();
				
				startsoundposition = player.position.z;
			}
		}

      if(deleteCounter>1)
	      DeleteTile();

      
            
    }


	/**
	 * Erstellung eines Abschnitts.
	 * int tileindex: Nummer des Abschnitts, das geladen werden soll
	 */
	public void SpawnTile(int tileIndex){
	GameObject go=Instantiate(tilePrefabs[tileIndex],transform.forward*zspawn*-1,transform.rotation);
	
	activeTiles.Add(go);
	zspawn=zspawn+tileLength;
}

	/**
	 * Löschen eines Abschnitts
	*/
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

	//Erhöhung der Runde und Überprüfung der Rundenanzahl. Soll diese Überschritten werden, dann soll der Game Over Bildschirm angezeigt werden und die Id und Hitcounter daten in die CSV Datei abgespeichert werden.
	public void NewRound()
    {
		
		
		
		

		if (round < end)
		{
			hitcounter[round - 1] = ui.GetComponent<UI>().GetHitCounter()-addresult;
			addresult = addresult + hitcounter[round - 1];
		}
		else
		{
			hitcounter[round - 1] = ui.GetComponent<UI>().GetHitCounter() - addresult;
			addresult = addresult + hitcounter[round - 1];

			if (!tutorial)
			{

				if (hitcounter.Length > 0)
				{

					TextWriter tw = new StreamWriter(filename, false);
					tw.WriteLine("Name,Round1,Round2,Round3,Round4,Round5,Round6,Gesamt");
					tw.Close();

					tw = new StreamWriter(filename, true);

					for(int i=0; i < hitcounter.Length; i++)
                    {
						Debug.Log(hitcounter[i]);
                    }
					tw.WriteLine(PlayerPrefs.GetString("id","9999") + "," + hitcounter[0] + "," + hitcounter[1] + "," + hitcounter[2] + "," + hitcounter[3] + "," + hitcounter[4] + "," + hitcounter[5]+","+ ui.GetComponent<UI>().GetHitCounter());


					tw.Close();
				}
			}
			Time.timeScale = 0;
			GameOverUI.SetActive(true);
		}
		round++;
		ui.GetComponent<UI>().MessageGoal();
	}
   
}
