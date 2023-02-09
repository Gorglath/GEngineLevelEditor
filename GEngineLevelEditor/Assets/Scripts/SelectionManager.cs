using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput m_playerInput = null;

    [Header("Parameters")]
    [SerializeField] private string m_mouseSelectionActionName = null;
    [SerializeField] private string m_mousePositionActionName = null;
    
    [Space(20.0f)]
    [SerializeField] private LayerMask m_objectSelectionLayer = 0;
    [SerializeField] private LayerMask m_gizmoSelectionLayer = 0;

    //helpers
    private Transform m_currentlySelectedObject = null;

    private void Update()
    {
        UpdateObjectSelection();
    }

    private void UpdateObjectSelection()
    {
        if (!m_playerInput.actions[m_mouseSelectionActionName].WasPressedThisFrame())
            return;

        Vector2 mousePosition = m_playerInput.actions[m_mousePositionActionName].ReadValue<Vector2>();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity,m_objectSelectionLayer))
        {
            HandleObjectSelect(hit);
            return;
        }

        if(Physics.Raycast(ray,out hit, Mathf.Infinity, m_gizmoSelectionLayer))
        {
            HandleGizmosSelect(hit);
        }
    }

    private void HandleGizmosSelect(RaycastHit hit)
    {

    }

    private void HandleObjectSelect(RaycastHit hit)
    {

    }

}
