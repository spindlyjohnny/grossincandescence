using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    public int cost; // cost of upgrade
    Player player;
    Text upgradename;
    public string namestring;
    Button button; // upgrades are buttons
    public Upgrade prev; // previous upgrade
    public bool locked;
    public Bonfire hub; // hub bonfire
    // Start is called before the first frame update
    void Start()
    {
        upgradename = GetComponentInChildren<Text>();
        upgradename.text = namestring + cost + " souls";
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        player = hub.player;
        button.interactable = !locked; // button is not interactable if upgrade is locked, interactable is it is not locked
        if (prev != null) { // check existence to not throw errors for base upgrades
            if (prev.locked) locked = false;
        }
    }
    public void IncreaseHealth(float value) {
        if (player.souls < cost) return;
        if (player.hitpoints == player.maxhealth) return;
        AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        player.souls -= cost;
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
        locked = true; // lock upgrade after getting it so it cannot be purchased again
        if (prev) prev.locked = false;
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Scrollbar>().gameObject);
    }
    public void IncreaseDamage(float value) {
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        if (player.souls < cost) return;
        if (weapon.damage == weapon.maxdamage) return;
        AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        player.souls -= cost;
        weapon.damage += value;
        locked = true;
        if (prev) prev.locked = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Scrollbar>().gameObject);
    }
}
