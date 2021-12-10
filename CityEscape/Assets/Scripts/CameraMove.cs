using UnityEngine;
/**
 * Skript, der die Kamera bewegt. Die Kamera soll sich automatisch mit dem Spieler bewegen
 */
public class CameraMove : MonoBehaviour
{
    //Position des Spielers
   public Transform target;
  
   //Wie flüssig die Kamera gehen soll
   public float smoothSpeed = 0.125f;
   //der wert, wie viel die kamera eigentlich vom spieler verschoben werden soll
   public Vector3 offset;
  
   /**
    * Bei FixedUpdate wird die Position der Kamera mithilfe von offset und target ausgerichtet.
    * Sie werden zusammenaddiert und mithilfe von smoothedPosition angepasst, damit die Position
    * der Kamera ständig automatisch nach dem Spieler (zsm. mithilfe von lookat) ausgerichtet ist
    */
   void FixedUpdate ()
   {
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
  
    transform.LookAt(target);
   }

}
