using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float movespeed = 5f,turnspeed;
    public Vector3 movement;
    LevelManager levelManager;
    public Vector3 respawnpoint;
    public float iframes; // invincibility time
    public int souls;
    public Text soulcount; // soul counter
    private float stamina;
    public float maxstamina;
    public Slider staminabar;
    Coroutine staminaregen;
    public GameObject bloodstain;
    public float dodgeamt = 3; // how far dodging pushes you
    public float dodgecooldown;
    float actdodgecooldown;
    public int heals;
    public Text healcount;
    public enum Players { P1, P2 }
    public Players playerNum = Players.P1;
    public enum Actions{attack,dodge}
    public CameraController cam;
    public bool isHealing;
    public GameObject deathvfx;
    bool canTurn;
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
        canTurn = true;
        isHealing = false;
        heals = 5;
        dead = false;
    }
    // Update is called once per frame
    void Update() {
        SetHealth(hitpoints, maxhitpoints);
        staminabar.value = stamina;
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.z);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Death", dead);
        anim.SetBool("Heal", isHealing);
        soulcount.text = souls.ToString();
        healcount.text = heals.ToString();
        movement = new Vector3(Input.GetAxis("Horizontal " + playerNum.ToString()), 0, Input.GetAxis("Vertical " + playerNum.ToString()));
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Dodge") && !isHit && !isHealing && !dead && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) canTurn = true;
        // set rotation of player while moving.
        Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
        if (movement.magnitude >= .1f && canTurn) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnspeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Attack " + playerNum.ToString())) ConsumeStamina(Actions.attack);
        if (actdodgecooldown <= 0) { // checks if dodge cooldown is 0 or less
            anim.ResetTrigger("Rolling");
            if (Input.GetButtonDown("Dodge " + playerNum.ToString())) {
                ConsumeStamina(Actions.dodge,25f);
            }
        } 
        else {
            actdodgecooldown -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Heal " + playerNum.ToString())) {
            StartCoroutine(Heal());
        }
        //if (dead && !bloodstain.GetComponent<Bloodstain>().collected) bloodstain.SetActive(false);
        if (hitpoints <= 0) {
            dead = true;
            if(!levelManager.gameoverscreen.activeSelf) StartCoroutine(Death());
            hitpoints = maxhitpoints;
            Bloodstain bs = FindObjectOfType<Bloodstain>();
            if (bs) bs.gameObject.SetActive(false);
        }
        if (levelManager.gameoverscreen.activeSelf) {
            Destroy(Instantiate(deathvfx, transform.position, Quaternion.identity), 1);
        }
    }
    private void FixedUpdate() {
        if (canMove && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !dead) rb.MovePosition(rb.position + movespeed * Time.deltaTime * movement.normalized);
        float delta = Time.fixedDeltaTime;
        if(cam != null) {
            cam.FollowTarget(delta);
            cam.Rotate(delta);
        }
    }
    public override void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Weapon>() && other.GetComponentInParent<Enemy>() != null && !dead) {
            if(other.GetComponentInParent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) Damaged(other);
        }
        if (other.GetComponent<Projectile>() && !dead) {
            Damaged(other);
        }
    }
    public virtual IEnumerator Death() {
        movement = Vector2.zero;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        Instantiate(bloodstain, transform.position, transform.rotation);
        bloodstain.GetComponent<Bloodstain>().souls = souls;
        bloodstain.GetComponent<Bloodstain>().collected = false;
        souls = 0;
        if(levelManager.bloodstaintimer != 0)levelManager.Respawn();
    }
    public override IEnumerator Hit() {
        if (dead) yield return null;
        isHit = true;
        StartCoroutine(Invincibility());
        yield return new WaitForEndOfFrame();
        isHit = false;
    }
    IEnumerator Invincibility() {
        Physics.IgnoreLayerCollision(3, 6, true); // disable collision with enemy layer
        yield return new WaitForSeconds(iframes);
        Physics.IgnoreLayerCollision(3, 6, false);
    }
    protected virtual void ConsumeStamina(Actions action,float amt = 15f) {
        if (!(stamina - amt >= 0) || !canMove) return; // check if player has enough stamina
        switch (action) {
            case Actions.attack:
                anim.SetTrigger("Attack");
                stamina -= amt;
                if (staminaregen != null) StopCoroutine(StaminaRegen()); // restart coroutine
                staminaregen = StartCoroutine(StaminaRegen());
                break;
            case Actions.dodge:
                Dodge();
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
    void Dodge() {
        actdodgecooldown = dodgecooldown;
        anim.SetTrigger("Rolling");
        Vector3 dodgedir = movement.normalized;
        movement = dodgeamt * dodgedir;
        StartCoroutine(Invincibility());
        //canMove = false;
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        //canMove = true;
    }
    IEnumerator Heal() {
        if (heals == 0) yield return null;
        isHealing = true;
        heals -= 1;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        isHealing = false;
        hitpoints += maxhitpoints * .25f;
        if (hitpoints > maxhitpoints) hitpoints = maxhitpoints;
    }
}
