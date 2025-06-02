using UnityEngine;

public class Mob : MonoBehaviour {

    public AudioClip mobSound;
    private AudioSource audioSource;
    public PlayerHealth playerHealth;
    private bool hasDamagedPlayer = false;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = mobSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (mobSound == null)
        {
            Debug.LogError("звука нет");
        }
    }
    private void PlayDamageSound()
    {
        if (audioSource != null && mobSound != null)
        {
            audioSource.PlayOneShot(mobSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && !hasDamagedPlayer)
        {
            if (playerHealth != null)
            {
                PlayDamageSound();
                playerHealth.TakeDamage();
                hasDamagedPlayer = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            hasDamagedPlayer = false;
        }
    }
}
