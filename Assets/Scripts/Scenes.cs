using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public string scene;
    public float changeTime;
    public bool changeAfterTime;

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
      SceneManager.LoadScene(scene);
    }

    public void Restart()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void NextLevel()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLevelAfterTime()
    {
      if(changeAfterTime == true)
      {
        Invoke("NextLevel", changeTime);
      }
    }
}
