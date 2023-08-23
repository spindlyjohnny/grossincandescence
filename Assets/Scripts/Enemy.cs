using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    //public float damage;
    NavMeshAgent agent;
    public Transform target;
    public bool spawned;
    protected LevelManager levelManager;
    public int soulvalue;
    public Player player;
    public bool isMoving; //to check if the enemy is moving
    protected Player[] players;
    protected float nextattacktime;
    public float attackrate;
    public float turnspeed;
    protected Vector3 dir;
    public float healthbarheight; 
    public float attackingtime; //disable moving while attacking
    private bool isattacking;
    private bool faceplayerattack;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
        players = FindObjectsOfType<Player>(); // find all players in order to find the closest one.
        FindClosestPlayer(); // finds closest player and sets it as the target to follow.
        //target = FindObjectOfType<Player>().transform;
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.gameoverscreen.activeSelf) return; // don't do anything if game is over.
        dir = transform.position - target.position; // distance between enemy and target
        SetHealth(hitpoints, maxhitpoints); // sets health bar value to health
        anim.SetBool("Hit", isHit);
        anim.SetBool("Moving", isMoving);
        
        if(ContainsParam("Death")) anim.SetBool("Death", dead); // ContainsParam function is used to check if "Death" anim param exists since not all enemy types have it
        
        if(canMove && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))agent.SetDestination(target.position); // follow player if can move and not attacking
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, healthbarheight, 0);
        
        if (agent.hasPath && dir.magnitude > agent.stoppingDistance)isMoving = true;
        else isMoving = false;
        
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        attackingtime += Time.deltaTime;
        if (dir.magnitude <= agent.stoppingDistance && Time.time >= nextattacktime) {  // check if enemy can attack and target is within range
            anim.SetTrigger("Attack");
            nextattacktime = Time.time + attackrate;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f) isattacking = false;
            if (isattacking == false) {
                attackingtime = 0;
                FacePlayerAttack();
                isattacking = true;
            }
        }
        if (attackingtime >= anim.GetCurrentAnimatorStateInfo(0).length) isattacking = false;
        // ensure enemy always faces player.
        FacePlayer();
        FindClosestPlayer(); // changes target according to distance.
    }
    public virtual IEnumerator Death() {
        if(agent)agent.velocity = Vector3.zero; // check for agent since not all enemies have agents
        if (!dead) dead = true;
        healthbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), bloodvfx.GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
        player.souls += soulvalue;
        if (spawned) levelManager.enemieskilled += 1; // only adds to the wave's enemy kill count if the enemy was spawned by a spawner.
    }
    public override IEnumerator Hit() {
        isHit = true;
        AudioManager.instance.PlaySFX(hitsound);
        yield return new WaitForEndOfFrame();
        isHit = false;
    }
    protected void FindClosestPlayer() { // finds closest player and sets them as the target and player vars.
        float closest = 999;
        for (int i = 0; i < players.Length; i++) { // iterate through list of players
            var dist = (players[i].transform.position - transform.position).magnitude; // get distance between enemy and current player
            if (dist < closest) {
                closest = dist;
            }
            if ((players[i].transform.position - transform.position).magnitude == closest) { // check if current player is the closest one
                target = players[i].transform;
                player = players[i];
            }
        }
    }
    bool ContainsParam(string paramname) { // check if an anim param exists
        foreach (var param in anim.parameters) {
            if (param.name == paramname) return true;
        }
        return false;
    }
    protected virtual void FacePlayer() {
        if (isattacking == true) return;
        Quaternion lookRotation = Quaternion.LookRotation(-(dir));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnspeed * Time.deltaTime);
    }
    void FacePlayerAttack() // look at player after an attack
    {
        Quaternion lookRotation = Quaternion.LookRotation(-(dir));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, (turnspeed * 20) * Time.deltaTime);
    }
}