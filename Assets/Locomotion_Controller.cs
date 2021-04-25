using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Locomotion_Controller : MonoBehaviour
{

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor leftInteractorRay;

    public XRRayInteractor rightInteractorRay;
    public bool EnableLeftTeleport {get; set; } = true;
    public bool EnableRightTeleport {get; set; } = true;

    // Update is called once per frame
    void Update()
    {

        if(leftTeleportRay) {
            bool isLeftRayHovering = leftInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);

            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && checkIfActivated(leftTeleportRay) && !isLeftRayHovering);
        }

        if(rightTeleportRay) {
            bool isRightRayHovering = rightInteractorRay.TryGetHitInfo(out Vector3 pos, out Vector3 norm, out int index, out bool validTarget);
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && checkIfActivated(rightTeleportRay) && !isRightRayHovering);
        }

    }

    public bool checkIfActivated(XRController controller) {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
