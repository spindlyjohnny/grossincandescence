using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Transform camTransform;
    public Transform camPivotTransform; // pivot around which camera turns
    public float lookspeed = 0.1f;
    public float followspeed = 0.1f;
    public float pivotspeed = 0.03f;
    float lookangle;
    float pivotangle;
    public float minimumpivot = -35;
    public float maximumpivot = 35;
    Player player;
    private void Awake() {
        player = target.GetComponent<Player>();
        player.cam = gameObject.GetComponent<CameraController>();
    }
    public void FollowTarget(float delta) {
        transform.position = Vector3.Lerp(transform.position, target.position, delta / followspeed);
    }
    public void Rotate(float delta) {
        lookangle += (player.movement.x * lookspeed) / delta;
        pivotangle -= (player.movement.y * pivotspeed) / delta;
        pivotangle = Mathf.Clamp(pivotangle, minimumpivot, maximumpivot);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookangle;
        Quaternion targetrotation = Quaternion.Euler(rotation);
        transform.rotation = targetrotation;

        rotation = Vector3.zero;
        rotation.x = pivotangle;
        targetrotation = Quaternion.Euler(rotation);
        camPivotTransform.rotation = targetrotation;
    }
}