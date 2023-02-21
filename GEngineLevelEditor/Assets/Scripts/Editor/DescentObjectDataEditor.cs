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
            case EDescentObjectType.WALL:
                CreateWallGUI(script);
                break;
            case EDescentObjectType.FLOOR:
                CreateFloorGUI(script);
                break;
            case EDescentObjectType.ENEMY:
                CreateEnemyGUI(script);
                break;
            case EDescentObjectType.PLAYER:
                break;
            case EDescentObjectType.PICKUP:
                CreatePickupGUI(script);
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

    private void CreateEnemyGUI(DescentObjectType objectType)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enemy Type");

        // Show and save the value of b
        objectType.m_enemyType = (EDescentEnemyType)EditorGUILayout.EnumPopup(objectType.m_enemyType);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enemy Health");

        // Show and save the value of b
        objectType.m_enemyHealth = EditorGUILayout.IntSlider(objectType.m_enemyHealth, 1,1000);

        EditorGUILayout.EndHorizontal();
    }
    private void CreateWallGUI(DescentObjectType objectType)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Wall Type");

        objectType.m_wallType = (EDescentWallType)EditorGUILayout.EnumPopup(objectType.m_wallType);

        EditorGUILayout.EndHorizontal();
    }

    private void CreateFloorGUI(DescentObjectType objectType)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Floor Type");

        objectType.m_floorType = (EDescentFloorType)EditorGUILayout.EnumPopup(objectType.m_floorType);

        EditorGUILayout.EndHorizontal();
    }
    private void CreatePickupGUI(DescentObjectType objectType)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Pickup Type");

        // Show and save the value of b
        objectType.m_pickupType = (EDescentPickupType)EditorGUILayout.EnumPopup(objectType.m_pickupType);

        EditorGUILayout.EndHorizontal();


        switch (objectType.m_pickupType)
        {
            case EDescentPickupType.NONE:
                break;
            case EDescentPickupType.SCORE:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Score Amount");

                objectType.m_scoreAmount = EditorGUILayout.IntSlider(objectType.m_scoreAmount, 1, 1000);

                EditorGUILayout.EndHorizontal();
                break;
            case EDescentPickupType.HEALTH:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Life Amount");

                objectType.m_lifeCount = EditorGUILayout.IntSlider(objectType.m_lifeCount, 1, 3);

                EditorGUILayout.EndHorizontal();
                break;
            case EDescentPickupType.AMMO:
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Ammo Amount");

                objectType.m_ammoCount = EditorGUILayout.IntSlider(objectType.m_ammoCount, 1, 120);

                EditorGUILayout.EndHorizontal();
                break;
            case EDescentPickupType.HOSTAGE:
                break;
            default:
                break;
        }
    }
}
