using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTV : Interactable
{
    public override void Activate()
    {
        TriggerOnInteract();

        // Temp monitoring stuff
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
