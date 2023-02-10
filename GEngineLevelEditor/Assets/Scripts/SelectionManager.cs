using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private string m_mouseSelectionActionName = null;
    [SerializeField] private string m_mousePositionActionName = null;
    
    [Space(20.0f)]
    [SerializeField] private LayerMask m_objectSelectionLayer = 0;
    [SerializeField] private LayerMask m_gizmoSelectionLayer = 0;

    //helpers
    private Transform m_currentlySelectedObject = null;
    private Transform m_currentlySelectedGizmo = null;
    private Vector3 m_mousePosition = Vector3.zero;
    private bool m_didSelectNewObject = false;
    private bool m_didSelectGizmo = false;
    private bool m_isHoldingSelect = false;
    public void UpdateSelectionManager(PlayerInput playerInput)
    {
        UpdateObjectSelection(playerInput);
    }

    public Vector3 GetMousePosition() { return m_mousePosition; } 
    private void UpdateObjectSelection(PlayerInput playerInput)
    {
        m_isHoldingSelect = playerInput.actions[m_mouseSelectionActionName].IsPressed();
        m_mousePosition = playerInput.actions[m_mousePositionActionName].ReadValue<Vector2>();
        
        if(playerInput.actions[m_mouseSelectionActionName].WasReleasedThisFrame())
        {
            m_currentlySelectedGizmo = null;
            m_didSelectGizmo = false;
        }

        if (!playerInput.actions[m_mouseSelectionActionName].WasPressedThisFrame())
            return;

        Ray ray = Camera.main.ScreenPointToRay(m_mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_gizmoSelectionLayer))
        {
            HandleGizmosSelect(hit);
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,m_objectSelectionLayer))
        {
            HandleObjectSelect(hit);
            return;
        }
        else
        {
            m_currentlySelectedObject = null;
            m_didSelectNewObject = true;
        }
    }

    private void HandleGizmosSelect(RaycastHit hit)
    {
        m_currentlySelectedGizmo = hit.transform;
        m_didSelectGizmo = true;
    }

    private void HandleObjectSelect(RaycastHit hit)
    {
        if (m_currentlySelectedObject == hit.transform)
            return;

        m_currentlySelectedObject = hit.transform;
        m_didSelectNewObject = true;

    }
    public Transform GetCurrentlySelectedObject() { return m_currentlySelectedObject; }
    public Transform GetCurrentlySelectedGizmo() { return m_currentlySelectedGizmo; }
    public bool GetDidSelectNewObject() { return m_didSelectNewObject; }
    public bool GetDidSelectNewGizmo() { return m_didSelectGizmo; }
    public void SetDidSelectNewObject(bool didSelectNewObject) { m_didSelectNewObject = didSelectNewObject; }
}
