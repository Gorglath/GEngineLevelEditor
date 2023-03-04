using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EScaleGizmoType
{
    NONE,
    FORWARD_AXIS,
    RIGHT_AXIS,
    UP_AXIS,
    ALL_AXIS
}
public class ScaleGizmos : Gizmo
{
    [Header("Parameters")]
    [SerializeField] private EScaleGizmoType m_scaleGizmoType = EScaleGizmoType.NONE;

    private void Start()
    {
        m_speedMultiplied = m_speed;
    }
    public override void MultiplySpeed(float value)
    {
        m_speedMultiplied = m_speed * value;
    }

    public override void UseGizmo(List<Transform> affectedObject, Vector3 mousePosition,bool isLocal)
    {
        if (mousePosition.magnitude == 0)
            return;

        if (m_previousMousePosition == mousePosition)
            return;


        UpdateAffectedObjectScale(affectedObject, mousePosition);
        m_previousMousePosition = mousePosition;
    }

    private void UpdateAffectedObjectScale(List<Transform> affectedObject, Vector3 mousePosition)
    {
        Vector3 scaleAxis = Vector3.zero;
        bool isInverted = (m_previousMousePosition.x > mousePosition.x);
        switch (m_scaleGizmoType)
        {
            case EScaleGizmoType.NONE:
                break;
            case EScaleGizmoType.FORWARD_AXIS:
                scaleAxis.z = 1;
                break;
            case EScaleGizmoType.RIGHT_AXIS:
                scaleAxis.x = 1;
                break;
            case EScaleGizmoType.UP_AXIS:
                scaleAxis.y = 1;
                break;
            case EScaleGizmoType.ALL_AXIS:
                scaleAxis = Vector3.one;
                break;
            default:
                break;
        }

        float scaleMultiplier = m_speedMultiplied * Time.deltaTime * (isInverted? -1:1);
        foreach (Transform affectedObjectTransform in affectedObject)
        {
            affectedObjectTransform.localScale += scaleAxis * scaleMultiplier;
        }
    }
}
