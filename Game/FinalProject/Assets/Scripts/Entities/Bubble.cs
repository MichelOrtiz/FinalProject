using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image itemSprite;
    public void InFrontOfPlayer(){
        if (PlayerManager.instance.facingDirection == "right")
        {
            transform.position = new Vector3(PlayerManager.instance.GetPosition().x + 2f, PlayerManager.instance.GetPosition().y + 1.9f,-3f);
        }
        else
        {
            transform.position = new Vector3(PlayerManager.instance.GetPosition().x - 2f, PlayerManager.instance.GetPosition().y + 1.9f,-3f);
        }
    }
    public void SetImage(Sprite img, int amount ){
        UpdateAmount(amount);
        itemSprite.sprite = img;
    }
    public void UpdateAmount(int amount){
        amountText.text = "x" + amount.ToString();
    }
}
