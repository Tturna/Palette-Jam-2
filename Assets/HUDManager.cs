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

    void Start()
    {
        taskManager = GameObject.FindObjectOfType<TaskManager>();
    }

    void FixedUpdate()
    {
        currentRuleText.text = taskManager.currentTask.ToString();

        tasksDone = taskManager.tasksDone;
        tasksDoneCounter.text = "(" + tasksDone + "/" + taskManager.tasks + ")";
    }
}
