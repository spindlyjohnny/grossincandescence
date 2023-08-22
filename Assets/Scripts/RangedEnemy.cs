using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectile;
    public Transform firept;
    public AudioClip spellsound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitpoints = maxhitpoints;
        levelManager = FindObjectOfType<LevelManager>();
        //target = FindObjectOfType<Player>().transform;
        transform.parent.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        players = FindObjectsOfType<Player>();
        FindClosestPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.gameoverscreen.activeSelf) return;
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
        FindClosestPlayer();
        dir = transform.position - target.position;
        FacePlayer();
    }
    public void Fire() {
        Destroy(Instantiate(projectile, firept.position,transform.rotation), 3f);
        AudioManager.instance.PlaySFX(spellsound);
    }
    public override IEnumerator Death() {
        if (!dead) dead = true;
        healthbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(Instantiate(bloodvfx, transform.position, transform.rotation), 1);
        Destroy(gameObject);
        player.souls += soulvalue;
        if (spawned) levelManager.enemieskilled += 1;
        
    }
}
