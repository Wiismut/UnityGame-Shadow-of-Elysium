using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public bool[] hasItems = new bool[3];
    public Sprite[] itemSprites = new Sprite[3];
    public Inventory inventory;

    void Start()
    {
        if (inventory == null)

        {
            inventory = GetComponent<Inventory>();
        }
    }


    public void AddItem(Sprite itemSprite)
    {
        for (int i = 0; i < hasItems.Length; i++)
        {
            if (!hasItems[i])
            {
                hasItems[i] = true;
                itemSprites[i] = itemSprite;

                if (inventory != null)
                {
                    inventory.UpdateUI();
                }
                else
                {
                    Debug.LogError("инвентарь не");
                }
                return;
            }
        }
    }
}
