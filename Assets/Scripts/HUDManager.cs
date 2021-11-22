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
        currentRuleText.text = taskManager.currentTask.name;
    }

    void Update()
    {
        tasksDone = taskManager.tasksDone;
        tasksDoneCounter.text = "(" + tasksDone + "/" + taskManager.totalTasks + ")";
    }

    public IEnumerator RuleBreak(float breakTime)
    {
        CrossOutAnimation.Play("RuleBreak");
        yield return new WaitForSeconds(breakTime);
        currentRuleText.text = taskManager.currentTask.name;
        CrossOutAnimation.Play("Idle");
    }
}
