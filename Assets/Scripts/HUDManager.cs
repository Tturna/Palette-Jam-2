using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text tasksDoneCounter;
    public Text currentRuleText;
    public int tasksDone;
    public int tasksTodo;
    public TaskManager taskManager;
    public Animator CrossOutAnimation;
    public static HUDManager instance;

    void Start()
    {
        taskManager = GameObject.FindObjectOfType<TaskManager>();
        instance = this;
        currentRuleText.text = taskManager.currentTask.name;
        tasksTodo = taskManager.totalTasks;
    }

    void Update()
    {
        tasksDone = taskManager.tasksDone;
        tasksDoneCounter.text = "(" + tasksDone + "/" + tasksTodo + ")";
    }

    public IEnumerator RuleBreak(float breakTime)
    {
        tasksTodo = taskManager.totalTasks;
        CrossOutAnimation.Play("RuleBreak");
        yield return new WaitForSeconds(breakTime);
        currentRuleText.text = taskManager.currentTask.name;
        yield return new WaitForSeconds(1.5f);
        CrossOutAnimation.Play("Idle");
    }
}
