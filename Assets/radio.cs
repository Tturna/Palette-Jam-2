using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    public GameObject Prompt;
    public GameObject anotherPrompt;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Prompt.SetActive(true);
            anotherPrompt.SetActive(false);     
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Prompt.SetActive(false);
            anotherPrompt.SetActive(true);    
        }
    }
}
