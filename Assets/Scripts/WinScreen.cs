using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject defaultButton; // button that is automatically selected
    public int scenetoload;
    public GameObject screen;
    LevelManager levelManager;
    [SerializeField]float soundlength;
    public float ogsoundlength;
    // Start is called before the first frame update
    void Start()
    {
        screen.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
        ogsoundlength = AudioManager.instance.eventsound.length;
        soundlength = ogsoundlength;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.waves <= 0 && levelManager.wavecomplete && soundlength > 0) { 
            screen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton);
            foreach (var i in FindObjectsOfType<Player>()) i.canMove = false;
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
            soundlength -= Time.deltaTime;
        }
        if(soundlength <= 0) {
            AudioManager.instance.StopSFX();
        }
    }
    public void NextLevel() {
        SceneManager.LoadScene(scenetoload);
    }
    public void BackToMain() {
        SceneManager.LoadScene(0);
    }
}
