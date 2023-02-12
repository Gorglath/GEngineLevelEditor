using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELocationGizmoType
{
    NONE,
    UP,
    FORWARD,
    RIGHT,
    UP_RIGHT,
    UP_FORWARD
}
public class LocationGizmo : Gizmo
{
    [Header("Parameters")]
    [SerializeField] private ELocationGizmoType m_locationGizmoType = ELocationGizmoType.NONE;
    
    private void Start()
    {
        m_speedMultiplied = m_speed;
    }
    
    public override void UseGizmo(Transform affectedObject, Vector3 mousePosition)
    {
        if (mousePosition.magnitude == 0)
            return;

        if (mousePosition == m_previousMousePosition)
            return;

        UpdateAffectedObjectLocation(affectedObject, mousePosition);
        m_previousMousePosition = mousePosition;
    }

    private void UpdateAffectedObjectLocation(Transform affectedObject,Vector3 mousePosition)
    {
        switch (m_locationGizmoType)
        {
            case ELocationGizmoType.NONE:
                return;
            case ELocationGizmoType.UP:
                MoveUp(affectedObject, mousePosition);
                break;
            case ELocationGizmoType.FORWARD:
                MoveForward(affectedObject, mousePosition);
                break;
            case ELocationGizmoType.RIGHT:
                MoveRight(affectedObject, mousePosition);
                break;
            case ELocationGizmoType.UP_RIGHT:
                MoveRight(affectedObject, mousePosition);
                MoveUp(affectedObject, mousePosition);
                break;
            case ELocationGizmoType.UP_FORWARD:
                MoveUp(affectedObject, mousePosition);
                MoveForward(affectedObject, mousePosition);
                break;
            default:
                return;
        }
    }

    private void MoveUp(Transform affectedObject,Vector3 mousePosition)
    {
        bool isInverted = (m_previousMousePosition.y > mousePosition.y);
        MoveAffectedObject(affectedObject, Vector3.up, isInverted);
    }
    private void MoveForward(Transform affectedObject, Vector3 mousePosition)
    {
        bool isInverted = (m_previousMousePosition.x > mousePosition.x);
        MoveAffectedObject(affectedObject, Vector3.forward, isInverted);
    }
    private void MoveRight(Transform affectedObject, Vector3 mousePosition)
    {
        bool isInverted = (m_previousMousePosition.x > mousePosition.x);
        MoveAffectedObject(affectedObject, Vector3.right, isInverted);
    }
    private void MoveAffectedObject(Transform affectedObject,Vector3 direction, bool m_isInverted)
    {
        affectedObject.position += direction * ((m_isInverted) ? -1 : 1) * Time.deltaTime * m_speedMultiplied;
    }

    public override void MultiplySpeed(float value)
    {
        m_speedMultiplied = m_speed * value;
    }
}
