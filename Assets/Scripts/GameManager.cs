using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Restart();
    }

    public void Exit()
    {
        Application.Quit();
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
}
