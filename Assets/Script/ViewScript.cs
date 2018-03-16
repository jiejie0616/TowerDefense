using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScript : MonoBehaviour {
    public int speed = 10;                                  // 方位键灵敏度
    public int mouseSpeed = 600;                            // 鼠标中键灵敏度

	// Update is called once per frame
    // W,A,S,D控制画面左右移动，鼠标中轴控制画面缩放
	void Update () {
        float h = Input.GetAxis("Horizontal");              // 获取左右键输入
        float v = Input.GetAxis("Vertical");                // 获取上下键输入
        float mouse = Input.GetAxis("Mouse ScrollWheel");   // 获取鼠标中轴输入
        // 摄像头移动
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.World);
	}
}
