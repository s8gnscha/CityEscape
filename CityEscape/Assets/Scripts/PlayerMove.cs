using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Movement vom Player. Beschränkt sich zunächst nur auf laufen
 */
public class PlayerMove : MonoBehaviour
{
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
	
	private int rotatecounter;

	private int horizontalInput;

	private float currentTime;

	private float downcurrentTime;

	private bool isFalling;
	private bool groundspawn;

	private Vector3 spawnPosition;
	private bool[] checkpoints=new bool[3];

	private float f;
    void Start()
    {
	    isFalling = false;
	    horizontalInput = -10;
        rotatecounter=0;
	    isWalling = false;
	    push = true;
	    line = 0;
	    currentTime = 2f;
	    downcurrentTime = 1f;
	    characterController= GetComponent<CharacterController>();
	    hero.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
	    isdown = false;
	    groundspawn = false;

	    for (int i = 0; i < 3; i++)
	    {
		    checkpoints[i] = false;
	    }
	    
    }
   
       
       /**
        * Bei fixedupdate wird durch die Velocity function vom rigidbody das automatische laufen vom Spieler generiert.
        * Die Auskommentieren sind weitere Ansätze die bisher noch nicht funktionieren ^^"
        */
       void Update()
       {
	       float x = Input.GetAxis("Horizontal");
	       isGrounded = Physics.CheckSphere(groundcheck.position, 0.4f, groundLayers)||Physics.CheckSphere(groundcheckfront.position, 0.4f, groundLayers);
	       //Debug.Log(groundspawn);
		       
	       Gravity(isGrounded);
	       WalkWall(x);
	       CheckWallWalking();
	       MoveForward();
	       MoveLeftRight(x);
	       CheckJump(isGrounded);
	       CheckDown();
	       GoDown();
	       CheckFalling();

       }


       void MoveForward()
       {
	       
	       //transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);
	       characterController.Move(new Vector3(0, 0, horizontalInput) * Time.deltaTime);
       }
       void Gravity(bool isGrounded)
       {
	       if ((isGrounded || isWalling) && velocity.y < 0)
	       {
		       velocity.y = 0;
		       
	       }
	       else
	       {

		       velocity.y += gravity * Time.deltaTime;
	       }
	       
	       
	       
       }
       
       void MoveLeftRight(float x)
       {
	       if (!isdown&&!isFalling)
	       {
		       if (!isWalling)
		       {
			       if (Input.GetKeyDown("left"))
			       {
				       if (line == 0)
				       {
					       characterController.Move(new Vector3(3f, 0, 0));
					       line = -1;
					     

				       }

				       else if (line == 1)
				       {

					       characterController.Move(new Vector3(3f, 0, 0));
					       line = 0;
					      
				       }
			       }

			       else if (Input.GetKeyDown("right"))
			       {
				       if (line == 0)
				       {

					       characterController.Move(new Vector3(-3f, 0, 0));
					       line = 1;
					       
				       }

				       else if (line == -1)
				       {

					       characterController.Move(new Vector3(-3f, 0, 0));
					       line = 0;
					      
				       }
			       }
		       }
		       else
		       {
			       if (Input.GetKeyDown("left"))
			       {
				       if (line == 0)
				       {
					       characterController.Move(new Vector3(3f, 0, 0));
					       line = -1;
					       currentTime = 2f;
					       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
					       isWalling = true;
					      

				       }

				       else if (line == 1)
				       {

					       //characterController.Move(new Vector3(7f, 0, 0));
					       StartCoroutine(JumpWallLeft());
					       
					      
				       }
			       }

			       else if (Input.GetKeyDown("right"))
			       {
				       if (line == 0)
				       {

					       characterController.Move(new Vector3(-3f, 0, 0));
					       line = 1;
					       currentTime = 2f;
					       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
					       isWalling = true;
					       
				       }

				       else if (line == -1)
				       {

					       //characterController.Move(new Vector3(-7f, 0, 0));
					       StartCoroutine(JumpWallRight());
					      
					       
				       }
			       }
		       }
	       }


       }
       
       IEnumerator JumpWallRight()
       {
	       characterController.Move(new Vector3(-3f, 0, 0));
	       
	       yield return new WaitForSeconds(0.1f);
	       characterController.Move(new Vector3(-4f, 0, 0));
	       line = 1;
	       currentTime = 2f;
	       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
	       isWalling = true;
	       isFalling = false;
       }
       
       IEnumerator JumpWallLeft()
       {
	       characterController.Move(new Vector3(3f, 0, 0));
	       
	       yield return new WaitForSeconds(0.1f);
	       characterController.Move(new Vector3(4f, 0, 0));
	       line = -1;
	       currentTime = 2f;
	       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
	       isWalling = true;
	       isFalling = false;
       }

       void CheckJump(bool isGrounded)
       {

		       if (!isWalling&&!isdown)
		       {
			       if (isGrounded && (Input.GetButtonDown("Jump")||Input.GetKeyDown("up")))
			       {


				       
				       velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
				       
			       }
			       characterController.Move(velocity*Time.deltaTime);
			       if(target.position.y>2.1f)
				       isFalling = true;
		       }

		       




       }

       void GoDown()
       {
	       if (!isWalling&&isGrounded)
	       {
		       if (Input.GetKeyDown("down"))
		       {
			       if (!isdown)
			       {
				       this.transform.Rotate(90, 0, 0);
				       characterController.height = 1f;
				       characterController.Move(new Vector3(0, -1, 0));
				       isdown = true;   
			       }

				       
				       
		       }
	       }
	      
	       if(isdown)
		       characterController.Move(velocity*Time.deltaTime);
       }

       void WalkWall(float x)
       {
	       if (push&&!isdown&&isGrounded)
	       {
		       if (Input.GetKeyDown("left"))
		       {
			       if (line == -1)
			       {
				       bool isWallLeft = Physics.CheckSphere(wallcheckleft.position, 0.9f, wallLayers);
				       if (isWallLeft)
				       {
					       f = 0.5f;
					       if (target.position.y < 3)
					       {
						       /*velocity.y = Mathf.Sqrt(3f * -2 * gravity);
						       characterController.Move(velocity*Time.deltaTime);*/
						       characterController.Move(new Vector3(f, 2.5f, 0));
						       isWalling = true;
					       }
					       
				       }
				       else
				       {
					       isWalling = false;
				       }
			       }
		       }

		       else if (Input.GetKeyDown("right"))
		       {
			       if (line == 1)
			       {
				       bool isWallRight = Physics.CheckSphere(wallcheckright.position, 0.9f, wallLayers);
				       if (isWallRight)
				       {

					       f = -0.5f;
					       if (target.position.y < 3)
					       {
						       /*velocity.y = Mathf.Sqrt(3f * -2 * gravity);
						       characterController.Move(velocity*Time.deltaTime);*/
						       characterController.Move(new Vector3(f, 2.5f, 0));
						       isWalling = true;
					       }
				       }
				       else
				       {
					       isWalling = false;
				       }
			       }
			      
		       }
	       }
       }

       void CheckWallWalking()
       {
	       bool isWallLeft = Physics.CheckSphere(wallcheckleft.position, 0.7f, wallLayers);
	       bool isWallRight = Physics.CheckSphere(wallcheckright.position, 0.7f, wallLayers);
	       if (isGrounded)
	       {
		       currentTime = 2f;
		       ui.GetComponent<UI>().MessageWallText("Wallrun:3");
		       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
	       }
	       if (isWalling)
	       {
		       currentTime -= 1 * Time.deltaTime;

		       hero.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
		       
			       
		       if (currentTime <= 0f)
		       {
			       isFalling = true;
			       isWalling = false;
			       currentTime = 2f;
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
		       
		       if (target.position.x > 7f)
		       {
			       float f = target.position.x - 7;
			       characterController.Move(new Vector3(-f, 0, 0));
		       }
		       else if (target.position.x < 1f)
		       {
			       float f = 1 - target.position.x;
			       characterController.Move(new Vector3(f, 0, 0));
		       }
			       
			       
	       }

	       if (!(isWallLeft || isWallRight))
		       isWalling = false;
	       
	       
	       
       }

       void CheckDown()
       {
	       if (isdown)
	       {
		       downcurrentTime -= 1 * Time.deltaTime;
		       if (downcurrentTime <= 0f||Input.GetKeyDown("up"))
		       {
			       this.transform.Rotate(-90, 0, 0);
			       characterController.height = 2f;
			       isdown = false;
			       downcurrentTime = 1f;
		       }


	       }
       }

       void CheckFalling()
       {
	       if (isGrounded)
		       isFalling = false;
       }


       public void SetSpawn()
       {


	        if (checkpoints[2])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2, 200);
		       characterController.enabled = true;
		       line=0;
		       for (int i = 0; i < 3; i++)
		       {
			       checkpoints[i] = false;
		       }
	       }
	       else if (checkpoints[1])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2,-202);
		       characterController.enabled = true;
		       line=0;
	       }
	        else if (checkpoints[0])
	       {
		       characterController.enabled = false;
		       target.transform.position=new Vector3(4, 2, -100);
		       characterController.enabled = true;
		       line=0;
	       }
       }

       public void SetCheckpoint(int checkpoint)
       {
	       checkpoints[checkpoint - 1] = true;
       }
       
}
