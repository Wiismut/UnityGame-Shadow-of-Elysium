using UnityEngine;
using System.Collections;

public class WaterCharacter : MonoBehaviour {
    private Animator animator;
    private static readonly int IsWater = Animator.StringToHash("IsWater");
    public PlayerHealth playerHealth;
    public float damageCooldown = 1f;
    private Coroutine damageCoroutine;
    private bool isInWater = false;
    private bool isOnPlatform = false;
    private Player player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth не назначено", this);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water") && !isInWater)
        {
            isInWater = true;
            UpdateWaterState();

            if (player != null)
            {
                player.SetWaterState(true);
            }
        }
    }    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water") && isInWater)
        {
            isInWater = false;
            UpdateWaterState();

            if (player != null)
            {
                player.SetWaterState(false);
            }
        }
    }        public void SetOnPlatform(bool state)
    {
        isOnPlatform = state;
        UpdateWaterState();
    }

    public bool IsOnPlatform() => isOnPlatform;
    public bool IsInWater() => isInWater;
    private void UpdateWaterState()
    {
        bool shouldBeInWater = isInWater && !isOnPlatform;
        animator.SetBool(IsWater, shouldBeInWater);

        if (shouldBeInWater && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(LoseHealthOverTime());
        }
        else if (!shouldBeInWater && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }
    private IEnumerator LoseHealthOverTime()
    {
        while (isInWater && !isOnPlatform)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamageWater();
            }
            else
            {
                Debug.LogWarning("PlayerHealth не назначено");
            }

            yield return new WaitForSeconds(damageCooldown);
        }

        damageCoroutine = null;
    }
}