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

    private float totalspeed;
    private float totalturn;
    public float timer = 0;
    Vector3 targetLastPos;
    // Start is called before the first frame update
    void Start()
    {
        totalspeed = speed;
        totalturn = turnspeed;
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

        //in 2 seconds make 
        if (timer >= 1)
        {
            timer = 1;
        }
        else
        {
            timer += Time.deltaTime;
        }
        speed = totalspeed * timer; //Increases time after spawn
        //turnspeed = totalturn * (1 - timer); //decrease turn rate

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
    IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        float time = 0;
        while (time < 0.5)
        {
            transform.position = Vector3.Lerp(startPos, target.position + new Vector3(0, yOffset, 0), time);
            transform.LookAt(target.position + new Vector3(0, yOffset, 0));
            time += Time.deltaTime * speed;
            targetLastPos = target.position;
            yield return null;
        }
        while (time >= 0.5)
        {

            //// Calculate the direction to move (target position - current position)
            //Vector3 moveDirection = (targetLastPos - transform.position).normalized;

            //// Update the position in the move direction
            //transform.position += Vector3.Lerp(1 * moveDirection * speed * Time.deltaTime, 60 * moveDirection * speed * Time.deltaTime, 0.5f);

            Vector3 forwardMovement = Vector3.Lerp(speed * Time.deltaTime * transform.forward, 40 * speed * Time.deltaTime * transform.forward, 0.5f);
            transform.position += forwardMovement;

            time += Time.deltaTime * speed;
            yield return null;
        }
    }
}
