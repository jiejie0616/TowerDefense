using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {
    public GameObject endUI;
    public Text MessageText;

    public static GameManage instance;
    private EnemySpawner enemySpawner;

    void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void win()
    {
        MessageText.text = "胜 利";
        endUI.SetActive(true);
    }

    // 游戏失败
    public void fail()
    {
        MessageText.text = "失 败";
        endUI.SetActive(true);
        enemySpawner.Stop();            // 停止生成敌人
    }

    public void OnButtonRestart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
