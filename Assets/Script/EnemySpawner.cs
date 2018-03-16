using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Wave[] waves;                            // 定义每波敌人参数
    public Transform start;                         // 敌人出生点
    public int waveRate = 3;                        // 每波敌人生成间隔
    public static int countEnemyAlive = 0;          // 生存敌人个数
    private Coroutine coroutine;

	// Use this for initialization
	void Start () {
        coroutine = StartCoroutine(SpawnEnemy());               // 启动线程
	}

    public void Stop()
    {
        StopCoroutine(coroutine);                   // 停止协程
    }

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)                 // 遍历每一波敌人
        {
            for (int i = 0; i < wave.count; ++i)    // 生成每个敌人
            {
                // 生成敌人
                GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
                countEnemyAlive++;                  // 生存敌人个数+1
                if (i != wave.count - 1)            // 敌人生成间隔
                    yield return new WaitForSeconds(wave.rate);
            }
            while (countEnemyAlive > 0)             // 上一波敌人全部死亡后，才能生成下一波敌人
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);      // 等待生成下一波敌人
        }
        while (countEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManage.instance.win();
    }
}
