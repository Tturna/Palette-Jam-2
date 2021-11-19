using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public delegate void OnInteractHandler();
    public event OnInteractHandler OnInteract;

    public virtual void TriggerOnInteract()
    {
        OnInteract?.Invoke();
    }

    public abstract void Activate();
}
