using System;
using System.Collections.Generic;
using Assets.InfinitySword.Scripts.Pattern;
using BTB3D.Scripts.Game.Level.Object;
using UnityEditor;
using UnityEngine;
using Serialization = Unity.VisualScripting.Serialization;

namespace BTB3D.Scripts.Game.Level
{
    public class MapToolWindow : EditorWindow
    {
        static MapToolWindow window;

        private Vector2 _scrollPosition;
        private static int _tabIndex = 0;
        private int _createGridIndex = 0;
        readonly string[] _tabSubject = { "Create", "Level" };
        private GameObject[] objects;
        private LevelObject prefab;
        private LevelAsset targetLevelAsset;

        public static void Open(MapTool tool)
        {
            if (window == null)
            {
                window = CreateInstance<MapToolWindow>();
            }
            window.objects = Resources.LoadAll<GameObject>(MapTool.instance.modelsFolderPath);
            window.Show();
        }


        public void OnGUI()
        {
            _tabIndex = GUILayout.Toolbar(_tabIndex, _tabSubject);
            switch (_tabIndex)
            {
                case 0:
                    OnGUI_Create();
                    break;
                case 1:
                    OnGUI_Level();
                    break;
            }
        }

        private void OnGUI_Create()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            {

                prefab = (LevelObject)EditorGUILayout.ObjectField("Level Object", prefab, typeof(LevelObject), true);

                if (GUILayout.Button("Create Level Object", EditorStyles.miniButton) && prefab)
                {
                    var instance = PrefabUtility.InstantiatePrefab(prefab, MapTool.instance.origin) as LevelObject;

                    instance.Initialize(objects[_createGridIndex]);

                    Selection.activeObject = instance;
                }
            }
            EditorGUILayout.EndVertical();




            EditorGUILayout.BeginVertical();
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                if (objects != null)
                {
                    List<GUIContent> contents = new List<GUIContent>();
                    foreach (var obj in objects)
                    {
                        var content = new GUIContent(obj.name, (Texture)AssetPreview.GetAssetPreview(obj)); // file name in the resources folder without the (.png) extension
                        contents.Add(content);
                    }

                    _createGridIndex = GUILayout.SelectionGrid(_createGridIndex, contents.ToArray(), 1, EditorStyles.objectFieldThumb, GUILayout.Width(300));
                }

                GUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();


            // file name in the resources folder without the (.png) extension

            //if (EditorGUILayout.DropdownButton(assetContents))
        }

        private void OnGUI_Level()
        {
            targetLevelAsset =
                (LevelAsset)EditorGUILayout.ObjectField("Level Object", targetLevelAsset, typeof(LevelAsset), true);


            if (GUILayout.Button("Save Level") && targetLevelAsset)
            {
                LevelAsset levelasset = targetLevelAsset;
                
                LevelObject[] allChildren = MapTool.instance.origin.GetComponentsInChildren<LevelObject>(true);
                List<LevelObjectAsset> assets = new List<LevelObjectAsset>();
                var objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(levelasset));

                foreach (var child in objects)
                {
                    if (child != levelasset)
                    {
                        AssetDatabase.RemoveObjectFromAsset(child);
                    }
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                levelasset.spawnPoint = MapTool.instance.spawnPoint.Serialize(levelasset);
                if (MapTool.instance.destination)
                {
                    levelasset.destination = MapTool.instance.destination.Serialize(levelasset);
                }
                

                foreach (var levelObject in allChildren)
                {
                    if (levelObject.transform.parent == MapTool.instance.origin)
                    {
                        var instance = levelObject.Serialize(levelasset);
                        instance.prefab = PrefabUtility.GetCorrespondingObjectFromSource(levelObject);

                        assets.Add(instance);
                    }
                }

                levelasset.objects = assets.ToArray();

                EditorUtility.SetDirty(levelasset);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Load Level") && targetLevelAsset)
            {
                LevelObject[] allChildren = MapTool.instance.origin.GetComponentsInChildren<LevelObject>(true);

                foreach (var child in allChildren)
                {
                    if (child != null)
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
                MapTool.instance.spawnPoint.Deserialize(targetLevelAsset.spawnPoint);
                MapTool.instance.destination.Deserialize(targetLevelAsset.destination);
                List<LevelObject> instances = new List<LevelObject>();
                foreach (var levelObjectAsset in targetLevelAsset.objects)
                {
                    var instance = PrefabUtility.InstantiatePrefab(levelObjectAsset.prefab, MapTool.instance.origin) as LevelObject;
                    instance.Deserialize(levelObjectAsset);
                    instances.Add(instance);
                }
                for(int i=0; i < instances.Count;i++)
                {
                    instances[i].PostDeserialize(targetLevelAsset.objects[i]);
                }
            }
        }
    }

    [CustomEditor(typeof(MapTool))]
    public class MapToolEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MapTool tool = (MapTool)target;
            if (GUILayout.Button("Window Open"))
            {
                MapToolWindow.Open(tool);
            }
        }

        public virtual void OnSceneGUI()
        {
            MapTool obj = (MapTool)target;
            SceneView sceneView = SceneView.lastActiveSceneView;
            var rotationF = Quaternion.LookRotation(sceneView.camera.transform.forward, sceneView.camera.transform.up);
            float size = 1.5f;
            if (obj.spawnPoint)
            {
                Handles.color = Color.green;
                if (Handles.Button(obj.spawnPoint.transform.position + Vector3.up, rotationF, size, size, Handles.RectangleHandleCap))
                {
                    Selection.activeObject = obj.spawnPoint.gameObject;
                }
            }
            if (obj.destination)
            {
                float sizeMax = Math.Max(Math.Max(obj.destination.boxCollider.size.x, obj.destination.boxCollider.size.y), obj.destination.boxCollider.size.z);
                Handles.color = new Vector4(1, 0.5f, 0, 1);
                if (Handles.Button(obj.destination.transform.position, rotationF, sizeMax, sizeMax, Handles.RectangleHandleCap))
                {
                    Selection.activeObject = obj.destination.gameObject;
                }
            }
        }
    }

    [ExecuteAlways]
    public class MapTool : Singleton<MapTool>
    {
        public string modelsFolderPath;
        public SpawnPoint spawnPoint;
        public Destination destination;
        public Transform origin;
        // Start is called before the first frame update
        // Update is called once per frame
        void OnDrawGizmos()
        {
            if (spawnPoint!=null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawMesh(GlobalLevelSetting.instance.playerMesh, spawnPoint.transform.position, Quaternion.identity, Vector3.one * 100);
                GUIStyle spawnStyle = new GUIStyle();
                spawnStyle.fontSize = 20;
                spawnStyle.alignment = TextAnchor.MiddleCenter;
                spawnStyle.normal.textColor = Color.green;
                Handles.Label(spawnPoint.transform.position + Vector3.up * 2.5f, "Spawn Point", spawnStyle);

            }

            if (destination != null)
            {
                Gizmos.color = new Vector4(1, 0.5f, 0, 0.5f);
                Gizmos.DrawCube(destination.transform.position, destination.boxCollider.size);
                GUIStyle destinationStyle = new GUIStyle();
                destinationStyle.fontSize = 20;
                destinationStyle.alignment = TextAnchor.MiddleCenter;
                destinationStyle.normal.textColor = new Vector4(1, 0.5f, 0, 1);
                Handles.Label(destination.transform.position + Vector3.up * (destination.boxCollider.size.y), "Destination", destinationStyle);

            }


        }
    }
}