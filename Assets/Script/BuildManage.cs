using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManage : MonoBehaviour {
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    private TurretData selectedTurretData;
    private MapCube selectedMapCube;

    private Animator upgradeCanvasAnimator;

    public GameObject upgradeCanvas;
    public Button upgradeButton;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 1000;

    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    void ChangeMoney(int change)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))                            // 监测鼠标左键点击 
        {
            if (!EventSystem.current.IsPointerOverGameObject())     // 鼠标没有点击 UI 图标
            {
                // 有鼠标所在位置发射射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;                                     // 存储射线碰撞物
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();     // 获取点击方块
                    // 当前已选择炮台且方块上没有放置炮台
                    if (selectedTurretData != null &&  mapCube.turretGo == null)
                    {
                        if (money > selectedTurretData.cost)                    // 钱够用
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else 
                        {
                            // 钱不够，则触发动画
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {
                        //  升级处理
                        // 若点击同一炮塔，并且面板已显示
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());               // 隐藏面板
                        }
                        else     // 否则显示面部
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }

    public void onLaserSelected(bool isOn)              // 当监听变量发生变化时触发
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void onMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }

    public void onStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    // 显示升级面板
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade)
    {
        StopCoroutine("HideUpgradeUI()");                   // 开始协程
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);                      // 显示面板
        upgradeCanvas.transform.position = pos;             // 放在合适的位置
        upgradeButton.interactable = !isDisableUpgrade;     // 升级按钮是否可用
    }

    // 隐藏升级面板
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeCanvas.SetActive(false);
    }

    // 点击按钮触发函数
    public void onUpgradeButtonDown()
    {
        if (money > selectedTurretData.costUpgraded)
        {
            ChangeMoney(-selectedTurretData.costUpgraded);
            selectedMapCube.UpdateTurret();
            StartCoroutine(HideUpgradeUI());
        }
        else
        {
            // 钱不够，则触发动画
            moneyAnimator.SetTrigger("Flicker");
        }
        
    }

    public void onDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
