using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameType
{
    NONE,
    DESCENT
}
public struct TransformStruct
{
    public Vector3 m_location;
    public Vector3 m_rotation;
    public Vector3 m_scale;
}
public class DataManager : MonoBehaviour
{
    [SerializeField] private Transform parent = null;
    public void Start()
    {
        SaveLevel(parent,EGameType.DESCENT);
    }
    public void SaveLevel(Transform levelParent, EGameType gameType)
    {
        string saveData = "";
        if (gameType == EGameType.DESCENT)
            saveData = SaveDescentLevel(levelParent);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/Level1.json", saveData);
    }

    private string SaveDescentLevel(Transform levelParent)
    {
        DescentObjectType objectType;
        TransformStruct transformStruct;

        DescentObjectDataStruct objectData;
        DescentEnemyDataStruct enemyStruct;
        DescentPickupDataStruct pickupStruct;
        DescentObstacleDataStruct obstacleStruct;

        Transform childTransform;
        string saveData = "";
        for (int i = 0; i < levelParent.childCount; i++)
        {
            childTransform = levelParent.GetChild(i);
            objectType = childTransform.GetComponent<DescentObjectType>();

            objectData.m_objectType = objectType.m_objectType.ToString();
            objectData.m_id = i;

            saveData += JsonUtility.ToJson(objectData, true);

            transformStruct.m_location = childTransform.position;
            transformStruct.m_rotation = childTransform.eulerAngles;
            transformStruct.m_scale = childTransform.localScale;

            saveData += JsonUtility.ToJson(transformStruct,true);

            switch (objectType.m_objectType)
            {
                case EDescentObjectType.ENEMY:
                    enemyStruct.m_enemyType = objectType.m_enemyType.ToString();
                    enemyStruct.m_enemyHealth = objectType.m_enemyHealth;

                    saveData += JsonUtility.ToJson(enemyStruct,true);
                    break;
                case EDescentObjectType.PICKUP:
                    pickupStruct.m_pickupType = objectType.m_pickupType.ToString();
                    pickupStruct.m_scoreAmount = objectType.m_scoreAmount;
                    pickupStruct.m_lifeCount = objectType.m_lifeCount;
                    pickupStruct.m_ammoAmount = objectType.m_ammoCount;

                    saveData += JsonUtility.ToJson(pickupStruct,true);
                    break;
                case EDescentObjectType.OBSTACLE:
                    obstacleStruct.m_obstacleType = objectType.m_obstacleType.ToString();

                    saveData += JsonUtility.ToJson(obstacleStruct,true);
                    break;
            }

            saveData += "\n------------------------------------------------\n";
        }

        return saveData;
    }
}


