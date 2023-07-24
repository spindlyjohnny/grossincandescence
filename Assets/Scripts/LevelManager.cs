using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    Player player;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled; // how many enemies have been killed
    public int totalenemiesinwave = 0; // how many enemies spawn in the wave
    public Room currentroom;
    public List<EnemySpawner> enemyspawns;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentroom.roomstart) {
            //PlayerPrefs.SetInt("Current Room", rooms.IndexOf(currentroom));
            if (enemieskilled == totalenemiesinwave) {
                wavecomplete = true;
                EndOfWave();
            }
            if (waves == 0 && currentroom.roomstart) {
                currentroom.roomstart = false;
                //AudioManager.instance.PlaySFX(AudioManager.instance.entranceSound);
            }
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
    }
    IEnumerator RespawnCo() {
        yield return new WaitForSeconds(0.25f);
        player.gameObject.SetActive(false); // deactivates player
        yield return new WaitForSeconds(waitToRespawn); // how long to wait before respawning player
        player.gameObject.SetActive(true); // reactivates player
        player.transform.position = player.respawnpoint; // moves player to respawn point
        player.dead = false;
    }
}
