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
    [SerializeField] private GameObject m_obstacleDataPanel = null;
    [Space(20.0f)]
    [SerializeField] private GameObject m_lifePickableDataPanel = null;
    [SerializeField] private GameObject m_scorePickableDataPanel = null;
    [SerializeField] private GameObject m_ammoPickableDataPanel = null;
    [SerializeField] private GameObject m_hostagePickableDataPanel = null;
    [Space(20.0f)]
    [SerializeField] private DropdownImageLinker m_DropdownImageLinker = null;
    //helpers
    private GameObject m_previouslyActiveDataPanel = null;
    private DescentObjectType m_currentlySelectedObject = null;
    bool m_didSelectNewObject = false;
    private void Start()
    {
      m_DropdownImageLinker.InitializeTextureGroups();
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
            if (m_DropdownImageLinker.GetDidSwitchWallTexture() || m_didSelectNewObject)
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.GetComponent<MeshRenderer>().material.SetTexture
                    ("_MainTex", m_DropdownImageLinker.GetCurrentWallTexture());

                m_DropdownImageLinker.ResetDidSwitchWallTexture();
            }
        }

        if (m_currentlySelectedObject.m_objectType == EDescentObjectType.FLOOR)
        {
            if (m_DropdownImageLinker.GetDidSwitchFloorTexture() || m_didSelectNewObject)
            {
                m_didSelectNewObject = false;
                m_currentlySelectedObject.GetComponent<MeshRenderer>().material.SetTexture
                    ("_MainTex", m_DropdownImageLinker.GetCurrentFloorTexture());

                m_DropdownImageLinker.ResetDidSwitchFloorTexture();
            }
        }
    }
    private GameObject GetDataPanelToActivate(DescentObjectType descentObjectType)
    {
        switch (descentObjectType.m_objectType)
        {
            case EDescentObjectType.WALL:
                return m_wallDataPanel;
            case EDescentObjectType.FLOOR:
                return m_floorDataPanel;
            case EDescentObjectType.ENEMY:
                return m_enemyDataPanel;
            case EDescentObjectType.PICKUP:
                switch (descentObjectType.m_pickupType)
                {
                    case EDescentPickupType.SCORE:
                        return m_scorePickableDataPanel;
                    case EDescentPickupType.HEALTH:
                        return m_lifePickableDataPanel;
                    case EDescentPickupType.AMMO:
                        return m_ammoPickableDataPanel;
                    case EDescentPickupType.HOSTAGE:
                        return m_hostagePickableDataPanel;
                    default:
                        return null;
                }
            case EDescentObjectType.OBSTACLE:
                return m_obstacleDataPanel;
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
