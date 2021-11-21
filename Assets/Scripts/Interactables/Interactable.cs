using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void OnInteractHandler(GameObject target);
    public event OnInteractHandler OnInteract;

    public string name;

    public virtual void TriggerOnInteract(GameObject target)
    {
        OnInteract?.Invoke(target);
    }

    public void Activate()
    {
          TriggerOnInteract(gameObject);
    }
}
