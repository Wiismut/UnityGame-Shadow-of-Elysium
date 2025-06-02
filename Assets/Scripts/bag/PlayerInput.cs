using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Inventory inventory;
    public AudioClip inventorySound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = inventorySound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (inventorySound == null)
        {
            Debug.LogError("звука нет");
        }

    }

    private void PlayInventorySound()
    {
        if (audioSource != null && inventorySound != null)
        {
            audioSource.PlayOneShot(inventorySound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventory.ToggleInventory();
            PlayInventorySound();
        }
    }
}
