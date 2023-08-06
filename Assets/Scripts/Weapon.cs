using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    //public float hitdist;
    //public float radius;
    //public RaycastHit hit;
    //public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Physics.SphereCast(transform.position + offset, radius,Vector3.forward,out hit,hitdist);
        //if (hit.collider != null) {
        //    print(hit.collider);
        //}
    }
    private void OnDrawGizmosSelected() {
        //Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
