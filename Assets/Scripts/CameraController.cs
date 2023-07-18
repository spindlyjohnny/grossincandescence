using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour {

    public Transform target; // Who the camera is targetting.

    [Header("Positioning")]
    public Vector3 positionOffset; // Where the camera is relative to the target.
    public Vector3 lookOffset; // Which part of the target the camera looks at.
    public float smoothing = 1f; // How fast the camera moves.

    [Header("Orientation")]
    public float lookAngle = 0;
    public bool overTheShoulder = false; // Does the camera stay behind the player?

    // Update is called once per frame
    void Update() {
        if (overTheShoulder) lookAngle = target.rotation.eulerAngles.y;
        Vector3 po = Quaternion.Euler(0, lookAngle, 0) * positionOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position + po,
            smoothing * Time.deltaTime
        );

        Vector3 lookPos = target.position + target.TransformDirection(lookOffset);
        transform.rotation = Quaternion.LookRotation(lookPos - transform.position);
    }

    // Draws the position of where the camera is at, and where it is looking at.
    void OnDrawGizmos() {
        if (Application.isPlaying) return;

        if (overTheShoulder) lookAngle = target.rotation.eulerAngles.y;
        Vector3 po = Quaternion.Euler(0, lookAngle, 0) * positionOffset,
                lo = target.TransformDirection(lookOffset);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(target.position + po, target.position + lo);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position + po);
        Gizmos.DrawIcon(target.position + po, "d_ViewToolOrbit On@2x", false);
        Gizmos.DrawIcon(target.position + lo, "d_ViewToolOrbit On", false);
    }
}