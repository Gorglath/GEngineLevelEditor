using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput m_playerInput = null;
    [SerializeField] private Transform m_cameraTransform = null;

    [Header("Parameters")]
    [SerializeField] private string m_movementChangeInputActionName = null;
    [SerializeField] private string m_movementDirectionActionName = null;
    [SerializeField] private string m_movementRotationActionName = null;
    [SerializeField] private string m_movementMultiplierActionName = null;
    [SerializeField] private string m_movementDoubleSpeedActionName = null;

    [Space(20.0f)]

    [Range(1.0f,100.0f)] [SerializeField] private float m_cameraMovementSpeed = 10.0f;
    [Range(100.0f, 1000.0f)] [SerializeField] private float m_cameraRotationSpeed = 10.0f;

    //helpers
    private float m_cameraMovementSpeedMultiplier = 1.0f;
    private float m_cameraMovementSpeedMultiplierStep = 0.0f;
    private float m_movementMultiplierAxisValue = 0.0f;
    private bool m_isControlingCamera = false;
    private bool m_previousCameraControlValue = false;
    private bool m_isDoubleSpeed = false;
    private Vector2 m_inputDirection = Vector2.zero;
    private Vector2 m_inputRotation = Vector2.zero;
    
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        UpdateCursor();
        UpdateCameraRotation();
        UpdateCameraMovement();
        UpdateMovementMultiplier();
    }

    void ProcessInput()
    {
        m_isControlingCamera = m_playerInput.actions[m_movementChangeInputActionName].IsPressed();
        m_isDoubleSpeed = m_playerInput.actions[m_movementDoubleSpeedActionName].IsPressed();
        m_inputDirection = m_playerInput.actions[m_movementDirectionActionName].ReadValue<Vector2>().normalized;
        m_inputRotation = m_playerInput.actions[m_movementRotationActionName].ReadValue<Vector2>().normalized;
        m_movementMultiplierAxisValue = m_playerInput.actions[m_movementMultiplierActionName].ReadValue<float>();
    }
    void UpdateCursor()
    {
        if (m_isControlingCamera == m_previousCameraControlValue)
            return;

        Cursor.visible = !m_isControlingCamera;

        m_previousCameraControlValue = m_isControlingCamera;
    }
    void UpdateCameraMovement()
    {
        if (!m_isControlingCamera)
            return;

        if (m_inputDirection.magnitude == 0)
            return;

        Vector3 newPosition = (m_cameraTransform.forward * m_inputDirection.y) + (m_cameraTransform.right * m_inputDirection.x);
        float movementMultiplier = Time.deltaTime * m_cameraMovementSpeed * m_cameraMovementSpeedMultiplier;
        m_cameraTransform.position += newPosition * movementMultiplier * ((m_isDoubleSpeed)? 2:1);
    }

    void UpdateCameraRotation()
    {
        if (!m_isControlingCamera)
            return;

        if (m_inputRotation.magnitude == 0)
            return;

        Vector3 newEulerRotation = new Vector3(
            m_cameraTransform.eulerAngles.x + m_inputRotation.y * m_cameraRotationSpeed * Time.deltaTime * -1,
            m_cameraTransform.eulerAngles.y + m_inputRotation.x * m_cameraRotationSpeed * Time.deltaTime, 0.0f);

        m_cameraTransform.rotation = Quaternion.Euler(newEulerRotation);
    }
    public void UpdateMovementMultiplier()
    {
        if (!m_isControlingCamera)
            return;

        if (m_movementMultiplierAxisValue > 0.0f)
            m_cameraMovementSpeedMultiplierStep = 0.05f;
        if (m_movementMultiplierAxisValue < 0.0f)
            m_cameraMovementSpeedMultiplierStep = -0.05f;

        if (m_cameraMovementSpeedMultiplierStep == 0.0f)
            return;


        m_cameraMovementSpeedMultiplier += m_cameraMovementSpeedMultiplierStep;
        m_cameraMovementSpeedMultiplierStep = 0.0f;

        if (m_cameraMovementSpeedMultiplier > 2.0f)
            m_cameraMovementSpeedMultiplier = 2.0f;
        if (m_cameraMovementSpeedMultiplier < 0.1f)
            m_cameraMovementSpeedMultiplier = 0.1f;
    }
}
