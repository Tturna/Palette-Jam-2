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

    void Start()
    {
      instance = this;
      if(changeAfterTime == true)
      {
        ChangeLevelAfterTime();
      }

      if (SceneManager.GetActiveScene().name == "Tutorial")
      {
          BgScript.instance.Audio.clip = null;
      }

      if (SceneManager.GetActiveScene().name == "GameScene")
      {
        BgScript.instance.Audio.clip = null;
          BgScript.instance.Audio.clip = BgScript.instance.gameplayMusic;
      }else
      {
        BgScript.instance.Audio.clip = null;
        BgScript.instance.Audio.clip = BgScript.instance.menuMusic;
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
      SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.click);
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
