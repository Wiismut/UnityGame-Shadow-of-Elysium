using UnityEngine;
using System.Collections;

public class SwampCharacter : MonoBehaviour {
    private Animator animator;
    private Player player;
    private static readonly int IsSubmerged = Animator.StringToHash("IsSubmerged");
    public float health = 100f;
    public float damageAmount = 10f;
    public float damageCooldown = 1f;
    private Coroutine damageCoroutine;
    private bool isInWater = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water") && !isInWater)
        {
            isInWater = true;
            animator.SetBool(IsSubmerged, true);
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(LoseHealthOverTime());
            }
            if (player != null)
            {
                player.SetWaterState(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water") && isInWater)
        {
            isInWater = false;
            animator.SetBool(IsSubmerged, false);
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            if (player != null)
            {
                player.SetWaterState(false);
            }
        }
    }

    private IEnumerator LoseHealthOverTime()
    {
        while (isInWater)
        {
            health -= damageAmount;
            Debug.Log("Health: " + health);
            yield return new WaitForSeconds(damageCooldown);
        }

        damageCoroutine = null;
    }
}
