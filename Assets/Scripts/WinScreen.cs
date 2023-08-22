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
    // Start is called before the first frame update
    void Start()
    {
        screen.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.waves <= 0 && levelManager.wavecomplete) { 
            screen.SetActive(true);
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
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
