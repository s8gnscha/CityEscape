using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{

    public Text goal;
    public Text hit;
	public Text wallText;

    void Start()
    {
        goal.text = "";
        hit.text = "";
		wallText.text="3";
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MessageHit()
    {
        StartCoroutine(Hit());
    }
    
    public void MessageGoal()
    {
        StartCoroutine(Goal());
    }

    public void MessageWallText(string countdown)
    {
        wallText.text=countdown;
    }
    
    IEnumerator Hit()
    {
        hit.text = "HIT";
        yield return new WaitForSeconds(0.5f);
        hit.text = "";
    }
    
    IEnumerator Goal()
    {
        goal.text = "GOAL";
        yield return new WaitForSeconds(1f);
        goal.text = "";


    }
}
