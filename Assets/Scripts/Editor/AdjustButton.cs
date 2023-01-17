using UnityEditor;
using UnityEngine;
using Stage;

namespace MyCustomEditor
{
    [CustomEditor(typeof(GridObjectBase), editorForChildClasses: true)]
    public class AdjustButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Up"))
            {
                var targetComponent = target as GridObjectBase;
                targetComponent?.Up();
            }

            if (GUILayout.Button("Down"))
            {
                var targetComponent = target as GridObjectBase;
                targetComponent?.Down();
            }

            if (GUILayout.Button("Right"))
            {
                var targetComponent = target as GridObjectBase;
                targetComponent?.Right();
            }

            if (GUILayout.Button("Left"))
            {
                var targetComponent = target as GridObjectBase;
                targetComponent?.Left();
            }

            if (GUILayout.Button("Adjust"))
            {
                var targetComponent = target as GridObjectBase;
                targetComponent?.Adjust();
                SetDirty();
            }
        }
    }
}