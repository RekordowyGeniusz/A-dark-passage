using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleModel : MonoBehaviour
{
  
    private void Awake()
    {
        gameObject.GetComponent<Transform>().localScale = new Vector2(15f, 15f);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.17f);
    }
}
