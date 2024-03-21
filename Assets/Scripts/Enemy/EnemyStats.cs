using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float HP = 100f;
    public int dmg = 10;
    public float speed = 0.5f;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public static EnemyStats instance;
    public GameObject HpBarPivot;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        EnemyHpBarController();
    }

    private void EnemyHpBarController()
    {
        HpBarPivot.transform.localScale = new Vector2(HP / 100, HpBarPivot.transform.localScale.y);
    }

    public void subHP(float amount)
    {
        HP -= amount;
      //  Debug.Log(HP);
    }
}
