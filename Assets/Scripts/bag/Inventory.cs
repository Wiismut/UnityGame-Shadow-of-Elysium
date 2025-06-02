using UnityEngine;

public class Inventory : MonoBehaviour {

    private Canvas canvas;
    public GameObject[] players;
    private Items items;
    public Transform inventorySlots;
    private Slot[] slots;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        foreach (GameObject player in players)
        {
            if (player != null && player.activeInHierarchy)
            {
                items = player.GetComponent<Items>();
                break;
            }
        }

        slots = inventorySlots.GetComponentsInChildren<Slot>();
    }

    public void ToggleInventory()
    {
        canvas.enabled = !canvas.enabled;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (items == null)
        {
            return;
        }

        int length = Mathf.Min(slots.Length, items.hasItems.Length);

        for (int i = 0; i < length; i++)
        {
            bool active = items.hasItems[i];
            slots[i].UpdateSlot(active, items.itemSprites[i]);
        }
    }
}
