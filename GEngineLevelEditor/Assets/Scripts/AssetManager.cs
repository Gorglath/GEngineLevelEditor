using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_assetMenuTransform = null;
    [SerializeField] private Transform m_subAssetMenuTransform = null;
    [Space(20.0f)]

    [SerializeField] private Transform m_levelParentTransform = null;
    
    [Space(20.0f)]
    
    [SerializeField] private GameObject m_buttonPrefab = null;

    [Space(20.0f)]
    
    [SerializeField] private AssetGroupScriptable[] m_assetGroupToSpawn = null;

    //helpers
    private Transform m_currentlySelectedObject = null;
    private Transform m_lastSpawnedObject = null;
    private int m_currentGroupIndex = -1;
    private bool m_didSpawnNewObject = false;
    private void Start()
    {
        if (m_assetGroupToSpawn.Length == 0)
            return;

        if (!m_assetMenuTransform)
            return;

        if (!m_buttonPrefab)
            return;

        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            //if (assetGroup.m_groupAssetTiles.Length == 0)
            //    continue;

            int parameter = i;
            Button button = Instantiate(m_buttonPrefab, m_assetMenuTransform).GetComponent<Button>();
            button.image.sprite = m_assetGroupToSpawn[parameter].m_assetGroupIcon;
            button.onClick.AddListener(delegate { OpenSubMenu(parameter); });
        }
    }

    public void OpenSubMenu(int index)
    {
        if (!m_subAssetMenuTransform)
            return;

        m_subAssetMenuTransform.gameObject.SetActive(true);
        for (int i = 0; i < m_subAssetMenuTransform.childCount; i++)
        {
            Destroy(m_subAssetMenuTransform.GetChild(i).gameObject);
        }

        for (int i = 0; i < m_assetGroupToSpawn[index].m_groupAssetTiles.Length; i++)
        {
            int parameter = i;
            Button button = Instantiate(m_buttonPrefab, m_subAssetMenuTransform).GetComponent<Button>();
            button.image.sprite = m_assetGroupToSpawn[index].m_groupAssetTiles[parameter].m_assetIconSprite;
            button.onClick.AddListener(delegate { SpawnSelectedIcon(parameter); });
        }

        m_currentGroupIndex = index;
    }

    public void SpawnSelectedIcon(int index)
    {
        if (!m_levelParentTransform)
            return;

        GameObject spawnedObject = Instantiate(m_assetGroupToSpawn[m_currentGroupIndex].m_groupAssetTiles[index].m_assetPrefab, m_levelParentTransform);
        Vector3 spawnLocation = Vector3.zero;
        
        if (m_currentlySelectedObject)
        {
            spawnLocation = m_currentlySelectedObject.position;
        }
        else
        {
            spawnLocation = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
        }

        spawnedObject.transform.position = spawnLocation;
        m_lastSpawnedObject = spawnedObject.transform;

        m_didSpawnNewObject = true;
    }
    public bool GetDidSpawnNewObject()
    {
        bool didSpawnNewObject = m_didSpawnNewObject;
        m_didSpawnNewObject = false;
        return didSpawnNewObject;
    }
    public Transform GetNewlySpawnedObject() { return m_lastSpawnedObject; }
    public void UpdateSelectedObject(Transform currentlySelectedObject)
    {
        if (!currentlySelectedObject)
            return;

        m_currentlySelectedObject = currentlySelectedObject;
    }

    public void ResetSelectedObject()
    {
        m_currentlySelectedObject = null;
    }

    public GameObject GetObjectPrefab(EDescentObjectType objectType, EDescentEnemyType enemyType = EDescentEnemyType.NONE
        , EDescentPickupType pickupType = EDescentPickupType.NONE, EDescentObstacleType obstacleType = EDescentObstacleType.NONE
        , EDescentWallType wallType = EDescentWallType.NONE, EDescentFloorType floorType = EDescentFloorType.NONE
        , EDescentPropType propType = EDescentPropType.NONE)
    {
        switch (objectType)
        {
            case EDescentObjectType.NONE:
                return null;
            case EDescentObjectType.WALL:
                return GetWallPrefab(wallType);
            case EDescentObjectType.FLOOR:
                return GetFloorPrefab(floorType);
            case EDescentObjectType.ENEMY:
                return GetEnemyPrefab(enemyType);
            case EDescentObjectType.PICKUP:
                return GetPickupPrefab(pickupType);
            case EDescentObjectType.OBSTACLE:
                return GetObstaclePrefab(obstacleType);
            case EDescentObjectType.PROP:
                return GetPropPrefab(propType);
            default:
                return null;
        }
    }
    private GameObject GetPropPrefab(EDescentPropType propType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if (m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType
                != EDescentObjectType.PROP)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_propType
                    == propType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }

    private GameObject GetWallPrefab(EDescentWallType wallType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if(m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType 
                != EDescentObjectType.WALL)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_wallType
                    == wallType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }

    private GameObject GetFloorPrefab(EDescentFloorType floorType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if (m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType
                != EDescentObjectType.FLOOR)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_floorType
                    == floorType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }
    private GameObject GetEnemyPrefab(EDescentEnemyType enemyType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if (m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType
                != EDescentObjectType.ENEMY)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_enemyType
                    == enemyType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }
    private GameObject GetPickupPrefab(EDescentPickupType pickupType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if (m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType
                != EDescentObjectType.PICKUP)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_pickupType
                    == pickupType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }

    private GameObject GetObstaclePrefab(EDescentObstacleType obstacleType)
    {
        for (int i = 0; i < m_assetGroupToSpawn.Length; i++)
        {
            if (m_assetGroupToSpawn[i].m_groupAssetTiles[0].m_assetPrefab.GetComponent<DescentObjectType>().m_objectType
                != EDescentObjectType.OBSTACLE)
                continue;

            for (int j = 0; j < m_assetGroupToSpawn[i].m_groupAssetTiles.Length; j++)
            {
                if (m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab.GetComponent<DescentObjectType>().m_obstacleType
                    == obstacleType)
                    return m_assetGroupToSpawn[i].m_groupAssetTiles[j].m_assetPrefab;
            }
        }

        return null;
    }
}
