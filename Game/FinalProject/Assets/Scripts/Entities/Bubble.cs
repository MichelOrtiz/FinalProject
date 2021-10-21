using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image itemSprite;
    public void SetImage(Sprite img, int amount ){
        UpdateAmount(amount);
        itemSprite.sprite = img;
    }
    public void UpdateAmount(int amount){
        amountText.text = "x" + amount.ToString();
    }
}
