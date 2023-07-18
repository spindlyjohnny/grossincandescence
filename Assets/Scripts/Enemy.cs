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
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //SetHealth(hitpoints, maxhitpoints);
        //anim.SetBool("Hit", isHit);
        agent.SetDestination(target.position);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
    }
    public override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if (other.GetComponentInParent<Player>() && other.GetComponentInParent<Player>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) player = other.GetComponentInParent<Player>();
        return;
    }
    public virtual IEnumerator Death() {
        player.souls += soulvalue;
        agent.velocity = Vector3.zero;
        if (!dead) {
            //Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
            dead = true;
        }
        //healthbar.gameObject.SetActive(false);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //Destroy(Instantiate(maskvfx, transform.position, transform.rotation), 1);
        yield return null;
        Destroy(gameObject);
        //if (spawned) levelManager.enemieskilled += 1; levelManager.totalenemieskilled += 1;
        //for (int i = 0; i < Random.Range(1, dropamt + 1); i++) {
        //    Instantiate(itemdrops[Random.Range(0, itemdrops.Length)], transform.position, transform.rotation);
        //}
    }
}
