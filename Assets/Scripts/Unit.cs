using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float hitpoints;
    public float maxhitpoints;
    public Slider healthbar;
    public AudioClip hitsound;
    public GameObject bloodvfx;
    public bool canMove,isHit;
    //public float knockbackforce;
    //public float knockbacklength;
    //public float knockbackcounter;
    //public Vector2 knockbackdir;
    public Animator anim;
    public Rigidbody rb;
    // Start is called before the first frame update
    public void TakeHit(float damage) {
        hitpoints -= damage;
    }
    public void SetHealth(float health, float maxHealth) {
        healthbar.value = health;
        healthbar.maxValue = maxHealth;
    }
    public IEnumerator Hit() {
        isHit = true;
        yield return new WaitForSeconds(1f);
        isHit = false;
    }
    public void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Weapon>() && !GetComponent<Collider>().isTrigger) {
            TakeHit(other.GetComponent<Weapon>().damage);
            StartCoroutine(Hit());
        }
    }
}
