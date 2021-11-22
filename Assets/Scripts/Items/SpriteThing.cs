using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteThing : MonoBehaviour
{
    [HideInInspector] public Sprite normalSprite;
    public Sprite highlightSprite;

    SpriteRenderer sr;
    bool blinking;
    float blinkInterval = 0.5f;
    float timer;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;
    }

    private void Update()
    {
        if (blinking)
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                timer = 0f;
                sr.sprite = sr.sprite == normalSprite ? highlightSprite : normalSprite;
            }
        }
    }

    public void SetBlink(bool state)
    {
        blinking = state;
        timer = 0f;
    }
}
