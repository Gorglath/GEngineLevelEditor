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
        Vector3 direction = (mousePosition - m_previousMousePosition).normalized;
        switch (m_locationGizmoType)
        {
            case ELocationGizmoType.NONE:
                return;
            case ELocationGizmoType.UP:
                MoveUp(affectedObject, direction);
                break;
            case ELocationGizmoType.FORWARD:
                MoveForward(affectedObject, direction);
                break;
            case ELocationGizmoType.RIGHT:
                MoveRight(affectedObject, direction);
                break;
            case ELocationGizmoType.UP_RIGHT:
                MoveUpRight(affectedObject, direction);
                break;
            case ELocationGizmoType.UP_FORWARD:
                MoveUpForward(affectedObject,direction);
                break;
            default:
                return;
        }
    }

    private void MoveUpRight(Transform affectedObject, Vector3 dir)
    {
        Vector3 direction = new Vector3(dir.x, dir.y, 0.0f);

        MoveAffectedObject(affectedObject, direction);
    }

    private void MoveUpForward(Transform affectedObject, Vector3 dir)
    {
        Vector3 direction = new Vector3(0.0f, dir.y, dir.x);

        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveUp(Transform affectedObject,Vector3 dir)
    {
        Vector3 direction = new Vector3(0.0f, dir.y, 0.0f);
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveForward(Transform affectedObject, Vector3 dir)
    {
        Vector3 direction = new Vector3(0.0f, 0.0f, dir.x);
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveRight(Transform affectedObject, Vector3 dir)
    {
        Vector3 direction = new Vector3(dir.x, 0.0f, 0.0f);
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveAffectedObject(Transform affectedObject,Vector3 direction)
    {
        affectedObject.position += direction * Time.deltaTime * m_speedMultiplied;
    }

    public override void MultiplySpeed(float value)
    {
        m_speedMultiplied = m_speed * value;
    }
}
