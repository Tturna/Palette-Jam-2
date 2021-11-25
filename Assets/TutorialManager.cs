using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    int tutorialDone;

    public GameObject anotherPrompt;
    public Sprite InteractSprite;
    public Sprite highlightSprite;
    public Sprite upArrow;
    public Sprite leftArrow;
    public Sprite radio;


    void Awake()
    {
        Interactable[] inter = FindObjectsOfType<Interactable>();

        for (int i = 0; i < inter.Length; i++)
        {
            inter[i].OnInteract += OnInteract;
        }

        if (PlayerPrefs.HasKey("tutorial"))
        {
            tutorialDone = PlayerPrefs.GetInt("tutorial");
        }else
        {
            PlayerPrefs.SetInt("tutorial", 0);
            tutorialDone = PlayerPrefs.GetInt("tutorial");
        }
    }

    void Start()
    {
        if (tutorialDone == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void OnInteract(GameObject target)
    {
        if (target.GetComponent<Interactable>().name == "Radio")
        {
            Debug.Log("Play Loud Music Done");
            BgScript.instance.Audio.clip = BgScript.instance.gameplayMusic;
            BgScript.instance.Audio.Play();
            Camera.main.GetComponent<CameraFollow>().followSpeed = 1f;
            target.GetComponent<SpriteRenderer>().sprite = radio;
            int rand = Random.Range(0,SfxManager.instance.WinSounds.Count);
            SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.WinSounds[rand]);
        }

        if (target.GetComponent<Interactable>().name == "Door")
        {
            target.GetComponent<Animator>().Play("Door");
            StartCoroutine(TutorialDone());
        }
    }

    IEnumerator TutorialDone()
    {
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("tutorial", 1);
        tutorialDone = PlayerPrefs.GetInt("tutorial");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && tutorialDone == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
