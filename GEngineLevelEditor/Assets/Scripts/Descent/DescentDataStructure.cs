using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DescentObjectDataStruct
{
    public string m_objectType;
    public int m_id;
}
[System.Serializable]
public struct DescentEnemyDataStruct
{
    public string m_enemyType;
    public int m_enemyHealth;
    public int m_enemyTypeIndex;
}

[System.Serializable]
public struct DescentPickupDataStruct
{
    public string m_pickupType;
    public int m_scoreAmount;
    public int m_lifeCount;
    public int m_ammoAmount;
}

[System.Serializable]
public struct DescentWallDataStruct
{
    public string m_wallType;
    public int m_textureId;
}

[System.Serializable]
public struct DescentFloorDataStruct
{
    public string m_floorType;
    public int m_textureId;
}

[System.Serializable]
public struct DescentPropDataStruct
{
    public string m_propType;
}
[System.Serializable]
public struct DescentObstacleDataStruct
{
    public string m_obstacleType;
}