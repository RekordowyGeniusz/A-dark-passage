using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestInteractable : MonoBehaviour
{
    [SerializeField]
    private GameObject swordItemObj;
    [SerializeField]
    private GameObject healthPotionItemObj;
    [SerializeField]
    private GameObject starItemObj;
    [SerializeField]
    private Sprite openSprite;
    public static ChestInteractable instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            gameObject.GetComponent<Collider2D>().excludeLayers = PlayerStats.instance.layerMask;
            GameManager.instance.ItemSpawnAmount(2, swordItemObj, gameObject.transform);
            GameManager.instance.ItemSpawnAmount(1, healthPotionItemObj, gameObject.transform);
            GameManager.instance.ItemSpawnAmount(2, starItemObj, gameObject.transform);

        }
    }
}
