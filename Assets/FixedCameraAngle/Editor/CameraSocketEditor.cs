using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace KreliStudio {
    [CustomEditor(typeof(CameraSocket))]
    public class CameraSocketEditor : Editor
    {
        #region Variables
        public CameraSocket cameraSocket;
        Vector2 posScroll;
        int colliderType = 0;
        #endregion

        #region Initialize Methods
        [MenuItem("GameObject/KreliStudio/Camera/Camera Scocket", false, 10)]
        public static void CreateCameraScocket()
        {
            PrepareGizmos();    
            GameObject obj = new GameObject("Camera Socket");
            Collider col = obj.AddComponent<BoxCollider>();
            col.isTrigger = true;
            CameraSocket cameraSocket = obj.AddComponent<CameraSocket>();
            if (Selection.activeTransform)
            {
                obj.transform.SetParent(Selection.activeTransform);
                obj.transform.localPosition = Vector3.zero;
            }
            cameraSocket.AddSocket();
            cameraSocket.colliders.Add(col);
            // set position components
            cameraSocket.transform.position += Vector3.up;
            cameraSocket.cameraNode[0].Position += Vector3.up * 2f + Vector3.back;
            cameraSocket.followPath[0].Position += Vector3.forward;
            ((BoxCollider)cameraSocket.colliders[0]).center += Vector3.down * 0.5f;
            Selection.activeGameObject = obj;
        }
        private static void PrepareGizmos()
        {
            if (!Directory.Exists("Assets/Gizmos"))
                Directory.CreateDirectory("Assets/Gizmos");
            PrepareGizmo("CameraSocket");
            PrepareGizmo("CameraSocket_LookAt");
            PrepareGizmo("CameraSocket_LookAt_Move");
            PrepareGizmo("CameraSocket_Move");
            PrepareGizmo("FollowNode");
            PrepareGizmo("MotionNode");
            PrepareGizmo("Transform");
        }
        private static void PrepareGizmo(string name)
        {
            string dPath = "Assets/Gizmos/" + name + ".png";
            string sPath = "Assets/FixedCameraAngle/Images/" + name + ".png";
            if (!File.Exists(dPath))
            {
                if (File.Exists(sPath))
                {
                    Debug.Log("[Fixed Camera Angles] Gizmos " + name +" prepare successful.");
                    File.Copy(sPath, dPath);
                    AssetDatabase.Refresh();
                }else
                    Debug.LogWarning("[Fixed Camera Angles] Gizmos " + name + " not found.");
            }
        }
        #endregion

        #region Behaviour Methods
        private void OnEnable()
        {
            cameraSocket = target as CameraSocket;
        }
        private void OnSceneGUI()
        {
            EditorGUI.BeginChangeCheck();
            Vector3 transformPosition = cameraSocket.transform.position;

            if (cameraSocket.gizmosHandles)
            {
                if (cameraSocket.subMenu == 0 || (cameraSocket.subMenu == 2 && cameraSocket.socketType == SocketType.STATIC)) // Main Socket subMenu
                {
                    if (Tools.current == Tool.Move)
                    {
                        cameraSocket.cameraNode[0].Position = Handles.PositionHandle(cameraSocket.cameraNode[0].Position, Quaternion.identity);
                    }
                    else
                    if (Tools.current == Tool.Rotate)
                    {
                        cameraSocket.cameraNode[0].rotation = Handles.RotationHandle(cameraSocket.cameraNode[0].rotation, cameraSocket.cameraNode[0].Position);
                    }
                }else
                if (cameraSocket.subMenu == 1) // Colliders subMenu
                {
                    for (int i = 0; i < cameraSocket.colliders.Count; i++)
                    {
                        if (cameraSocket.colliders[i])
                        {
                            Handles.Label(cameraSocket.colliders[i].bounds.center + Vector3.up, new GUIContent("Collider " + i), EditorStyles.whiteMiniLabel);

                            if (Tools.current == Tool.Move)
                            {
                                if (cameraSocket.colliders[i].GetType() == typeof(SphereCollider))
                                    ((SphereCollider)cameraSocket.colliders[i]).center = Handles.PositionHandle(((SphereCollider)cameraSocket.colliders[i]).center + transformPosition, Quaternion.identity) - transformPosition;
                                if (cameraSocket.colliders[i].GetType() == typeof(BoxCollider))
                                    ((BoxCollider)cameraSocket.colliders[i]).center = Handles.PositionHandle(((BoxCollider)cameraSocket.colliders[i]).center + transformPosition, Quaternion.identity) - transformPosition;

                            }
                            else if (Tools.current == Tool.Scale)
                            {
                                if (cameraSocket.colliders[i].GetType() == typeof(SphereCollider))
                                    ((SphereCollider)cameraSocket.colliders[i]).radius = Handles.RadiusHandle(Quaternion.identity, cameraSocket.colliders[i].bounds.center, ((SphereCollider)cameraSocket.colliders[i]).radius);
                                if (cameraSocket.colliders[i].GetType() == typeof(BoxCollider))
                                    ((BoxCollider)cameraSocket.colliders[i]).size = Handles.ScaleHandle(((BoxCollider)cameraSocket.colliders[i]).size, cameraSocket.colliders[i].bounds.center, Quaternion.identity, HandleUtility.GetHandleSize(cameraSocket.colliders[i].bounds.center));


                            }
                        }
                    }
                }
                else if (cameraSocket.subMenu == 2) // Path subMenu
                {
                    if (cameraSocket.socketType == SocketType.MOTION)
                    {
                        for (int i = 0; i < cameraSocket.cameraNode.Count; i++)
                        {
                            if (Tools.current == Tool.Move)
                            {
                                cameraSocket.cameraNode[i].Position = Handles.PositionHandle(cameraSocket.cameraNode[i].Position, Quaternion.identity);
                                cameraSocket.followPath[i].Position = Handles.PositionHandle(cameraSocket.followPath[i].Position, Quaternion.identity);
                            }
                            if (Tools.current == Tool.Rotate)
                            {
                                cameraSocket.cameraNode[i].rotation = Handles.RotationHandle(cameraSocket.cameraNode[i].rotation, cameraSocket.cameraNode[i].Position);
                            }
                            if (i==0)
                                Handles.Label(cameraSocket.cameraNode[i].Position + Vector3.down * 0.5f, new GUIContent("Main Node"), EditorStyles.whiteMiniLabel);
                            else
                                Handles.Label(cameraSocket.cameraNode[i].Position + Vector3.down * 0.5f, new GUIContent("Node " + i), EditorStyles.whiteMiniLabel);

                        }
                    }
                }
            }
            if (EditorGUI.EndChangeCheck() && !Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawImage("CameraSocketLabel");

            EditorGUILayout.BeginVertical("Box");
            if (cameraSocket.cameraNode.Count == 0)
            {
                EditorGUI.HelpBox(EditorGUILayout.GetControlRect(false,50), "Please create Camera Socket via menu \"GameObject/ KreliStudio/ Camera/ Camera Socket\".", MessageType.Error);
            }
            else
            {
                cameraSocket.subMenu = GUILayout.Toolbar(cameraSocket.subMenu, new string[] { "Socket", "Colliders", "Motion", "Gizmos" });
                switch (cameraSocket.subMenu)
                {
                    default:
                    case 0:
                        // Socket, main node section
                        DrawSocketInspector();
                        break;
                    case 1:
                        // List of colliders
                        DrawCollidersInspector();
                        break;
                    case 2:
                        // Motion path Type section
                        DrawMotionInspector();
                        break;
                    case 3:
                        // Debug section
                        DrawDebugInpector();
                        break;
                }
            }
            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck() && !Application.isPlaying)
            {
                SceneView.RepaintAll();
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        #endregion

        #region Helper Methods
        private void DrawSocketInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Target");
            cameraSocket.target = (Transform)EditorGUILayout.ObjectField(new GUIContent("Target", "Target that launches the Camera Socket"), cameraSocket.target, typeof(Transform), true);
            cameraSocket.lookAtTarget = EditorGUILayout.Toggle(new GUIContent("Look At Target", "Camera follows the target"), cameraSocket.lookAtTarget);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Main Node");
            cameraSocket.cameraNode[0].Position = EditorGUILayout.Vector3Field(new GUIContent("Camera Position", "Position of the main node"), cameraSocket.cameraNode[0].Position);
            cameraSocket.cameraNode[0].rotation = Quaternion.Euler(EditorGUILayout.Vector3Field(new GUIContent("Camera Rotation", "Rotation of the main node"), cameraSocket.cameraNode[0].rotation.eulerAngles));
            if (cameraSocket.socketType == SocketType.MOTION)
            {
                cameraSocket.followPath[0].Position = EditorGUILayout.Vector3Field(new GUIContent("Follow Position", "Position of the main follow node"), cameraSocket.followPath[0].Position);
            }
            if (GUILayout.Button(new GUIContent(" Align With View", EditorGUIUtility.IconContent("ViewToolOrbit").image, "Set position and rotation of the main node to the current scene view")))
            {
                cameraSocket.cameraNode[0].Position = SceneView.lastActiveSceneView.camera.transform.position;
                cameraSocket.cameraNode[0].rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                SceneView.RepaintAll();
            }
            EditorGUILayout.EndVertical();
        }
        private void DrawCollidersInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Collider List");
            EditorGUILayout.BeginHorizontal();
            colliderType = EditorGUILayout.Popup(new GUIContent("Collider Type"), colliderType, new string[] { "Box", "Sphere"});
            EditorGUILayout.EndHorizontal();
            string icon;
            switch (colliderType)
            {
                default:
                case 0:
                    icon = "BoxCollider Icon";
                    break;
                case 1:
                    icon = "SphereCollider Icon";
                    break;
            }
            if (GUILayout.Button(new GUIContent(" New Collider", EditorGUIUtility.IconContent(icon).image),GUILayout.Height(20)))
            {
                cameraSocket.AddCollider(colliderType);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical("Box");
            posScroll = EditorGUILayout.BeginScrollView(posScroll);
            for (int i = 0; i < cameraSocket.colliders.Count; i++)
            {
                if (cameraSocket.colliders[i])
                {
                    EditorGUILayout.BeginHorizontal();
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField(new GUIContent("Collider " + i), cameraSocket.colliders[i], typeof(Collider), true);
                    GUI.enabled = true;
                    if (cameraSocket.colliders.Count > 1)
                    {
                        if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("CollabConflict").image, "Delete"), GUILayout.Width(25), GUILayout.Height(20)))
                        {
                            cameraSocket.RemoveCollider(i);
                            SceneView.RepaintAll();
                            return;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    cameraSocket.RemoveCollider(i);
                    i--;
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        private void DrawMotionInspector()
        {
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Node List");
            cameraSocket.socketType = (SocketType)EditorGUILayout.EnumPopup(new GUIContent("Socket Type", "Camera may be stationary or move along the path"), cameraSocket.socketType);
            if (cameraSocket.socketType == SocketType.MOTION)
            {
                cameraSocket.pathMode = (PathMode)EditorGUILayout.EnumPopup(new GUIContent("Path Mode", "Camera moves in straight lines or curves betwwen nodes"), cameraSocket.pathMode);

                cameraSocket.smoothPath = EditorGUILayout.Toggle(new GUIContent("Smooth Path", "Camera moves smoothly between far nodes inside path"), cameraSocket.smoothPath);
                if (cameraSocket.smoothPath)
                {
                    cameraSocket.smoothTransition = EditorGUILayout.Toggle(new GUIContent("Smooth Transition", "Camera moves smoothly to this Camera Sockets from another"), cameraSocket.smoothTransition);
                    cameraSocket.smoothness = EditorGUILayout.FloatField(new GUIContent("Smoothness"), cameraSocket.smoothness);
                    cameraSocket.smoothness = Mathf.Clamp(cameraSocket.smoothness, 0.1f, 100f);
                }
                EditorGUILayout.Space();
                if (GUILayout.Button(new GUIContent(" New Node", EditorGUIUtility.IconContent("EditCollider").image), GUILayout.Height(20)))
                {
                    if (cameraSocket.cameraNode.Count > 0)
                    {
                        cameraSocket.AddSocket();
                    }

                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical("Box");
                posScroll = EditorGUILayout.BeginScrollView(posScroll);
                for (int i = 1; i < cameraSocket.cameraNode.Count; i++)
                {
                    EditorGUILayout.BeginVertical("HelpBox");
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    cameraSocket.cameraNode[i].isFoldout = EditorGUILayout.Foldout(cameraSocket.cameraNode[i].isFoldout, new GUIContent("Node " + i), true);
                    if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("ViewToolOrbit").image, "Align With View"), GUILayout.Width(35), GUILayout.Height(20)))
                    {
                        cameraSocket.cameraNode[i].Position = SceneView.lastActiveSceneView.camera.transform.position;
                        cameraSocket.cameraNode[i].rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                        SceneView.RepaintAll();
                    }
                    if (i == 1)
                        GUI.enabled = false;
                    if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("CollabPush").image, "Move Up"), GUILayout.Width(25), GUILayout.Height(20)))
                    {
                        MoveElement(i, -1);
                        SceneView.RepaintAll();
                        return;
                    }
                    GUI.enabled = true;
                    if (i == cameraSocket.cameraNode.Count - 1)
                        GUI.enabled = false;
                    if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("CollabPull").image, "Move Down"), GUILayout.Width(25), GUILayout.Height(20)))
                    {
                        MoveElement(i, 1);
                        SceneView.RepaintAll();
                        return;
                    }
                    GUI.enabled = true;
                    if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("CollabConflict").image, "Delete"), GUILayout.Width(25), GUILayout.Height(20)))
                    {
                        cameraSocket.RemoveSocket(i);
                        SceneView.RepaintAll();
                        return;
                    }
                    EditorGUILayout.EndHorizontal();
                    if (cameraSocket.cameraNode[i].isFoldout)
                    {
                        cameraSocket.cameraNode[i].Position = EditorGUILayout.Vector3Field(new GUIContent("Camera Position", "Position of the node"), cameraSocket.cameraNode[i].Position);
                        cameraSocket.cameraNode[i].rotation = Quaternion.Euler(EditorGUILayout.Vector3Field(new GUIContent("Camera Rotation", "Rotation of the node"), cameraSocket.cameraNode[i].rotation.eulerAngles));
                        cameraSocket.followPath[i].Position = EditorGUILayout.Vector3Field(new GUIContent("Follow Position", "Position of the follow node"), cameraSocket.followPath[i].Position);
                    }
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.EndVertical();

        }
        private void DrawDebugInpector()
        {
            if (cameraSocket.cameraNode.Count > 1)
            {
                EditorGUILayout.BeginVertical("HelpBox");
                DrawTitle("Camera Position on path");
                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), cameraSocket.Ratio, "Segment " + (cameraSocket.Segment + 1) + "/" + (cameraSocket.cameraNode.Count - 1) + ", Ratio " + (cameraSocket.Ratio * 100f).ToString("0.0") + "%");
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Gizmos");
            cameraSocket.gizmosIcon = EditorGUILayout.Toggle(new GUIContent("Icons"), cameraSocket.gizmosIcon);
            cameraSocket.gizmosFrustum = EditorGUILayout.Toggle(new GUIContent("Camera Views"), cameraSocket.gizmosFrustum);
            cameraSocket.gizmosCollider = EditorGUILayout.Toggle(new GUIContent("Colliders"), cameraSocket.gizmosCollider);
            cameraSocket.gizmosMotion = EditorGUILayout.Toggle(new GUIContent("Path"), cameraSocket.gizmosMotion);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("HelpBox");
            DrawTitle("Others");
            cameraSocket.gizmosHandles = EditorGUILayout.Toggle(new GUIContent("Handles"), cameraSocket.gizmosHandles);
            cameraSocket.gizmosColliderColor = EditorGUILayout.ColorField(new GUIContent("Collider Color"), cameraSocket.gizmosColliderColor);
            cameraSocket.gizmosPathColor = EditorGUILayout.ColorField(new GUIContent("Path Color"), cameraSocket.gizmosPathColor);
            cameraSocket.gizmosCurveScale = EditorGUILayout.IntSlider(new GUIContent("Path Scale"), cameraSocket.gizmosCurveScale, 3, 20);
            EditorGUILayout.EndVertical();


        }
        private void DrawImage(string fileName)
        {
            Rect logoPosition = GUILayoutUtility.GetRect(128, 512, 32, 64);
            Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/FixedCameraAngle/Images/" + fileName + ".png");
            GUI.DrawTexture(logoPosition, logo, ScaleMode.ScaleToFit);
        }
        private void MoveElement(int i,int moveBy)
        {
            Node node = cameraSocket.cameraNode[i];
            cameraSocket.cameraNode.RemoveAt(i);
            cameraSocket.cameraNode.Insert(i+moveBy, node);

            node = cameraSocket.followPath[i];
            cameraSocket.followPath.RemoveAt(i);
            cameraSocket.followPath.Insert(i + moveBy, node);

        }
        private void DrawTitle(string _title)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(new GUIContent(_title), EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}
