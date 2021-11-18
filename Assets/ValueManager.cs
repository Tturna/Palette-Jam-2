using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueManager : MonoBehaviour
{
    [HideInInspector]
    public bool active;
    public GameObject valueManager;
    public static ValueManager instance;
    public bool debuggable;
    public GameObject guidelines;
    public Toggle guidelinesToggle;

    [Header("Input Fields")]
    public InputField playerSpeed;
    public InputField playerSprintSpeed;
    public InputField cameraAheadAmount;
    public InputField cameraAheadSpeed;
    public InputField enemySpeed;
    public InputField pickupRange;
    public InputField flightTime;
    public InputField flightSpeed;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(!active)
            {
                valueManager.SetActive(true);
                active = true;
            }else
            {
                active = false;
                valueManager.SetActive(false);
            }
        }
    }

    public void ToggleGuidelines(bool active)
    {
        if(!active)
        {
            guidelines.SetActive(false);
        }else
        {
            guidelines.SetActive(true);
        }
    }
}
