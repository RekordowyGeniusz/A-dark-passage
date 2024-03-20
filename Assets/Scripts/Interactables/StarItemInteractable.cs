using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItemInteractable : MonoBehaviour
{
    public IEnumerator OnInteract()
    {
        yield return new WaitForSeconds(.5f);
        PlayerStats.instance.AddPoints(StarItemStats.instance.pointsAdded);
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
