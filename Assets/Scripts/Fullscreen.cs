using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public bool fullscreen;
    
    void Start()
    {
        fullscreen = true;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen" + ": " + isFullscreen);
        fullscreen = isFullscreen;
        SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.click);
    }
}
