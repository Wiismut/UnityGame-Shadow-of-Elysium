using UnityEngine;
using System.Collections;

public class Swamp : MonoBehaviour {
    public PlayerHealth playerHealth;
    private Coroutine _damageCoroutine;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && _damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && _damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DealDamage()
    {
        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamageWater();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
