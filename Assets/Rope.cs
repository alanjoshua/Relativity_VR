using UnityEngine;
[ExecuteInEditMode]

// Reference from https://wirewhiz.com/how-to-make-a-vr-grappling-gun-for-steamvr/

public class Rope : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float TilesPerMeter;//how much you want to steach the texture 
    public Material CableMaterial;//texture you want to streach 

    void Update()
    {
        float dist = Vector3.Distance(EndPoint.position, StartPoint.position);

        transform.position = (StartPoint.position + EndPoint.position) / 2;
        transform.localScale = new Vector3(transform.localScale.x, dist/2f, transform.localScale.z);
        transform.rotation =  Quaternion.LookRotation(EndPoint.position-StartPoint.position)*Quaternion.Euler(90, 0, 0);

        CableMaterial.mainTextureScale = new Vector2(CableMaterial.mainTextureScale.x, dist/TilesPerMeter);
    }
}
