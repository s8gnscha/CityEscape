using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * kümmert sich um das steuern das hauptmenüs
 */
public class MainMenuHandler : MonoBehaviour
{

    private string id;
    public GameObject mainMenu;
    public GameObject idEingabe;
    public Text idTextMainMenu;
    public Text idText;
    // Start is called before the first frame update
    void Start()
    {
        idEingabe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        idText.text = id;
    }

    //ID Eingabe soll als Menü geladen werden
    public void GoToIDEingabe()
    {
        idEingabe.SetActive(true);
        mainMenu.SetActive(false);
    }


    //Hauptmenü soll als Menü geladen werden
    public void GoToMainMenu()
    {
        idTextMainMenu.text = PlayerPrefs.GetString("id","9999");
        PlayerPrefs.SetString("id", id);
        idEingabe.SetActive(false);
        mainMenu.SetActive(true);
    }


    //Eingabe der ID Nummer. int number ist die Zahl die eingegeben wurde
    public void GiveNumber(int number)
    {
        id = id + number;
    }

    //Löschen der gesamten ID
    public void Erase()
    {
        id = "";
    }
}
