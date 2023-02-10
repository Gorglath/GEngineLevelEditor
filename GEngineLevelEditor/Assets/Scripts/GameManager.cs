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


    private void Update()
    {
        UpdateManagers();
        UpdateManagersCommunication();
    }

    private void UpdateManagersCommunication()
    {
        if (m_selectionManager.GetDidSelectNewObject())
        {
            m_selectionManager.SetDidSelectNewObject(false);
            m_gizmosManager.UpdateGizmosLocation(m_selectionManager.GetCurrentlySelectedObject());
            return;
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
        if (!m_cameraManager)
            return;

        m_cameraManager.UpdateCameraManager(m_playerInput);

        if (m_cameraManager.GetIsCameraActive())
            return;

        if (!m_selectionManager)
            return;

        m_selectionManager.UpdateSelectionManager(m_playerInput);

        if (!m_gizmosManager)
            return;

        m_gizmosManager.UpdateGizmos(m_playerInput,m_selectionManager.GetMousePosition());
    }
}
