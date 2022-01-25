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
	public Text counterText;
	private int hitcounter;
	private float currentTime;
    void Start()
    {
		currentTime=2f;
		hitcounter=0;
        goal.text = "";
        hit.text = "";
		wallText.text="3";
		counterText.text="HitCounter:"+hitcounter;
    }

	void Update(){
	currentTime -= 1 * Time.deltaTime;
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
		if(currentTime<=0f){
        StartCoroutine(Hit());
		hitcounter++;
		counterText.text="HitCounter:"+hitcounter;
}
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
