using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour
{

    public Transform target;
    public float distance = 2.8f;
    public float targetHeight = 2.0f;
    private float x = 0.0f;
    private float y = 0.0f;
    private Quaternion rotation;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    void LateUpdate() {
   if(!target)
      return;
   
      y = target.eulerAngles.y;
 
 
  // ROTATE CAMERA:
   rotation = Quaternion.Euler(x, y, 0);
   //transform.rotation = rotation;
   
   // POSITION CAMERA:
   var position = target.position - (Vector3.forward * distance + new Vector3(0,-targetHeight,0));
   transform.position = position;
}
}
