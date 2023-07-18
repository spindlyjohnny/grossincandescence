using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float movespeed = 5f,turnspeed;
    Vector3 movement;
    LevelManager levelManager;
    public Vector3 respawnpoint;
    public float iframes;
    public int souls;
    public Text soulcount;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
    }
    // Update is called once per frame
    void Update() {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        anim.SetBool("Hit", isHit);
        soulcount.text = souls.ToString();
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnspeed, 0);
        if (Input.GetButtonDown("Attack")) anim.SetTrigger("Attack");
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
        }
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement.normalized * movespeed * Time.deltaTime);
    }
    public void Death() {
        dead = true;
        rb.velocity = Vector2.zero;
        //Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
    }
    public override IEnumerator Hit() {
        isHit = true;
        Physics.IgnoreLayerCollision(3, 6, true);
        yield return new WaitForSeconds(iframes);
        isHit = false;
        Physics.IgnoreLayerCollision(3, 6, false);
    }
}
