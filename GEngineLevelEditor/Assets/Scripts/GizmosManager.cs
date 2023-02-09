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
    [SerializeField] private PlayerInput m_playerInput = null; 

    [Space(20.0f)]

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
    private void Update()
    {
        UpdateGizmosInput();
    }

    private void UpdateGizmosInput()
    {
        if (m_playerInput.actions[m_locationGizmoActionName].WasPressedThisFrame())
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

        if (m_playerInput.actions[m_rotationGizmoActionName].WasPressedThisFrame())
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

        if (m_playerInput.actions[m_scaleGizmoActionName].WasPressedThisFrame())
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
}
