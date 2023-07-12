using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    public float movespeed = 5f,turnspeed;
    Animator anim;
    Vector3 velocity;
    Vector3 movement;
    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //add gravity acceleration every frame
        if (controller.isGrounded) {
            velocity = Vector3.zero;
        }
        else { 
            velocity += Physics.gravity * Time.deltaTime; 
        }
        Vector3 displacement = transform.TransformDirection(movement.normalized) + velocity; // convert mvmt from global coords to local coords.
        controller.Move(displacement * movespeed * Time.deltaTime);
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnspeed, 0);
        if (Input.GetButtonDown("Attack")) anim.SetTrigger("Attack");
    }
}
