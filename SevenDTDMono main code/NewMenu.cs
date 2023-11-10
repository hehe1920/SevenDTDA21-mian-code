namespace SevenDTDMono
{
    using SevenDTDMono.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class NewMenu : MonoBehaviour
    {
        public int _group;
        private static string _itemcount = "2000";
        private string buffSearch = "";
        private GUIStyle centeredLabelStyle;
        private string[] CheatsString = new string[] { "玩家选项", "透视选项", "Buff选项", "其它选项" };
        public float counter;
        public UnityEngine.UI.Text counterText;
        private int currentIndex;
        private GUIStyle customBoxStyleGreen;
        private GUIStyle defBoxStyle;
        private bool drawMenu = true;
        private float floatValue;
        private bool FoldBuff;
        private bool FoldCBuff;
        private bool foldout1;
        private bool foldout2;
        private bool foldout3;
        private bool foldout4;
        private bool foldout5;
        private bool FoldPassive;
        private bool FoldPGV;
        private bool FoldPlayer;
        private bool FoldZombie;
        private string inputFloat = "2";
        private string inputPassive = string.Empty;
        private string inputPassiveEffects = string.Empty;
        private string inputValuModType = string.Empty;
        private bool lastBuffAdded;
        private bool lastzombieadded;
        private string passiveSearch = "";
        private string PGVSearch = "";
        private Vector2 scrollBuff;
        private Vector2 scrollBuffBTN = Vector2.zero;
        private Vector2 scrollCBuff;
        private Vector2 scrollKill;
        private Vector2 ScrollMenu0 = Vector2.zero;
        private Vector2 ScrollMenu1 = Vector2.zero;
        private Vector2 ScrollMenu2 = Vector2.zero;
        private Vector2 ScrollMenu3 = Vector2.zero;
        private Vector2 scrollPassive;
        private Vector2 scrollPGV;
        private Vector2 scrollPlayer;
        private Vector2 scrollZombie;
        private Vector2 test;
        public string Text;
        private ConcurrentDictionary<object, bool> ToggleBools = new ConcurrentDictionary<object, bool>();
        private int ValueModifierTypesIndex;
        private int windowID;
        private Rect windowRect;

        private Texture2D MakeTexture(int width, int height, Color color)
        {
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
            Texture2D textured = new Texture2D(width, height);
            textured.SetPixels(colors);
            textured.Apply();
            return textured;
        }

        private void Menu0()
        {
            CGUILayout.BeginHorizontal((Action) (() => CGUILayout.BeginVertical(GUI.skin.box, delegate {
                Setting.SV2["ScrollMenu0"] = GUILayout.BeginScrollView(Setting.SV2["ScrollMenu0"], Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(delegate {
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.Button("添加技能点", new Action(Cheat.skillpoints), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("自杀", new Action(Cheat.KillSelf), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("提升等级", new Action(Cheat.levelup), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("获取我的ID", new Action(Cheat.Getplayer), Array.Empty<GUILayoutOption>());
                        if (Setting.SB.Count > 1)
                        {
                            CGUILayout.RButton("被AI忽视", "_ignoreByAI", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("创造/调试模式", Setting.RB, "CmDm", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.SButton("名字争夺", "_nameScramble", null, Array.Empty<GUILayoutOption>());
                        }
                    });
                    CGUILayout.BeginVertical(delegate {
                        if (CGUILayout.Button("传送到标记点", Array.Empty<GUILayoutOption>()))
                        {
                            Objects.ELP.TeleportToPosition(new Vector3(Objects.ELP.markerPosition.ToVector3().x, Objects.ELP.markerPosition.ToVector3().y + 2f, Objects.ELP.markerPosition.ToVector3().z), false, null);
                        }
                        CGUILayout.Button("快速完成任务", Setting.RB, "_QuestComplete", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速制作", Setting.RB, "_instantCraft", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速拆解", Setting.RB, "_instantScrap", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速熔炼", Setting.RB, "_instantSmelt", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("循环获取最后任务奖励", Setting.RB, "_LOQuestRewards", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("交易开放 24/7", "_EtraderOpen", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(delegate {
                            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                            _itemcount = GUILayout.TextField(_itemcount, 10, options);
                            if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                            {
                                try
                                {
                                    int num = int.Parse(_itemcount);
                                    if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                                    {
                                        Objects.ELP.inventory.holdingItemStack.count = num;
                                        Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                                    }
                                    else
                                    {
                                        Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }, Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }, Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
            GUI.DragWindow();
        }

        private void Menu1()
        {
            CGUILayout.BeginHorizontal(GUI.skin.box, delegate {
                CGUILayout.BeginVertical(delegate {
                    Settings.crosshair = GUILayout.Toggle(Settings.crosshair, "十字准星", Array.Empty<GUILayoutOption>());
                    Settings.fovCircle = GUILayout.Toggle(Settings.fovCircle, "显示自瞄范围", Array.Empty<GUILayoutOption>());
                    Settings.playerBox = GUILayout.Toggle(Settings.playerBox, "玩家方框", Array.Empty<GUILayoutOption>());
                    Settings.playerName = GUILayout.Toggle(Settings.playerName, "玩家名称", Array.Empty<GUILayoutOption>());
                    Settings.playerCornerBox = GUILayout.Toggle(Settings.playerCornerBox, "玩家角框", Array.Empty<GUILayoutOption>());
                    Settings.chams = GUILayout.Toggle(Settings.chams, "上色", Array.Empty<GUILayoutOption>());
                    Settings.playerHealth = GUILayout.Toggle(Settings.playerHealth, "玩家血量", Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    Settings.zombieBox = GUILayout.Toggle(Settings.zombieBox, "僵尸方框", Array.Empty<GUILayoutOption>());
                    Settings.zombieName = GUILayout.Toggle(Settings.zombieName, "僵尸名称", Array.Empty<GUILayoutOption>());
                    Settings.zombieHealth = GUILayout.Toggle(Settings.zombieHealth, "僵尸血量", Array.Empty<GUILayoutOption>());
                    Settings.zombieCornerBox = GUILayout.Toggle(Settings.zombieCornerBox, "僵尸角框", Array.Empty<GUILayoutOption>());
                    Settings.noWeaponBob = GUILayout.Toggle(Settings.noWeaponBob, "武器无摇摆", Array.Empty<GUILayoutOption>());
                });
            }, Array.Empty<GUILayoutOption>());
            CGUILayout.BeginVertical(GUI.skin.box, delegate {
                GUIStyle style1 = new GUIStyle(GUI.skin.label) {
                    alignment = TextAnchor.LowerCenter,
                    fontSize = 13,
                    padding = new RectOffset(0, 0, -4, 0)
                };
                CGUILayout.BeginHorizontal((Action) (() => GUILayout.Label("编辑", Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.HorizontalScrollbarWithLabel("攻击/分钟", ref Settings._FL_APM, 500f);
                    CGUILayout.HorizontalScrollbarWithLabel("跳跃力量", ref Settings._FL_jmp, 99f);
                    CGUILayout.HorizontalScrollbarWithLabel("收获翻倍", ref Settings._FL_harvest, 100f);
                    CGUILayout.HorizontalScrollbarWithLabel("冲刺速度", ref Settings._FL_run, 100f);
                    CGUILayout.HorizontalScrollbarWithLabel("杀死DMG XM", ref Settings._FL_killdmg, 99999f);
                    CGUILayout.HorizontalScrollbarWithLabel("堵塞DMG", ref Settings._FL_blokdmg, 99999f);
                });
                CGUILayout.BeginVertical(GUI.skin.box, delegate {
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxHeight(170f) };
                    Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                    CGUILayout.BeginHorizontal(delegate {
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                        });
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                        });
                    }, Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                }, Array.Empty<GUILayoutOption>());
            }, Array.Empty<GUILayoutOption>());
            GUI.DragWindow();
        }

        private void Menu2()
        {
            CGUILayout.BeginHorizontal(this.defBoxStyle, delegate {
                Setting.SV2["ScrollMenu2"] = GUILayout.BeginScrollView(Setting.SV2["ScrollMenu2"], Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal((Action) (() => CGUILayout.BeginVertical("buffs", this.defBoxStyle, delegate {
                    GUILayout.Space(20f);
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.BeginVertical(delegate {
                            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MinHeight(80f) };
                            Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                            CGUILayout.BeginHorizontal(delegate {
                                CGUILayout.BeginVertical(delegate {
                                    CGUILayout.Button("移除所有有效的Buff", new Action(Cheat.RemoveAllBuff), Array.Empty<GUILayoutOption>());
                                    CGUILayout.Button("清除作弊Buff", new Action(Cheat.ClearCheatBuff), Array.Empty<GUILayoutOption>());
                                });
                                CGUILayout.BeginVertical(delegate {
                                    CGUILayout.Button("添加作弊Buffs到P", new Action(Cheat.AddCheatBuff), Array.Empty<GUILayoutOption>());
                                    CGUILayout.Button("添加效果组", new Action(Cheat.AddEffectGroup), Array.Empty<GUILayoutOption>());
                                });
                            }, Array.Empty<GUILayoutOption>());
                            GUILayout.EndScrollView();
                        });
                        CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                        this.FoldCBuff = CGUILayout.FoldableMenuHorizontal(this.FoldCBuff, "自定义Buffs", delegate {
                            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MinHeight(100f) };
                            this.scrollCBuff = GUILayout.BeginScrollView(this.scrollCBuff, options);
                            Cheat.GetListCBuffs(Objects.ELP, Objects._listCbuffs);
                            GUILayout.EndScrollView();
                        }, 300f, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginVertical(GUI.skin.box, () => CGUILayout.BeginVertical(() => this.FoldPassive = CGUILayout.FoldableMenuHorizontal(this.FoldPassive, "被动效果", delegate {
                            CGUILayout.BeginHorizontal(delegate {
                                Cheat.inputPassiveEffects = GUILayout.TextField(Cheat.inputPassiveEffects, 50, Array.Empty<GUILayoutOption>());
                                this.inputPassive = ValueModifierTypesToString(this.ValueModifierTypesIndex);
                                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(25f) };
                                this.inputFloat = GUILayout.TextField(this.inputFloat, 10, options);
                                GUILayoutOption[] buttonOptions = new GUILayoutOption[] { GUILayout.MaxWidth(100f) };
                                if (CGUILayout.Button("", ref this.ValueModifierTypesIndex, buttonOptions))
                                {
                                    PassiveEffect.ValueModifierTypes valueModifierTypesIndex = (PassiveEffect.ValueModifierTypes) this.ValueModifierTypesIndex;
                                }
                                GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.MaxWidth(40f) };
                                if (CGUILayout.Button("添加", optionArray3))
                                {
                                    try
                                    {
                                        PassiveEffects effects;
                                        float num = float.Parse(this.inputFloat);
                                        if (Enum.TryParse<PassiveEffects>(Cheat.inputPassiveEffects, out effects))
                                        {
                                            Cheat.AddPassive(effects, num, (PassiveEffect.ValueModifierTypes) this.ValueModifierTypesIndex);
                                            Log.Out(this.inputPassiveEffects);
                                            Log.Out($"{effects} added");
                                        }
                                        else
                                        {
                                            Log.Out(this.inputPassiveEffects);
                                            Log.Out($"{effects} not added");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }, Array.Empty<GUILayoutOption>());
                            CGUILayout.BeginHorizontal(delegate {
                                GUILayout.Label("被动搜索", Array.Empty<GUILayoutOption>());
                                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(225f), GUILayout.Height(20f) };
                                this.passiveSearch = GUILayout.TextField(this.passiveSearch, options);
                            }, Array.Empty<GUILayoutOption>());
                            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.MinHeight(100f) };
                            this.scrollPassive = GUILayout.BeginScrollView(this.scrollPassive, optionArray1);
                            Cheat.GetListPassiveEffects(this.passiveSearch);
                            GUILayout.EndScrollView();
                        }, 300f, Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                        Setting.SB1["FoldBuff"] = CGUILayout.FoldableMenuHorizontal(Setting.SB1["FoldBuff"], "滚动全部Buff", delegate {
                            CGUILayout.BeginHorizontal(delegate {
                                GUILayout.Label("搜索Buff", Array.Empty<GUILayoutOption>());
                                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(225f), GUILayout.Height(20f) };
                                this.buffSearch = GUILayout.TextField(this.buffSearch, options);
                            }, Array.Empty<GUILayoutOption>());
                            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.MinHeight(100f) };
                            Setting.SV2["scrollBuff"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuff"], optionArray1);
                            Cheat.GetList(this.lastBuffAdded, Objects.ELP, Objects._listBuffClass, this.buffSearch);
                            GUILayout.EndScrollView();
                        }, 300f, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                        Setting.SB1["FoldPGV"] = CGUILayout.FoldableMenuHorizontal(Setting.SB1["FoldPGV"], "滚动特权", delegate {
                            CGUILayout.BeginHorizontal(delegate {
                                GUILayout.Label("搜索特权", Array.Empty<GUILayoutOption>());
                                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(225f), GUILayout.Height(20f) };
                                this.PGVSearch = GUILayout.TextField(this.PGVSearch, options);
                            }, Array.Empty<GUILayoutOption>());
                            CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.MinHeight(250f), GUILayout.MaxHeight(450f) };
                            Setting.SV2["scrollPGV"] = GUILayout.BeginScrollView(Setting.SV2["scrollPGV"], optionArray1);
                            Cheat.ListPGV(this.PGVSearch);
                            GUILayout.EndScrollView();
                        }, 300f, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }, Array.Empty<GUILayoutOption>());
            GUI.DragWindow();
        }

        private void Menu3()
        {
            CGUILayout.BeginVertical(GUI.skin.box, delegate {
                this.ScrollMenu3 = GUILayout.BeginScrollView(this.ScrollMenu3, Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.MinHeight(200f) };
                Setting.SB1["FoldZombie"] = CGUILayout.FoldableMenuHorizontal(Setting.SB1["FoldZombie"], "滚动僵尸", delegate {
                    CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f) };
                    Setting.SV2["scrollZombie"] = GUILayout.BeginScrollView(Setting.SV2["scrollZombie"], options);
                    CGUILayout.BeginVertical(GUI.skin.box, () => Cheat.ListZombie1(), Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                }, 300f, optionArray1);
                CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                Setting.SB1["FoldPlayer"] = CGUILayout.FoldableMenuHorizontal(Setting.SB1["FoldPlayer"], "滚动玩家", delegate {
                    CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MinHeight(250f), GUILayout.MaxHeight(450f) };
                    Setting.SV2["scrollPlayer"] = GUILayout.BeginScrollView(Setting.SV2["scrollPlayer"], options);
                    Cheat.ListPlayer1();
                    GUILayout.EndScrollView();
                }, 300f, Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                Setting.SB1["foldout5"] = CGUILayout.FoldableMenuHorizontal(Setting.SB1["foldout5"], "可折叠菜单5", () => CGUILayout.BeginVertical(GUI.skin.box, () => CGUILayout.BeginHorizontal(this.customBoxStyleGreen, delegate {
                    CGUILayout.BeginVertical(GUI.skin.box, delegate {
                        GUILayout.Label("实验", this.centeredLabelStyle, Array.Empty<GUILayoutOption>());
                        Settings.aimbot = GUILayout.Toggle(Settings.aimbot, "自瞄 (L-alt)", Array.Empty<GUILayoutOption>());
                        Settings.magicBullet = GUILayout.Toggle(Settings.magicBullet, "魔术子弹 (L-alt)", Array.Empty<GUILayoutOption>());
                    }, Array.Empty<GUILayoutOption>());
                    CGUILayout.BeginVertical(this.customBoxStyleGreen, delegate {
                        GUILayout.Label("L4菜单", this.centeredLabelStyle, Array.Empty<GUILayoutOption>());
                        if (CGUILayout.Button("日志PGVC到文件", Array.Empty<GUILayoutOption>()))
                        {
                            Objects.LogprogclassClassesToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
                        }
                        if (CGUILayout.Button("\x00b4日志Buff到文件", Array.Empty<GUILayoutOption>()))
                        {
                            Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                        }
                    }, Array.Empty<GUILayoutOption>());
                }, Array.Empty<GUILayoutOption>()), Array.Empty<GUILayoutOption>()), 300f, Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(this.customBoxStyleGreen, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }, Array.Empty<GUILayoutOption>());
            GUI.DragWindow();
        }

        private void OnDestroy()
        {
            Objects.ELP.Buffs.RemoveBuffs();
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.window);
            if (Setting.SB["IsGameStarted"])
            {
                style.normal.textColor = Color.green;
                style.onNormal.textColor = Color.green;
            }
            else
            {
                style.normal.textColor = Color.red;
                style.onNormal.textColor = Color.red;
            }
            if (this.drawMenu)
            {
                this.windowRect = GUILayout.Window(this.windowID, this.windowRect, new GUI.WindowFunction(this.Window), "SevenDTDMono", style, Array.Empty<GUILayoutOption>());
            }
            this.defBoxStyle = new GUIStyle(GUI.skin.box);
            this.defBoxStyle.padding = new RectOffset(0, 0, 0, 0);
            this.customBoxStyleGreen = new GUIStyle(GUI.skin.box);
            this.customBoxStyleGreen.normal.background = this.MakeTexture(2, 2, new Color(0f, 1f, 0f, 0.5f));
            this.centeredLabelStyle = new GUIStyle(GUI.skin.label);
            this.centeredLabelStyle.alignment = TextAnchor.MiddleCenter;
        }

        private void Start()
        {
            this.windowID = new Random(Environment.TickCount).Next(0x3e8, 0xffff);
            this.windowRect = new Rect(10f, 400f, 400f, 500f);
            GUI.color = Color.white;
            Debug.LogWarning("THIS IS Start New!!!!");
        }

        private void Update()
        {
            if (Input.anyKey && Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.F2))
                {
                    this.drawMenu = !this.drawMenu;
                }
                if (Settings.selfDestruct)
                {
                    Settings.selfDestruct = false;
                    Object.Destroy(Loader.gameObject);
                }
                bool aimbot = Settings.aimbot;
            }
        }

        private static string ValueModifierTypesToString(int index)
        {
            PassiveEffect.ValueModifierTypes types = (PassiveEffect.ValueModifierTypes) index;
            return types.ToString();
        }

        private void Window(int windowID)
        {
            this.windowRect.height = 500f;
            this._group = CGUILayout.Toolbar4(this._group, this.CheatsString, GUI.skin.box, Array.Empty<GUILayoutOption>());
            switch (this._group)
            {
                case 0:
                    this.Menu0();
                    break;

                case 1:
                    this.Menu1();
                    break;

                case 2:
                    this.Menu2();
                    break;

                case 3:
                    this.Menu3();
                    break;
            }
            GUILayout.Space(10f);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NewMenu.<>c <>9 = new NewMenu.<>c();
            public static Action <>9__56_0;
            public static Action <>9__56_1;
            public static Action <>9__56_2;
            public static Action <>9__56_3;
            public static Action <>9__56_4;
            public static Action <>9__56_5;
            public static Action <>9__57_0;
            public static Action <>9__57_1;
            public static Action <>9__57_2;
            public static Action <>9__57_3;
            public static Action <>9__57_4;
            public static Action <>9__57_5;
            public static Action <>9__57_6;
            public static Action <>9__57_7;
            public static Action <>9__57_8;
            public static Action <>9__57_9;
            public static Action <>9__58_10;
            public static Action <>9__58_11;
            public static Action <>9__58_4;
            public static Action <>9__58_9;
            public static Action <>9__59_4;

            internal void <Menu0>b__56_0()
            {
                CGUILayout.BeginVertical(GUI.skin.box, delegate {
                    Setting.SV2["ScrollMenu0"] = GUILayout.BeginScrollView(Setting.SV2["ScrollMenu0"], Array.Empty<GUILayoutOption>());
                    CGUILayout.BeginHorizontal(delegate {
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.Button("添加技能点", new Action(Cheat.skillpoints), Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("自杀", new Action(Cheat.KillSelf), Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("提升等级", new Action(Cheat.levelup), Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("获取我的ID", new Action(Cheat.Getplayer), Array.Empty<GUILayoutOption>());
                            if (Setting.SB.Count > 1)
                            {
                                CGUILayout.RButton("被AI忽视", "_ignoreByAI", null, Array.Empty<GUILayoutOption>());
                                CGUILayout.Button("创造/调试模式", Setting.RB, "CmDm", null, Array.Empty<GUILayoutOption>());
                                CGUILayout.SButton("名字争夺", "_nameScramble", null, Array.Empty<GUILayoutOption>());
                            }
                        });
                        CGUILayout.BeginVertical(delegate {
                            if (CGUILayout.Button("传送到标记点", Array.Empty<GUILayoutOption>()))
                            {
                                Objects.ELP.TeleportToPosition(new Vector3(Objects.ELP.markerPosition.ToVector3().x, Objects.ELP.markerPosition.ToVector3().y + 2f, Objects.ELP.markerPosition.ToVector3().z), false, null);
                            }
                            CGUILayout.Button("快速完成任务", Setting.RB, "_QuestComplete", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("快速制作", Setting.RB, "_instantCraft", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("快速拆解", Setting.RB, "_instantScrap", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("快速熔炼", Setting.RB, "_instantSmelt", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("循环获取最后任务奖励", Setting.RB, "_LOQuestRewards", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("交易开放 24/7", "_EtraderOpen", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.BeginHorizontal(delegate {
                                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                                NewMenu._itemcount = GUILayout.TextField(NewMenu._itemcount, 10, options);
                                if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                                {
                                    try
                                    {
                                        int num = int.Parse(NewMenu._itemcount);
                                        if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                                        {
                                            Objects.ELP.inventory.holdingItemStack.count = num;
                                            Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                                        }
                                        else
                                        {
                                            Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }, Array.Empty<GUILayoutOption>());
                        });
                    }, Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                }, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu0>b__56_1()
            {
                Setting.SV2["ScrollMenu0"] = GUILayout.BeginScrollView(Setting.SV2["ScrollMenu0"], Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(delegate {
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.Button("添加技能点", new Action(Cheat.skillpoints), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("自杀", new Action(Cheat.KillSelf), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("提升等级", new Action(Cheat.levelup), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("获取我的ID", new Action(Cheat.Getplayer), Array.Empty<GUILayoutOption>());
                        if (Setting.SB.Count > 1)
                        {
                            CGUILayout.RButton("被AI忽视", "_ignoreByAI", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.Button("创造/调试模式", Setting.RB, "CmDm", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.SButton("名字争夺", "_nameScramble", null, Array.Empty<GUILayoutOption>());
                        }
                    });
                    CGUILayout.BeginVertical(delegate {
                        if (CGUILayout.Button("传送到标记点", Array.Empty<GUILayoutOption>()))
                        {
                            Objects.ELP.TeleportToPosition(new Vector3(Objects.ELP.markerPosition.ToVector3().x, Objects.ELP.markerPosition.ToVector3().y + 2f, Objects.ELP.markerPosition.ToVector3().z), false, null);
                        }
                        CGUILayout.Button("快速完成任务", Setting.RB, "_QuestComplete", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速制作", Setting.RB, "_instantCraft", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速拆解", Setting.RB, "_instantScrap", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("快速熔炼", Setting.RB, "_instantSmelt", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("循环获取最后任务奖励", Setting.RB, "_LOQuestRewards", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("交易开放 24/7", "_EtraderOpen", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.BeginHorizontal(delegate {
                            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                            NewMenu._itemcount = GUILayout.TextField(NewMenu._itemcount, 10, options);
                            if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                            {
                                try
                                {
                                    int num = int.Parse(NewMenu._itemcount);
                                    if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                                    {
                                        Objects.ELP.inventory.holdingItemStack.count = num;
                                        Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                                    }
                                    else
                                    {
                                        Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }, Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }

            internal void <Menu0>b__56_2()
            {
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.Button("添加技能点", new Action(Cheat.skillpoints), Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("自杀", new Action(Cheat.KillSelf), Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("提升等级", new Action(Cheat.levelup), Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("获取我的ID", new Action(Cheat.Getplayer), Array.Empty<GUILayoutOption>());
                    if (Setting.SB.Count > 1)
                    {
                        CGUILayout.RButton("被AI忽视", "_ignoreByAI", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("创造/调试模式", Setting.RB, "CmDm", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.SButton("名字争夺", "_nameScramble", null, Array.Empty<GUILayoutOption>());
                    }
                });
                CGUILayout.BeginVertical(delegate {
                    if (CGUILayout.Button("传送到标记点", Array.Empty<GUILayoutOption>()))
                    {
                        Objects.ELP.TeleportToPosition(new Vector3(Objects.ELP.markerPosition.ToVector3().x, Objects.ELP.markerPosition.ToVector3().y + 2f, Objects.ELP.markerPosition.ToVector3().z), false, null);
                    }
                    CGUILayout.Button("快速完成任务", Setting.RB, "_QuestComplete", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("快速制作", Setting.RB, "_instantCraft", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("快速拆解", Setting.RB, "_instantScrap", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("快速熔炼", Setting.RB, "_instantSmelt", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("循环获取最后任务奖励", Setting.RB, "_LOQuestRewards", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("交易开放 24/7", "_EtraderOpen", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.BeginHorizontal(delegate {
                        GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                        NewMenu._itemcount = GUILayout.TextField(NewMenu._itemcount, 10, options);
                        if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                        {
                            try
                            {
                                int num = int.Parse(NewMenu._itemcount);
                                if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                                {
                                    Objects.ELP.inventory.holdingItemStack.count = num;
                                    Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                                }
                                else
                                {
                                    Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                                }
                            }
                            catch
                            {
                            }
                        }
                    }, Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu0>b__56_3()
            {
                CGUILayout.Button("添加技能点", new Action(Cheat.skillpoints), Array.Empty<GUILayoutOption>());
                CGUILayout.Button("自杀", new Action(Cheat.KillSelf), Array.Empty<GUILayoutOption>());
                CGUILayout.Button("提升等级", new Action(Cheat.levelup), Array.Empty<GUILayoutOption>());
                CGUILayout.Button("获取我的ID", new Action(Cheat.Getplayer), Array.Empty<GUILayoutOption>());
                if (Setting.SB.Count > 1)
                {
                    CGUILayout.RButton("被AI忽视", "_ignoreByAI", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("创造/调试模式", Setting.RB, "CmDm", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.SButton("名字争夺", "_nameScramble", null, Array.Empty<GUILayoutOption>());
                }
            }

            internal void <Menu0>b__56_4()
            {
                if (CGUILayout.Button("传送到标记点", Array.Empty<GUILayoutOption>()))
                {
                    Objects.ELP.TeleportToPosition(new Vector3(Objects.ELP.markerPosition.ToVector3().x, Objects.ELP.markerPosition.ToVector3().y + 2f, Objects.ELP.markerPosition.ToVector3().z), false, null);
                }
                CGUILayout.Button("快速完成任务", Setting.RB, "_QuestComplete", null, Array.Empty<GUILayoutOption>());
                CGUILayout.Button("快速制作", Setting.RB, "_instantCraft", null, Array.Empty<GUILayoutOption>());
                CGUILayout.Button("快速拆解", Setting.RB, "_instantScrap", null, Array.Empty<GUILayoutOption>());
                CGUILayout.Button("快速熔炼", Setting.RB, "_instantSmelt", null, Array.Empty<GUILayoutOption>());
                CGUILayout.Button("循环获取最后任务奖励", Setting.RB, "_LOQuestRewards", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("交易开放 24/7", "_EtraderOpen", null, Array.Empty<GUILayoutOption>());
                CGUILayout.BeginHorizontal(delegate {
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                    NewMenu._itemcount = GUILayout.TextField(NewMenu._itemcount, 10, options);
                    if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                    {
                        try
                        {
                            int num = int.Parse(NewMenu._itemcount);
                            if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                            {
                                Objects.ELP.inventory.holdingItemStack.count = num;
                                Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                            }
                            else
                            {
                                Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                            }
                        }
                        catch
                        {
                        }
                    }
                }, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu0>b__56_5()
            {
                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                NewMenu._itemcount = GUILayout.TextField(NewMenu._itemcount, 10, options);
                if (CGUILayout.Button(" 设置持有物品", Array.Empty<GUILayoutOption>()))
                {
                    try
                    {
                        int num = int.Parse(NewMenu._itemcount);
                        if (Objects.ELP.inventory.holdingItemStack.count >= 1)
                        {
                            Objects.ELP.inventory.holdingItemStack.count = num;
                            Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} set to {num}");
                        }
                        else
                        {
                            Debug.LogWarning($"{Objects.ELP.inventory.holdingItem.Name} Could not be set to {num}");
                        }
                    }
                    catch
                    {
                    }
                }
            }

            internal void <Menu1>b__57_0()
            {
                CGUILayout.BeginVertical(delegate {
                    Settings.crosshair = GUILayout.Toggle(Settings.crosshair, "十字准星", Array.Empty<GUILayoutOption>());
                    Settings.fovCircle = GUILayout.Toggle(Settings.fovCircle, "显示自瞄范围", Array.Empty<GUILayoutOption>());
                    Settings.playerBox = GUILayout.Toggle(Settings.playerBox, "玩家方框", Array.Empty<GUILayoutOption>());
                    Settings.playerName = GUILayout.Toggle(Settings.playerName, "玩家名称", Array.Empty<GUILayoutOption>());
                    Settings.playerCornerBox = GUILayout.Toggle(Settings.playerCornerBox, "玩家角框", Array.Empty<GUILayoutOption>());
                    Settings.chams = GUILayout.Toggle(Settings.chams, "上色", Array.Empty<GUILayoutOption>());
                    Settings.playerHealth = GUILayout.Toggle(Settings.playerHealth, "玩家血量", Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    Settings.zombieBox = GUILayout.Toggle(Settings.zombieBox, "僵尸方框", Array.Empty<GUILayoutOption>());
                    Settings.zombieName = GUILayout.Toggle(Settings.zombieName, "僵尸名称", Array.Empty<GUILayoutOption>());
                    Settings.zombieHealth = GUILayout.Toggle(Settings.zombieHealth, "僵尸血量", Array.Empty<GUILayoutOption>());
                    Settings.zombieCornerBox = GUILayout.Toggle(Settings.zombieCornerBox, "僵尸角框", Array.Empty<GUILayoutOption>());
                    Settings.noWeaponBob = GUILayout.Toggle(Settings.noWeaponBob, "武器无摇摆", Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu1>b__57_1()
            {
                GUIStyle style = new GUIStyle(GUI.skin.label) {
                    alignment = TextAnchor.LowerCenter,
                    fontSize = 13,
                    padding = new RectOffset(0, 0, -4, 0)
                };
                CGUILayout.BeginHorizontal((Action) (() => GUILayout.Label("编辑", Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.HorizontalScrollbarWithLabel("攻击/分钟", ref Settings._FL_APM, 500f);
                    CGUILayout.HorizontalScrollbarWithLabel("跳跃力量", ref Settings._FL_jmp, 5f);
                    CGUILayout.HorizontalScrollbarWithLabel("收获翻倍", ref Settings._FL_harvest, 20f);
                    CGUILayout.HorizontalScrollbarWithLabel("冲刺速度", ref Settings._FL_run, 25f);
                    CGUILayout.HorizontalScrollbarWithLabel("杀死DMG XM", ref Settings._FL_killdmg, 1000f);
                    CGUILayout.HorizontalScrollbarWithLabel("堵塞DMG", ref Settings._FL_blokdmg, 10000f);
                });
                CGUILayout.BeginVertical(GUI.skin.box, delegate {
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxHeight(170f) };
                    Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                    CGUILayout.BeginHorizontal(delegate {
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                        });
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                        });
                    }, Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                }, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__57_2()
            {
                Settings.crosshair = GUILayout.Toggle(Settings.crosshair, "十字准星", Array.Empty<GUILayoutOption>());
                Settings.fovCircle = GUILayout.Toggle(Settings.fovCircle, "显示自瞄范围", Array.Empty<GUILayoutOption>());
                Settings.playerBox = GUILayout.Toggle(Settings.playerBox, "玩家方框", Array.Empty<GUILayoutOption>());
                Settings.playerName = GUILayout.Toggle(Settings.playerName, "玩家名称", Array.Empty<GUILayoutOption>());
                Settings.playerCornerBox = GUILayout.Toggle(Settings.playerCornerBox, "玩家角框", Array.Empty<GUILayoutOption>());
                Settings.chams = GUILayout.Toggle(Settings.chams, "上色", Array.Empty<GUILayoutOption>());
                Settings.playerHealth = GUILayout.Toggle(Settings.playerHealth, "玩家血量", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__57_3()
            {
                Settings.zombieBox = GUILayout.Toggle(Settings.zombieBox, "僵尸方框", Array.Empty<GUILayoutOption>());
                Settings.zombieName = GUILayout.Toggle(Settings.zombieName, "僵尸名称", Array.Empty<GUILayoutOption>());
                Settings.zombieHealth = GUILayout.Toggle(Settings.zombieHealth, "僵尸血量", Array.Empty<GUILayoutOption>());
                Settings.zombieCornerBox = GUILayout.Toggle(Settings.zombieCornerBox, "僵尸角框", Array.Empty<GUILayoutOption>());
                Settings.noWeaponBob = GUILayout.Toggle(Settings.noWeaponBob, "武器无摇摆", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__57_4()
            {
                GUILayout.Label("编辑", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__57_5()
            {
                CGUILayout.HorizontalScrollbarWithLabel("攻击/分钟", ref Settings._FL_APM, 500f);
                CGUILayout.HorizontalScrollbarWithLabel("跳跃力量", ref Settings._FL_jmp, 5f);
                CGUILayout.HorizontalScrollbarWithLabel("收获翻倍", ref Settings._FL_harvest, 20f);
                CGUILayout.HorizontalScrollbarWithLabel("冲刺速度", ref Settings._FL_run, 25f);
                CGUILayout.HorizontalScrollbarWithLabel("杀死DMG XM", ref Settings._FL_killdmg, 1000f);
                CGUILayout.HorizontalScrollbarWithLabel("堵塞DMG", ref Settings._FL_blokdmg, 10000f);
            }

            internal void <Menu1>b__57_6()
            {
                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxHeight(170f) };
                Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                CGUILayout.BeginHorizontal(delegate {
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                    });
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }

            internal void <Menu1>b__57_7()
            {
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu1>b__57_8()
            {
                CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__57_9()
            {
                CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu2>b__58_10()
            {
                CGUILayout.Button("移除所有有效的Buff", new Action(Cheat.RemoveAllBuff), Array.Empty<GUILayoutOption>());
                CGUILayout.Button("清除作弊Buff", new Action(Cheat.ClearCheatBuff), Array.Empty<GUILayoutOption>());
            }

            internal void <Menu2>b__58_11()
            {
                CGUILayout.Button("添加作弊Buffs到P", new Action(Cheat.AddCheatBuff), Array.Empty<GUILayoutOption>());
                CGUILayout.Button("添加效果组", new Action(Cheat.AddEffectGroup), Array.Empty<GUILayoutOption>());
            }

            internal void <Menu2>b__58_4()
            {
                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MinHeight(80f) };
                Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                CGUILayout.BeginHorizontal(delegate {
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.Button("移除所有有效的Buff", new Action(Cheat.RemoveAllBuff), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("清除作弊Buff", new Action(Cheat.ClearCheatBuff), Array.Empty<GUILayoutOption>());
                    });
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.Button("添加作弊Buffs到P", new Action(Cheat.AddCheatBuff), Array.Empty<GUILayoutOption>());
                        CGUILayout.Button("添加效果组", new Action(Cheat.AddEffectGroup), Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }

            internal void <Menu2>b__58_9()
            {
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.Button("移除所有有效的Buff", new Action(Cheat.RemoveAllBuff), Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("清除作弊Buff", new Action(Cheat.ClearCheatBuff), Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.Button("添加作弊Buffs到P", new Action(Cheat.AddCheatBuff), Array.Empty<GUILayoutOption>());
                    CGUILayout.Button("添加效果组", new Action(Cheat.AddEffectGroup), Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu3>b__59_4()
            {
                Cheat.ListZombie1();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0
        {
            public static readonly NewMenu.<>c__0 <>9 = new NewMenu.<>c__0();
            public static Action <>9__0_0;
            public static Action <>9__0_1;
            public static Action <>9__0_2;
            public static Action <>9__0_3;
            public static Action <>9__0_4;
            public static Action <>9__0_5;
            public static Action <>9__0_6;
            public static Action <>9__0_7;
            public static Action <>9__0_8;
            public static Action <>9__0_9;

            internal void <Menu1>b__0_0()
            {
                CGUILayout.BeginVertical(delegate {
                    Settings.crosshair = GUILayout.Toggle(Settings.crosshair, "十字准星", Array.Empty<GUILayoutOption>());
                    Settings.fovCircle = GUILayout.Toggle(Settings.fovCircle, "显示自瞄范围", Array.Empty<GUILayoutOption>());
                    Settings.playerBox = GUILayout.Toggle(Settings.playerBox, "玩家方框", Array.Empty<GUILayoutOption>());
                    Settings.playerName = GUILayout.Toggle(Settings.playerName, "玩家名称", Array.Empty<GUILayoutOption>());
                    Settings.playerCornerBox = GUILayout.Toggle(Settings.playerCornerBox, "玩家角框", Array.Empty<GUILayoutOption>());
                    Settings.chams = GUILayout.Toggle(Settings.chams, "上色", Array.Empty<GUILayoutOption>());
                    Settings.playerHealth = GUILayout.Toggle(Settings.playerHealth, "玩家血量", Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    Settings.zombieBox = GUILayout.Toggle(Settings.zombieBox, "僵尸方框", Array.Empty<GUILayoutOption>());
                    Settings.zombieName = GUILayout.Toggle(Settings.zombieName, "僵尸名称", Array.Empty<GUILayoutOption>());
                    Settings.zombieHealth = GUILayout.Toggle(Settings.zombieHealth, "僵尸血量", Array.Empty<GUILayoutOption>());
                    Settings.zombieCornerBox = GUILayout.Toggle(Settings.zombieCornerBox, "僵尸角框", Array.Empty<GUILayoutOption>());
                    Settings.noWeaponBob = GUILayout.Toggle(Settings.noWeaponBob, "武器无摇摆", Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu1>b__0_1()
            {
                GUIStyle style1 = new GUIStyle(GUI.skin.label) {
                    alignment = TextAnchor.LowerCenter,
                    fontSize = 13,
                    padding = new RectOffset(0, 0, -4, 0)
                };
                CGUILayout.BeginHorizontal((Action) (() => GUILayout.Label("编辑", Array.Empty<GUILayoutOption>())), Array.Empty<GUILayoutOption>());
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.HorizontalScrollbarWithLabel("攻击/分钟", ref Settings._FL_APM, 500f);
                    CGUILayout.HorizontalScrollbarWithLabel("跳跃力量", ref Settings._FL_jmp, 99f);
                    CGUILayout.HorizontalScrollbarWithLabel("收获翻倍", ref Settings._FL_harvest, 100f);
                    CGUILayout.HorizontalScrollbarWithLabel("冲刺速度", ref Settings._FL_run, 100f);
                    CGUILayout.HorizontalScrollbarWithLabel("杀死DMG XM", ref Settings._FL_killdmg, 99999f);
                    CGUILayout.HorizontalScrollbarWithLabel("堵塞DMG", ref Settings._FL_blokdmg, 99999f);
                });
                CGUILayout.BeginVertical(GUI.skin.box, delegate {
                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxHeight(170f) };
                    Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                    CGUILayout.BeginHorizontal(delegate {
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                        });
                        CGUILayout.BeginVertical(delegate {
                            CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                            CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                        });
                    }, Array.Empty<GUILayoutOption>());
                    GUILayout.EndScrollView();
                }, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__0_2()
            {
                Settings.crosshair = GUILayout.Toggle(Settings.crosshair, "十字准星", Array.Empty<GUILayoutOption>());
                Settings.fovCircle = GUILayout.Toggle(Settings.fovCircle, "显示自瞄范围", Array.Empty<GUILayoutOption>());
                Settings.playerBox = GUILayout.Toggle(Settings.playerBox, "玩家方框", Array.Empty<GUILayoutOption>());
                Settings.playerName = GUILayout.Toggle(Settings.playerName, "玩家名称", Array.Empty<GUILayoutOption>());
                Settings.playerCornerBox = GUILayout.Toggle(Settings.playerCornerBox, "玩家角框", Array.Empty<GUILayoutOption>());
                Settings.chams = GUILayout.Toggle(Settings.chams, "上色", Array.Empty<GUILayoutOption>());
                Settings.playerHealth = GUILayout.Toggle(Settings.playerHealth, "玩家血量", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__0_3()
            {
                Settings.zombieBox = GUILayout.Toggle(Settings.zombieBox, "僵尸方框", Array.Empty<GUILayoutOption>());
                Settings.zombieName = GUILayout.Toggle(Settings.zombieName, "僵尸名称", Array.Empty<GUILayoutOption>());
                Settings.zombieHealth = GUILayout.Toggle(Settings.zombieHealth, "僵尸血量", Array.Empty<GUILayoutOption>());
                Settings.zombieCornerBox = GUILayout.Toggle(Settings.zombieCornerBox, "僵尸角框", Array.Empty<GUILayoutOption>());
                Settings.noWeaponBob = GUILayout.Toggle(Settings.noWeaponBob, "武器无摇摆", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__0_4()
            {
                GUILayout.Label("编辑", Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__0_5()
            {
                CGUILayout.HorizontalScrollbarWithLabel("攻击/分钟", ref Settings._FL_APM, 500f);
                CGUILayout.HorizontalScrollbarWithLabel("跳跃力量", ref Settings._FL_jmp, 99f);
                CGUILayout.HorizontalScrollbarWithLabel("收获翻倍", ref Settings._FL_harvest, 100f);
                CGUILayout.HorizontalScrollbarWithLabel("冲刺速度", ref Settings._FL_run, 100f);
                CGUILayout.HorizontalScrollbarWithLabel("杀死DMG XM", ref Settings._FL_killdmg, 99999f);
                CGUILayout.HorizontalScrollbarWithLabel("堵塞DMG", ref Settings._FL_blokdmg, 99999f);
            }

            internal void <Menu1>b__0_6()
            {
                GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxHeight(170f) };
                Setting.SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(Setting.SV2["scrollBuffBTN"], options);
                CGUILayout.BeginHorizontal(delegate {
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                    });
                    CGUILayout.BeginVertical(delegate {
                        CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                        CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                    });
                }, Array.Empty<GUILayoutOption>());
                GUILayout.EndScrollView();
            }

            internal void <Menu1>b__0_7()
            {
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
                });
                CGUILayout.BeginVertical(delegate {
                    CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                    CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
                });
            }

            internal void <Menu1>b__0_8()
            {
                CGUILayout.RButton("堵塞DMG", "_BL_Blockdmg", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("杀死DMG", "_BL_Kill", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("收获翻倍", "_BL_Harvest", null, Array.Empty<GUILayoutOption>());
            }

            internal void <Menu1>b__0_9()
            {
                CGUILayout.RButton("冲刺速度", "_BL_Run", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("跳跃力量", "_BL_Jmp", null, Array.Empty<GUILayoutOption>());
                CGUILayout.RButton("攻击/分钟", "_BL_APM", null, Array.Empty<GUILayoutOption>());
            }
        }
    }
}

