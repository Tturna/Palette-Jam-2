using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFridge : Interactable
{
    public override void Activate()
    {
        name = "Fridge";
        TriggerOnInteract(gameObject);

        // Temp monitoring stuff
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
