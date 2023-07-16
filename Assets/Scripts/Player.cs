using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public CharacterController controller;
    public float movespeed = 5f,turnspeed;
    Vector3 velocity;
    Vector3 movement;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
    }

    // Update is called once per frame
    void Update() {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        anim.SetBool("Hit", isHit);
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
        //if (knockbackcounter > 0) {
        //    knockbackcounter -= Time.deltaTime;
        //    movement = knockbackdir.normalized * knockbackforce;
        //}
    }
   //void CollisionDetection() {
   //     RaycastHit hit;
   //     Physics.SphereCast(transform.position + new Vector3(0,1,0), controller.radius, Vector3.forward, out hit, Mathf.Infinity);
   //     if (hit.collider == null) return;
   //     if (hit.collider.GetComponent<Enemy>()) {
   //         TakeHit(hit.collider.GetComponent<Enemy>().damage);
   //         knockbackdir = transform.position - hit.transform.position;
   //         anim.SetTrigger("Hit");
   //     }
   // }
    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position + new Vector3(0,1,0), controller.radius);
    //}
}
