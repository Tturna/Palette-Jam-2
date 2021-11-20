using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSandwich : Interactable
{
    public override void Activate()
    {
        name = "TheSandwich";
        TriggerOnInteract(gameObject);

        // Temp monitoring stuff
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
