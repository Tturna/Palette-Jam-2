using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public bool fullscreen;

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen" + ": " + isFullscreen);
        fullscreen = isFullscreen;
        SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.click);
    }
}
