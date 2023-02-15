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
        GameObject spawnedObject = Instantiate(m_assetGroupToSpawn[m_currentGroupIndex].m_groupAssetTiles[index].m_assetPrefab);
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
}
