using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestInteractable : InteractionManager
{
    [SerializeField]
    private GameObject lootObj;
    protected override void OnInteract()
    {
        base.OnInteract();
        interacted = true;

        this.GetComponent<SpriteRenderer>().color = new Color(0.32f, 0.44f, 0.196f, 1f);
        
        for(int i = 1; i <= 5; i++)
        {
            float rand = Random.Range(0f, 1f);
            Instantiate(lootObj, transform.parent).transform.position = new Vector3(transform.position.x - rand, transform.position.y, transform.position.z);
        }
    }

}
