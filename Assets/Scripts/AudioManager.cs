using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource sfxaudio, musicaudio;
    public AudioClip eventsound;
    // Start is called before the first frame update
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }
    public void StopMusic() {
        musicaudio.Stop();
    }
    public void PlayMusic(AudioClip clip) {
        if (musicaudio.isPlaying) return;
        musicaudio.clip = clip;
        musicaudio.Play();
    }
    public void PauseMusic() {
        musicaudio.Pause();
    }
    public void ResumeMusic() {
        musicaudio.UnPause();
    }
    public void PlaySFX(AudioClip clip,bool vary = false) {
        if (vary) { 
            sfxaudio.pitch = Random.Range(1, 4);
            sfxaudio.PlayOneShot(clip);
        } 
        else {
            sfxaudio.PlayOneShot(clip);
        }
    }
}
