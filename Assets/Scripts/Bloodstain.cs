using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloodstain : MonoBehaviour
{
    public int souls; // souls in bloodstain
    public GameObject buttonprompt;
    public Player player; // player that died
    public bool collected;
    // Start is called before the first frame update
    void Start()
    {
        buttonprompt.SetActive(false);
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>(); // set camera of canvas for button prompt
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] isplayer = Physics.BoxCastAll(transform.position, new Vector3(0.5f, .5f, .5f), Vector3.up, Quaternion.identity, Mathf.Infinity, LayerMask.GetMask("Player")); // check if there's a player touching bloodstain
        // check if button prompt is active, button has been pressed, and that player within boxcast is the same player that dropped the bloodstain.
        if (buttonprompt.activeSelf && Input.GetButtonDown("Submit " + player.playerNum.ToString()) && isplayer[0].transform.gameObject.GetComponent<Player>() == player) {
            gameObject.SetActive(false);
            player.souls += souls; // adds souls stored in bloodstain to player's souls
            collected = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.eventsound);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            player = other.GetComponent<Player>(); // store reference to add souls to correct player
            buttonprompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            buttonprompt.SetActive(false);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
