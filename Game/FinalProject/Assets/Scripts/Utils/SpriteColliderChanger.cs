using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColliderChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color colorWhenCollide;
    [SerializeField] private List<String> expectedTags;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (collisionHandler == null)
        {
            collisionHandler = GetComponent<CollisionHandler>();
        }
        
        collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;

    }

    void Start()
    {
        spriteRenderer.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = CollidingWithExpectedTag() ? colorWhenCollide : defaultColor;
    }

    bool IsExpectedTag(String tag)
    {
        return expectedTags.Exists(e => e == tag);
    }
    bool CollidingWithExpectedTag()
    {
        if (!collisionHandler.Contacts.Exists(c => c == null))
        {
            foreach (var expectedTag in expectedTags)
            {
                if (collisionHandler.Contacts.Exists(c => c?.tag == expectedTag))
                {
                    spriteRenderer.color = colorWhenCollide;
                    return true;
                }
            }
        }
        return false;
    }

    void collisionHandler_EnterContact(GameObject contact)
    {
        //spriteRenderer.color = CollidingWithExpectedTag() ? colorWhenCollide : defaultColor;
    }

    void collisionHandler_ExitContact(GameObject contact)
    {
        //spriteRenderer.color = CollidingWithExpectedTag() ? colorWhenCollide : defaultColor;
    }
}
