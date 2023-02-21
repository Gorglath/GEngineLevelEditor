using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private PlayerInput m_playerInput = null;

    [Space(20.0f)]

    [SerializeField] private CameraManager m_cameraManager = null;
    [SerializeField] private SelectionManager m_selectionManager = null;
    [SerializeField] private GizmosManager m_gizmosManager = null;
    [SerializeField] private UIManager m_uiManager = null;
    [SerializeField] private AssetManager m_assetManager = null;

    [Space(20.0f)]

    [SerializeField] private DescentDataUIManager m_descentUIDataManager = null;
    private void Update()
    {
        UpdateManagers();
        UpdateManagersCommunication();
    }

    private void UpdateManagersCommunication()
    {
        if(m_assetManager.GetDidSpawnNewObject())
        {
            m_selectionManager.SelectObject(m_assetManager.GetNewlySpawnedObject());
            m_gizmosManager.UpdateGizmosLocation(m_selectionManager.GetCurrentlySelectedObject());
            m_uiManager.SelectedNewObject(m_selectionManager.GetCurrentlySelectedObject());
            m_descentUIDataManager.DisplayObjectUIData(m_selectionManager.GetCurrentlySelectedObject());
            return;
        }
        if (m_uiManager.GetIsSelectingUI())
            return;

        if (m_selectionManager.GetDidSelectNewObject())
        {
            m_selectionManager.SetDidSelectNewObject(false);
            m_gizmosManager.UpdateGizmosLocation(m_selectionManager.GetCurrentlySelectedObject());
            m_uiManager.SelectedNewObject(m_selectionManager.GetCurrentlySelectedObject());
            m_assetManager.UpdateSelectedObject(m_selectionManager.GetCurrentlySelectedObject());
            m_descentUIDataManager.DisplayObjectUIData(m_selectionManager.GetCurrentlySelectedObject());
            return;
        }
        else
        {
            if (!m_selectionManager.GetCurrentlySelectedObject())
            {
                m_uiManager.UnselectedObject();
                m_gizmosManager.UnselectedObject();
                m_assetManager.ResetSelectedObject();
                m_descentUIDataManager.RemoveObjectUIData();
            }
        }

        if(m_selectionManager.GetDidSelectNewGizmo())
        {
            m_gizmosManager.HandleSelectedGizmo(m_selectionManager.GetCurrentlySelectedGizmo());
        }
        else
        {
            m_gizmosManager.HandleSelectedGizmo(null);
        }

        if(m_cameraManager.GetIsDoubleSpeed())
        {
            m_gizmosManager.DoubleSpeedGizmo();
            return;
        }
        else if (m_cameraManager.GetIsHalfSpeed())
        {
            m_gizmosManager.HalfSpeedGizmo();
        }
        else
        {
            m_gizmosManager.ResetSpeedGizmo();
        }
    }
    private void UpdateManagers()
    {
        if (!m_descentUIDataManager)
            return;

        m_descentUIDataManager.UpdateDescentDataUIManager();

        if (!m_cameraManager)
            return;

        m_cameraManager.UpdateCameraManager(m_playerInput);

        if (m_cameraManager.GetIsCameraActive())
            return;

        if (!m_gizmosManager)
            return;

        m_gizmosManager.UpdateGizmos(m_playerInput, m_selectionManager.GetMousePosition());

        if (!m_uiManager)
            return;

        m_uiManager.RefreshTransform();

        if (m_uiManager.GetIsSelectingUI())
            return;



        if (!m_selectionManager)
            return;

        m_selectionManager.UpdateSelectionManager(m_playerInput);
    }
}
