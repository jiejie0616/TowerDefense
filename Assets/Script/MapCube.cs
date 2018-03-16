using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;
    public GameObject buildEffect;
    private Renderer renderer;
    [HideInInspector]
    public bool isUpgraded = false;

    public TurretData turretData;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // 建造炮塔
    public void BuildTurret(TurretData turretData)
    {
        // 实例化炮塔模型
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        isUpgraded = false;
        this.turretData = turretData;
        // 产生建造特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);                     // 1.5秒后销毁特效
    }

    // 升级炮塔
    public void UpdateTurret()
    {
        if (isUpgraded) return;
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        isUpgraded = true;
        // 产生特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);                     // 1.5秒后销毁特效
    }

    // 拆毁炮台
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;             // 初始化
        turretGo = null;
        turretData = null;
        // 产生特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);                     // 1.5秒后销毁特效
    }

    void OnMouseEnter()
    {
        if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
