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

    [HideInInspector] public Task currentTask;
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
            Debug.Log("Don't steal people's stuff done");
            TaskDone();
        }
    }

    void OnItemLand(GameObject item, Vector2 pos)
    {
        try
        {
            if (item.GetComponent<Item>().itemType == Item.ItemType.Food && (pos - (Vector2)GameObject.Find("point_programmers").transform.position).magnitude < 3)
            {
                // Don't feed the programmers done
                Debug.Log("Don't feed the programmers done");
                TaskDone();
            }
            else if (item.GetComponent<Item>().itemType == Item.ItemType.Marker && (pos - (Vector2)GameObject.Find("point_whiteboard").transform.position).magnitude < 3)
            {
                // Don't mess up the whiteboard done
                Debug.Log("Don't mess up the whiteboard done");
                TaskDone();
            }
            else if ((pos - (Vector2)GameObject.Find("point_trashcan").transform.position).magnitude < 3)
            {
                // Don't litter done
                Debug.Log("Don't litter done");
                TaskDone();
            }
            else if ((pos - (Vector2)GameObject.Find("point_employees").transform.position).magnitude < 3)
            {
                // Don't annoy other employees done
                Debug.Log("Don't annoy other employees done");
                TaskDone();
            }
            else if (item.GetComponent<Item>().itemType == Item.ItemType.Drink)
            {
                // Don't spill drinks in the office done
                Debug.Log("Don't spill drinks in the office done");
                TaskDone();
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
            Debug.Log("Don't touch the TV done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Fridge")
        {
            // Don't leave the fridge door open done
            Debug.Log("Don't leave the fridge door open done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Radio")
        {
            // Don't play loud music done
            Debug.Log("Don't play loud music done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Printer")
        {
            // Use the printer sparingly done
            Debug.Log("Use the printer sparingly done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Tap")
        {
            // Don't leave the tap running done
            Debug.Log("Don't leave the tap running done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Elevator")
        {
            // Don't mess with the elevator done
            Debug.Log("Don't mess with the elevator done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "Router")
        {
            // Don't touch the Wi-Fi router done
            Debug.Log("Don't touch the Wi-Fi router done");
            TaskDone();
        }
        else if (target.GetComponent<Interactable>().name == "TheSandwich")
        {
            // Don't eat the sandwich done
            Debug.Log("Don't eat the sandwich done");
            TaskDone();
        }

        Debug.Log("Interact");
    }

    void TaskDone()
    {
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
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
    }
}
