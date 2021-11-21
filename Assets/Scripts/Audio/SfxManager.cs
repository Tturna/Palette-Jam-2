using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;

    [Header("Manager Voicelines")]
    public List<AudioClip> ManagerCatchingPlayer;
    public List<AudioClip> ManagerChasingPlayer;

    [Header("Employees")]
    public List<AudioClip> EmployeesCursing;

    [Header("SFX")]
    public List<AudioClip> WinSounds;

    [Header("All")]
    public List<AudioClip> AudioClips;

    public static SfxManager instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        AudioClips.AddRange(ManagerCatchingPlayer);
        AudioClips.AddRange(ManagerChasingPlayer);
        AudioClips.AddRange(EmployeesCursing);
        AudioClips.AddRange(WinSounds);
    }
}
