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
    }
    public void DisplayObjectUIData(Transform selectedObject)
    {
        if (!selectedObject)
            return;

        DescentObjectType descentObjectType = selectedObject.GetComponent<DescentObjectType>();
        
        if (!descentObjectType)
            return;

        GameObject dataPanel = GetDataPanelToActivate(descentObjectType);

        if (!dataPanel)
            return;

        ActivateDataPanel(dataPanel);
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
                return null;
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
