using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloodstain : MonoBehaviour
{
    public int souls;
    public GameObject buttonprompt;
    public Player player;
    public bool collected;
    // Start is called before the first frame update
    void Start()
    {
        buttonprompt.SetActive(false);
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonprompt.activeSelf && Input.GetButtonDown("Submit " + player.playerNum.ToString())) {
            gameObject.SetActive(false);
            player.souls += souls;
            collected = true;
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
}
