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
    // Start is called before the first frame update
    void Start()
    {
        upgradename = GetComponentInChildren<Text>();
        upgradename.text = namestring + cost + " souls";
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponentInParent<Bonfire>().player;
    }
    public void IncreaseHealth(float value) {
        if (player.souls < cost) return;
        player.maxhitpoints += value;
        player.hitpoints = player.maxhitpoints;
    }
    public void IncreaseDamage(float value) {
        if (player.souls < cost) return;
        player.GetComponentInChildren<Weapon>().damage += value;
    }
}
