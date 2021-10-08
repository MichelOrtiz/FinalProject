using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LootChest", menuName = "Inventory/LootChest", order = 0)]
public class LootChest : ScriptableObject {
    [SerializeField] private Item[] items;
    [SerializeField] private float[] spawnProbability;
    public Sprite sprite;
    [SerializeField] private int minMoney;
    [SerializeField] private int maxMoney;
    [SerializeField] private int minItems;
    [SerializeField] private int maxItems;
    [SerializeField] private GameObject prefab;
    private Vector3 spawnPoint;
    
    public List<ObjectProbability<Item>> Berries;
    
    public void Open(){
        int numItems = RandomGenerator.NewRandom(minItems,maxItems);
        Item[] newItems = new Item[numItems];
        for (int i = 0; i < maxItems; i++)
        {
            newItems[i] = RandomGenerator.MatchedElement<Item>(Berries);
        }
        int money = RandomGenerator.NewRandom(minMoney,maxMoney);
        /*Item[] newItems = new Item[numItems]; 
        for(int i=0;i<newItems.Length;){
            for(int x=0;x<items.Length;x++){
                if(Random.value > spawnProbability[x]){
                    newItems[i] = items[x];
                    i++;
                    break;
                }
            }
        }*/

        Inventory.instance.AddMoney(money);

        for(int i=0;i<newItems.Length;i++){
            prefab.GetComponent<Inter>().SetItem(newItems[i]);
            GameObject x = Instantiate(prefab,spawnPoint,Quaternion.identity);
            x.SetActive(true);
        }
    }
    public void SetSpawnPoint(Vector3 newPoint){
        spawnPoint = newPoint;
    }

}

