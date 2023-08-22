using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesspawned; // number of enemies spawned
    public int[] enemiestospawn; // number of enemies to spawn for each wave
    public GameObject[] instprefab; // types of enemies to spawn
    public float instrate; // time between each enemy spawning
    LevelManager levelManager;
    Player player;
    float nextinsttime;
    public bool canSpawn;
    public int currentwave; // iterate through enemiestospawn
    
    // Start is called before the first frame update
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
        currentwave = 0;
    }

    // Update is called once per frame
    void Update() {

        if (canSpawn) {
            Spawn();
        }
        if (!levelManager.currentroom.roomstart && levelManager.wavecomplete) gameObject.SetActive(false); // deactivates object if room is complete to reduce number of objects to load
    }
    void Spawn() {
        if (enemiesspawned <= enemiestospawn[currentwave] - 1 && !player.dead) { // remember that length of enemiestospawn has to be 1 more than the number of waves
            if (Time.time < nextinsttime) return;
            GameObject go = Instantiate(instprefab[Random.Range(0, instprefab.Length)], transform.position, transform.rotation); // spawns a random enemy type
            go.GetComponentInChildren<Enemy>().spawned = true;
            go.GetComponentInChildren<Enemy>().canMove = true;
            nextinsttime = Time.time + instrate;
            enemiesspawned++;
        }
    }
}
