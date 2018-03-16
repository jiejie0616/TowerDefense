using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]                       // 序列化
public class TurretData{
    public GameObject turretPrefab;         // 炮塔模型
    public int cost;                        // 建造炮塔花费
    public GameObject turretUpgradedPrefab; // 升级炮塔模型
    public int costUpgraded;                // 升级花费
    public TurretType type;                 // 炮台类型
}

public enum TurretType                      // 炮塔类型
{
    LaserTurret,
    MissileTurret,
    StandardTurret
}
