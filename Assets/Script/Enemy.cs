using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private Transform[] points;             // 存储敌人路径 
    private int index = 0;                  // 存储当前目的地结点序号
    public int speed = 10;                  // 敌人速度
    public float hp = 100;
    private float totalHp;
    public Slider hpSlider;
    public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
        points = WayPoints.positions;       // 获取路径
        totalHp = hp;
	}
	
	// Update is called once per frame
	void Update () {
        Move();                             // 控制敌人移动
	}

    void Move()
    {
        // 敌人移动
        transform.Translate((points[index].position - transform.position).normalized * Time.deltaTime * speed);
        // 若到达目的地，则更新目的地
        if (Vector3.Distance(points[index].position, transform.position) < 0.2f)
        {
            if (index < points.Length - 1)
            {
                index++;
            }
            else
            {
                reachDis();                 // 到达终点
            }
        }
    }

    void reachDis()
    {
        // 到达终点后销毁敌人
        GameObject.Destroy(this.gameObject);
        GameManage.instance.fail();
    }

    void OnDestroy()
    {
        EnemySpawner.countEnemyAlive--;     // 存活敌人个数-1
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;           // 更新血条
        if (hp <= 0)
        {
            GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
