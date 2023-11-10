namespace SevenDTDMono
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Settings : MonoBehaviour
    {
        public static Color _cidle = Color.red;
        public static Color _cOFF = Color.red;
        public static Color _cON = Color.green;
        public static float _FL_APM = 2f;
        public static float _FL_blokdmg = 0.5f;
        public static float _FL_dmg = 0.5f;
        public static float _FL_harvest = 2f;
        public static float _FL_jmp = 0.5f;
        public static float _FL_killdmg = 0.5f;
        public static float _FL_run = 0.5f;
        public static bool _infDurability = false;
        public static bool _instantCraft = false;
        public static bool _InstantLoot = false;
        public static bool _instantScrap = false;
        public static bool _instantSmelt = false;
        public static bool _nameScramble = false;
        public static bool _trystackitems = false;
        public static bool aimbot;
        public static bool ASMloaded = false;
        public static bool ASMPreload = false;
        public static Dictionary<string, bool> BD = new Dictionary<string, bool>();
        public static GUIStyle BgStyle;
        public static UnityEngine.UI.Text BOOLText;
        public static GUIStyle BtnStyle;
        public static GUIStyle BtnStyle1;
        public static GUIStyle BtnStyle2;
        public static GUIStyle BtnStyle3;
        public static Dictionary<string, bool> BTS = new Dictionary<string, bool>();
        public static bool chams = false;
        public static bool cm;
        public static bool CmDm;
        internal static bool crosshair = false;
        internal static bool drawDebug = false;
        public static bool drpbp;
        public static bool fovCircle = false;
        public static bool infiniteAmmo;
        public static bool IsGameStarted;
        public static bool IsGameStartMenu;
        public static bool IsVarsLoaded;
        public static GUIStyle LabelStyle;
        public static bool magicBullet;
        public static bool noWeaponBob;
        public static GUIStyle OffStyle;
        public static bool onht;
        public static GUIStyle OnStyle;
        public static bool playerBox = false;
        public static bool PlayerBox = false;
        internal static bool playerCornerBox = false;
        public static bool playerHealth = false;
        public static bool playerName = false;
        public static bool PlayerName = false;
        public static bool reloadBuffs = false;
        internal static bool selfDestruct = false;
        public static bool speed;
        public static bool StartMenuStarted = false;
        public static string Text = "Text";
        public static GUIStyle ToggStyle1;
        public static GUIStyle ToggStyle2;
        public static bool zombieBox = false;
        public static bool zombieCornerBox = false;
        public static bool zombieHealth = false;
        public static bool zombieName = false;

        public static void Styles()
        {
            if (BgStyle == null)
            {
                BgStyle = new GUIStyle();
                BgStyle.normal.textColor = Color.white;
                BgStyle.onNormal.textColor = Color.white;
                BgStyle.active.textColor = Color.white;
                BgStyle.onActive.textColor = Color.white;
            }
            if (LabelStyle == null)
            {
                LabelStyle = new GUIStyle();
                LabelStyle.normal.textColor = Color.white;
                LabelStyle.onNormal.textColor = Color.white;
                LabelStyle.active.textColor = Color.white;
                LabelStyle.onActive.textColor = Color.white;
            }
            if (OffStyle == null)
            {
                OffStyle = new GUIStyle();
                OffStyle.normal.textColor = Color.white;
                OffStyle.onNormal.textColor = Color.white;
                OffStyle.active.textColor = Color.white;
                OffStyle.onActive.textColor = Color.white;
            }
            if (OnStyle == null)
            {
                OnStyle = new GUIStyle();
                OnStyle.normal.textColor = Color.white;
                OnStyle.onNormal.textColor = Color.white;
                OnStyle.active.textColor = Color.white;
                OnStyle.onActive.textColor = Color.white;
            }
            if (BtnStyle == null)
            {
                BtnStyle = new GUIStyle();
                BtnStyle.normal.textColor = Color.white;
                BtnStyle.onNormal.textColor = Color.white;
                BtnStyle.active.textColor = Color.white;
                BtnStyle.onActive.textColor = Color.white;
            }
            if (BtnStyle1 == null)
            {
                BtnStyle1 = new GUIStyle();
                BtnStyle1.normal.textColor = _cOFF;
                BtnStyle1.onNormal.textColor = _cOFF;
                BtnStyle1.active.textColor = _cON;
                BtnStyle1.onActive.textColor = _cON;
                BtnStyle1.onHover.textColor = Color.yellow;
            }
            if (ToggStyle1 == null)
            {
                ToggStyle1 = new GUIStyle();
                ToggStyle1.onHover.textColor = Color.yellow;
                ToggStyle1.alignment = TextAnchor.MiddleRight;
            }
        }
    }
}

