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
    
    public override void UseGizmo(List<Transform> affectedObject, Vector3 mousePosition,bool isLocalSpace)
    {
        if (mousePosition.magnitude == 0)
            return;

        if (mousePosition == m_previousMousePosition)
            return;

        UpdateAffectedObjectLocation(affectedObject, mousePosition,isLocalSpace);
        m_previousMousePosition = mousePosition;
    }

    private void UpdateAffectedObjectLocation(List<Transform> affectedObject,Vector3 mousePosition,bool isLocalSpace)
    {
        Vector3 direction = (mousePosition - m_previousMousePosition).normalized;
        switch (m_locationGizmoType)
        {
            case ELocationGizmoType.NONE:
                return;
            case ELocationGizmoType.UP:
                MoveUp(affectedObject, direction,isLocalSpace);
                break;
            case ELocationGizmoType.FORWARD:
                MoveForward(affectedObject, direction, isLocalSpace);
                break;
            case ELocationGizmoType.RIGHT:
                MoveRight(affectedObject, direction, isLocalSpace);
                break;
            case ELocationGizmoType.UP_RIGHT:
                MoveUpRight(affectedObject, direction, isLocalSpace);
                break;
            case ELocationGizmoType.UP_FORWARD:
                MoveUpForward(affectedObject,direction, isLocalSpace);
                break;
            default:
                return;
        }
    }

    private void MoveUpRight(List<Transform> affectedObject, Vector3 dir, bool isLocalSpace)
    {
        Vector3 direction;
        if (!isLocalSpace)
            direction = new Vector3(dir.x, dir.y, 0.0f);
        else
            direction = (affectedObject[affectedObject.Count - 1].up * dir.y
                + affectedObject[affectedObject.Count - 1].right * dir.x).normalized;
        MoveAffectedObject(affectedObject, direction);
    }

    private void MoveUpForward(List<Transform> affectedObject, Vector3 dir, bool isLocalSpace)
    {
        Vector3 direction;

        if (!isLocalSpace)
            direction = new Vector3(0.0f, dir.y, dir.x);
        else
            direction = (affectedObject[affectedObject.Count - 1].up * dir.y
                + affectedObject[affectedObject.Count - 1].forward * dir.x).normalized;

        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveUp(List<Transform> affectedObject,Vector3 dir, bool isLocalSpace)
    {
        Vector3 direction;
        if (!isLocalSpace)
            direction = new Vector3(0.0f, dir.y, 0.0f);
        else
            direction = affectedObject[affectedObject.Count - 1].up * dir.y;
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveForward(List<Transform> affectedObject, Vector3 dir, bool isLocalSpace)
    {
        Vector3 direction;
        if (!isLocalSpace)
            direction = new Vector3(0.0f, 0.0f, dir.x);
        else
            direction = affectedObject[affectedObject.Count - 1].forward * dir.y;
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveRight(List<Transform> affectedObject, Vector3 dir, bool isLocalSpace)
    {
        Vector3 direction;

        if (!isLocalSpace)
            direction = new Vector3(dir.x, 0.0f, 0.0f);
        else
            direction = affectedObject[affectedObject.Count - 1].right * dir.y;
        
        MoveAffectedObject(affectedObject, direction);
    }
    private void MoveAffectedObject(List<Transform> affectedObject,Vector3 direction)
    {
        foreach (Transform affectedObjectTransform in affectedObject)
        {
            affectedObjectTransform.position += direction * Time.deltaTime * m_speedMultiplied;
        }
    }

    public override void MultiplySpeed(float value)
    {
        m_speedMultiplied = m_speed * value;
    }
}
