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

    private void ItemSpawnAmount(int amount, GameObject itemObj)
    {
        for (int i = 1; i <= amount; i++)
        {
            float rand = Random.Range(0f, 2f);
            Instantiate(itemObj, gameObject.transform).transform.position = new Vector3(transform.position.x - rand, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0.32f, 0.44f, 0.196f, 1f);
            gameObject.GetComponent<Collider2D>().excludeLayers = PlayerStats.instance.layerMask;
            ItemSpawnAmount(2, swordItemObj);
            ItemSpawnAmount(1, healthPotionItemObj);
        }
    }
}
