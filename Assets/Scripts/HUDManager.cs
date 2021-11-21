using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text tasksDoneCounter;
    public Text currentRuleText;
    public int tasksDone;
    public TaskManager taskManager;
    public Animator CrossOutAnimation;
    public static HUDManager instance;

    void Start()
    {
        taskManager = GameObject.FindObjectOfType<TaskManager>();
        instance = this;
    }

    void Update()
    {
        currentRuleText.text = taskManager.currentTask.name;

        tasksDone = taskManager.tasksDone;
        tasksDoneCounter.text = "(" + tasksDone + "/" + taskManager.totalTasks + ")";
    }
}
