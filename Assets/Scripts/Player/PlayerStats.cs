using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public LayerMask layerMask;
    public RectTransform panel;
    public GameObject winPanel;
    public GameObject defeatPanel;
    public TextMeshProUGUI text;
    public int attackDmg = 10;
    public float HP = 100f;
    public int points = 0;
    private int lives = 3;
    public static PlayerStats instance;
    public Animator animator;
    private bool dead = false;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfAlive();
        UIController();
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
    public void DecreaseHealth(float count)
    {
        if(HP - count >= 0f)
        {
            HP -= count;
        }

    }
    public void AddPoints(int count)
    {
        points += count;
    }
    public void Decreasepoints(int count)
    {
        points -= count;
    }
    private void CheckIfAlive()
    {
        if(HP <= 0 && !dead)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            dead = true;
            animator.SetInteger("Speed", 0);
            animator.Play("Die");
            lives--;
            Invoke(nameof(GameManager.instance.Stop), 2.27f);
            Invoke(nameof(defeatPanelSetActive), 2.27f);
        }
    }
    private void defeatPanelSetActive()
    {
        defeatPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 12)
        {
            StartCoroutine(twoSecondsCooldown());
        }
        if(collision.gameObject.layer == 13)
        {
            Decreasepoints(10);
        }
        if(collision.gameObject.layer == 14)
        {
            winPanel.SetActive(true);
            GameManager.instance.Stop();
        }
    }
    private IEnumerator twoSecondsCooldown()
    {
        DecreaseHealth(10);
        yield return new WaitForSeconds(2f);
    }

    private void UIController()
    {
        if (HP > 0)
        {
            panel.localScale = new Vector2(HP / 100, panel.localScale.y);
        }
        else
        {
            panel.localScale = new Vector2(0f, panel.localScale.y);
        }
        text.text = ": " + points.ToString();
    }
}
