using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemInteractable : MonoBehaviour
{

    public IEnumerator OnInteract()
    {
        yield return new WaitForSeconds(.5f);
        PlayerStats.instance.AddAttackDmg(SwordItemStats.dmgMultiplier);
        Destroy(gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(OnInteract());
        }
    }
}
