using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    //public float damage;
    NavMeshAgent agent;
    public Transform target;
    public Transform spawner;
    public bool spawned;
    protected LevelManager levelManager;
    public int soulvalue;
    public Player player;
    public bool isMoving; //to check if the skeleton is moving
    Player[] players;
    protected float nextattacktime;
    public float attackrate;
    public float turnspeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
        target = FindObjectOfType<Player>().transform;
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        players = FindObjectsOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - target.position;
        SetHealth(hitpoints, maxhitpoints);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Moving", isMoving);
        if(ContainsParam("Death")) anim.SetBool("Death", dead);
        agent.SetDestination(target.position); // follow player
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 2, 0);
        if (agent.hasPath && dir.magnitude > agent.stoppingDistance)isMoving = true;
        else isMoving = false;
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        if(dir.magnitude <= agent.stoppingDistance && Time.time >= nextattacktime) {
            anim.SetTrigger("Attack");
            nextattacktime = Time.time + attackrate;
        }
        // ensure enemy always faces player.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnspeed * Time.deltaTime);
        FindClosestPlayer();
    }
    public virtual IEnumerator Death() {
        if(agent)agent.velocity = Vector3.zero;
        if (!dead) dead = true;
        healthbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        Destroy(gameObject);
        player.souls += soulvalue;
        if (spawned) levelManager.enemieskilled += 1; // only adds to the wave's enemy kill count if the enemy was spawned by a spawner.
    }
    public override IEnumerator Hit() {
        isHit = true;
        yield return new WaitForEndOfFrame();
        isHit = false;
    }
    void FindClosestPlayer() {
        float closest = 999; float furthest = 0;
        for (int i = 0; i < players.Length; i++) {
            var dist = (players[i].transform.position - transform.position).magnitude;
            if (dist < closest) {
                closest = dist;
            }
            else if (dist > furthest) {
                furthest = dist;
            }
            if ((players[i].transform.position - transform.position).magnitude == closest) {
                target = players[i].transform;
                player = players[i];
            }
        }
    }
    bool ContainsParam(string paramname) {
        foreach (var param in anim.parameters) {
            if (param.name == paramname) return true;
        }
        return false;
    }
}