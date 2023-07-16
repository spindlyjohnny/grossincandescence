using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Unit>()) {
            Unit unit = other.GetComponent<Unit>();
            unit.TakeHit(damage);
            StartCoroutine(unit.Hit());
        }
    }
}
