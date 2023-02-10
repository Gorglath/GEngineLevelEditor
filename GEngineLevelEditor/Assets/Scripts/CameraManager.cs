using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform m_cameraTransform = null;

    [Header("Parameters")]
    [SerializeField] private string m_movementChangeInputActionName = null;
    [SerializeField] private string m_movementDirectionActionName = null;
    [SerializeField] private string m_movementRotationActionName = null;
    [SerializeField] private string m_movementMultiplierActionName = null;
    [SerializeField] private string m_movementDoubleSpeedActionName = null;
    [SerializeField] private string m_movementHalfSpeedActionName = null;

    [Space(20.0f)]

    [Range(1.0f,100.0f)] [SerializeField] private float m_cameraMovementSpeed = 10.0f;
    [Range(100.0f, 1000.0f)] [SerializeField] private float m_cameraRotationSpeed = 10.0f;

    //helpers
    private float m_cameraMovementSpeedMultiplier = 1.0f;
    private float m_cameraMovementSpeedMultiplierStep = 0.0f;
    private float m_movementMultiplierAxisValue = 0.0f;
    private bool m_isControlingCamera = false;
    private bool m_previousIsCameraControlValue = false;
    private bool m_isDoubleSpeed = false;
    private bool m_isHalfSpeed = false;
    private Vector2 m_inputDirection = Vector2.zero;
    private Vector2 m_inputRotation = Vector2.zero;
    
    // Update is called once per frame
    public void UpdateCameraManager(PlayerInput playerInput)
    {
        ProcessInput(playerInput);
        UpdateCursor();
        UpdateCameraRotation();
        UpdateCameraMovement();
        UpdateMovementMultiplier();
    }

    private void ProcessInput(PlayerInput playerInput)
    {
        m_isControlingCamera = playerInput.actions[m_movementChangeInputActionName].IsPressed();
        m_isDoubleSpeed = playerInput.actions[m_movementDoubleSpeedActionName].IsPressed();
        m_isHalfSpeed = playerInput.actions[m_movementHalfSpeedActionName].IsPressed();
        m_inputDirection = playerInput.actions[m_movementDirectionActionName].ReadValue<Vector2>().normalized;
        m_inputRotation = playerInput.actions[m_movementRotationActionName].ReadValue<Vector2>().normalized;
        m_movementMultiplierAxisValue = playerInput.actions[m_movementMultiplierActionName].ReadValue<float>();
    }
    private void UpdateCursor()
    {
        if (m_isControlingCamera == m_previousIsCameraControlValue)
            return;

        Cursor.visible = !m_isControlingCamera;

        m_previousIsCameraControlValue = m_isControlingCamera;
    }
    private void UpdateCameraMovement()
    {
        if (!m_isControlingCamera)
            return;

        if (m_inputDirection.magnitude == 0)
            return;

        Vector3 newPosition = (m_cameraTransform.forward * m_inputDirection.y) + (m_cameraTransform.right * m_inputDirection.x);
        float movementMultiplier = Time.deltaTime * m_cameraMovementSpeed * m_cameraMovementSpeedMultiplier;
        float speedMultiplier = ((m_isDoubleSpeed) ? 2 : 1);
        if (m_isHalfSpeed)
            speedMultiplier = 0.5f;

        m_cameraTransform.position += newPosition * movementMultiplier * speedMultiplier;
    }

    private void UpdateCameraRotation()
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
    private void UpdateMovementMultiplier()
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

    public bool GetIsCameraActive() { return m_isControlingCamera; }
    public bool GetIsDoubleSpeed() { return m_isDoubleSpeed; }
    public bool GetIsHalfSpeed() { return m_isHalfSpeed; }
}
