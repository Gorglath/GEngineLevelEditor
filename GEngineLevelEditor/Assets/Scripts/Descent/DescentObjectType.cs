using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDescentObjectType
{
    NONE,
    TRANSFORM,
    ENEMY,
    PLAYER,
    PICKUP,
    OBSTACLE
}

public enum EDescentEnemyType
{
    NONE,
    NORMAL,
    FAST,
    BURST,
    BOSS
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
    [HideInInspector]
    public EDescentEnemyType m_enemyType = EDescentEnemyType.NONE;
    [HideInInspector]
    public EDescentObstacleType m_obstacleType = EDescentObstacleType.NONE;
    [HideInInspector]
    public EDescentPickupType m_pickupType = EDescentPickupType.NONE;
    

}
