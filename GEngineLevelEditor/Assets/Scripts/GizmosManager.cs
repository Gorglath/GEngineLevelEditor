using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EGizmoState
{
    LOCATION,
    ROTATION,
    SCALE
}
public enum EGizmoOrientation
{
    LOCAL,
    WORLD
}
public class GizmosManager : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField]private GameObject m_locationGizmos = null;
    [SerializeField]private GameObject m_rotationGizmos = null;
    [SerializeField]private GameObject m_scaleGizmos = null;

    [Header("Parameters")]
    [SerializeField] private string m_locationGizmoActionName = null;
    [SerializeField] private string m_rotationGizmoActionName = null;
    [SerializeField] private string m_scaleGizmoActionName = null;

    //helpers
    private EGizmoState m_currentGizmoState = EGizmoState.LOCATION;
    private Gizmo m_currentlySelectedGizmo = null;
    private List<Transform> m_currentlySelectedObject = new List<Transform>();
    private bool m_isLocalSpaceTransform = false;
    public void SetIsLocalSpaceTransform(bool value)
    {
        if (m_isLocalSpaceTransform == value)
            return;
        m_isLocalSpaceTransform = value;
        UpdateGizmoOrientation();
    }
    public void UpdateGizmos(PlayerInput playerInput, Vector3 mousePosition)
    {
        UpdateGizmosLocation();
        UpdateGizmosInput(playerInput);
        UpdateSelectedGizmo(mousePosition);
    }

    private void UpdateGizmosLocation()
    {
        if (m_currentlySelectedObject.Count == 0)
            return;

        m_locationGizmos.transform.position = m_currentlySelectedObject[m_currentlySelectedObject.Count -1].position;
        m_rotationGizmos.transform.position = m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].position;
        m_scaleGizmos.transform.position = m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].position;
    }
    public void UnselectedObject()
    {
        m_currentlySelectedObject.Clear();

        m_locationGizmos.SetActive(false);
        m_rotationGizmos.SetActive(false);
        m_scaleGizmos.SetActive(false);
    }
    private void UpdateSelectedGizmo(Vector3 mousePosition)
    {
        if (!m_currentlySelectedGizmo)
            return;

        if (m_currentlySelectedObject.Count == 0)
            return;

        m_currentlySelectedGizmo.UseGizmo(m_currentlySelectedObject,mousePosition,m_isLocalSpaceTransform);
    }

    private void UpdateGizmosInput(PlayerInput playerInput)
    {
        if (playerInput.actions[m_locationGizmoActionName].WasPressedThisFrame())
        {
            if (m_currentGizmoState == EGizmoState.LOCATION)
                return;

            m_currentGizmoState = EGizmoState.LOCATION;

            m_locationGizmos.SetActive(true);
            m_rotationGizmos.SetActive(false);
            m_scaleGizmos.SetActive(false);

            if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
            {
                m_locationGizmos.transform.forward = Vector3.forward;
            }
            else if (m_isLocalSpaceTransform)
            {
                m_locationGizmos.transform.rotation =
                  Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
                  m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
            }
            return;
        }

        if (playerInput.actions[m_rotationGizmoActionName].WasPressedThisFrame())
        {
            if (m_currentGizmoState == EGizmoState.ROTATION)
                return;

            m_currentGizmoState = EGizmoState.ROTATION;

            m_locationGizmos.SetActive(false);
            m_rotationGizmos.SetActive(true);
            m_scaleGizmos.SetActive(false);

            if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
                m_rotationGizmos.transform.forward = Vector3.forward;
            else if (m_isLocalSpaceTransform)
            {
                m_rotationGizmos.transform.rotation =
                   Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
                   m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
            }
            return;
        }

        if (playerInput.actions[m_scaleGizmoActionName].WasPressedThisFrame())
        {
            if (m_currentGizmoState == EGizmoState.SCALE)
                return;

            m_currentGizmoState = EGizmoState.SCALE;
            
            m_locationGizmos.SetActive(false);
            m_rotationGizmos.SetActive(false);
            m_scaleGizmos.SetActive(true);

            if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
                m_scaleGizmos.transform.forward = Vector3.forward;
            else if (m_isLocalSpaceTransform)
            {
                m_scaleGizmos.transform.rotation =
                  Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
                  m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
            }
            return;
        }
    }

    private void UpdateGizmoOrientation()
    {
        if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
        {
            m_locationGizmos.transform.forward = Vector3.forward;
        }
        else if (m_isLocalSpaceTransform)
        {
            m_locationGizmos.transform.rotation =
              Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
              m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
        }

        if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
        { 
            m_rotationGizmos.transform.forward = Vector3.forward; 
        }
        else if (m_isLocalSpaceTransform)
        {
            m_rotationGizmos.transform.rotation =
               Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
               m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
        }

        if (!m_isLocalSpaceTransform && m_currentlySelectedObject.Count > 0)
        {
            m_scaleGizmos.transform.forward = Vector3.forward;
        }
        else if (m_isLocalSpaceTransform)
        {
            m_scaleGizmos.transform.rotation =
              Quaternion.LookRotation(m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].forward,
              m_currentlySelectedObject[m_currentlySelectedObject.Count - 1].up);
        }
    }
    public void HandleSelectedGizmo(Transform selectedGizmo)
    {
        if(!selectedGizmo)
        {
            m_currentlySelectedGizmo = null;
            return;
        }
        if (m_currentlySelectedGizmo)
            return;

        m_currentlySelectedGizmo = selectedGizmo.GetComponent<Gizmo>();
        m_currentlySelectedGizmo.MultiplySpeed(1.0f);
    }
    public void ResetSpeedGizmo()
    {
        if (!m_currentlySelectedGizmo)
            return;

        m_currentlySelectedGizmo.MultiplySpeed(1.0f);
    }
    public void DoubleSpeedGizmo()
    {
        if (!m_currentlySelectedGizmo)
            return;

        m_currentlySelectedGizmo.MultiplySpeed(2.0f);
    }

    public void HalfSpeedGizmo()
    {
        if (!m_currentlySelectedGizmo)
            return;

        m_currentlySelectedGizmo.MultiplySpeed(0.5f);
    }

    public void ActivateGizmoOnSelection()
    {
        if(m_currentGizmoState == EGizmoState.LOCATION)
        {
            m_locationGizmos.SetActive(true);
            m_rotationGizmos.SetActive(false);
            m_scaleGizmos.SetActive(false);
        }
        else if(m_currentGizmoState == EGizmoState.ROTATION)
        {
            m_locationGizmos.SetActive(false);
            m_rotationGizmos.SetActive(true);
            m_scaleGizmos.SetActive(false);
        }
        else
        {
            m_locationGizmos.SetActive(false);
            m_rotationGizmos.SetActive(false);
            m_scaleGizmos.SetActive(true);
        }

        UpdateGizmoOrientation();
    }
    public void UpdateGizmosLocation(List<Transform> newlySelectedObject)
    {
        if (newlySelectedObject.Count == 0)
            return;

        ActivateGizmoOnSelection();
        m_currentlySelectedObject = newlySelectedObject;
    }
}
