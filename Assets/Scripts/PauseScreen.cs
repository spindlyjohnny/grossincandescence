using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject defaultButton,menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton);
            foreach (var i in FindObjectsOfType<Player>()) i.canMove = false;
            AudioManager.instance.PauseMusic();
            menu.SetActive(true);
        }
    }
    public void Resume() {
        Time.timeScale = 1;
        foreach (var i in FindObjectsOfType<Player>()) i.canMove = true;
        AudioManager.instance.ResumeMusic();
        menu.SetActive(false);
    }
    public void BackToMain() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
