namespace SevenDTDMono
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SceneDebugger : MonoBehaviour
    {
        private ConcurrentDictionary<object, bool> ExpandedObjects = new ConcurrentDictionary<object, bool>();
        private List<string> ExpandedObjs = new List<string>();
        private Vector2 HierarchyScrollPos;
        private int HierarchyWidth = 400;
        private Rect HierarchyWindow;
        private int HierarchyWindowId;
        private int InspectorWidth = 350;
        private Rect InspectorWindow;
        private int Margin = 50;
        private Vector2 ProjectScrollPos;
        private int ProjectWidth = 400;
        private Rect ProjectWindow;
        private Vector2 PropertiesScrollPos;
        private string SearchText = "";
        private Transform SelectedGameObject;

        private void DisplayGameObject(GameObject gameObj, int level)
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space((float) (level * 20));
            Color color = GUI.color;
            if (this.SelectedGameObject == gameObj.transform)
            {
                GUI.color = Color.green;
            }
            if (!gameObj.activeSelf && (gameObj.transform.childCount == 0))
            {
                GUI.color = Color.magenta;
            }
            else if (gameObj.transform.childCount == 0)
            {
                GUI.color = Color.yellow;
            }
            else if (!gameObj.activeSelf)
            {
                GUI.color = Color.red;
            }
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
            if (GUILayout.Toggle(this.ExpandedObjs.Contains(gameObj.name), gameObj.name, options))
            {
                if (!this.ExpandedObjs.Contains(gameObj.name))
                {
                    this.ExpandedObjs.Add(gameObj.name);
                    this.SelectedGameObject = gameObj.transform;
                }
            }
            else if (this.ExpandedObjs.Contains(gameObj.name))
            {
                this.ExpandedObjs.Remove(gameObj.name);
                this.SelectedGameObject = gameObj.transform;
            }
            GUI.color = color;
            GUILayout.EndHorizontal();
            if (this.ExpandedObjs.Contains(gameObj.name))
            {
                for (int i = 0; i < gameObj.transform.childCount; i++)
                {
                    this.DisplayGameObject(gameObj.transform.GetChild(i).gameObject, level + 1);
                }
            }
        }

        private void HierarchyWindowMethod(int id)
        {
            GUILayout.BeginVertical(GUIContent.none, GUI.skin.box, new GUILayoutOption[0]);
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.ExpandWidth(true) };
            this.SearchText = GUILayout.TextField(this.SearchText, options);
            GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
            GUILayout.Button("搜索", optionArray2);
            GUILayout.EndHorizontal();
            List<GameObject> source = new List<GameObject>();
            foreach (Transform transform in Object.FindObjectsOfType<Transform>())
            {
                if (transform.parent == null)
                {
                    source.Add(transform.gameObject);
                }
            }
            if (this.SelectedGameObject == null)
            {
                this.SelectedGameObject = source.First<GameObject>().transform;
            }
            GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.Height(this.HierarchyWindow.height / 3f), GUILayout.ExpandWidth(true) };
            this.HierarchyScrollPos = GUILayout.BeginScrollView(this.HierarchyScrollPos, optionArray3);
            foreach (GameObject obj2 in source)
            {
                this.DisplayGameObject(obj2, 0);
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUIContent.none, GUI.skin.box, new GUILayoutOption[0]);
            this.PropertiesScrollPos = GUILayout.BeginScrollView(this.PropertiesScrollPos, new GUILayoutOption[0]);
            string name = this.SelectedGameObject.name;
            for (Transform transform2 = this.SelectedGameObject.parent; transform2 != null; transform2 = transform2.parent)
            {
                name = transform2.name + "/" + name;
            }
            GUILayout.Label(name, new GUILayoutOption[0]);
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Label(this.SelectedGameObject.gameObject.layer.ToString() + " : " + LayerMask.LayerToName(this.SelectedGameObject.gameObject.layer), new GUILayoutOption[0]);
            GUILayout.FlexibleSpace();
            GUILayoutOption[] optionArray4 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
            this.SelectedGameObject.gameObject.SetActive(GUILayout.Toggle(this.SelectedGameObject.gameObject.activeSelf, "活跃", optionArray4));
            if (GUILayout.Button("?", new GUILayoutOption[0]))
            {
                Console.WriteLine("?");
            }
            if (GUILayout.Button("X", new GUILayoutOption[0]))
            {
                Object.Destroy(this.SelectedGameObject.gameObject);
            }
            GUILayout.EndHorizontal();
            foreach (Component component in this.SelectedGameObject.GetComponents<Component>())
            {
                GUILayout.BeginHorizontal(GUIContent.none, GUI.skin.box, new GUILayoutOption[0]);
                if (component is Behaviour)
                {
                    GUILayoutOption[] optionArray5 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
                    (component as Behaviour).enabled = GUILayout.Toggle((component as Behaviour).enabled, "", optionArray5);
                }
                GUILayout.Label(component.GetType().Name + " : " + component.GetType().Namespace, new GUILayoutOption[0]);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("?", new GUILayoutOption[0]))
                {
                    Console.WriteLine("?");
                }
                if (!(component is Transform) && GUILayout.Button("X", new GUILayoutOption[0]))
                {
                    Object.Destroy(component);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        public void OnEnable()
        {
            this.HierarchyWindowId = this.GetHashCode();
            this.HierarchyWindow = new Rect((float) ((Screen.width - this.HierarchyWidth) - this.Margin), (float) this.Margin, (float) this.HierarchyWidth, (float) (Screen.height - (this.Margin * 2)));
            this.ProjectWindow = new Rect((this.HierarchyWindow.x - this.Margin) - this.ProjectWidth, (float) this.Margin, (float) this.ProjectWidth, (float) (Screen.height - (this.Margin * 2)));
            this.InspectorWindow = new Rect((this.ProjectWindow.x - this.Margin) - this.InspectorWidth, (float) this.Margin, (float) this.InspectorWidth, (float) (Screen.height - (this.Margin * 2)));
        }

        public void OnGUI()
        {
            if (Settings.drawDebug)
            {
                this.HierarchyWindow = GUILayout.Window(this.HierarchyWindowId, this.HierarchyWindow, new GUI.WindowFunction(this.HierarchyWindowMethod), "层次结构", new GUILayoutOption[0]);
                this.ProjectWindow = GUILayout.Window(this.ProjectWindowId, this.ProjectWindow, new GUI.WindowFunction(this.ProjectWindowMethod), "项目", new GUILayoutOption[0]);
            }
        }

        private void ProjectWindowMethod(int id)
        {
            GUILayout.BeginVertical(GUIContent.none, GUI.skin.box, new GUILayoutOption[0]);
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Height(this.ProjectWindow.height / 3f), GUILayout.ExpandWidth(true) };
            this.ProjectScrollPos = GUILayout.BeginScrollView(this.ProjectScrollPos, options);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
                this.ExpandedObjects[assembly] = GUILayout.Toggle(this.ExpandedObjects.ContainsKey(assembly) ? this.ExpandedObjects[assembly] : false, assembly.GetName().Name, optionArray2);
                if (this.ExpandedObjects[assembly])
                {
                    foreach (Type type in (from t in assembly.GetTypes()
                        where (t.IsClass && !t.IsAbstract) && !t.ContainsGenericParameters
                        select t).ToList<Type>())
                    {
                        if (type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Count<FieldInfo>(f => (f.Name != "OffsetOfInstanceIDInCPlusPlusObject")) != 0)
                        {
                            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                            Color color = GUI.color;
                            GUILayout.Space(20f);
                            GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
                            this.ExpandedObjects[type] = GUILayout.Toggle(this.ExpandedObjects.ContainsKey(type) ? this.ExpandedObjects[type] : false, type.Name, optionArray3);
                            GUI.color = color;
                            GUILayout.EndHorizontal();
                            if (this.ExpandedObjects[type])
                            {
                                foreach (FieldInfo info in type.GetFields(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
                                {
                                    if (info.Name != "OffsetOfInstanceIDInCPlusPlusObject")
                                    {
                                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                                        GUILayout.Space(40f);
                                        GUILayoutOption[] optionArray4 = new GUILayoutOption[] { GUILayout.ExpandWidth(false) };
                                        this.ExpandedObjects[info] = GUILayout.Toggle(this.ExpandedObjects.ContainsKey(info) ? this.ExpandedObjects[info] : false, info.Name + " : " + info.FieldType?.ToString(), GUI.skin.label, optionArray4);
                                        GUILayout.EndHorizontal();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void Start()
        {
        }

        private int InspectorWindowId =>
            (this.ProjectWindowId + 1);

        private int ProjectWindowId =>
            (this.HierarchyWindowId + 1);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SceneDebugger.<>c <>9 = new SceneDebugger.<>c();
            public static Func<Type, bool> <>9__24_0;
            public static Func<FieldInfo, bool> <>9__24_1;

            internal bool <ProjectWindowMethod>b__24_0(Type t) => 
                ((t.IsClass && !t.IsAbstract) && !t.ContainsGenericParameters);

            internal bool <ProjectWindowMethod>b__24_1(FieldInfo f) => 
                (f.Name != "OffsetOfInstanceIDInCPlusPlusObject");
        }
    }
}

