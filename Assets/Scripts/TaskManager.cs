using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] int tasksOnScreen;
    [SerializeField] int tasksInGame;

    // List of all possible tasks

    // List of tasks to use for this game

    void Start()
    {
        // Populate used task list with random tasks
        PopulateTaskList();

        // Show top 5 in UI
        UpdateUI();
    }

    void Update()
    {
        
    }

    void PopulateTaskList()
    {
        // Create new temporary list for easy random picking
        //List<> all = allTasks;

        //// Populate the tasks list with random tasks
        //for (int i = 0; i < tasksInGame; i++)
        //{
        //    int r = Random.Range(0, all.Count); // Get random index
        //    tasks.Add(all[r]); // Add task from temporary list
        //    all.RemoveAt(r); // Remove task from temporary list to prevent duplicates
        //}
    }

    void UpdateUI()
    {

    }
}
