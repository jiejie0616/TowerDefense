using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {
    // 存储路径结点方位
    public static Transform[] positions;

    void Awake()
    {
        positions = new Transform[transform.childCount];    // 初始化路径数组
        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = transform.GetChild(i);           // 存储所有路径结点方位
        }
    }
}
