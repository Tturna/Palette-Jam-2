// This script handles items interaction
// This should be on the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemManager : MonoBehaviour
{
    [SerializeField] Vector2 carriedItemPoint;

    List<GameObject> nearItems = new List<GameObject>();

    GameObject carriedItem;
    [SerializeField] Vector2 mouseDir;

    bool isCarrying;

    private void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isCarrying) ThrowItem(); else PickUpItem();
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
    }

    void ThrowItem()
    {
        isCarrying = false;

        // Yeet the child
        carriedItem.transform.SetParent(null);
        carriedItem.GetComponent<Item>().Yeet(mouseDir);
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
