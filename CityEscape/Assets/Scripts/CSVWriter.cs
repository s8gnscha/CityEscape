using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/**
 * Skript, welches überschreiben der Hit Daten und der ID in die .csv zuständig ist
 * 
 * 
 */
public class CSVWriter : MonoBehaviour
{

    //Der Pfad, in welcher die CSV Datei abgespeichert werden soll
    string filename = "";

   

    //public PlayerList myPlayerlist = new PlayerList();


    // Start is called before the first frame update
    void Start()
    {
        filename = Application.persistentDataPath + "/test.csv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /**
     * Übernimmt das aufschreiben in die .csv Datei.
     * String name: Die ID des Probanden
     * int[] rounds: Die Hits in den jeweiligen RUnden
     */
    public void WriteCSV(string name, int[] rounds)
    {

        
        if (rounds.Length > 0)
        {
            
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name,Round1,Round2,Round3,Round4,Round5,Round6");
            tw.Close();

            tw = new StreamWriter(filename, true);

    
                tw.WriteLine(name + "," + rounds[0] + "," + rounds[1] + "," + rounds[2] + "," + rounds[3] + "," + rounds[4] + "," + rounds[5]);

            
            tw.Close();
        }


    }
}
