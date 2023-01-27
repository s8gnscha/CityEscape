using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * Movement vom Player. Bestehend final aus laufen, sliden, wandlauf, springen. zudem werden alle aktionen des spielers da aus gesteuert z.B. hits, physik etc.
 */
public class PlayerMove : MonoBehaviour
{

	public Animator playerAnimator;
	public CharacterController characterController;

	public Transform target;
	public GameObject hero;

	public GameObject ui;
	
	private bool isGrounded;
	private bool isWalling;

	private bool isdown;
	[SerializeField] private LayerMask groundLayers;
	[SerializeField] private LayerMask wallLayers;
	
	
	//Position des Spielers
	
	[SerializeField] private float gravity = -9.81f;
	private float jumpHeight = 1f;

	private int line;
	private bool push;
	private Vector3 velocity;

	public Transform groundcheck;
	public Transform groundcheckfront;

	public Transform wallcheckleft;
	public Transform wallcheckright;

	private bool climb;

	private int horizontalInput;

	private float currentTime;

	

	private float downcurrentTime;

	private bool isFalling;


	

	private Vector3 spawnPosition;
	private bool[] checkpoints=new bool[4];

	public Slider runbar;
	public Gradient gradient;
	public Image fill;

	public GameObject swipeManager;

	private Vector3 targetPosition;

	private bool leftrightmove;

	private float movevalue = 3f;

	private float movevaluebig = 6f;

	private float f;

	private bool noZ;
	
	//Initialisierung der Daten. auserdem checkpoints auf false setzen
	void Awake () {
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 60;
	}
    void Start()
    {
	    noZ = false;
	    spawnPosition=new Vector3(transform.position.x, transform.position.y, transform.position.z);
	    isFalling = false;
	    horizontalInput = -10;
	     targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	    isWalling = false;
	    climb = false;
	    push = true;
	    line = 0;
	    currentTime = 2f;
	    leftrightmove = true;
	    downcurrentTime = 0.5f;
	    characterController= GetComponent<CharacterController>();
	    hero.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
	    isdown = false;
	    

	    fill.color = gradient.Evaluate(1f);

	    for (int i = 0; i < 4; i++)
	    {
		    checkpoints[i] = false;
	    }
	    
    }
   
       
       /**
        * Bei fixedupdate wird durch die Velocity function vom rigidbody das automatische laufen vom Spieler generiert.
        * 
        */
       void Update()
       {

	       
	       
	
       }

	//Ausführen und überprüfen der verschiedenen mechaniken. zudem überprüfung, ob der spieler auf dem boden ist.
       void FixedUpdate()
       {
	       float x = Input.GetAxis("Horizontal");
	       if(!noZ)
	       isGrounded = Physics.CheckSphere(groundcheck.position, 0.4f, groundLayers)||Physics.CheckSphere(groundcheckfront.position, 0.4f, groundLayers);
	       if (leftrightmove)
	       {
		       MoveLeftRight(x);
		       Gravity(isGrounded);
		       WalkWall(x);
		       CheckWallWalking();
		       CheckJump(isGrounded);
		       CheckDown();
		       GoDown();
		       CheckFalling();
		       FallToDeep();
	       }
	       MoveForward();
	       


	       
       }


	//Spieler bewegt sich entlang der Z Achse vorwärts
       void MoveForward()
       {
	       
	       
	       if (leftrightmove && !noZ)
	       {
		       characterController.Move(new Vector3(0, 0, horizontalInput) * Time.deltaTime);
	       }
       }

	//Überprüfen der gravitation, welches davon abhängt, ob der spieler wandlauf oder auf dem boden ist. ist das der fall, dann soll fallen gestoppt werden
       void Gravity(bool isGrounded)
       {
	       if ((isGrounded || isWalling) && velocity.y < 0)
	       {
		       velocity.y = 0;
		       playerAnimator.SetBool("jump", false);
	       }
	       else
	       {
		       
		       velocity.y += gravity * Time.deltaTime;
	       }

	       



       }
       

	//Überprüfen, ob aktion für rechts oder links laufen gemacht wird und ob das möglich ist. soll das der fall sein, dann soll er um eine gewisse einheit in die jeweilige richtung bewegen.
       void MoveLeftRight(float x)
       {
	       targetPosition.y = transform.position.y;
	       targetPosition.z = transform.position.z;
	       if ((!isFalling)&&((transform.position.x-targetPosition.x)*(transform.position.x-targetPosition.x)<0.1f))
	       {
		       if (!isWalling)
		       {
			       if(SwipeManager.swipeLeft||Input.GetKeyDown("left"))
			       {
				       if (line == 0)
				       {
					      
					       
					      targetPosition += Vector3.right * movevalue;
					       line = -1;
					       if (isdown)
					       {
						       
						       playerAnimator.SetBool("slide", false);
						       characterController.height = 2f;
						       isdown = false;
						       downcurrentTime = 0.5f;
					       }

				       }

				       else if (line == 1)
				       {
					       
					      
					       
					       
					     
					       line = 0;
					       if (isdown)
					       {
						       
						       playerAnimator.SetBool("slide", false);
						       characterController.height = 2f;
						       isdown = false;
						       downcurrentTime = 0.5f;
					       }
					       targetPosition += Vector3.right *movevalue;
				       }
			       }

			       else if(SwipeManager.swipeRight||Input.GetKeyDown("right"))
			       {
				       if (line == 0)
				       {
					       
					      
				
					       line = 1;
					       if (isdown)
					       {
						      
						       playerAnimator.SetBool("slide", false);
						       characterController.height = 2f;
						       isdown = false;
						       downcurrentTime = 0.5f;
					       }
					       targetPosition += Vector3.left * movevalue;
				       }

				       else if (line == -1)
				       {

					     
					       
					       
					 
					       line = 0;
					       if (isdown)
					       {
						       
						       playerAnimator.SetBool("slide", false);
						       characterController.height = 2f;
						       isdown = false;
						       downcurrentTime = 0.5f;
					       }
					       targetPosition += Vector3.left * movevalue;
				       }
			       }
		       }
		       else
		       {
			       if (SwipeManager.swipeLeft||Input.GetKeyDown("left"))
			       {
				       if (line == 0)
				       {
					       
					       targetPosition += Vector3.right * movevalue;
					       line = -1;
					       currentTime = 2f;
					       
					       runbar.normalizedValue = currentTime;
					       fill.color = gradient.Evaluate(runbar.normalizedValue);
					       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
					       isWalling = true;
					      

				       }

				       else if (line == 1)
				       {

					
					       targetPosition += Vector3.right * movevaluebig;
					       line = -1;
					       currentTime = 2f;
					     
					       runbar.normalizedValue = currentTime;
					       fill.color = gradient.Evaluate(runbar.normalizedValue);
					       isWalling = true;
				       }
			       }

			       else if (SwipeManager.swipeRight||Input.GetKeyDown("right"))
			       {
				       if (line == 0)
				       {

					      
					       targetPosition += Vector3.left * movevalue;
					       line = 1;
					       currentTime = 2f;
					     
					       runbar.normalizedValue = currentTime;
					       fill.color = gradient.Evaluate(runbar.normalizedValue);
					       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
					       isWalling = true;
					       
				       }

				       else if (line == -1)
				       {

					       
					       targetPosition += Vector3.left * movevaluebig;
					       line = 1;
					       currentTime = 2f;
					       
					       runbar.normalizedValue = currentTime;
					       fill.color = gradient.Evaluate(runbar.normalizedValue);
					       isWalling = true;
				       }
			       }
		       }
		       
	       }


	       if (leftrightmove)
	       {
		       if (transform.position.x != targetPosition.x)
		       {
			       Vector3 diff = targetPosition - transform.position;
			       Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
			       if (moveDir.sqrMagnitude < diff.magnitude)
				       characterController.Move(moveDir);
			       else
				       characterController.Move(diff);

			       if (isWalling)
			       {
				       if ((transform.position.x - targetPosition.x) * (transform.position.x - targetPosition.x) >
				           0.00001f)
				       {
					      
					       if (line == -1)
					       {
						       playerAnimator.SetBool("jump", false);
						       playerAnimator.SetBool("wallrunLeft", true);
						       playerAnimator.SetBool("wallrunRight", false);
					       
					       }
					       if (line == 1)
					       {
						       playerAnimator.SetBool("jump", false);
						       playerAnimator.SetBool("wallrunLeft", false);
						       playerAnimator.SetBool("wallrunRight", true);
					       
					       }
				       }
			       }
		       }
		       else
		       {
			       if (isWalling)
			       {
				       
			       }
		       }
	       }
	       
	       





       }
       
      
	//Checken, ob es möglich ist zu springen (nämlich dann, wenn man auf dem boden ist) und ob die aktion dafür ausgeführt wurde. sollte das der fall sein, bekommt der spieler einen schub entlang der y achse.
       void CheckJump(bool isGrounded)
       {

		       if (!isWalling&&!isdown)
		       {
			       if (isGrounded && (SwipeManager.swipeUp||Input.GetKeyDown("up")))
			       {

				       playerAnimator.SetBool("jump", true);
				       velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
				       
			       }
			       characterController.Move(velocity*Time.deltaTime);
			       if (target.position.y > 2.1f)
			       {
				      
				       isFalling = true;
			       }
		       }

		       




       }


	//Überprüfen, ob sliden möglich ist (Boden sein und kein wandlauf) und ob aktion dafür gemacht wurde.
       void GoDown()
       {
	       if (!isWalling&&isGrounded)
	       {
		       if (SwipeManager.swipeDown||Input.GetKeyDown("down"))
		       {
			       if (!isdown)
			       {
				     
				       
				       playerAnimator.SetBool("slide", true);
				       
				       characterController.height = 1f;
				       
				      
				       isdown = true;   
			       }
			       
				       
				       
		       }
	       }

	       if (isdown)
	       {
		       characterController.Move(velocity*Time.deltaTime);
	       }
	      

       }

	//Überprüfen ob wandlauf möglich ist(Wenn wand da ist) Und ob die notwendige aktion dafür gemacht wurde.
       void WalkWall(float x)
       {
	       if (push&&!isdown&&isGrounded)
	       {
		       if (SwipeManager.swipeLeft||Input.GetKeyDown("left"))
		       {
			       if (line == -1)
			       {
				       bool isWallLeft = Physics.CheckSphere(wallcheckleft.position, 1f, wallLayers);
				       if (isWallLeft)
				       {
					       
					       

							   playerAnimator.SetBool("wallrunLeft", true);
						       
						       characterController.Move(new Vector3(0, 2.5f, 0));
						       
						       isWalling = true;
					       
					       
				       }
				       else
				       {
					       
					       isWalling = false;
				       }
			       }
		       }

		       else if (SwipeManager.swipeRight||Input.GetKeyDown("right"))
		       {
			       if (line == 1)
			       {
				       bool isWallRight = Physics.CheckSphere(wallcheckright.position, 1f, wallLayers);
				       if (isWallRight)
				       {

					       
					       
					           playerAnimator.SetBool("wallrunRight", true);
						       
						       characterController.Move(new Vector3(0, 2.5f, 0));
						       
						       isWalling = true;
					       
				       }
				       else
				       {
					       
					       isWalling = false;
				       }
			       }
			      
		       }
	       }
	       
	     
       }


	//Überprüft, ob wandlauf immer noch aktiv ist. wenn ja, soll counter runter gehen. wenn counter runter oder keine wand, dann soll der spieler fallen
       void CheckWallWalking()
       {
	       bool isWallLeft = Physics.CheckSphere(wallcheckleft.position, 0.9f, wallLayers);
	       bool isWallRight = Physics.CheckSphere(wallcheckright.position, 0.9f, wallLayers);
	       if (isGrounded)
	       {
		       currentTime = 2f;
		       
		       runbar.normalizedValue = currentTime;
		       fill.color = gradient.Evaluate(runbar.normalizedValue);
		       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
		       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
	       }
	       if (isWalling)
	       {
		      
		       currentTime -= 1 * Time.deltaTime;
		       
		       runbar.value = currentTime;
		       fill.color = gradient.Evaluate(runbar.normalizedValue);

		       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
		       
			       
		       if (currentTime <= 0f||SwipeManager.swipeDown||Input.GetKeyDown("down"))
		       {
			       
			       isFalling = true;
			       isWalling = false;
			       currentTime = 2f;
			       
			       playerAnimator.SetBool("falling", true);
			       ui.GetComponent<UI>().MessageWallText("Wallrun:0");
			       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		       }
		       else if (currentTime <= 0.66f)
		       {
			       ui.GetComponent<UI>().MessageWallText("Wallrun:1");  
			       hero.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.64f, 0.0f));
		       }
		       else if (currentTime <= 1.33f)
		       {
			       ui.GetComponent<UI>().MessageWallText("Wallrun:2");
			       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
		       }
	       }
	       else
	       {

		       if(velocity.y<-8f)
		       playerAnimator.SetBool("falling", true);
	       }

	       if ((transform.position.x-targetPosition.x)*(transform.position.x-targetPosition.x)<0.1f)
	       {
		       if (!(isWallLeft || isWallRight))
		       {
			       isWalling = false;
			       
			       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
		       }
	       }







       }

	//Checken, ob der spieler immer noch slidet. wenn ja, dann counter soll der counter runtergehen. ist der counter unten oder slide wurde abgebrochen, dann soll der spieler aufstehen
       void CheckDown()
       {
	       if (isdown)
	       {
		       downcurrentTime -= 1 * Time.deltaTime;
		       if (downcurrentTime <= 0f||SwipeManager.swipeUp||Input.GetKeyDown("up"))//(downcurrentTime <= 0f||Input.GetKeyDown("up"))
		       {
			       
			       playerAnimator.SetBool("slide", false);
			       characterController.height = 2f;
			       isdown = false;
			       downcurrentTime = 0.5f;
		       }


	       }
       }


	//Überprüfen, ob der spieler immer noch am fallen ist
       void CheckFalling()
       {
	       if (isGrounded&&isFalling)
	       {
		       isFalling = false;
		       playerAnimator.SetBool("falling", false);
		       playerAnimator.SetBool("wallrunLeft", false);
		       playerAnimator.SetBool("wallrunRight", false);
		      
		       
	       }
       }

	//zurückehren zum spawn. wenn der spieler runterfällt in eine schlucht, dann kehrt in dieser gesetzten position zurück.
       public void SetSpawn()
       {

	       if (checkpoints[3])
	       {
		       
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2, 200);
		       characterController.enabled = true;
		       targetPosition.x = 4;
		       line=0;
		       for (int i = 0; i < 3; i++)
		       {
			       checkpoints[i] = false;
		       }  
	       }
	       else if (checkpoints[2])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2, -350);
		       characterController.enabled = true;
		       targetPosition.x = 4;
		       line=0;
	       }
	       else if (checkpoints[1])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2,-202);
		       characterController.enabled = true;
		       targetPosition.x = 4;
		       line=0;
	       }
	        else if (checkpoints[0])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2, -100);
		       characterController.enabled = true;
		       targetPosition.x = 4;
		       line=0;
	       }
       }

	//Setzen des checkpoints
       public void SetCheckpoint(Vector3 checkpoint)
       {
	       
	       spawnPosition = checkpoint;

       }

	//Springen zur wallrunsituation. Notwendig zum debuggen
       public void GoToWallrun()
       {
	       characterController.enabled = false;
	       target.transform.position=new Vector3(4, 2, -325);
	       characterController.enabled = true;
	       targetPosition.x = 4;
	       line=0;
       }

	//Flüssiges übergehen des spielers auf den bahnen
       void SmoothMove(Vector3 position1,Vector3 position2)
       {
	       if (position1 != position2)
	       {
		       Vector3 diff = position2 - position1;
		       Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
		       if (moveDir.sqrMagnitude < diff.magnitude)
			       characterController.Move(moveDir);
		       else
			       characterController.Move(diff);
	       }
	       else
	       {
		       climb = false;
	       }
	       
       }


	//Hit soll angezeigt werden
      public void ShowHit()
       {
	       StartCoroutine(HitAnimation());

       }


	//Abspielen der hit animation
       IEnumerator HitAnimation()
       {
	       leftrightmove = false;
	       playerAnimator.SetBool("fail", true);
	       yield return new WaitForSeconds(0.5f);
	       playerAnimator.SetBool("fail", false);
	       leftrightmove = true;
       }


	//Überprüfen, ob man zu tief fällt. ist man zu tief entlang der y achse, dann soll der spieler zurück zum jeweiligen spawn kommen
       void FallToDeep()
       {
	       if (transform.position.y < 12)
	       {
		       isFalling = true;
		       isGrounded = false;
		       noZ = true;
	       }
	       if (transform.position.y <= -10)
	       {
		       isGrounded = true;
		       noZ = false;
		       ui.GetComponent<UI>().MessageHit();
		       ShowHit();
		       characterController.enabled = false;
		       target.transform.position=new Vector3(-5.2f, spawnPosition.y, spawnPosition.z);
		       characterController.enabled = true;
		       targetPosition.x = -5.2f;
		       line=0;
	       }
       }
       
}
