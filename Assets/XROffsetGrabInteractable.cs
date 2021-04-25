using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XROffsetGrabInteractable : XRGrabInteractable
{

    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    void Start() {
        if(!attachTransform) {
            GameObject grab = new GameObject("Grab pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }
 
    protected override void OnSelectEntering(XRBaseInteractor interactor) {
        
        if(interactor is XRDirectInteractor) {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else {
            attachTransform.position = initialAttachLocalPos;
            attachTransform.rotation = initialAttachLocalRot;
        }

        base.OnSelectEntering(interactor);
    }


}