using UnityEngine;
using System;
public class BlinkingSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    [SerializeField] private float timeHidden;
    private float curTimeHidden;
    [SerializeField] private float timeBtwBlink;
    private float curTimeBtwBlink;

    void Start()
    {
        
    }

    void Update()
    {
        if (curTimeBtwBlink > timeBtwBlink)
        {
            if (curTimeHidden > timeHidden)
            {
                SpriteRenderer.enabled = true;
                curTimeHidden = 0;
                curTimeBtwBlink = 0;
            }
            else
            {
                SpriteRenderer.enabled = false;
                curTimeHidden += Time.deltaTime;
            }
        }
        else
        {
            curTimeBtwBlink += Time.deltaTime;
        }
    }
}