using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObstacleContoller))]
public class ObstacleBuilderEditor : Editor
{
    private string[] obstacleTypeNames = { "MovingPattern", "SuddenMovement", "RotatingPattern" };
    private int obstacleSelectionIndex = 0;

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Obstacle Builder: choose obstacle type... (all pending further updates)");

        obstacleSelectionIndex = GUILayout.Toolbar(obstacleSelectionIndex, obstacleTypeNames);
        GUILayout.Space(10f);
        ObstacleContoller obstacle = (ObstacleContoller)target;

        if (obstacleSelectionIndex == 0)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.MovingPattern;

            GUILayout.Label("MovingPattern obstacle will move in the selected direction first,\nthen move the opposite direction (repeating)");
            GUILayout.Space(10f);

            obstacle.direction = (ObstacleContoller.Direction)EditorGUILayout.EnumPopup("Direction", obstacle.direction);
            obstacle.TraverseDistance = EditorGUILayout.FloatField("Traverse Distance", obstacle.TraverseDistance);
            obstacle.speed = EditorGUILayout.FloatField("Speed", obstacle.speed);
        }
        else if (obstacleSelectionIndex == 1)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.SuddenMovement;

            GUILayout.Label("Not yet implemented");
            GUILayout.Label("SuddenMovement obstacle will hold its position until the player\nenters its trigger box to move in a direction (not repeating)");
            GUILayout.Space(10f);
        }
        else if (obstacleSelectionIndex == 2)
        {
            obstacle.obstacleType = ObstacleContoller.ObstacleType.RotatingPattern;

            GUILayout.Label("Not yet implemented");
            GUILayout.Space(10f);
        }
    }
}
