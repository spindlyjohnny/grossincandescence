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
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(hitpoints, maxhitpoints);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Death", dead);
        healthbar.gameObject.transform.position = transform.position + new Vector3(0, 2, 0);
        if (hitpoints <= 0) {
            StartCoroutine(Death());
        }
        if (Time.time >= nextattacktime) {
            anim.SetTrigger("Attack");
            nextattacktime = Time.time + attackrate;
        }
    }
    public void Fire() {
        Destroy(Instantiate(projectile, firept.position, Quaternion.identity), 3f);
    }
    public override void OnTriggerEnter(Collider other) {
        if(other.GetComponentInParent<Weapon>() && other.GetComponentInParent<Player>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !dead) {
            player = other.GetComponentInParent<Player>();
            Damaged(other);
        }
    }
}
