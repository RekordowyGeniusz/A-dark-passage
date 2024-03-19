using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public LayerMask layerMask;
    private int attackDmg = 1;
    public int HP = 100;
    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void AddAttackDmg(int count)
    {
        attackDmg += count;
    }
    public void DecreaseAttackDmg(int count)
    {
        attackDmg -= count;
    }
    public void AddHealth(int count)
    {
        HP += count;
    }
    public void DecreaseHealth(int count)
    {
        HP -= count;
    }
}
