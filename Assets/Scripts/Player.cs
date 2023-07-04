using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    public float speed = 5f;
    //Animator anim;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //add gravity acceleration every frame
        if (controller.isGrounded) velocity = Vector3.zero;
        else velocity += Physics.gravity * Time.deltaTime;
        Vector3 displacement = transform.TransformDirection(movement.normalized) + velocity; // convert mvmt from global coords to local coords.
        controller.Move(displacement * speed * Time.deltaTime);
        //anim.SetFloat("moveX", movement.x);
        //anim.SetFloat("moveY", movement.z);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        //if (Input.GetButtonDown("Fire1")) anim.SetTrigger("Attack");
    }
}
