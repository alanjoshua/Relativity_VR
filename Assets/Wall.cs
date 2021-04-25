using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float timeEachSide = 2;
    public float speed = 1;
    private float timeSoFar = 0;
    private int side = 1;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if(timeSoFar < timeEachSide) {
            transform.position = transform.position + new Vector3(side * speed * Time.deltaTime, 0, 0);
            timeSoFar += Time.deltaTime;
        }
        else {
            timeSoFar = 0;
            side *= -1;
        }

    }
}
