using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    Player[] players;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled; // how many enemies have been killed
    public int totalenemiesinwave = 0; // how many enemies spawn in the wave
    public Room currentroom;
    public List<EnemySpawner> enemyspawns;
    public float ogbloodstaintimer;
    public float bloodstaintimer;
    public Text bloodstaintimertext;
    public GameObject gameoverscreen;
    public AudioClip levelmusic;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
        bloodstaintimer = ogbloodstaintimer;
        bloodstaintimertext.transform.parent.gameObject.SetActive(false); // deactivate bloodstain timer on start
        AudioManager.instance.StopMusic(); // stops music from previous scene
    }

    // Update is called once per frame
    void Update()
    {
        bloodstaintimertext.text = bloodstaintimer.ToString("F0");
        if (currentroom.roomstart) {
            if (enemieskilled >= totalenemiesinwave) { // check if all enemies for the wave are dead
                wavecomplete = true;
                EndOfWave();
            }
            if (waves == 0 && currentroom.roomstart) {
                currentroom.roomstart = false;
            }
        }
        if(FindObjectOfType<Bloodstain>() != null && !FindObjectOfType<WinScreen>().screen.activeSelf) { // checks if there is a bloodstain and that the level hasnt been cleared
            bloodstaintimertext.transform.parent.gameObject.SetActive(true);
            bloodstaintimer -= Time.deltaTime;
        }
        if (FindObjectOfType<Bloodstain>(true).collected) { // bloodstain has been collected, reset timer and hide it.
            bloodstaintimer = ogbloodstaintimer;
            bloodstaintimertext.transform.parent.gameObject.SetActive(false);
        }
        if (bloodstaintimer <= 0) {
            foreach (var i in players) { // kill all players if bloodstain timer runs out
                i.hitpoints = 0;
                StartCoroutine(i.TrueDeath());
                AudioManager.instance.StopMusic();
            }
            bloodstaintimer = ogbloodstaintimer;
            gameoverscreen.SetActive(true);
        }
    }
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }
    void EndOfWave() {
        if (waves > 0) {
            waves -= 1;
            enemieskilled = 0; // reset for next wave
            totalenemiesinwave = 0;
            foreach (var i in enemyspawns) {
                i.canSpawn = false; // prevent spawners from spawning after wave ends
                i.currentwave += 1;
                totalenemiesinwave += i.enemiestospawn[i.currentwave]; // adds up number of enemies to spawn from each spawner for the current wave
                i.enemiesspawned = 0; // reset number of enemies spawned from each spawner
            }
        }
        foreach (var i in FindObjectsOfType<Bonfire>(true)) i.gameObject.SetActive(true);
    }
    IEnumerator RespawnCo() {
        //yield return new WaitForSeconds(0.25f);
        for(int i = 0; i < players.Length; i++) {
            if (players[i].dead) {
                players[i].GetComponent<Collider>().enabled = false; // deactivates player collider
                yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
                players[i].GetComponent<Collider>().enabled = true; // reactivates player collider
                players[i].transform.position = players[i].respawnpoint; // moves player to respawn point
                players[i].dead = false;
            }
        }
       
    }
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
