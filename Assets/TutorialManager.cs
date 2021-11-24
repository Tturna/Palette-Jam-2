using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    bool tutorialDone;

    public GameObject anotherPrompt;
    public Sprite InteractSprite;
    public Sprite highlightSprite;
    public Sprite upArrow;
    public Sprite leftArrow;
    public Sprite radio;


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
            Camera.main.GetComponent<CameraFollow>().followSpeed = 1f;
            anotherPrompt.transform.position = transform.position;
            target.GetComponent<SpriteRenderer>().sprite = radio;
            int rand = Random.Range(0,SfxManager.instance.WinSounds.Count);
            SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.WinSounds[rand]);
        }

        if (target.GetComponent<Interactable>().name == "Door")
        {
            target.GetComponent<Animator>().Play("Door");
            tutorialDone = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && tutorialDone)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
