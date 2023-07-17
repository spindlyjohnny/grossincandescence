using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    public float damage;
    NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("Hit", isHit);
        agent.SetDestination(target.position);
    }
}
