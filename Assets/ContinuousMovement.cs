using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class ContinuousMovement : MonoBehaviour
{

    public XRNode inputSource;
    private Vector2 inputAxis;
    private XRRig rig;
    private CharacterController character;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    public float speed = 1f;
    public LayerMask groundLayer;
    public float additionalHeight = 20;

    public Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        rig = GetComponent<XRRig>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate() {
        prevPos = transform.position;
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = Vector3.zero;

        if(CheckIfGrounded()) {
            direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        }

        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime * speed);
    }


    private bool CheckIfGrounded() {

        Vector3 rayStart = transform.TransformPoint(capsuleCollider.center);
        float rayLegnth = capsuleCollider.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, capsuleCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLegnth, groundLayer);

        return hasHit;
    }

    private void CapsuleFollowHeadset() {

        capsuleCollider.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        capsuleCollider.center = new Vector3(capsuleCenter.x, capsuleCollider.height/2, capsuleCenter.z);

    }

}
