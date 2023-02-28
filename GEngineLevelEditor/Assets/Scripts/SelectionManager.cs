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
    private List<Transform> m_currentlySelectedObject = new List<Transform>();
    private Transform m_currentlySelectedGizmo = null;
    private Vector3 m_mousePosition = Vector3.zero;
    private bool m_didSelectNewObject = false;
    private bool m_didSelectGizmo = false;
    private bool m_isMultiSelectionEnabled = false;
    public void SetIsMultiSelectionEnabled(bool value) { m_isMultiSelectionEnabled = value; }
    public void UpdateSelectionManager(PlayerInput playerInput)
    {
        UpdateObjectSelection(playerInput);
    }

    public void UnselectObject()
    {
        m_didSelectNewObject = false;
        m_currentlySelectedObject.Clear();
    }
    public void SelectObject(Transform objectToSelect)
    {
        if (!objectToSelect)
            return;

        m_currentlySelectedObject.Clear();
        m_currentlySelectedObject.Add(objectToSelect);
        m_didSelectNewObject = true;
    }
    public void SelectObject(List<Transform> objectToSelect)
    {
        if (objectToSelect.Count == 0)
            return;

        m_currentlySelectedObject = objectToSelect;
        m_didSelectNewObject = true;
    }
    public Vector3 GetMousePosition() { return m_mousePosition; } 
    private void UpdateObjectSelection(PlayerInput playerInput)
    {
        m_mousePosition = playerInput.actions[m_mousePositionActionName].ReadValue<Vector2>();
        
        if(playerInput.actions[m_mouseSelectionActionName].WasReleasedThisFrame())
        {
            Cursor.visible = true;
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
            if(m_currentlySelectedObject.Count > 0)
                m_currentlySelectedObject.Clear();
            m_didSelectNewObject = true;
        }
    }

    private void HandleGizmosSelect(RaycastHit hit)
    {
        Cursor.visible = false;
        m_currentlySelectedGizmo = hit.transform;
        m_didSelectGizmo = true;
    }

    private void HandleObjectSelect(RaycastHit hit)
    {
        if (m_currentlySelectedObject.Contains(hit.transform))
            return;

        if (!m_isMultiSelectionEnabled)
            m_currentlySelectedObject.Clear();
        m_currentlySelectedObject.Add(hit.transform);
        m_didSelectNewObject = true;

    }
    public List<Transform> GetCurrentlySelectedObject() { return m_currentlySelectedObject; }
    public Transform GetCurrentlySelectedGizmo() { return m_currentlySelectedGizmo; }
    public bool GetDidSelectNewObject() { return m_didSelectNewObject; }
    public bool GetDidSelectNewGizmo() { return m_didSelectGizmo; }
    public void SetDidSelectNewObject(bool didSelectNewObject) { m_didSelectNewObject = didSelectNewObject; }
}
