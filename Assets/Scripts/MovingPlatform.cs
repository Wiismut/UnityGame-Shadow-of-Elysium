using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            WaterCharacter waterCharacter = collision.GetComponent<WaterCharacter>();
            if (waterCharacter != null)
            {
                waterCharacter.SetOnPlatform(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);

            WaterCharacter waterCharacter = collision.GetComponent<WaterCharacter>();
            if (waterCharacter != null)
            {
                waterCharacter.SetOnPlatform(false);
            }
        }
    }

    private void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player"))
            {
                child.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}