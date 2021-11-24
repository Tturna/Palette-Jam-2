// This script handles player interactions with interactable objects that can not be picked up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> nearObjects = new List<GameObject>();

    void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (nearObjects.Count == 0) return;

        nearObjects[nearObjects.Count - 1].GetComponent<Interactable>().Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters an interactable object's interaction area
        if (collision.gameObject.CompareTag("Interactable"))
        {
            nearObjects.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            GetComponent<PlayerMovement>().canMove = false;
            FindObjectOfType<TaskManager>().EndGame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits an interactable object's interaction area
        if (collision.gameObject.CompareTag("Interactable"))
        {
            nearObjects.Remove(collision.gameObject);
        }
    }
}
