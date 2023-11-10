namespace SevenDTDMono
{
    using System;
    using UnityEngine;
    using UnityExplorer;

    public class Loader
    {
        public static AssemblyHelper assemblyHelper;
        public static string baseName = "GameObject";
        internal static GameObject gameObject;
        public static int index = 1;
        public static string newObjectName = baseName;

        public static void InitializeUnityExplorer()
        {
            if (assemblyHelper.AreAllAssembliesLoaded() && !Settings.ASMloaded)
            {
                Settings.ASMloaded = true;
                ExplorerStandalone.CreateInstance();
            }
        }

        public static void Load()
        {
            gameObject = new GameObject();
            assemblyHelper = new AssemblyHelper();
            assemblyHelper.TryLoad();
            gameObject.AddComponent<Objects>();
            gameObject.AddComponent<NewMenu>();
            gameObject.AddComponent<Cheat>();
            gameObject.AddComponent<Settings>();
            gameObject.AddComponent<ESP>();
            gameObject.AddComponent<Visuals>();
            gameObject.AddComponent<Aimbot>();
            gameObject.AddComponent<Render>();
            gameObject.AddComponent<SceneDebugger>();
            gameObject.AddComponent<CBuffs>();
            Object.DontDestroyOnLoad(gameObject);
        }

        public static void Unload()
        {
            Object.Destroy(gameObject.GetComponent<SceneDebugger>());
            Object.Destroy(gameObject);
        }
    }
}

