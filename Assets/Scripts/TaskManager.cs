using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Task
{
    public string name;
}

public class TaskManager : MonoBehaviour
{
    // General
    [SerializeField] int tasksOnScreen;
    [SerializeField] int tasksInGame;

    // List of all possible tasks
    [SerializeField] List<Task> allTasks = new List<Task>();

    // List of tasks to use for this game
    List<Task> tasks = new List<Task>();

    void Start()
    {
        // Populate used task list with random tasks
        PopulateTaskList();

        // Show top 5 in UI
        UpdateUI();

        // Subscribe to events
        Item[] items = FindObjectsOfType<Item>();
        Interactable[] inter = FindObjectsOfType<Interactable>();

        for (int i = 0; i < items.Length; i++)
        {
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

    void PopulateTaskList()
    {
        // Create new temporary list for easy random picking
        List<Task> all = allTasks;

        // Populate the tasks list with random tasks
        for (int i = 0; i < tasksInGame; i++)
        {
            int r = Random.Range(0, all.Count); // Get random index
            tasks.Add(all[r]); // Add task from temporary list
            all.RemoveAt(r); // Remove task from temporary list to prevent duplicates
        }
    }

    void UpdateUI()
    {

    }

    void OnItemLand(GameObject item, Vector2 pos)
    {
        Debug.Log("Item landed");
    }

    void OnInteract(GameObject target)
    {
        Debug.Log("Interact");
    }
}
