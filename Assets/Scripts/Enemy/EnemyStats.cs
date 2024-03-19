using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP = 100;
    public int dmg = 10;
    public float speed = 0.5f;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public static EnemyStats instance;
    private void Awake()
    {
        instance = this;
        gameObject.GetComponent<Transform>().localScale = new Vector2(15f,15f);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.15f,0.18f);
    }
}
