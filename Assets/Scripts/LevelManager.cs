using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    Player player;
    public bool wavecomplete;
    public int waves;
    public int enemieskilled;
    public int totalenemiesinwave = 0;
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
                StartCoroutine(EndOfWave());
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
    IEnumerator EndOfWave() {
        if (waves > 0) {
            waves -= 1;
            enemieskilled = 0;
            totalenemiesinwave = 0;
            foreach (var i in enemyspawns) {
                i.canSpawn = false;
                i.currentwave += 1;
                totalenemiesinwave += i.enemiestospawn[i.currentwave];
                i.enemiesspawned = 0;
            }
            yield return new WaitForSeconds(4f);
            foreach (var i in enemyspawns) {
                i.canSpawn = true;
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
