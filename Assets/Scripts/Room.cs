using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {
    public int roomwaves; // number of waves
    public bool roomstart; // controls if enemies should start spawning or not
    public EnemySpawner[] myenemyspawns; // spawners to control
    Player player;
    LevelManager levelManager;
    //WaveClearText clearText;
    //[SerializeField] GameObject[] triggers;
    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        //clearText = FindObjectOfType<WaveClearText>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            RoomTrigger();
        }
    }
    void RoomTrigger() {
        roomstart = true;
        levelManager.waves = roomwaves;
        levelManager.currentroom = gameObject.GetComponent<Room>();
        levelManager.wavecomplete = false;
        levelManager.enemyspawns.Clear();
        //clearText.gameObject.SetActive(true);
        //clearText.GetComponent<Text>().color = new Color32(0,0,0,255);
        foreach (var i in myenemyspawns) {
            levelManager.enemyspawns.Add(i);
            i.canSpawn = true;
        }
        foreach (var i in levelManager.enemyspawns) {
            levelManager.totalenemiesinwave += i.enemiestospawn[i.currentwave]; // adds up the number of enemies to spawn from each spawner for the current wave
        }
        player.respawnpoint = transform.position;
        gameObject.SetActive(false);
        //AudioManager.instance.PlaySFX(AudioManager.instance.exitSound);
    }
}