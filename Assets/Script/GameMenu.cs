using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    // 点击开始游戏按钮
    public void OnButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    // 点击退出按钮
    public void OnButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();      
#endif
    }
}
