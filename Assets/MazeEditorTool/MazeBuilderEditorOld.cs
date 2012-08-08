/*using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MazeBuilder))]
[CanEditMultipleObjects]
public class MazeBuilderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MazeBuilder mb = target as MazeBuilder;

        GUILayout.Label("\nCustom Controls\n\n");
        mb.percentChanceDeployable = EditorGUILayout.Slider("Chance of deployable", mb.percentChanceDeployable, 0.0f, 100f);
        mb.percentChanceBonanza = EditorGUILayout.Slider("Chance of Bonanza room", mb.percentChanceBonanza, 0.0f, 100f);
        mb.percentChanceReplaceCashWithMine = EditorGUILayout.Slider("Chance of mine instead of cash", mb.percentChanceReplaceCashWithMine, 0.0f, 100f);
        mb.percentChanceReplaceSoloCashWithBigBucks = EditorGUILayout.Slider("Chance of Big Bucks instead of cash", mb.percentChanceReplaceSoloCashWithBigBucks, 0.0f, 100f);
        GUILayout.Space(20f);
        GUILayout.Label("Preload Text");
        mb.preload = EditorGUILayout.TextArea(mb.preload, GUILayout.Height(150f));
    }

}*/
