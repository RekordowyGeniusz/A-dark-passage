using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public LayerMask layerMask;
    public RectTransform panel;
    public GameObject winPanel;
    public TextMeshProUGUI pointsTXT;
    public TextMeshProUGUI infoTXT;
    public GameObject resetBtn;
    public TextMeshProUGUI lifes;
    public TextMeshProUGUI winPoints;
    public GameObject defeatPanel;
    public TextMeshProUGUI text;
    public int attackDmg = 10;
    public float HP = 100f;
    public int points = 0;
    public int lives = 3;
    public static PlayerStats instance;
    public Animator animator;
    private bool dead = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadStats();
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfAlive();
        UIController();
    }
    public void LoadStats()
    {
        if(PlayerPrefs.HasKey("Points"))
        {
            points = PlayerPrefs.GetInt("Points");
        }
        else
        {
            points = 0;
        }
        if (PlayerPrefs.HasKey("Lives"))
        {
            lives = PlayerPrefs.GetInt("Lives");
        }
        else
        {
            lives = 3;
        }
        if (PlayerPrefs.HasKey("Dmg"))
        {
            attackDmg = PlayerPrefs.GetInt("Dmg");
        }
        else
        {
            attackDmg = 10;
        }

        lifes.text = "x" + lives;
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
        if(HP + count > 100)
        {
            HP = 100;
        }
        else
        {
            HP += count;
        }
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
            PlayerPrefs.SetInt("Lives", lives);
            Invoke(nameof(GameManager.instance.Stop), 2.27f);
            Invoke(nameof(defeatPanelSetActive), 2.27f);
        }
    }
    private void defeatPanelSetActive()
    {
        if (lives <= 0)
        {
            resetBtn.SetActive(false);
            infoTXT.text = "All lifes depleted - no resets available!";
        }
        else
        {
            resetBtn.SetActive(true);
            infoTXT.text = "You died!";
        }
        defeatPanel.SetActive(true);
        pointsTXT.text = points.ToString();
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
            if(points < 35 && SceneManager.GetActiveScene().buildIndex == 1)
            {
                loose("Didn't get 35 points!");
            }
            else if(points >= 35 && SceneManager.GetActiveScene().buildIndex == 1)
            {
                win();
                points *= 2;
            }
            if(points < 76 && SceneManager.GetActiveScene().buildIndex == 2)
            {
                loose("Didn't get 52 points!");
            }
            else if(points >= 76 && SceneManager.GetActiveScene().buildIndex == 2)
            {
                win();
                points *= 2;
            }
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                win();
                points *= 3;
            }

        }
    }
    public void win()
    {
        GameManager.instance.Stop();
        winPanel.SetActive(true);

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            winPoints.text = points.ToString() + " x2";
        }
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            winPoints.text = points.ToString() + " x3";
        }
    }
    public void loose(string info)
    {
        defeatPanel.SetActive(true);
        pointsTXT.text = points.ToString();
        infoTXT.text = info;
        GameManager.instance.Stop();
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
