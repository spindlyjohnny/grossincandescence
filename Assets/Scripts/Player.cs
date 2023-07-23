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
    public float iframes; // invincibility time
    public int souls;
    public Text soulcount; // soul counter
    private float stamina;
    public float maxstamina;
    public Slider staminabar;
    Coroutine staminaregen;
    public enum Actions{attack,dodge,block}
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
        respawnpoint = transform.position;
        staminabar.value = stamina;
        staminabar.maxValue = maxstamina;
        stamina = maxstamina;
        canMove = true;
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
        if(canMove)transform.Rotate(0, Input.GetAxis("Mouse X") * turnspeed, 0);
        if (Input.GetButtonDown("Attack")) ConsumeStamina(Actions.attack,15f);
        if (hitpoints <= 0) {
            dead = true;
            Death();
            levelManager.Respawn();
            hitpoints = maxhitpoints;
        }
    }
    private void FixedUpdate() {
        if (canMove) rb.MovePosition(rb.position + movement.normalized * movespeed * Time.deltaTime);
    }
    public void Death() {
        dead = true;
        movement = Vector2.zero;
        //Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
    }
    public override IEnumerator Hit() {
        isHit = true;
        StartCoroutine(Invincibility());
        isHit = false;
        yield return null;
    }
    IEnumerator Invincibility() {
        Physics.IgnoreLayerCollision(3, 6, true); // disable collision with layer 6
        yield return new WaitForSeconds(iframes);
        Physics.IgnoreLayerCollision(3, 6, false);
    }
    protected virtual void ConsumeStamina(Actions action,float amt) {
        if (!(stamina - amt >= 0) || !canMove) return; // check if player has enough stamina
        switch (action) {
            case Actions.attack:
                anim.SetTrigger("Attack");
                stamina -= amt;
                if (staminaregen != null) StopCoroutine(StaminaRegen()); // restart coroutine
                staminaregen = StartCoroutine(StaminaRegen());
                break;
            case Actions.dodge:
                stamina -= amt;
                if (staminaregen != null) StopCoroutine(StaminaRegen());
                staminaregen = StartCoroutine(StaminaRegen());
                break;
            case Actions.block:
                stamina -= amt;
                if (staminaregen != null) StopCoroutine(StaminaRegen());
                staminaregen = StartCoroutine(StaminaRegen());
                break;
        }
    }
    IEnumerator StaminaRegen() {
        yield return new WaitForSeconds(2f);
        while(stamina < maxstamina) {
            stamina += maxstamina / 100;
            yield return new WaitForSeconds(0.1f);
        }
        staminaregen = null;
    }
}
