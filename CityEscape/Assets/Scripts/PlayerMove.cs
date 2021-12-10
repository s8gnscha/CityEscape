
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   public Rigidbody rb;

 	public Transform trans;
   
     
       void Start()
       {
			
           	rb = GetComponent<Rigidbody>();
			
       }
   
       void FixedUpdate()
       {
				/*float speed=5f;
				rb.velocity=new Vector3(0,0,-speed);
                  //rb.AddForce(0, 0, -700 * Time.deltaTime);
					/*Vector3 pos = transform.position;
 					pos.y = pos.y-(50*Time.deltaTime);
 					transform.position = pos;*/
					
       }
}
