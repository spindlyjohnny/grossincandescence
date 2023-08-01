using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Transform camTransform;
    public Transform camPivotTransform; // pivot around which camera turns
    //Vector3 campos;
    //LayerMask ignorelayers;
    public float lookspeed = 0.1f;
    //public static CameraController singleton;
    public float followspeed = 0.1f;
    public float pivotspeed = 0.03f;
    //float defaultpos;
    float lookangle;
    float pivotangle;
    public float minimumpivot = -35;
    public float maximumpivot = 35;
    Player player;
    private void Awake() {
        //defaultpos = camTransform.localPosition.z;
        player = target.GetComponent<Player>();
        player.cam = gameObject.GetComponent<CameraController>();
    }
    public void FollowTarget(float delta) {
       // Vector3 targetpos = Vector3.Lerp(transform.position, target.position, delta / followspeed);
        transform.position = Vector3.Lerp(transform.position, target.position, delta / followspeed);
    }
    public void Rotate(float delta) {
        lookangle += (player.movement.x * lookspeed) / delta;
        pivotangle -= (player.movement.y * pivotspeed) / delta;
        pivotangle = Mathf.Clamp(pivotangle, minimumpivot, maximumpivot);
        //Vector3 rotation = Vector3.zero;
        //rotation.y = lookangle;
        //Quaternion targetrotation = Quaternion.Euler(rotation);
        transform.rotation = Quaternion.Euler(0, lookangle, 0);
        //transform.rotation = targetrotation;

        //rotation = Vector3.zero;
        //rotation.x = pivotangle;
        //targetrotation = Quaternion.Euler(rotation);
        camPivotTransform.localRotation = Quaternion.Euler(pivotangle, 0, 0);
        //camPivotTransform.rotation = targetrotation;
    }
    //[Header("Positioning")]
    //public Vector3 positionOffset = new Vector3(0, 1, -3);
    //public Vector3 lookOffset = new Vector3(0, 1, -3);
    //public float smoothing = 1f;
    //[Header("Orientation")]
    //public float lookAngle = 0;
    //public bool overTheShoulder = false;

    //void OnDrawGizmosSelected() {
    //    if (Application.isPlaying) return;

    //    if (overTheShoulder) lookAngle = target.rotation.eulerAngles.y;
    //    Vector3 po = Quaternion.Euler(0, lookAngle, 0) * positionOffset,
    //            lo = target.TransformDirection(lookOffset);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(target.position + po, target.position + lo);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, target.position + po);
    //    Gizmos.DrawIcon(target.position + po, "d_ViewToolOrbit On@2x", false);
    //    Gizmos.DrawIcon(target.position + lo, "d_ViewToolOrbit On", false);
    //}

    // Update is called once per frame
    //void Update() {
    //    if (overTheShoulder) lookAngle = target.rotation.eulerAngles.y;

    //    // Rotate the offset by lookAngle.
    //    Vector3 o = Quaternion.Euler(0, lookAngle, 0) * positionOffset;

    //    // Moves the camera to the desired location.
    //    transform.position = Vector3.Lerp(
    //        transform.position,
    //        target.position + o,
    //        Time.deltaTime * smoothing
    //    );

    //    // Makes the camera look at where we want it to look.
    //    Vector3 lo = target.TransformDirection(lookOffset);
    //    transform.rotation = Quaternion.Lerp(
    //        transform.rotation,
    //        Quaternion.LookRotation(lo - o),
    //        Time.deltaTime * smoothing
    //    );
    //}
}