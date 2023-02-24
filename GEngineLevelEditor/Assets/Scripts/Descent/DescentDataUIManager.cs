using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescentDataUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject m_wallDataPanel = null;
    [SerializeField] private GameObject m_floorDataPanel = null;
    [Space(20.0f)]
    [SerializeField] private GameObject m_enemyDataPanel = null;
    [SerializeField] private GameObject m_pickupDataPanel = null;
    [Space(20.0f)]
    [SerializeField] private DropdownImageLinker m_dropdownImageLinker = null;
    [SerializeField] private DescentEnemyLinker m_descentEnemyLinker = null;
    [SerializeField] private DescentPickupLinker m_descentPickupLinker = null;
    //helpers
    private GameObject m_previouslyActiveDataPanel = null;
    private DescentObjectType m_currentlySelectedObject = null;
    bool m_didSelectNewObject = false;
    private void Start()
    {
        m_dropdownImageLinker.InitializeTextureGroups();
        m_descentEnemyLinker.InitializeDropDown();
        m_descentPickupLinker.InitializeDropDown();
    }
    public void RemoveObjectUIData()
    {
        if (!m_previouslyActiveDataPanel)
            return;

        m_previouslyActiveDataPanel.SetActive(false);
        m_previouslyActiveDataPanel = null;
        m_currentlySelectedObject = null;
        m_didSelectNewObject = false;
    }
    public void DisplayObjectUIData(Transform selectedObject)
    {
        if (!selectedObject)
            return;

        DescentObjectType descentObjectType = selectedObject.GetComponent<DescentObjectType>();
        
        if (!descentObjectType)
            return;

        m_currentlySelectedObject = descentObjectType;
        m_didSelectNewObject = true;

        GameObject dataPanel = GetDataPanelToActivate(descentObjectType);

        if (!dataPanel)
            return;

        ActivateDataPanel(dataPanel);
    }

    public void UpdateDescentDataUIManager()
    {
        if (!m_currentlySelectedObject)
            return;

        if (m_currentlySelectedObject.m_objectType == EDescentObjectType.WALL)
        {
            if (m_dropdownImageLinker.GetDidSwitchWallTexture() ||
                (m_didSelectNewObject && m_currentlySelectedObject.m_wallTextureIndex == -1))
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.GetComponent<MeshRenderer>().material.SetTexture
                    ("_MainTex", m_dropdownImageLinker.GetCurrentWallTexture());

                m_currentlySelectedObject.m_wallTextureIndex = m_dropdownImageLinker.GetCurrentWallTextureIndex();
                m_dropdownImageLinker.ResetDidSwitchWallTexture();
            }
        }

        if (m_currentlySelectedObject.m_objectType == EDescentObjectType.FLOOR)
        {
            if (m_dropdownImageLinker.GetDidSwitchFloorTexture() || 
                (m_didSelectNewObject && m_currentlySelectedObject.m_floorTextureIndex == -1))
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.GetComponent<MeshRenderer>().material.SetTexture
                    ("_MainTex", m_dropdownImageLinker.GetCurrentFloorTexture());

                m_currentlySelectedObject.m_floorTextureIndex = m_dropdownImageLinker.GetCurrentFloorTextureIndex();
                m_dropdownImageLinker.ResetDidSwitchFloorTexture();
            }
        }

        if(m_currentlySelectedObject.m_objectType == EDescentObjectType.ENEMY)
        {
            if (m_descentEnemyLinker.GetDidChangeEnemyType()
                || (m_didSelectNewObject && m_currentlySelectedObject.m_enemyTypeIndex == -1))
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.m_enemyTypeIndex = m_descentEnemyLinker.GetSelectedEnemyTypeIndex();
                m_currentlySelectedObject.m_enemyType = m_descentEnemyLinker.GetNewEnemyType();
            }
            else if(m_descentEnemyLinker.GetDidChangeHealth()
                || (m_didSelectNewObject && m_currentlySelectedObject.m_enemyHealth == -1))
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.m_enemyHealth = (int)m_descentEnemyLinker.GetNewEnemyHealth();
            }
        }

        if(m_currentlySelectedObject.m_objectType == EDescentObjectType.PICKUP)
        {
            if(m_descentPickupLinker.GetDidChangePickupType()
                || (m_didSelectNewObject && m_currentlySelectedObject.m_pickupTypeIndex == -1))
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.m_pickupTypeIndex = m_descentPickupLinker.GetSelectedPickupTypeIndex();
                m_currentlySelectedObject.m_pickupType = m_descentPickupLinker.GetNewPickupType();
            }
            else if(m_descentPickupLinker.GetDidChangeSlider()
                || (m_didSelectNewObject && m_currentlySelectedObject.m_pickupTypeIndex == -1))
            {
                m_didSelectNewObject = false;
                switch (m_currentlySelectedObject.m_pickupType)
                {
                    case EDescentPickupType.SCORE:
                        m_currentlySelectedObject.m_scoreAmount = (int)m_descentPickupLinker.GetNewPickupValue();
                        break;
                    case EDescentPickupType.HEALTH:
                        m_currentlySelectedObject.m_lifeCount = (int)m_descentPickupLinker.GetNewPickupValue();
                        break;
                    case EDescentPickupType.AMMO:
                        m_currentlySelectedObject.m_ammoCount = (int)m_descentPickupLinker.GetNewPickupValue();
                        break;
                    case EDescentPickupType.HOSTAGE:
                        m_currentlySelectedObject.m_scoreAmount = (int)m_descentPickupLinker.GetNewPickupValue();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private GameObject GetDataPanelToActivate(DescentObjectType descentObjectType)
    {
        switch (descentObjectType.m_objectType)
        {
            case EDescentObjectType.WALL:
                m_dropdownImageLinker.SetSelectedWallTextureIndex(descentObjectType.m_wallTextureIndex);
                return m_wallDataPanel;
            case EDescentObjectType.FLOOR:
                m_dropdownImageLinker.SetSelectedFloorTextureIndex(descentObjectType.m_floorTextureIndex);
                return m_floorDataPanel;
            case EDescentObjectType.ENEMY:
                m_descentEnemyLinker.SetSelectedObjectEnemyType(descentObjectType.m_enemyTypeIndex,descentObjectType.m_enemyHealth);
                return m_enemyDataPanel;
            case EDescentObjectType.PICKUP:
                m_descentPickupLinker.SetSelectedObjectPropType(descentObjectType.m_pickupTypeIndex
                    , descentObjectType.m_scoreAmount, descentObjectType.m_ammoCount, descentObjectType.m_lifeCount);
                return m_pickupDataPanel;
            default:
                return null;
        }
    }
    
    private void ActivateDataPanel(GameObject DataPanel)
    {
        if (m_previouslyActiveDataPanel)
            m_previouslyActiveDataPanel.SetActive(false);

        DataPanel.SetActive(true);
        m_previouslyActiveDataPanel = DataPanel;
    }
}
