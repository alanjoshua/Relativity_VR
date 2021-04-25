using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Adapted from:  https://wirewhiz.com/how-to-make-a-vr-grappling-gun-for-steamvr/

public class GrappleGun : MonoBehaviour
{
    public float hookExtentionSpeed;
    public float hookRetractionMaxSpeed;
    public float hookRetractionMinSpeed;
    public float playerRetractionSpeed;
    public Transform connectionPoint;
    public GameObject rope;
    public GameObject activeHook;
    public GameObject staticHook;
    public GrapplingHook hook;
    public bool isTriggerPressed {set;get;} = false;
    public int forwardDir;
    public Rigidbody playerRB;
    public ContinuousMovement playerControls;
    public float retractionTime;
    private bool isDropFirst = false;

    void Start() {
        staticHook.SetActive(true);
        rope.SetActive(false);
        activeHook.SetActive(false);  
    }

    void Update() {

            if(GetComponent<XRGrabInteractable>().isSelected) {

                    isDropFirst = true;
                
                // Hook is attached to something and the player is pressing the trigger button
                // This should retract the playet to the attached point
                if(hook.attached && isTriggerPressed) {

                        Debug.Log("player retracting");

                        hook.retracting_player = true;
                        hook.retracting_empty = false;
                        hook.extending = false;

                        hook.transform.parent = transform.parent;

                        // hook.hook_rb.isKinematic = false;

                }
                
                // hook is attached but player is not pressing the retract button
                // This should retract the hook and the player shouldn't be pulled anymore

                else if(hook.attached && !isTriggerPressed) {
                        Debug.Log("empty retracting though hook got attached");

                        hook.retracting_player = false;
                        hook.retracting_empty = true;
                        hook.attached = false;
                        hook.extending = false;

                        hook.transform.parent = transform.parent;

                        // hook.hook_rb.isKinematic = true;
                }
                
                // Player is pressing the trigger button and the hook is not yet attached to anything
                // Keep extending the hook
                else if(isTriggerPressed && !hook.attached) {
                        Debug.Log("extending");

                        hook.extending = true;
                        hook.retracting_player = false;
                        hook.retracting_empty = false;

                        // hook.hook_rb.isKinematic = true;

                        staticHook.SetActive(false);
                        rope.SetActive(true);
                        activeHook.SetActive(true);

                        hook.transform.parent = transform.parent;
                }

                // empty retracting
                else if(!isTriggerPressed && !hook.attached && !hook.isAtResetPos){
                        Debug.Log("empty retracting");

                        // hook.hook_rb.isKinematic = true;

                        hook.retracting_player = false;
                        hook.retracting_empty = true;
                        hook.attached = false;
                        hook.extending = false;

                        hook.transform.parent = transform.parent;
                }

                if(hook.attached) {
                        // playerRB.useGravity = false;
                }
                
            }
          
            // If player drops the grapple gun
            else {
                // Debug.Log("gun dropped");\
                if(isDropFirst) {
                 resetHook();
                 isDropFirst = false;
                }
            }

    }

    public void resetHook() {
                staticHook.SetActive(true);
                rope.SetActive(false);
                activeHook.SetActive(false);

                hook.hook_rb.isKinematic = false;
                
                // Reset hook params
                hook.retracting_empty = false;
                hook.retracting_player = false;
                hook.extending = false;
                hook.attached = false;

                hook.hook_rb.isKinematic = false;
                hook.isAtResetPos = true;

                hook.hook_rb.angularVelocity = Vector3.zero;
                hook.hook_rb.velocity = Vector3.zero;

                playerRB.useGravity = true;

                playerRB.angularVelocity = Vector3.zero;
                // playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);

                activeHook.transform.position = staticHook.transform.position;
                activeHook.transform.rotation = staticHook.transform.rotation; 

                hook.transform.parent = transform;
    }

    void OnCollisionEnter(Collision collision) {
            
            // Check whether gun has collider with hook. This usually means the current action (retracting_empty / retracting_player) has finised
             
            Debug.Log("Gun has collided with something");
            
            // If hook is not extending and hook collides with gun
            if(!hook.extending && collision.collider.GetComponent<GrapplingHook>()) {
                    
                    hook.retracting_empty = false;
                    hook.retracting_player = false;
                    hook.attached = false;

                    hook.hook_rb.isKinematic = false;
                    hook.isAtResetPos = true;

                    playerRB.angularVelocity = Vector3.zero;
                    playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);

                    hook.hook_rb.angularVelocity = Vector3.zero;
                    hook.hook_rb.velocity = Vector3.zero;

                    hook.transform.parent = transform;

                    staticHook.SetActive(true);
                    rope.SetActive(false);
                    activeHook.SetActive(false);

                    activeHook.transform.position = staticHook.transform.position;
                    activeHook.transform.rotation = staticHook.transform.rotation;

                    Debug.Log("Gun has collided with hook. Retraction complete.");
            }
    }
}
