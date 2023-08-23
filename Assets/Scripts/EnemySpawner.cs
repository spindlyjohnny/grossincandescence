using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesspawned; // number of enemies spawned
    public int[] enemiestospawn; // number of enemies to spawn for each wave
    public GameObject[] instprefab; // types of enemies to spawn
    LevelManager levelManager;
    Player player;
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
            if(currentwave < 5) {
                GameObject go = Instantiate(instprefab[Random.Range(0, instprefab.Length - 1)], transform.position, transform.rotation); // spawns a mage or skeleton
                go.GetComponentInChildren<Enemy>().spawned = true;
                go.GetComponentInChildren<Enemy>().canMove = true;
            }
            else if (currentwave >= 5){
                GameObject go = Instantiate(instprefab[Random.Range(0, instprefab.Length)], transform.position, transform.rotation); // spawns any enemy type
                go.GetComponentInChildren<Enemy>().spawned = true;
                go.GetComponentInChildren<Enemy>().canMove = true;
            }
            enemiesspawned++;
        }
    }
}
