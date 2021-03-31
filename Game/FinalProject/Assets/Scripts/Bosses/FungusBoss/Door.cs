using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] public bool isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the door if closed. Closes if open.
    /// </summary>
    /// <returns></returns>
    public void Activate()
    {
        isOpen = !isOpen;
        UpdateState();
    }

    private void UpdateState()
    {
        if (isOpen)
        {
            door.GetComponent<BoxCollider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            door.GetComponent<BoxCollider2D>().enabled = transform;
            door.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
}
