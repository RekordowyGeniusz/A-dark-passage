using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI personalRecord;
    public static GameManager instance;
    private void Awake()
    {
        SetRecord();

        instance = this;
    }
    private void SetRecord()
    {
        if (personalRecord != null)
        {
            if (PlayerPrefs.HasKey("Points"))
            {
                personalRecord.text = PlayerPrefs.GetInt("Points").ToString();
            }
            else
            {
                personalRecord.text = "0";
            }
        }
    }

    public void LoadScene(int scene)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Save(0, scene, 3);
            SceneManager.LoadScene(scene);
            Restart();
        }
        else
        {

            Save(PlayerStats.instance.points, scene, PlayerStats.instance.lives);
            SceneManager.LoadScene(scene);
            Restart();
        }
        
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || !PlayerPrefs.HasKey("Level"))
        {
            Save(0, 1, PlayerStats.instance.lives);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        Restart();
    }

    public void ItemSpawnAmount(int amount, GameObject itemObj, Transform parent)
    {
        for (int i = 1; i <= amount; i++)
        {
            float rand = Random.Range(0f, 2f);
            GameObject item = Instantiate(itemObj, null);
            item.transform.position = new Vector3(parent.position.x - rand, parent.position.y, parent.transform.position.z);
            item.SetActive(true);

        }

    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Restart() { 
        Time.timeScale = 1;
    }

    public void Save(int points, int scene, int hearts)
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetInt("Level", scene);
        PlayerPrefs.SetInt("Lives", hearts);
        PlayerPrefs.Save();
    }
}
