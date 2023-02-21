using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameType
{
    NONE,
    DESCENT
}
public struct TransformDataStruct
{
    public Vector3 m_location;
    public Vector3 m_rotation;
    public Vector3 m_scale;
}
public class DataManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_levelParent = null;
    [SerializeField] private AssetManager m_assetManger = null;
    private void Start()
    {
        LoadLevel(EGameType.DESCENT);
    }
    public void SaveLevel(EGameType gameType)
    {
        string saveData = "";
        if (gameType == EGameType.DESCENT)
            saveData = SaveDescentLevel();

        System.IO.File.WriteAllText(Application.dataPath + "/Level1.json", saveData);
    }
    public void LoadLevel(EGameType gameType)
    {
        for (int i = 0; i < m_levelParent.childCount; i++)
        {
            Destroy(m_levelParent.GetChild(i).gameObject);
        }

        string saveData = System.IO.File.ReadAllText(Application.dataPath + "/Level1.json");
        if (saveData.Length == 0)
            return;

        string[] saveDataArray = saveData.Split('=',StringSplitOptions.RemoveEmptyEntries);

        if (gameType == EGameType.DESCENT)
            LoadDescentGame(saveDataArray);

    }
    private string SaveDescentLevel()
    {
        DescentObjectType objectType;
        TransformDataStruct transformStruct;

        DescentObjectDataStruct objectData;
        DescentEnemyDataStruct enemyStruct;
        DescentPickupDataStruct pickupStruct;
        DescentObstacleDataStruct obstacleStruct;
        DescentFloorDataStruct floorStruct;
        DescentWallDataStruct wallStruct;
        DescentPropDataStruct propStruct;

        Transform childTransform;
        string saveData = "";
        for (int i = 0; i < m_levelParent.childCount; i++)
        {
            childTransform = m_levelParent.GetChild(i);
            objectType = childTransform.GetComponent<DescentObjectType>();

            objectData.m_objectType = objectType.m_objectType.ToString();
            objectData.m_id = i;

            saveData += JsonUtility.ToJson(objectData, true);

            saveData += "\n~\n";

            transformStruct.m_location = childTransform.position;
            transformStruct.m_rotation = childTransform.eulerAngles;
            transformStruct.m_scale = childTransform.localScale;

            saveData += JsonUtility.ToJson(transformStruct,true);

            saveData += "\n~\n";

            switch (objectType.m_objectType)
            {
                case EDescentObjectType.WALL:
                    wallStruct.m_wallType = objectType.m_wallType.ToString();

                    saveData += JsonUtility.ToJson(wallStruct, true);
                    break;
                case EDescentObjectType.FLOOR:
                    floorStruct.m_floorType = objectType.m_floorType.ToString();

                    saveData += JsonUtility.ToJson(floorStruct, true);
                    break;
                case EDescentObjectType.PROP:
                    propStruct.m_propType = objectType.m_propType.ToString();

                    saveData += JsonUtility.ToJson(propStruct, true);
                    break;
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

            saveData += "\n=";
        }

        return saveData;
    }

    private void LoadDescentGame(string[] objects)
    {
        string[] objectComponents;
        foreach (string objectData in objects)
        {
            if (objectData.Length == 0)
                continue;

            objectComponents = objectData.Split('~');
            DescentObjectDataStruct objectDataStruct = JsonUtility.FromJson<DescentObjectDataStruct>(objectComponents[0]);
            TransformDataStruct objectTransformStruct = JsonUtility.FromJson<TransformDataStruct>(objectComponents[1]);
            EDescentObjectType objectType = Enum.Parse<EDescentObjectType>(objectDataStruct.m_objectType);

            GameObject objectRef = GetObjectPrefab(objectType,objectComponents[2]);
            if (!objectRef)
                return;

            GameObject instanceRef = Instantiate(objectRef, m_levelParent);

            instanceRef.transform.position = objectTransformStruct.m_location;
            instanceRef.transform.eulerAngles = objectTransformStruct.m_rotation;
            instanceRef.transform.localScale = objectTransformStruct.m_scale;
        }
    }
    private GameObject GetObjectPrefab(EDescentObjectType objectType,string objectComponent)
    {
        switch (objectType)
        {
            case EDescentObjectType.WALL:
                if (objectComponent.Contains("NORMAL"))
                    return m_assetManger.GetObjectPrefab(objectType,EDescentEnemyType.NONE
                        ,EDescentPickupType.NONE,EDescentObstacleType.NONE,EDescentWallType.NORMAL);
                else
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.NONE, EDescentObstacleType.NONE, EDescentWallType.RAMP);
            case EDescentObjectType.FLOOR:
                if (objectComponent.Contains("NORMAL"))
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.NONE, EDescentObstacleType.NONE, EDescentWallType.NONE, EDescentFloorType.NORMAL);
                else
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.NONE, EDescentObstacleType.NONE, EDescentWallType.NONE,EDescentFloorType.RAMP);
            case EDescentObjectType.ENEMY:
                return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NORMAL);
            case EDescentObjectType.PICKUP:
                if (objectComponent.Contains("HEALTH"))
                {
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.HEALTH);
                }
                else if (objectComponent.Contains("AMMO"))
                {
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.AMMO);
                }
                else if (objectComponent.Contains("SCORE"))
                {
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.SCORE);
                }
                else
                {
                    return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.HOSTAGE);
                }
            case EDescentObjectType.OBSTACLE:
                return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                      , EDescentPickupType.NONE,EDescentObstacleType.SAW);
            case EDescentObjectType.PROP:
                return m_assetManger.GetObjectPrefab(objectType, EDescentEnemyType.NONE
                        , EDescentPickupType.NONE,EDescentObstacleType.NONE,EDescentWallType.NONE,EDescentFloorType.NONE
                        ,EDescentPropType.COMPUTER);
            default:
                return null;
        }
    }
}


