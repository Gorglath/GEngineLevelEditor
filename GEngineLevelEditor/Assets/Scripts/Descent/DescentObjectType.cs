using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDescentObjectType
{
    NONE,
    WALL,
    FLOOR,
    ENEMY,
    PLAYER,
    PICKUP,
    OBSTACLE,
    PROP
}

public enum EDescentEnemyType
{
    NONE,
    NORMAL,
    FAST,
    BURST,
    BOSS
}
public enum EDescentWallType
{
    NONE,
    NORMAL,
    RAMP
}
public enum EDescentFloorType
{
    NONE,
    NORMAL,
    RAMP
}
public enum EDescentPropType
{
    NONE,
    COMPUTER
}
public enum EDescentPickupType
{
    NONE,
    SCORE,
    HEALTH,
    AMMO,
    HOSTAGE
}

public enum EDescentObstacleType
{
    NONE,
    SAW
}
public class DescentObjectType : MonoBehaviour
{
    [Header("Parameters")]
    public EDescentObjectType m_objectType = EDescentObjectType.NONE;
    public EDescentEnemyType m_enemyType = EDescentEnemyType.NONE;
    public EDescentObstacleType m_obstacleType = EDescentObstacleType.NONE;
    public EDescentPickupType m_pickupType = EDescentPickupType.NONE;
    public EDescentFloorType m_floorType = EDescentFloorType.NONE;
    public EDescentWallType m_wallType = EDescentWallType.NONE;
    public EDescentPropType m_propType = EDescentPropType.NONE;
    [Space(20.0f)]
    public int m_enemyHealth = 10;
    public int m_scoreAmount = 10;
    public int m_ammoCount = 10;
    public int m_lifeCount = 1;
    [Space(20.0f)]
    public int m_wallTextureIndex = -1;
    public int m_floorTextureIndex = -1;
    public int m_enemyTypeIndex = -1;
    public int m_pickupTypeIndex = -1;
}
