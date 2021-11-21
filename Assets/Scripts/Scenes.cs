using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public string scene;
    public float changeTime;
    public bool changeAfterTime;

    public Animator transitionAnim;
    public float waitTime;

    void Awake()
    {
      if(changeAfterTime == true)
      {
        ChangeLevelAfterTime();
      }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
          Restart(); 
        }
    }

    public void SelectScene()
    {
      StartCoroutine(TransitionForParticularScene(scene));
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
