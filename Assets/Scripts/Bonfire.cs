using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    public GameObject buttonprompt;
    public GameObject menu;
    public Player player;
    public Text locationtext;
    public string locationname;
    LevelManager levelManager;
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        buttonprompt.SetActive(false);
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonprompt.activeSelf && Input.GetButtonDown("Submit")){
            menu.SetActive(true);
            Time.timeScale = 0;
            foreach (var i in FindObjectsOfType<Player>()) i.canMove = false;
        }
        locationtext.text = locationname;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            player = other.GetComponent<Player>(); // store reference to deduct souls from correct player
            buttonprompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            buttonprompt.SetActive(false);
        }
    }
    public void Leave() {
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) i.canMove = true;
    }
    public void StartWave() {
        foreach (var i in levelManager.enemyspawns) {
            i.canSpawn = true;
        }
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) i.canMove = true;
    }
    public void Travel() {
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) { 
            i.canMove = true;
            i.transform.position = destination.position;
            //FindObjectOfType<CameraController>().smoothing = FindObjectOfType<CameraController>().ogsmoothing;
        }
    }
}
