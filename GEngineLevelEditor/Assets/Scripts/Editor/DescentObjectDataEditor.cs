using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DescentObjectType))]
public class DescentObjectDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DescentObjectType script = (DescentObjectType)target;

        switch (script.m_objectType)
        {
            case EDescentObjectType.NONE:
                break;
            case EDescentObjectType.TRANSFORM:
                break;
            case EDescentObjectType.ENEMY:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Enemy Type");

                // Show and save the value of b
                script.m_enemyType = (EDescentEnemyType)EditorGUILayout.EnumPopup(script.m_enemyType);

                EditorGUILayout.EndHorizontal();
                break;
            case EDescentObjectType.PLAYER:
                break;
            case EDescentObjectType.PICKUP:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Pickup Type");

                // Show and save the value of b
                script.m_pickupType = (EDescentPickupType)EditorGUILayout.EnumPopup(script.m_pickupType);

                EditorGUILayout.EndHorizontal();
                break;
            case EDescentObjectType.OBSTACLE:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Obstacle Type");

                script.m_obstacleType = (EDescentObstacleType)EditorGUILayout.EnumPopup(script.m_obstacleType);

                EditorGUILayout.EndHorizontal();
                break;
            default:
                break;
        }
    }
}
