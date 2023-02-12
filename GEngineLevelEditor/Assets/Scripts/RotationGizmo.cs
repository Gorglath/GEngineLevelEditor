using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERotationGizmoType
{
    NONE,
    FORWARD_AXIS,
    RIGHT_AXIS,
    UP_AXIS
}
public class RotationGizmo : Gizmo
{
    [Header("Parameters")]
    [SerializeField] private ERotationGizmoType m_rotationGizmoType = ERotationGizmoType.NONE;

    private void Start()
    {
        m_speedMultiplied = m_speed;
    }
    public override void MultiplySpeed(float value)
    {
        m_speedMultiplied = m_speed * value;
    }

    public override void UseGizmo(Transform affectedObject, Vector3 mousePosition)
    {
        if (mousePosition.magnitude == 0)
            return;

        if (m_previousMousePosition == mousePosition)
            return;

        UpdateAffectedObjectRotation(affectedObject, mousePosition);
        m_previousMousePosition = mousePosition;
    }

    private void UpdateAffectedObjectRotation(Transform affectedObject,Vector3 mousePosition)
    {
        Vector3 rotationAxis = Vector3.zero;
        bool isInverted = m_previousMousePosition.x > mousePosition.x;
        switch (m_rotationGizmoType)
        {
            case ERotationGizmoType.NONE:
                return;
            case ERotationGizmoType.FORWARD_AXIS:
                rotationAxis.z = 1;
                break;
            case ERotationGizmoType.RIGHT_AXIS:
                rotationAxis.x = 1;
                break;
            case ERotationGizmoType.UP_AXIS:
                rotationAxis.y = 1;
                break;
            default:
                return;
        }

        float newRotationMultiplier = m_speedMultiplied * ((isInverted) ? -1.0f : 1.0f) * Time.deltaTime;
        
        affectedObject.Rotate(rotationAxis * newRotationMultiplier);
    }
}
