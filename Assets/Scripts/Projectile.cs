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
    Coroutine homing;
    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        players = FindObjectsOfType<Player>();
        FindClosestPlayer();
        dir = target.position - transform.position;
        if (homing != null) StopCoroutine(Move());
        homing = StartCoroutine(Move());
        //rb.velocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        dir = target.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0),turnspeed * Time.deltaTime);
        //Vector3 rotationamt = Vector3.Cross(transform.forward,dir.normalized);
        //rb.angularVelocity = rotationamt * turnspeed;
        //Quaternion lookdir = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookdir, turnspeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) Destroy(gameObject);
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
    IEnumerator Move() {
        Vector3 startPos = transform.position;
        float time = 0;
        while(time < 3) {
            transform.position = Vector3.Lerp(startPos, target.position + new Vector3(0,yOffset,0), time);
            transform.LookAt(target.position + new Vector3(0, yOffset, 0));
            time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
