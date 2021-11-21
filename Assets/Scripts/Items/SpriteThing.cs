using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteThing : MonoBehaviour
{
    [HideInInspector] public Sprite normalSprite;
    public Sprite highlightSprite;

    private void Awake()
    {
        normalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
