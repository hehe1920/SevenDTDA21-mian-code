namespace SevenDTDMono
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class Objects : MonoBehaviour
    {
        public static GameManager _gameManager = Object.FindObjectOfType<GameManager>();
        public static List<BuffClass> _listBuffClass = new List<BuffClass>();
        public static List<BuffClass> _listCbuffs = new List<BuffClass>();
        public static List<EntityEnemy> _listEntityEnemy = new List<EntityEnemy>();
        public static List<EntityItem> _listEntityItem = new List<EntityItem>();
        public static List<EntityPlayer> _listEntityPlayer = new List<EntityPlayer>();
        public static List<ProgressionClass> _listProgressionClass = new List<ProgressionClass>();
        public static List<ProgressionValue> _listProgressionValue = new List<ProgressionValue>();
        public static List<EntityZombie> _listZombies = new List<EntityZombie>();
        public static MinEffectController _minEC = new MinEffectController();
        public static MinEffectController _minEffectController = new MinEffectController();
        public static XUiM_PlayerInventory _playerinv;
        public static WorldBase _worldbase;
        private float Cachestart;
        public static BuffClass CheatBuff;
        public static EntityPlayerLocal ELP;
        public static EntityTrader Etrader;
        private float fixedUpdateCount;
        private float lastCacheItems;
        private float lastCachePlayer;
        private float lastCacheZombies;
        public static List<TileEntityLootContainer> TELC1 = new List<TileEntityLootContainer>();
        public static TileEntity tileEntity;
        private float updateCount;
        private float updateFixedUpdateCountPerSecond;
        private float updateUpdateCountPerSecond;

        private void Awake()
        {
            base.StartCoroutine(this.Loop());
            Debug.LogWarning("THIS IS Awake!!!!");
        }

        private static string EscapeForCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            if (!value.Contains<char>(',') && !value.Contains<char>('"'))
            {
                return value;
            }
            return ("\"" + value.Replace("\"", "\"\"") + "\"");
        }

        private void FixedUpdate()
        {
            this.fixedUpdateCount++;
            if (Setting.SB.Count > 1)
            {
                if (GameManager.Instance.World != null)
                {
                    Setting.SB["hasStartedOnce"] = true;
                    Setting.SB["IsGameStarted"] = true;
                    Setting.SB["IsGameMainMenu"] = false;
                }
                else if (GameManager.Instance.World == null)
                {
                    Setting.SB["IsGameMainMenu"] = true;
                    Setting.SB["IsGameStarted"] = false;
                    if (Setting.SB["hasStartedOnce"] && Setting.SB["IsGameMainMenu"])
                    {
                        Setting.SB["BoolReset"] = true;
                        Setting.initReset();
                        Setting.SB["hasStartedOnce"] = false;
                    }
                }
            }
        }

        private void initCheatBuff()
        {
            CheatBuff.Name = "CheatBuff";
            CheatBuff.DamageType = EnumDamageTypes.None;
            CheatBuff.Description = "This is a " + CheatBuff.Name;
            CheatBuff.DurationMax = 1E+08f;
            CheatBuff.Icon = "ui_game_symbol_agility";
            CheatBuff.ShowOnHUD = true;
            CheatBuff.Hidden = false;
            CheatBuff.IconColor = new Color(0.22f, 0.4f, 1f, 100f);
            CheatBuff.DisplayType = EnumEntityUINotificationDisplayMode.IconOnly;
            CheatBuff.LocalizedName = "CheatBuff Delux";
            CheatBuff.Description = "This is the buff that manages all modiefer values we add to the player,\n for every passive effect we adding we adding it to thsi Buffclass to avoid crashes and nullreferences \n Also to avoid not being able to edit future values easier. \n i have not yet figured out how i can make the slider modifers realtime modifers or how to avoid passiveffects stacking  ";
            CheatBuff.Effects = _minEffectController;
            BuffManager.Buffs.Add(CheatBuff.Name, CheatBuff);
            _listBuffClass.Add(CheatBuff);
            Debug.LogWarning($"Buff {CheatBuff.Name} has ben added to {_listBuffClass} ");
            Log.Out(CheatBuff.Name + " Has been initieted");
            List<MinEffectGroup> list = new List<MinEffectGroup>();
            MinEffectGroup item = new MinEffectGroup {
                OwnerTiered = true
            };
            List<PassiveEffect> list2 = new List<PassiveEffect>();
            PassiveEffect effect = new PassiveEffect {
                MatchAnyTags = true,
                Type = PassiveEffects.None,
                Modifier = PassiveEffect.ValueModifierTypes.base_add
            };
            effect.Values = new float[] { 20f };
            list2.Add(effect);
            item.PassiveEffects = list2;
            List<PassiveEffects> list1 = new List<PassiveEffects> {
                PassiveEffects.None
            };
            item.PassivesIndex = list1;
            list.Add(item);
            _minEffectController.EffectGroups = list;
            HashSet<PassiveEffects> set1 = new HashSet<PassiveEffects> {
                PassiveEffects.None
            };
            _minEffectController.PassivesIndex = set1;
            Setting.buffsDict = BuffManager.Buffs.Keys;
        }

        private static void Log_listBuffClass(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Buff Name,Damage Type,Description");
                    foreach (BuffClass class2 in _listBuffClass)
                    {
                        string str = class2.DamageSource.ToString();
                        string str2 = class2.Effects.EffectGroups[0].ToString();
                        writer.WriteLine(EscapeForCsv(class2.Name) + ",,");
                    }
                }
                Console.WriteLine("Buff classes have been logged to the file.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occurred while logging buff classes: " + exception.Message);
            }
        }

        public static void LogprogclassClassesToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("name,type,sortorder");
                    foreach (ProgressionValue value2 in _listProgressionValue)
                    {
                        string name = value2.Name;
                        string str2 = value2.ProgressionClass.Type.ToString();
                        string str3 = value2.ProgressionClass.ListSortOrder.ToString();
                        writer.WriteLine(EscapeForCsv(name) + "," + EscapeForCsv(str2) + "," + EscapeForCsv(str3));
                    }
                }
                Console.WriteLine("progressionclasses as been logged");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occurred while logging buff classes: " + exception.Message);
            }
        }

        [IteratorStateMachine(typeof(<Loop>d__30))]
        private IEnumerator Loop() => 
            new <Loop>d__30(0) { <>4__this = this };

        private void OnDestroy()
        {
            Debug.LogWarning("THIS IS ON DESTROY OBJ!!!!");
        }

        private void OnDisable()
        {
            Debug.LogWarning("THIS IS ON Disable OBJ!!!!");
        }

        private void OnDisconnectedFromServer()
        {
            Debug.LogWarning("THIS IS  OnDisconnectedFromServer!!!!");
        }

        private void OnEnable()
        {
            Debug.LogWarning("THIS IS ON Enable OBJ!!!!");
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label")) {
                fontSize = 15,
                normal = { textColor = Color.green }
            };
            GUI.Label(new Rect(10f, 50f, 200f, 50f), "Update(FPS): " + this.updateUpdateCountPerSecond.ToString(), style);
            GUI.Label(new Rect(10f, 70f, 200f, 50f), "FixedUpdate: " + this.updateFixedUpdateCountPerSecond.ToString(), style);
        }

        private void Start()
        {
            GameObject gameObject = Object.FindObjectOfType<UIPanel>().gameObject;
            this.lastCachePlayer = Time.time + 5f;
            this.lastCacheZombies = Time.time + 3f;
            this.lastCacheItems = Time.time + 4f;
            this.lastCacheItems = Time.time + 1000f;
            this.Cachestart = Time.time + 10f;
            _minEC.EffectGroups = _minEffectController.EffectGroups;
            _minEC.PassivesIndex = _minEffectController.PassivesIndex;
            Debug.LogWarning("THIS IS Start OBJ!!!!");
        }

        private void Update()
        {
            this.updateCount++;
            if (((Setting.SB.Count <= 0) || (Setting.RB.Count <= 0)) || (Setting.SB1.Count <= 0))
            {
                Setting.initBools();
            }
            else
            {
                Setting.SB["IsVarsLoaded"] = Setting.VSB.Values.All<bool>(b => b);
                if (Setting.SB["IsGameStarted"] && !Setting.SB["IsVarsLoaded"])
                {
                    try
                    {
                        if (!Setting.VSB["Cheatbuff"])
                        {
                            Debug.LogWarning($"amount of buffs before load {BuffManager.Buffs.Count}");
                            if (CheatBuff == null)
                            {
                                CheatBuff = new BuffClass("");
                                Debug.Log($"{CheatBuff} as been init as a BuffClass() ");
                            }
                            else if (CheatBuff != null)
                            {
                                Debug.LogWarning(CheatBuff.Name + " begin init");
                                this.initCheatBuff();
                                Debug.LogWarning(CheatBuff.Name + " finished init");
                                int count = BuffManager.Buffs.Count;
                                Debug.LogWarning($"amount of buffs after init {count}");
                                Setting.VSB["Cheatbuff"] = true;
                                Debug.LogWarning($"1 = {Setting.VSB["Cheatbuff"]}");
                            }
                            else
                            {
                                Log.Out(CheatBuff.Name + " Has Not been init");
                            }
                        }
                        else if (!Setting.VSB["BuffClasses"])
                        {
                            Log.Out("Reloading buffs");
                            _listBuffClass.Clear();
                            foreach (KeyValuePair<string, BuffClass> pair in BuffManager.Buffs)
                            {
                                _listBuffClass.Add(pair.Value);
                            }
                            _listBuffClass.Sort((Comparison<BuffClass>) ((buff1, buff2) => string.Compare(buff1.Name, buff2.Name)));
                            Log_listBuffClass(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listBuffClass.txt"));
                            Setting.VSB["BuffClasses"] = true;
                            Debug.LogWarning($"2 = {Setting.VSB["BuffClasses"]}");
                        }
                        else if (!Setting.VSB["Cbuff"])
                        {
                            string rss = "SevenDTDMono.Features.Buffs.Cbuffs.XML";
                            Log.Out(rss + " begin Load....");
                            _listCbuffs.Clear();
                            CBuffs.LoadCustomXml(rss);
                            _listCbuffs.Sort((Comparison<BuffClass>) ((a, b) => string.Compare(a.Name, b.Name)));
                            Debug.LogWarning(rss + " finished load!");
                            Setting.VSB["Cbuff"] = true;
                            Debug.LogWarning($"3 = {Setting.VSB["Cbuff"]}");
                        }
                        else if (!Setting.VSB["PGV"] && (ELP.Progression != null))
                        {
                            if (_listProgressionValue.Count <= 0)
                            {
                                Debug.LogWarning("List empty");
                            }
                            _listProgressionValue.Clear();
                            foreach (KeyValuePair<int, ProgressionValue> pair2 in ELP.Progression.GetDict())
                            {
                                bool flag;
                                ProgressionValue value2 = pair2.Value;
                                if (value2 == null)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    ProgressionClass progressionClass = value2.ProgressionClass;
                                    flag = ((progressionClass != null) ? progressionClass.Name : null) > null;
                                }
                                if (flag)
                                {
                                    _listProgressionValue.Add(pair2.Value);
                                }
                            }
                            _listProgressionValue.Sort((Comparison<ProgressionValue>) ((a, b) => string.Compare(a.Name, b.Name)));
                            Setting.VSB["PGV"] = true;
                            Debug.LogWarning($"4 = {Setting.VSB["PGV"]}");
                        }
                        Setting.SB["IsVarsLoaded"] = Setting.VSB.Values.All<bool>(b => b);
                        Debug.LogWarning($"AllLoaded =  {Setting.SB["IsVarsLoaded"]}");
                    }
                    catch
                    {
                    }
                }
                if (((ELP != null) && Setting.RB["reloadBuffs"]) && (_listBuffClass.Count <= 1))
                {
                    Log.Out("Reloading buffs");
                    foreach (KeyValuePair<string, BuffClass> pair3 in BuffManager.Buffs)
                    {
                        _listBuffClass.Add(pair3.Value);
                    }
                    Setting.RB["reloadBuffs"] = false;
                }
            }
            if (Time.time >= this.lastCachePlayer)
            {
                ELP = Object.FindObjectOfType<EntityPlayerLocal>();
                Etrader = Object.FindObjectOfType<EntityTrader>();
                this.lastCachePlayer = Time.time + 5f;
            }
            else if (Time.time >= this.lastCacheZombies)
            {
                _listZombies = Object.FindObjectsOfType<EntityZombie>().ToList<EntityZombie>();
                _listEntityEnemy = Object.FindObjectsOfType<EntityEnemy>().ToList<EntityEnemy>();
                this.lastCacheZombies = Time.time + 3f;
            }
            else if (Time.time >= this.lastCacheItems)
            {
                _listEntityItem = Object.FindObjectsOfType<EntityItem>().ToList<EntityItem>();
                this.lastCacheItems = Time.time + 4f;
            }
        }

        public static List<EntityPlayer> PlayerList
        {
            get
            {
                if ((GameManager.Instance != null) && (GameManager.Instance.World != null))
                {
                    return GameManager.Instance.World.GetPlayers();
                }
                return new List<EntityPlayer>();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Objects.<>c <>9 = new Objects.<>c();
            public static Func<bool, bool> <>9__28_0;
            public static Comparison<BuffClass> <>9__28_1;
            public static Comparison<ProgressionValue> <>9__28_2;
            public static Func<bool, bool> <>9__28_3;
            public static Comparison<BuffClass> <>9__28_4;

            internal bool <Update>b__28_0(bool b) => 
                b;

            internal int <Update>b__28_1(BuffClass buff1, BuffClass buff2) => 
                string.Compare(buff1.Name, buff2.Name);

            internal int <Update>b__28_2(ProgressionValue a, ProgressionValue b) => 
                string.Compare(a.Name, b.Name);

            internal bool <Update>b__28_3(bool b) => 
                b;

            internal int <Update>b__28_4(BuffClass a, BuffClass b) => 
                string.Compare(a.Name, b.Name);
        }

        [CompilerGenerated]
        private sealed class <Loop>d__30 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public Objects <>4__this;

            [DebuggerHidden]
            public <Loop>d__30(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                Objects objects = this.<>4__this;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    objects.updateUpdateCountPerSecond = objects.updateCount;
                    objects.updateFixedUpdateCountPerSecond = objects.fixedUpdateCount;
                    objects.updateCount = 0f;
                    objects.fixedUpdateCount = 0f;
                }
                else
                {
                    this.<>1__state = -1;
                }
                this.<>2__current = new WaitForSeconds(1f);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

