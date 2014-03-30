using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObstacleContoller))]
public class ObstacleBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Obstacle Builder: choose obstacle type... (all pending further updates)");

		ObstacleContoller obstacle = (ObstacleContoller)target;

		obstacle.obstacleSelectionIndex = GUILayout.Toolbar( obstacle.obstacleSelectionIndex, obstacle.obstacleTypeNames);
        GUILayout.Space(10f);

		if ( obstacle.obstacleSelectionIndex == 0)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.MovingPattern;

            GUILayout.Label("MovingPattern obstacle will move in the selected direction first,\nthen move the opposite direction (repeating)");
            GUILayout.Space(10f);

            obstacle.direction = (ObstacleContoller.Direction)EditorGUILayout.EnumPopup("Direction", obstacle.direction);
            obstacle.TraverseDistance = EditorGUILayout.FloatField("Traverse Distance", obstacle.TraverseDistance);
            obstacle.speed = EditorGUILayout.FloatField("Speed", obstacle.speed);
        }
		else if ( obstacle.obstacleSelectionIndex == 1)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.SuddenMovement;

            GUILayout.Label("SuddenMovement obstacle will hold its position until the player\nenters its trigger box to 'drop' in a direction direction (not repeating)");
            GUILayout.Space(10f);

            obstacle.direction = (ObstacleContoller.Direction)EditorGUILayout.EnumPopup("Direction to 'drop' towards", obstacle.direction);
            obstacle.TraverseDistance = EditorGUILayout.FloatField("Drop Distance", obstacle.TraverseDistance);
            obstacle.speed = EditorGUILayout.FloatField("Speed", obstacle.speed);
        }
		else if ( obstacle.obstacleSelectionIndex == 2)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.RotatingPattern;

			obstacle.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", obstacle.rotationSpeed);
			obstacle.useRotationOffset = EditorGUILayout.Toggle("Use Offset", obstacle.useRotationOffset);
			obstacle.rotationOffset = EditorGUILayout.FloatField("Rotation Offset", obstacle.rotationOffset);
        }
    }
}
