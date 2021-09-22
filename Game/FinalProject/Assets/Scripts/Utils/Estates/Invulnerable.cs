using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Invulnerable")]
public class Invulnerable : State
{
    PlayerManager player;
    BlinkingSprite blinkingSprite;
    public override void StartAffect(StatesManager newManager){
        base.StartAffect(newManager);
        player = manager.hostEntity as PlayerManager;
        if (player.collisionHandler != null)
        {
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Fake");
        }
        
        player.isImmune = true;
        blinkingSprite = player.GetComponentInChildren<BlinkingSprite>();
        blinkingSprite.enabled = true;

    }
    public override void Affect(){
        currentTime += Time.deltaTime;
        if(currentTime>=duration){
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        if (player.collisionHandler != null)
        {
            player.collisionHandler.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        player.isImmune = false;
        blinkingSprite.enabled = false;
        Debug.Log("Stopped Invulnerable");
        base.StopAffect();
    }
}
