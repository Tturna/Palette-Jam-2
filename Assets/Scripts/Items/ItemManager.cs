// This script handles items interaction
// This should be on the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
public class ItemManager : MonoBehaviour
{
    [SerializeField] Vector2 carriedItemPoint;

    List<GameObject> nearItems = new List<GameObject>();

    GameObject carriedItem;
    [SerializeField] Vector2 mouseDir;

    public static ItemManager instance;

    [HideInInspector]
    public bool isCarrying;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isCarrying) ThrowItem(); else PickUpItem();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isCarrying) isCarrying = false;
            // Just placing it down without yeeting it
            carriedItem.transform.SetParent(null);
        }

        // Calculate cursor direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mouseDir = (mousePos - transform.position).normalized;
    }

    void PickUpItem()
    {
        if (nearItems.Count == 0) return;

        carriedItem = nearItems[nearItems.Count-1];
        isCarrying = true;

        // Set the item as a child of the player
        carriedItem.transform.SetParent(transform);
        carriedItem.transform.localPosition = carriedItemPoint;

        // Call event
        carriedItem.GetComponent<Item>().TriggerOnGrabLand(carriedItem);
    }

    void ThrowItem()
    {
        isCarrying = false;

        // Yeet the child
        carriedItem.transform.SetParent(null);
        carriedItem.GetComponent<Item>().Yeet(mouseDir);
        StartCoroutine(DelayR(0.5f));
    }

    IEnumerator DelayR(float delay) // I'm sorry my brain is not working anymore. I just want to get this done with lol
    {
        yield return new WaitForSeconds(delay);
        nearItems.Remove(carriedItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is in the pickup range of an item
        if (collision.gameObject.CompareTag("Pickup"))
        {
            nearItems.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player leaves the pickup range of an item
        if (collision.gameObject.CompareTag("Pickup"))
        {
            nearItems.Remove(collision.gameObject);
        }
    }
}
