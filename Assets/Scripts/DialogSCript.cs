using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogSCript : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    private string[] sentences = {
                        "UNITY: Welcome, my fellow subject", 
                        "HERO: 'Fellow subject?' Excuse me? And is this a talking statue?",
                        "UNITY: Yes, I am Unity, the god and creator of the Unity game engine, the best game engine out there.",
                        "HERO: Huh, if you really are a God, can you teleport me into a minimalistic VR world right now? ",
                        "UNITY: Sure, if that is what you want.",
                        "HERO: ...",
                        "HERO: WOOOAH WHAT IS HAPPENING!"};
                        // "HERO: You said this is the 'best' game engine, didn't you?",
                        // "UNITY: Yes I did. So what?",
                        // "HERO: Well, shouldn't the best game engine be able to create the perfect game instantaneously just based on what the user is thinking, but Unity clearly cannot do that right now.",
                        // "UNITY: Erm....Best in the sense, as perfect as possible based on our current technology.",
                        // "HERO: Huh, if you say so.",
                        // "HERO: Let me seem, typing in a different language should be a simple think to do even based on our current technological limitations.",
                        // "HERO: こにちは",
                        // "HERO: ....",
                        // "HERO: The best game engine, but needs additional steps just to be able to switch to a different language.",
                        // "HERO: Humans have been able to switch instantly between writing different languages for thousands of years, but the super advanced Unity cannot.",
                        // "UNITY: You are just a noob and thus you don't know the proper way to do it.",
                        // "HERO: Maybe, but it is reasonable to expect the best game engine to make typing in different languages be seemless.",
                        // "UNITY: Urgh, wait, let me look into it.",
                        // "UNITY: ...",
                        // "UNITY: You seem to be using TextMeshPro to handle the dialog system. Let me quickly change the font for you...",
                        // "UNITY: こにちは",
                        // "UNITY: See, it works now?",
                        // "HERO: It sure would be nice to have the God of Unity with me at all times to do things like this for me.",
                        // "UNITY: ....",
                        // "UNITY: Okay, we are done here. Bye"
                        // };
    private int index;
    public float typingSpeed;
    public GameObject continueButton;

    public TMP_FontAsset japFont;
    public TMP_FontAsset origFont;

    IEnumerator Type() {

        textDisplay.text = "";
        foreach(char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    IEnumerator Type2() {

        while(index <= sentences.Length - 1) {
            textDisplay.text = "";
            continueButton.SetActive(false);

            foreach(char letter in sentences[index].ToCharArray()) {
                textDisplay.text += letter;
                
                yield return new WaitForSeconds(typingSpeed);
            }
            index++;
            continueButton.SetActive(true);
            yield return new WaitForSeconds(4);
        }
        SceneManager.LoadScene(sceneBuildIndex: 1);

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type2());
    }

    // Update is called once per frame
    void Update()
    {
        // if(textDisplay.text == sentences[index]) {
        //     continueButton.SetActive(true);
        // }
        
    }

    public void NextSentence() {

        // if(index == 13) {
        //     textDisplay.font = japFont;
        // }
        // if(index == 14) {
        //     textDisplay.font = origFont;
        // }
        Debug.Log("Next sentence called");

        continueButton.SetActive(false);
        if(index < sentences.Length - 1) {
            index++;
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

}
