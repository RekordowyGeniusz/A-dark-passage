using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : InteractionManager
{
    protected override void OnInteract()
    {
        base.OnInteract();
        interacted = true;

        this.GetComponent<SpriteRenderer>().color = new Color(0.32f, 0.44f, 0.196f, 1f);
    }

}
