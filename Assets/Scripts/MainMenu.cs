using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject defaultButton; // button that is automatically selected (for controller navigation)
    public AudioClip menumusic;
    // Start is called before the first frame update
    void Start()
    {
        if (!AudioManager.instance.GetComponent<AudioSource>().isPlaying) StartCoroutine(SwitchMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play() {
        SceneManager.LoadScene(2);
    }
    public void Quit() {
        Application.Quit();
    }
    public void Credits() {
        SceneManager.LoadScene("Credits");
    }
    public void Back() {
        SceneManager.LoadScene("Main Menu");
    }
    IEnumerator SwitchMusic() {
        AudioManager.instance.StopMusic();
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayMusic(menumusic);
    }
}
