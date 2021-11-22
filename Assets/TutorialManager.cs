using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    bool tutorialDone;

    void Start()
    {
        Interactable[] inter = FindObjectsOfType<Interactable>();

        for (int i = 0; i < inter.Length; i++)
        {
            inter[i].OnInteract += OnInteract;
        }

        tutorialDone = false;
    }

    void OnInteract(GameObject target)
    {
        if (target.GetComponent<Interactable>().name == "Radio")
        {
            Debug.Log("Play Loud Music Done");
            BgScript.instance.Audio.clip = BgScript.instance.gameplayMusic;
            BgScript.instance.Audio.Play();
            tutorialDone = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && tutorialDone)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
