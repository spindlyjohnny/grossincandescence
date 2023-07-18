using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesspawned;
    public int[] enemiestospawn;
    // Start is called before the first frame update
    public GameObject[] instprefab;
    public float instrate;
    LevelManager levelManager;
    Player player;
    float nextinsttime;
    public bool canSpawn;
    public int currentwave;
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
        //if (!levelManager.currentroom.GetComponent<Room>().roomstart && levelManager.wavecomplete) gameObject.SetActive(false);
    }
    void Spawn() {
        if (enemiesspawned <= enemiestospawn[currentwave] - 1 && !player.dead) { // remember that length of enemiestospawn has to be 1 more than the number of waves
            if (Time.time < nextinsttime) return;
            GameObject go = Instantiate(instprefab[Random.Range(0, instprefab.Length)], transform.position, transform.rotation);
            go.GetComponentInChildren<Enemy>().spawned = true;
            go.GetComponentInChildren<Enemy>().canMove = true;
            go.GetComponentInChildren<Enemy>().spawner = transform;
            nextinsttime = Time.time + instrate;
            enemiesspawned++;
        }
    }
}
