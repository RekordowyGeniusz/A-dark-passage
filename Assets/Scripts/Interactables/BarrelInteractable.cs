using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelInteractable : MonoBehaviour
{
    public GameObject starObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10) { 
            gameObject.SetActive(false);
            GameManager.instance.ItemSpawnAmount(1, starObj, gameObject.transform);
        }
    }
}
