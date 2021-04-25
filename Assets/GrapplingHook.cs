using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public bool attached {get; set;} = false;
    public bool extending{get;set;} = false;
    public bool retracting_empty {get; set;} = false;
    public bool retracting_player {get;set;} = false;
    public bool isAtResetPos = true;
    public Transform retractionPoint;
    public GrappleGun gun;
    public Rigidbody hook_rb;
    private Vector3 attachedPos;
    private Quaternion attachedRot;
    private bool isCurrentlyExtending = false;
    private float timeElapsedSinceRetractStart = 0;

    // Start is called before the first frame update
    void Start() {
        hook_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if(extending) {

            Debug.Log("extending");

            isAtResetPos = false;
            isCurrentlyExtending = true;
            timeElapsedSinceRetractStart = 0;

            RaycastHit hit;
            Vector3 dir = gun.forwardDir * transform.forward;

            hook_rb.AddForce(dir * gun.hookExtentionSpeed, ForceMode.Acceleration);

            // if (Physics.Raycast(transform.position, dir, out hit, rb.velocity.magnitude * Time.deltaTime)) {
            //     if(!hit.collider.GetComponent<GrappleGun>()) {
            //         transform.position = hit.point;
            //         attached = true;
            //         attachedPos = transform.position;
            //         attachedRot = transform.rotation;
            //     }
            // }
            // else {
            //         rb.AddForce(dir * grappleGunObj.speed, ForceMode.VelocityChange);
            //     }

        }

        if(retracting_empty) {
            
            Debug.Log("retracting empty");

            if(timeElapsedSinceRetractStart >= gun.retractionTime) {
                gun.resetHook();
            }

            else {

                if(isCurrentlyExtending) {
                    hook_rb.AddForce(-hook_rb.velocity, ForceMode.VelocityChange);
                }

                Vector3 dist = retractionPoint.position - transform.position;
                float mag = dist.sqrMagnitude;

                hook_rb.AddForce(Mathf.Max(1f/Mathf.Max(mag, 1) * gun.hookRetractionMaxSpeed, gun.hookRetractionMinSpeed) * Vector3.Normalize(dist), ForceMode.Acceleration);

                isAtResetPos = false;

                timeElapsedSinceRetractStart += Time.fixedDeltaTime;

            }
            // float mag = (transform.position - retractionPoint.position).sqrMagnitude;
            // if(mag < 0.05) {
            //      Debug.Log("Retraction complete. isGrappling set to false");

            //     retracting_empty = false;
               
            //     // gun.playerRB.angularVelocity = Vector3.zero;
            //     // gun.playerRB.velocity = Vector3.zero;

            //     gun.playerRB.angularVelocity = Vector3.zero;
            //     gun.playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);

            //     gun.staticHook.SetActive(true);
            //     gun.rope.SetActive(false);
            //     gun.activeHook.SetActive(false);

            //     hook_rb.angularVelocity = Vector3.zero;
            //     hook_rb.velocity = Vector3.zero;

            //     isAtResetPos = true;

            //     transform.position = gun.staticHook.transform.position;
            //     transform.rotation = gun.staticHook.transform.rotation;

            // }

        }

        if(retracting_player) {
            gun.playerRB.AddForce(Vector3.Normalize(transform.position - retractionPoint.position) * gun.playerRetractionSpeed, ForceMode.Acceleration);
        }

        if(attached) {
            if(attachedPos!=null && attachedRot!=null) {
                transform.position = attachedPos;
                transform.rotation = attachedRot;
            }
        }
       
       isCurrentlyExtending = false;

    }

    void OnCollisionEnter(Collision collision) {
        
        //if we haven't attached to anything, attach to the collision

        if (extending && !collision.collider.GetComponent<GrappleGun>()) {
            attached = true;
            extending = false;

            attachedPos = transform.position;
            attachedRot = transform.rotation;

            Debug.Log("Hook collided with object");
        }

    }


}
