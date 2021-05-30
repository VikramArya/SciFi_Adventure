using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    //private Rigidbody myRigidbody;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    private Vector3 moveDirection;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    public float knockbackForce;
    public float knockbackTime;
    private float knockbackCounter;

    // Start is called before the first frame update
    void Start()
    {
        //myRigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*myRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, myRigidbody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        if(Input.GetButtonDown("Jump"))
        {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpForce, myRigidbody.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if(knockbackCounter <= 0)
        {

            float YStore = moveDirection.y;

            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = YStore;

            if(controller.isGrounded)
            {
                moveDirection.y = 0f;
                if(Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                } 
            }

        } else {
            knockbackCounter -= Time.deltaTime;
        }
            
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);

            // Move the player in Different Directions based on camera

            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 )
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

        anim.SetBool("IsGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        moveDirection = direction * knockbackForce;
        moveDirection.y = knockbackForce;
    }
    
}
