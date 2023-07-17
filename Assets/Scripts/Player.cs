using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public float movespeed = 5f,turnspeed;
    Vector3 movement;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        anim.SetBool("Hit", isHit);
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnspeed, 0);
        if (Input.GetButtonDown("Attack")) anim.SetTrigger("Attack");
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement.normalized * movespeed * Time.deltaTime);
    }
}
