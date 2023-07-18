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
    public float stamina;
    public Slider staminabar;
    public enum Actions{attack,dodge,block}
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
        staminabar.value = stamina;
        staminabar.maxValue = stamina;
    }
    // Update is called once per frame
    void Update() {
        SetHealth(hitpoints, maxhitpoints);
        staminabar.value = stamina;
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        anim.SetBool("Hit", isHit);
        soulcount.text = souls.ToString();
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Rotate(0, Input.GetAxis("Mouse X") * turnspeed, 0);
        if (Input.GetButtonDown("Attack")) ConsumeStamina(Actions.attack);
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
    protected virtual void ConsumeStamina(Actions action) {
        switch (action) {
            case Actions.attack:
                anim.SetTrigger("Attack");
                stamina -= 1;
                break;
            case Actions.dodge:
                stamina -= 1;
                break;
            case Actions.block:
                stamina -= 1;
                break;
        }
    }
}
