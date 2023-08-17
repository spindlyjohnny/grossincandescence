using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    public Transform firept;
    public float detectionradius;
    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        levelManager = FindObjectOfType<LevelManager>();
        //target = FindObjectOfType<Player>().transform;
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Death", dead);
        anim.SetBool("Moving", isMoving);
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 2, 0);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        if (Time.time >= nextattacktime) {
            anim.SetTrigger("Attack");
            nextattacktime = Time.time + attackrate;
        }
        Collider[] players = Physics.OverlapSphere(transform.position,detectionradius,LayerMask.GetMask("Player"));
        if(players[0] != null)player = players[0].GetComponent<Player>();
        dir = transform.position - player.transform.position;
        FacePlayer();
    }
    public void Fire() {
        Destroy(Instantiate(projectile, firept.position, Quaternion.identity), 3f);
    }
    private void FixedUpdate() {
        if (player != null && dir.magnitude < detectionradius && dir.magnitude > 1f) {
            canMove = true;
            isMoving = true;
        } 
        else { 
            isMoving = false;
            canMove = false;
        }
        if (canMove) rb.MovePosition(rb.position + movespeed * Time.deltaTime * (dir + 2 * player.transform.position).normalized);
    }
    public override IEnumerator Death() {
        if (!dead) dead = true;
        healthbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        Destroy(gameObject);
        player.souls += soulvalue;
        if (spawned) levelManager.enemieskilled += 1;
        
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,detectionradius);
    }
    protected override void FacePlayer() {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0, angle, 0);
        float dotprod = Vector3.Dot(dir.normalized, transform.forward);
        if (dotprod < 0) transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnspeed * Time.deltaTime);
    }
}
