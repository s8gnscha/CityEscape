
using UnityEngine;


/**
 * Movement vom Player. Beschränkt sich zunächst nur auf laufen
 */
public class PlayerMove : MonoBehaviour
{
	
	//Rigidbody vom Spieler
   public Rigidbody rb;
	//Position des Spielers
 	public Transform trans;
   
     
       void Start()
       {
			
           	rb = GetComponent<Rigidbody>();
			
       }
   
       
       /**
        * Bei fixedupdate wird durch die Velocity function vom rigidbody das automatische laufen vom Spieler generiert.
        * Die Auskommentieren sind weitere Ansätze die bisher noch nicht funktionieren ^^"
        */
       void FixedUpdate()
       {
				float speed=5f;
				rb.velocity=new Vector3(0,0,-speed);
                  //rb.AddForce(0, 0, -700 * Time.deltaTime);
					/*Vector3 pos = transform.position;
 					pos.y = pos.y-(50*Time.deltaTime);
 					transform.position = pos;*/
					
       }
}
