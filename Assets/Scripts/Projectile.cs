using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    Rigidbody rb;
    public float speed;
    Player[] players;
    public Transform target;
    Vector3 dir;
    public float turnspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        players = FindObjectsOfType<Player>();
        FindClosestPlayer();
        dir = target.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationamt = Vector3.Cross(transform.forward,dir.normalized);
        rb.angularVelocity = rotationamt * turnspeed;
    }
    void FindClosestPlayer() {
        float closest = 999; float furthest = 0;
        for (int i = 0; i < players.Length; i++) {
            var dist = (players[i].transform.position - transform.position).magnitude;
            if (dist < closest) {
                closest = dist;
            } else if (dist > furthest) {
                furthest = dist;
            }
            if ((players[i].transform.position - transform.position).magnitude == closest) {
                target = players[i].transform;
            }
        }
    }
}
