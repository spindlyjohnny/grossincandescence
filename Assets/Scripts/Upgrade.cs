using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public int cost;
    Player player;
    Text upgradename;
    public string namestring;
    Button button;
    public Upgrade prev;
    public bool locked;
    public Bonfire hub;
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
        button.interactable = !locked;
        if(prev != null) {
            if (prev.locked) locked = false;
        }
    }
    public void IncreaseHealth(float value) {
        if (player.souls < cost) return;
        AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        player.souls -= cost;
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
        locked = true;
    }
    public void IncreaseDamage(float value) {
        if (player.souls < cost) return;
        AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        player.souls -= cost;
        Weapon weapon = player.GetComponentInChildren<Weapon>();
        weapon.damage += value;
        locked = true;
    }
}
