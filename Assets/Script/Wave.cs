using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自定义每波敌人的参数
[System.Serializable]                   // 序列化
public class Wave
{
    public GameObject enemyPrefab;      // 敌人模型
    public int count;                   // 敌人数量
    public float rate;                  // 敌人生成间隔
}
