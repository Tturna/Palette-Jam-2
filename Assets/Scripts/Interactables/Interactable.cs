using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public delegate void OnInteractHandler(GameObject target);
    public event OnInteractHandler OnInteract;

    public virtual void TriggerOnInteract(GameObject target)
    {
        OnInteract?.Invoke(target);
    }

    public abstract void Activate();
}
