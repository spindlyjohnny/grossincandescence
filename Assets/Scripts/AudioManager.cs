using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // singleton
    [SerializeField] AudioSource sfxaudio, musicaudio; // audiosources for music and sfx
    public AudioClip eventsound;
    // Start is called before the first frame update
    private void Awake() {
        if(instance == null) { // create singleton
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
    public void StopSFX() {
        sfxaudio.Stop();
    }
    public void PlayMusic(AudioClip clip) {
        if (musicaudio.isPlaying) return;
        musicaudio.clip = clip;
        musicaudio.Play();
    }
    public void PauseMusic() {
        musicaudio.volume = 0.5f;
    }
    public void ResumeMusic() {
        musicaudio.volume = 1f;
    }
    public void PlaySFX(AudioClip clip) {
        sfxaudio.PlayOneShot(clip); 
    }
}
