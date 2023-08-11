using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    public Transform firept;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        levelManager = FindObjectOfType<LevelManager>();
        //target = FindObjectOfType<Player>().transform;
        //transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //SetHealth(hitpoints, maxhitpoints);
        anim.SetBool("Hit", isHit);
        //healthbar.gameObject.transform.position = transform.position + new Vector3(0, 2, 0);
        if (hitpoints <= 0) {
            Death();
        }
        if (Time.time >= nextattacktime) {
            anim.SetTrigger("Attack");
            nextattacktime = Time.time + attackrate;
        }
    }
    public void Fire() {
        Instantiate(projectile, firept.position, Quaternion.identity);
    }
}
