using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bonfire : MonoBehaviour
{
    public GameObject buttonprompt;
    public GameObject menu,defaultButton; // defaultButton is the button that is selected when the menu pops up
    public Player player;
    public Text locationtext;
    public string locationname;
    LevelManager levelManager;
    public Transform destination;
    public AudioClip bonfiresound;
    // Hub bonfire only
    public Scrollbar scrollBar;
    public RectTransform rect; // content of scroll view
    // Start is called before the first frame update
    void Start()
    {
        buttonprompt.SetActive(false);
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>(); // set camera of canvas for button prompt
        levelManager = FindObjectOfType<LevelManager>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonprompt.activeSelf && Input.GetButtonDown("Submit " + player.playerNum.ToString())){
            buttonprompt.SetActive(false);
            AudioManager.instance.PauseMusic();
            AudioManager.instance.PlaySFX(bonfiresound);
            menu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton); // set button that is currently selected (to navigate UI with controller)
            Time.timeScale = 0;
            foreach (var i in FindObjectsOfType<Player>()) i.canMove = false;
            if (player) {
                player.hitpoints = player.maxhitpoints;
                player.heals = 5;
            }
        }
        locationtext.text = locationname;
        if (menu.activeSelf && Input.GetButtonDown("Cancel")) Leave();
        // for hub bonfire. scroll bar of upgrade panel scrolls with currently selected upgrade.
        // checks if scroll bar exists(so no errors are thrown for normal bonfire), the scroll bar isn't selected, and the selected object is a child of the content gameobject.
        if (scrollBar != null && EventSystem.current.currentSelectedGameObject != scrollBar.gameObject && EventSystem.current.currentSelectedGameObject.transform.parent == rect) {
            scrollBar.value = 1 + (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition.y / rect.rect.height);
            // sets position of scroll bar to the fraction of height of content gameobject that the currently selected object is positioned at on the y-axis.
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>() /*&& !other.GetComponent<Bloodstain>()*/) {
            player = other.GetComponent<Player>(); // store reference to deduct souls from correct player when player purchases upgrades
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
        AudioManager.instance.ResumeMusic();
    }
    public void StartWave() {
        if (levelManager.waves == 0) return;
        foreach (var i in levelManager.enemyspawns) {
            i.canSpawn = true; //reset spawners
        }
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) i.canMove = true;
        foreach (var i in FindObjectsOfType<Bonfire>()) i.gameObject.SetActive(false);
        AudioManager.instance.ResumeMusic();
    }
    public void Travel() {
        menu.SetActive(false);
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) { 
            i.canMove = true;
            i.transform.position = destination.position;
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        if (locationname == "Firelink Shrine") AudioManager.instance.ResumeMusic(); // resumes music if travelling back to level, stops music if travelling to hub.
    }
}
