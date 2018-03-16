using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    // 存储炮塔攻击范围内的敌人
    private List<GameObject> enemys = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")             // 敌人进入
        {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {   
        if (col.tag == "Enemy")             // 敌人离开
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1;        // 攻击频率      
    private float timer = 0;                // 记录时间

    public GameObject bulletPrefab;         // 子弹模型
    public Transform firePosition;          // 子弹生成位置
    public Transform head;                  // 炮塔头部位置

    public bool isLaser = false;
    public int damageRate = 80;
    public LineRenderer laserRenderer;
    public GameObject laserEffect;

    void Start()
    {
        timer = attackRateTime;             // 有敌人立刻攻击
    }

    void Update()
    {
        if (enemys.Count > 0)
        {
            if (enemys[0] != null)
            {
                // 炮塔头部转向敌人位置
                Vector3 targetPosition = enemys[0].transform.position;
                targetPosition.y = head.position.y;         // 要注意高度相同
                head.LookAt(targetPosition);
            }
            if (!isLaser)
            {
                timer += Time.deltaTime;
                if (timer >= attackRateTime)                    // 攻击
                {
                    timer -= attackRateTime;
                    Attack();
                }
            }
            else if(enemys.Count > 0)
            {
                if (laserRenderer.enabled == false)
                {
                    laserRenderer.enabled = true;
                }
                if (enemys[0] == null)             // 若第一个敌人为空
                {
                    updateEnemys();                 // 更新攻击范围内敌人
                }
                if (enemys.Count > 0)
                {
                    enemys[0].GetComponent<Enemy>().TakeDamage(damageRate*Time.deltaTime);
                    // 设置激光的开始位置和结尾位置
                    laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                    laserEffect.transform.position = enemys[0].transform.position;
                    Vector3 pos = transform.position;
                    pos.y = enemys[0].transform.position.y;
                    laserEffect.transform.LookAt(pos);
                    laserEffect.SetActive(true);
                }
            }
        }
        else
        {
            if (laserEffect != null)
            {
                laserEffect.SetActive(false);
            }
            if (laserRenderer != null)
            {
                laserRenderer.enabled = false;
            }
        }
    }

    void Attack()
    {
        if (enemys[0] == null)             // 若第一个敌人为空
        {
            updateEnemys();                 // 更新攻击范围内敌人
        }
        if (enemys.Count > 0)
        {
            // 实例化子弹
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().setTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    // 去除已经死亡的敌人
    void updateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; ++i)
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }
        for (int i = 0; i < emptyIndex.Count; ++i)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
