using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour {

    public GameObject cam1;
    public GameObject cam2;
    public GameObject playerCam;
    public GameObject player;
    public PlayerController2 playerController;
    public GameObject canvas;
    public GameObject dialogCam;
    public GameObject dialogManager;
    public GameObject statue;

    // Start is called before the first frame update
    void Start() {
        playerController = player.GetComponent<PlayerController2>();
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence() {
        
        canvas.SetActive(false);
        dialogCam.SetActive(false);
        dialogManager.SetActive(false);
        statue.SetActive(false);

        playerController.shouldUpdate = false;
        cam1.SetActive(true);
        cam2.SetActive(false);
        player.GetComponent<Animator>().SetBool("isDancing", true);
        Debug.Log("First cam");
        yield return new WaitForSeconds(7);

        cam2.SetActive(true);
        cam1.SetActive(false);
        Debug.Log("setting cam2 to active");
        yield return new WaitForSeconds(7);
        
        cam2.SetActive(false);
        playerCam.SetActive(true);
        playerController.shouldUpdate = true;
        player.GetComponent<Animator>().SetBool("isDancing", false);

        cam2.SetActive(false);
        canvas.SetActive(true);
        dialogCam.SetActive(true);
        dialogManager.SetActive(true);
        statue.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
