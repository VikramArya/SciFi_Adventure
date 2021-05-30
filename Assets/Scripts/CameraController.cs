using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public bool useOffsetValues;

    public float rotateSpeed;
    public Transform pivot;

    public float maxVeiwAngle;
    public float minVeiwAngle;

    public bool invertY; 
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(canMove)
        {
            pivot.transform.position = target.transform.position;

            //Get the XPosition of the mouse & rotate target
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            pivot.Rotate(0f, horizontal, 0f);
            //Get the YPosition and rotate the pivot
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            //pivot.Rotate(vertical, 0, 0);
            if(invertY)
            {
                pivot.Rotate(vertical, 0, 0);
            } else {
                pivot.Rotate(-vertical, 0, 0);
            }

            //Limit the Up - Down camera rotation
            if(pivot.rotation.eulerAngles.x > maxVeiwAngle && pivot.rotation.eulerAngles.x < 180f)
            {
                pivot.rotation = Quaternion.Euler(maxVeiwAngle, 0 , 0);
            }

            if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minVeiwAngle)
            {
                pivot.rotation = Quaternion.Euler(360f + minVeiwAngle, 0 , 0);
            }

            //Move the camera based on the the position 
            float desiredXAngle = pivot.eulerAngles.x;
            float desiredYAngle = pivot.eulerAngles.y;  
            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0f);
            transform.position = target.position - (rotation * offset); 
    

            //transform.position = target.position - offset;
            if(transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
            }
            transform.LookAt(target);
        }
    }
}
