using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Skript, der die Kamera bewegt. Die Kamera soll sich automatisch mit dem Spieler bewegen
 */
public class CameraMove : MonoBehaviour
{
    //Position des Spielers
   public Transform target;
  
   //Wie flüssig die Kamera gehen soll
   public float smoothSpeed = 1f;
   //der wert, wie viel die kamera eigentlich vom spieler verschoben werden soll
   public Vector3 offset;

   public float cameraX;
   /**
    * Bei FixedUpdate wird die Position der Kamera mithilfe von offset und target ausgerichtet.
    * Sie werden zusammenaddiert und mithilfe von smoothedPosition angepasst, damit die Position
    * der Kamera ständig automatisch nach dem Spieler (zsm. mithilfe von lookat) ausgerichtet ist
    */
   void FixedUpdate ()
   {
    Vector3 desiredPosition = new Vector3(cameraX,0,target.position.z) + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
    
    
   }

}
