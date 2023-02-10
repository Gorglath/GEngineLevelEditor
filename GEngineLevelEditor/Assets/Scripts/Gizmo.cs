using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gizmo : MonoBehaviour
{
    abstract public void UseGizmo(Transform affectedObject, Vector3 mousePosition);
    abstract public void MultiplySpeed(float value);

    //helpers
    protected Vector3 m_previousMousePosition = Vector3.zero;
}
