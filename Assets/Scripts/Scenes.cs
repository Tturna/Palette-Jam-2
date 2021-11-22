using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public float changeTime;
    public bool changeAfterTime;

    public Animator transitionAnim;
    public float waitTime;

    public static Scenes instance;

    void Awake()
    {
      instance = this;
      if(changeAfterTime == true)
      {
        ChangeLevelAfterTime();
      }

      if (SceneManager.GetActiveScene().name == "GameScene")
      {
          BgScript.instance.Audio.clip = BgScript.instance.gameplayMusic;
          BgScript.instance.Audio.Play();
      }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
          Restart(); 
        }
    }

    public void SelectScene(string sceneName)
    {
      StartCoroutine(TransitionForParticularScene(sceneName));
    }

    public void Restart()
    {
      StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
      StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ChangeLevelAfterTime()
    {
      if(changeAfterTime == true)
      {
        Invoke("NextLevel", changeTime);
      }
    }

    IEnumerator Transition(int scene)
    {
      transitionAnim.SetTrigger("end");
      yield return new WaitForSeconds(waitTime);
      SceneManager.LoadSceneAsync(scene);
    }

    IEnumerator TransitionForParticularScene(string scene)
    {
      transitionAnim.SetTrigger("end");
      yield return new WaitForSeconds(waitTime);
      SceneManager.LoadSceneAsync(scene);
    }
}
