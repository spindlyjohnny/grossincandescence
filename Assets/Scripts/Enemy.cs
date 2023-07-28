using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    public float damage;
    NavMeshAgent agent;
    public Transform target;
    public Transform spawner;
    public bool spawned;
    LevelManager levelManager;
    public int soulvalue;
    public Player player;
    public bool isMoving; //to check if the skeleton is moving
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
        target = FindObjectOfType<Player>().transform;
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Moving", isMoving);
        agent.SetDestination(target.position); // follow player
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 2, 0);
        if (agent.hasPath)isMoving = true;
        else isMoving = false;
        if (hitpoints <= 0) {
            Death();
        }
        if((transform.position - target.position).magnitude <= agent.stoppingDistance) {
            anim.SetTrigger("Attack");
        }
    }
    public override void OnTriggerEnter(Collider other) {
        // sets player var to whichever player attacks the enemy and checks if the enemy got hit by a weapon. This is to add souls to the correct player upon death.
        if (other.GetComponent<Weapon>() && other.GetComponentInParent<Player>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) player = other.GetComponentInParent<Player>();
        base.OnTriggerEnter(other);
    }
    public virtual void Death() {
        player.souls += soulvalue;
        agent.velocity = Vector3.zero;
        if (!dead) {
            Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
            dead = true;
        }
        healthbar.gameObject.SetActive(false);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
        Destroy(gameObject);
        if (spawned) levelManager.enemieskilled += 1; // only adds to the wave's enemy kill count if the enemy was spawned by a spawner.
    }
}
