using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionInteractable : MonoBehaviour
{
    public IEnumerator OnInteract()
    {
        yield return new WaitForSeconds(.5f);
        PlayerStats.instance.AddHealth(HealthPotionStats.HealingPower);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(OnInteract());
        }
    }
}
