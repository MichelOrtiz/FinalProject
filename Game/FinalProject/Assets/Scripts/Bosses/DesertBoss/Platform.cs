using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Color defaultColor;
    [SerializeField] private Color colorWhenTarget;
    [SerializeField] private Color colorOnEffect;

    [SerializeField] private State effect;
    [SerializeField] private float baseEffectTime;
    private float effectTime;
    public bool onEffect;
    public bool isTarget;
    private PlayerManager player;
    private bool touchingPlayer;
    void Start()
    {
        player = PlayerManager.instance;
        //defaultMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        //ScenesManagers.GetObjectsOfType<Platform>().Find(p => p.gameObject.name == this.gameObject.name).GetComponent<Platform>() = this;
        isTarget = false;
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTarget && !onEffect)
        {
            gameObject.layer = LayerMask.NameToLayer("Obstacles");
            //gameObject.GetComponent<SpriteRenderer>().material = materialWhenTarget;
            gameObject.GetComponent<SpriteRenderer>().color = colorWhenTarget;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }


        if (onEffect)
        {
            if (effectTime < baseEffectTime)
            {
                if (touchingPlayer)
                {
                    if (!player.GetComponent<StatesManager>().currentStates.Contains(effect))
                    {
                        player.GetComponent<StatesManager>().AddState(effect);
                    }
                }
                effectTime += Time.deltaTime;
            }
            else
            {
                onEffect = false;
                //gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
                gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
                effectTime = 0;
            }
        }
    }

    public void ActivateEffect()
    {
        onEffect = true;

        //gameObject.GetComponent<SpriteRenderer>().material = materialOnEffect;
        gameObject.GetComponent<SpriteRenderer>().color = colorOnEffect;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
        if (other.gameObject.tag == "Spit")
        {
            onEffect = true;
            //gameObject.GetComponent<SpriteRenderer>().material = materialOnEffect;
            gameObject.GetComponent<SpriteRenderer>().color = colorOnEffect;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spit")
        {
            isTarget = false;
        }
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}
