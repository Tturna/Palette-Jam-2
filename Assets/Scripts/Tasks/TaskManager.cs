using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string name;
}

public class TaskManager : MonoBehaviour
{
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject endScreen;

    // List of tasks to use for this game
    public List<Task> tasks = new List<Task>();

    // List of task names that are done
    List<string> doneTasks = new List<string>();

    public Task currentTask;
    [HideInInspector] public int tasksDone;
    [HideInInspector] public int totalTasks;

    void Awake()
    {
        totalTasks = tasks.Count;
        // Populate used task list with random tasks
        RandomizeTaskList();

        // Set current task
        currentTask = tasks[0];

        // Show current task in UI
        UpdateUI();

        // Subscribe to events
        Item[] items = FindObjectsOfType<Item>();
        Interactable[] inter = FindObjectsOfType<Interactable>();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].OnItemGrab += OnItemGrab;
            items[i].OnItemLand += OnItemLand;
        }

        for (int i = 0; i < inter.Length; i++)
        {
            inter[i].OnInteract += OnInteract;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            TaskDone("Don't Run In The Office");
        }
    }

    void RandomizeTaskList()
    {
        // Create new temporary list for easy random picking

        // Randomize the elements in the tasks list
        for (int i = 0; i < tasks.Count; i++)
        {
            Task temp = tasks[i];
            int randomIndex = Random.Range(i, tasks.Count);
            tasks[i] = tasks[randomIndex];
            tasks[randomIndex] = temp;
        }
    }

    void UpdateUI()
    {

    }

    void OnItemGrab(GameObject item)
    {
        if (item.GetComponent<Item>().itemType == Item.ItemType.Property)
        {
            // Don't steal people's stuff done
            TaskDone("Don't steal people's stuff");
        }
    }

    void OnItemLand(GameObject item, Vector2 pos)
    {
        try
        {
            if (item.GetComponent<Item>().itemType == Item.ItemType.Food && (pos - (Vector2)GameObject.Find("point_programmers").transform.position).magnitude < 3)
            {
                // Don't feed the programmers done
                TaskDone("Don't feed the programmers");
            }
            else if (item.GetComponent<Item>().itemType == Item.ItemType.Marker && (pos - (Vector2)GameObject.Find("point_whiteboard").transform.position).magnitude < 3)
            {
                // Don't mess up the whiteboard done
                TaskDone("Don't mess up the whiteboard");
            }
            else if ((pos - (Vector2)GameObject.Find("point_trashcan").transform.position).magnitude < 3)
            {
                // Don't litter done
                TaskDone("Don't litter");
            }
            else if ((pos - (Vector2)GameObject.Find("point_employees").transform.position).magnitude < 3)
            {
                // Don't annoy other employees done
                TaskDone("Don't annoy other employees");
            }
            else if (item.GetComponent<Item>().itemType == Item.ItemType.Drink)
            {
                // Don't spill drinks in the office done
                TaskDone("Don't spill drinks in the office");
            }
        }
        catch
        {
            Debug.LogWarning("Some task points are probably missing. Assume incorrect behavior.");
        }
    }

    void OnInteract(GameObject target)
    {
        if (target.GetComponent<Interactable>().name == "TV")
        {
            // Don't touch the TV done
            TaskDone("Don't touch the TV");
        }
        else if (target.GetComponent<Interactable>().name == "Fridge")
        {
            // Don't leave the fridge door open done
            TaskDone("Don't leave the fridge door open");
        }
        else if (target.GetComponent<Interactable>().name == "Radio")
        {
            // Don't play loud music done
            TaskDone("Don't play loud music");
        }
        else if (target.GetComponent<Interactable>().name == "Printer")
        {
            // Use the printer sparingly done
            TaskDone("Use the printer sparingly");
        }
        else if (target.GetComponent<Interactable>().name == "Tap")
        {
            // Don't leave the tap running done
            TaskDone("Don't leave the tap running");
        }
        else if (target.GetComponent<Interactable>().name == "Elevator")
        {
            // Don't mess with the elevator done
            TaskDone("Don't mess with the elevator");
        }
        else if (target.GetComponent<Interactable>().name == "Router")
        {
            // Don't touch the Wi-Fi router done
            TaskDone("Don't touch the Wi-Fi router");
        }
        else if (target.GetComponent<Interactable>().name == "TheSandwich")
        {
            // Don't eat the sandwich done
            TaskDone("Don't eat the sandwich");
        }
        else if (target.GetComponent<Interactable>().name == "Doge")
        {
            // Don't let the dog free done
            TaskDone("Don't let the dog free");
            target.GetComponent<Doge>().FuckingZoom();
        }

        Debug.Log("Interact");
    }

    public void TaskDone(string name)
    {
        // Check if this is not the current task
        if (currentTask.name != name) return; // Prevent doing a task that you don't have

        // Check if the task is already done
        if (doneTasks.Contains(name)) return;

        // if not, add it to the list of done tasks
        else doneTasks.Add(name);

        Debug.Log(name + " done!");
        tasksDone++;
        tasks.RemoveAt(0);

        // Everything done
        // Give last task (get out)
        if (tasksDone >= totalTasks)
        {
            endPoint.SetActive(true);
            return;
        }

        currentTask = tasks[0];


        int rand = Random.Range(0, SfxManager.instance.WinSounds.Count);

        SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.WinSounds[rand]);

        HUDManager.instance.CrossOutAnimation.Play("RuleBreak");
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
    }
}
