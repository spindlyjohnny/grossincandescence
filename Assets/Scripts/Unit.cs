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
    public bool canMove,isHit,dead;
    public Animator anim;
    public Rigidbody rb;
    public float knockbackforce;
    public float knockbacklength;
    public float knockbackcounter;
    public Vector2 knockbackdir;
    // Start is called before the first frame update
    public void TakeHit(float damage) {
        hitpoints -= damage;
    }
    public void SetHealth(float health, float maxHealth) {
        healthbar.value = health;
        healthbar.maxValue = maxHealth;
    }
    public virtual IEnumerator Hit() { // function is set to virtual to account for different wait lengths
        isHit = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        isHit = false;
    }
    protected void OnEnable() {
        isHit = false;
        Physics.IgnoreLayerCollision(3, 6, false);
    }
    public virtual void OnTriggerEnter(Collider other) {
        if (other.GetComponentInParent<Weapon>() && other.GetComponentInParent<Unit>().anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !dead) { // take damage if other is a weapon and is an attacking unit
            Damaged(other);
        }
    }
    protected virtual void Damaged(Collider other) {
        TakeHit(other.GetComponentInParent<Weapon>().damage);
        StartCoroutine(Hit());
    }
}
