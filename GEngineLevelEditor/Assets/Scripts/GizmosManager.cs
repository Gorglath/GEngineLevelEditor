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
    private EGizmoOrientation m_currentGizmoOrientation = EGizmoOrientation.WORLD;
    private Gizmo m_currentlySelectedGizmo = null;
    private Transform m_currentlySelectedObject = null;
    public void UpdateGizmos(PlayerInput playerInput, Vector3 mousePosition)
    {
        UpdateGizmosInput(playerInput);
        UpdateSelectedGizmo(mousePosition);
    }

    private void UpdateSelectedGizmo(Vector3 mousePosition)
    {
        if (!m_currentlySelectedGizmo)
            return;

        if (!m_currentlySelectedObject)
            return;

        m_currentlySelectedGizmo.UseGizmo(m_currentlySelectedObject,mousePosition);
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

            m_locationGizmos.transform.forward = Vector3.forward;
            
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

            m_rotationGizmos.transform.forward = Vector3.forward;

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

            m_scaleGizmos.transform.forward = Vector3.forward;

            return;
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
    }
    public void UpdateGizmosLocation(Transform newlySelectedObject)
    {
        if (!newlySelectedObject)
            return;

        m_locationGizmos.transform.SetParent(newlySelectedObject);
        m_locationGizmos.transform.localPosition = Vector3.zero;

        m_rotationGizmos.transform.SetParent(newlySelectedObject);
        m_rotationGizmos.transform.localPosition = Vector3.zero;

        m_scaleGizmos.transform.SetParent(newlySelectedObject);
        m_scaleGizmos.transform.localPosition = Vector3.zero;

        ActivateGizmoOnSelection();
        m_currentlySelectedObject = newlySelectedObject;
    }
}