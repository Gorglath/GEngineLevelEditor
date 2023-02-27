using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gizmo : MonoBehaviour
{
    [Header("Parameters")]
    [Range(1.0f, 100.0f)] [SerializeField] protected float m_speed = 1.0f;

    abstract public void UseGizmo(List<Transform> affectedObject, Vector3 mousePosition);
    abstract public void MultiplySpeed(float value);

    //helpers
    protected Vector3 m_previousMousePosition = Vector3.zero;
    protected float m_speedMultiplied = 1.0f;

}
