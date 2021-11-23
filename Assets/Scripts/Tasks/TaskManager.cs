using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string name;
    [HideInInspector] public bool isCompleted;

    public Task () { }

    public Task (string name_)
    {
        name = name_;
    }
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
    [SerializeField] GameObject Office;
    int numOfTimesPrinterUsed = 0;

    [SerializeField] GameObject currentTaskTodo;
    [SerializeField] GameObject taskCounter;

    void Start()
    {
        totalTasks = tasks.Count;
        // Populate used task list with random tasks
        RandomizeTaskList();

        // Set current task
        currentTask = tasks[0];

        // Update item highlights depending on the task
        UpdateItemHighlights();

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
            TaskDone("Don't run in the office");
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
            if (TaskDone("Don't steal people's stuff")) SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.phoneThrow); return;
        }
    }

    void OnItemLand(GameObject item, Vector2 pos)
    {
        try
        {
            if (item.GetComponent<Item>().itemType == Item.ItemType.Food && (pos - (Vector2)GameObject.Find("point_programmers").transform.position).magnitude < 3)
            {
                // Don't feed the programmers done
                int rand = Random.Range(0,SfxManager.instance.EmployeesCursing.Count);
                if (TaskDone("Don't feed the programmers")) GameObject.FindGameObjectWithTag("Employee").GetComponent<Animator>().Play("Employee_Angry");  SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.EmployeesCursing[rand]); return;
            }
            if (item.GetComponent<Item>().itemType == Item.ItemType.Marker && (pos - (Vector2)GameObject.Find("point_whiteboard").transform.position).magnitude < 3)
            {
                // Don't mess up the whiteboard done
                if (TaskDone("Don't mess up the whiteboard")) Office.GetComponent<Animator>().Play("Whiteboard"); return;
            }
            if (item.GetComponent<Item>().itemType == Item.ItemType.Food || item.GetComponent<Item>().itemType == Item.ItemType.Drink)
            {
                // Don't litter done
                if (TaskDone("Don't litter")) return;
            }
            if ((pos - (Vector2)GameObject.Find("point_employees").transform.position).magnitude < 3)
            {
                // Don't annoy other employees done
                int rand = Random.Range(0,SfxManager.instance.EmployeesCursing.Count);
                if (TaskDone("Don't annoy other employees")) GameObject.FindGameObjectWithTag("Employee").GetComponent<Animator>().Play("Employee_Angry"); SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.EmployeesCursing[rand]); return;
            }
            if (item.GetComponent<Item>().itemType == Item.ItemType.Drink)
            {
                // Don't spill drinks in the office done
                if (TaskDone("Don't spill drinks in the office")) SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.mugThrow); return;
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
            Office.GetComponent<Animator>().Play("Fridge");
        }
        else if (target.GetComponent<Interactable>().name == "Printer")
        {

            numOfTimesPrinterUsed++;
            Office.GetComponent<Animator>().Play("Printer");

            if (numOfTimesPrinterUsed >= 2)
            {
                // Use the printer sparingly done
                TaskDone("Use the printer sparingly 2x");
            }
            
        }
        else if (target.GetComponent<Interactable>().name == "Tap")
        {
            // Don't leave the tap running done
            TaskDone("Don't leave the tap running");
            Office.GetComponent<Animator>().Play("Tap");
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
            TaskDone("Don't eat the sandvich");
            SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.sandvich);
        }
        else if (target.GetComponent<Interactable>().name == "Doge")
        {
            // Don't let the dog free done
            TaskDone("Don't let the dog free");
            target.GetComponent<Doge>().FuckingZoom();
        }

        Debug.Log("Interact");
    }

    public bool TaskDone(string name)
    {
        // Check if this is not the current task
        if (currentTask.name != name) return false; // Prevent doing a task that you don't have

        // Check if the task is already done
        if (doneTasks.Contains(name)) return false;

        // if not, add it to the list of done tasks
        else doneTasks.Add(name);

        Debug.Log(name + " done!");
        StartCoroutine(HUDManager.instance.RuleBreak(3.5f));
        tasksDone++;
        tasks.RemoveAt(0);
        currentTask.isCompleted = true;

        // Everything done
        // Give last task (get out)
        if (tasksDone >= totalTasks)
        {

            endPoint.SetActive(true);
            currentTask = new Task("Get out!");
            return true;
        }

        currentTask = tasks[0];
        UpdateItemHighlights();


        int rand = Random.Range(0, SfxManager.instance.WinSounds.Count);

        SfxManager.instance.Audio.PlayOneShot(SfxManager.instance.WinSounds[rand]);
        return true;
    }

    public void EndGame()
    {
        currentTaskTodo.SetActive(false);
            taskCounter.SetActive(false);
        endScreen.SetActive(true);
    }

    void UpdateItemHighlights()
    {
        // Reset all highlights
        foreach (SpriteThing st in FindObjectsOfType<SpriteThing>())
        {
            st.SetBlink(false);
        }

        // Highlight all relevant items
        try
        {
            if (currentTask.name == "Don't feed the programmers")
            {
                Item[] items = FindObjectsOfType<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemType == Item.ItemType.Food)
                    {
                        items[i].gameObject.GetComponent<SpriteThing>().SetBlink(true);
                    }
                }
            }
            else if (currentTask.name == "Don't eat the sandwich")
            {
                GameObject g = GameObject.Find("Sandwich_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't spill drinks in the office")
            {
                Item[] items = FindObjectsOfType<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemType == Item.ItemType.Drink)
                    {
                        items[i].gameObject.GetComponent<SpriteThing>().SetBlink(true);
                    }
                }
            }
            else if (currentTask.name == "Don't leave the fridge door open")
            {

                GameObject g = GameObject.Find("Fridge_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't annoy other employees")
            {
                Item[] items = FindObjectsOfType<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    items[i].gameObject.GetComponent<SpriteThing>().SetBlink(true);
                }
            }
            else if (currentTask.name == "Use the printer sparingly")
            {

                GameObject g = GameObject.Find("Printer_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't leave the tap running")
            {

                GameObject g = GameObject.Find("Sink_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't mess with the elevator")
            {

                GameObject g = GameObject.Find("Elevator_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't litter")
            {
                Item[] items = FindObjectsOfType<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemType == Item.ItemType.General || items[i].itemType == Item.ItemType.Drink || items[i].itemType == Item.ItemType.Food)
                    {
                        items[i].gameObject.GetComponent<SpriteThing>().SetBlink(true);
                    }
                }
            }
            else if (currentTask.name == "Don't touch the TV" && TaskDone("Don't touch the TV") == false)
            {

                GameObject g = GameObject.Find("TV_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't touch the Wi-Fi router")
            {

                GameObject g = GameObject.Find("Router_Interactable");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
            else if (currentTask.name == "Don't steal people's stuff")
            {
                Item[] items = FindObjectsOfType<Item>();

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].itemType == Item.ItemType.Property)
                    {
                        items[i].gameObject.GetComponent<SpriteThing>().SetBlink(true);
                    }
                }
            }
            else if (currentTask.name == "Don't mess up the whiteboard" && TaskDone("Don't mess up the whiteboard") == false)
            {

                GameObject g = GameObject.Find("Marker");
                g.GetComponent<SpriteThing>().SetBlink(true);
            }
        }
        catch { } // kek
    }
}
