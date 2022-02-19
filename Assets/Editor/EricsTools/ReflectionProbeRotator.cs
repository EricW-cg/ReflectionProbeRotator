using UnityEngine;
using UnityEditor;


namespace EricsTools
{
    /// <summary>
    /// Only works for angles 90, -90, 180
    /// </summary>
    public class ReflectionProbeRotator : EditorWindow
    {
        private static GUIStyle labelStyle;



        [MenuItem("Tools/Erics Tools/Reflection Probe Rotator")]
        public static void ShowWindow()
        {
            EditorWindow ProbeRotatorWindow = EditorWindow.GetWindow<ReflectionProbeRotator>("Reflection Probe Rotator");

            // Unity seems to eat the ui sometimes if a min size isn't set, 
            // then the window won't come back until Unity is restarted
            // this forces the minimum size
            ProbeRotatorWindow.minSize = new Vector2(270f, 300f);
            
            labelStyle = EditorStyles.wordWrappedLabel;
            labelStyle.fontStyle = FontStyle.Bold;
        }


        private void OnGUI()
        {
            GUILayout.Space(20f);
            
            GUILayout.Label("Reflection Probe Rotator rotates probe bounds at 90 or 180 degree increments.", labelStyle);
            GUILayout.Space(10f);
            GUILayout.Label("Multi-select all the probes you want to rotate then hit one of the buttons.", labelStyle);
            GUILayout.Space(10f);

            GUILayout.Label("Rotate selected probe bounds around X", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Rotate 90")) { RotateProbes(RotateBy.XPlus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate -90")) { RotateProbes(RotateBy.XMinus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate 180")) { RotateProbes(RotateBy.X180); }
            GUILayout.EndHorizontal();

            GUILayout.Space(20f);

            GUILayout.Label("Rotate selected probe bounds around Y", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Rotate 90")) { RotateProbes(RotateBy.YPlus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate -90")) { RotateProbes(RotateBy.YMinus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate 180")) { RotateProbes(RotateBy.Y180); }
            GUILayout.EndHorizontal();

            GUILayout.Space(20f);

            GUILayout.Label("Rotate selected probe bounds around Z", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Rotate 90")) { RotateProbes(RotateBy.ZPlus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate -90")) { RotateProbes(RotateBy.ZMinus90); }

            GUILayout.Space(10f);
            if (GUILayout.Button("Rotate 180")) { RotateProbes(RotateBy.Z180); }
            GUILayout.EndHorizontal();

            EditorWindow.GetWindow<SceneView>().Repaint();
        }

        private enum RotateBy
        {
            XPlus90,
            XMinus90,
            X180,
            YPlus90,
            YMinus90,
            Y180,
            ZPlus90,
            ZMinus90,
            Z180
        }

        private void RotateProbes(RotateBy rotateBy)
        {
            GameObject[] selections = Selection.gameObjects;
            for (int i = 0; i < Selection.count; i++)
            {
                var reflectionProbe = selections[i]?.GetComponent<ReflectionProbe>();
                if (reflectionProbe == null)
                {
                    Debug.Log($"{selections[i].name} doesn't contain a reflection probe");
                    continue;
                }


                switch (rotateBy)
                {
                    // rotate around x
                    case (RotateBy.XPlus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.x, reflectionProbe.size.z, reflectionProbe.size.y);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.x, reflectionProbe.center.z * -1f, reflectionProbe.center.y);
                        break;
                    case (RotateBy.XMinus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.x, reflectionProbe.size.z, reflectionProbe.size.y);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.x, reflectionProbe.center.z, reflectionProbe.center.y * -1f);
                        break;
                    case (RotateBy.X180):
                        reflectionProbe.center = new Vector3(reflectionProbe.center.x, reflectionProbe.center.y * -1f, reflectionProbe.center.z * -1f);
                        break;

                    // rotate around y
                    case (RotateBy.YPlus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.z, reflectionProbe.size.y, reflectionProbe.size.x);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.z, reflectionProbe.center.y, reflectionProbe.center.x * -1f);
                        break;
                    case (RotateBy.YMinus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.z, reflectionProbe.size.y, reflectionProbe.size.x);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.z * -1f, reflectionProbe.center.y, reflectionProbe.center.x);
                        break;
                    case (RotateBy.Y180):
                        reflectionProbe.center = new Vector3(reflectionProbe.center.x * -1f, reflectionProbe.center.y, reflectionProbe.center.z * -1f);
                        break;

                    // rotate around z
                    case (RotateBy.ZPlus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.y, reflectionProbe.size.x, reflectionProbe.size.z);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.y * -1f, reflectionProbe.center.x, reflectionProbe.center.z);
                        break;
                    case (RotateBy.ZMinus90):
                        reflectionProbe.size = new Vector3(reflectionProbe.size.y, reflectionProbe.size.x, reflectionProbe.size.z);
                        reflectionProbe.center = new Vector3(reflectionProbe.center.y, reflectionProbe.center.x * -1f, reflectionProbe.center.z);
                        break;
                    case (RotateBy.Z180):
                        reflectionProbe.center = new Vector3(reflectionProbe.center.x * -1f, reflectionProbe.center.y * -1f, reflectionProbe.center.z);
                        break;
                }
            }
        }
    }
}