using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgScript : MonoBehaviour
{
    public static BgScript instance;
    public AudioSource Audio;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        
        Audio = GetComponent<AudioSource>();
    }
}

