using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    public Image buttonprompt;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        buttonprompt.gameObject.SetActive(false);
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonprompt.gameObject.activeSelf && Input.GetButtonDown("Submit")){
            menu.SetActive(true);
            Time.timeScale = 0;
            foreach (var i in FindObjectsOfType<Player>()) i.canMove = false;
        }   
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            buttonprompt.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>()) {
            buttonprompt.gameObject.SetActive(false);
        }
    }
}
