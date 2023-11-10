namespace SevenDTDMono
{
    using SevenDTDMono.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class Cheat : MonoBehaviour
    {
        public static string inputFloat = "1";
        public static string inputPassiveEffects = "none";
        public static Transform parentTransform;

        public static void AddCheatBuff()
        {
            if (BuffManager.GetBuff(Objects.CheatBuff.Name) == null)
            {
                Log.Out($"Buff {Objects.CheatBuff} has ben added");
                Objects.ELP.Buffs.AddBuff(Objects.CheatBuff.Name, -1, true, false, false, -1f);
                Log.Out($"Buff {Objects.CheatBuff.Name} has ben added to{Objects.ELP.Buffs.ActiveBuffs.GetInternalArray<BuffValue>()} ");
            }
            else
            {
                Debug.LogWarning("Buff " + Objects.CheatBuff.Name + " was already added to the system");
                if (Objects.ELP.Buffs.GetBuff(Objects.CheatBuff.Name) == null)
                {
                    Objects.ELP.Buffs.AddBuff(Objects.CheatBuff.Name, -1, true, false, false, -1f);
                    Log.Out("Buff " + Objects.CheatBuff.Name + " was Added to to Player again");
                }
            }
        }

        public static void AddEffectGroup()
        {
            Debug.LogWarning("adding effectGroup");
            List<MinEffectGroup> list1 = new List<MinEffectGroup>();
            MinEffectGroup item = new MinEffectGroup {
                OwnerTiered = true,
                PassiveEffects = new List<PassiveEffect>(),
                PassivesIndex = new List<PassiveEffects>()
            };
            list1.Add(item);
            Objects._minEffectController.EffectGroups = list1;
        }

        public static void AddPassive(PassiveEffects passiveEffects, float value, PassiveEffect.ValueModifierTypes valueModifierTypes)
        {
            if (!Objects.ELP.Buffs.HasBuff("CheatBuff"))
            {
                try
                {
                    Log.Out(Objects.CheatBuff.Name + " was not active, try adding");
                    Objects.ELP.Buffs.AddBuff("CheatBuff", -1, true, false, false, -1f);
                }
                catch
                {
                }
            }
            List<PassiveEffect> list = new List<PassiveEffect>();
            MinEffectGroup group = Objects._minEffectController.EffectGroups[0];
            PassiveEffect effect2 = new PassiveEffect {
                MatchAnyTags = true,
                Modifier = valueModifierTypes,
                Type = passiveEffects
            };
            effect2.Values = new float[] { value };
            PassiveEffect item = effect2;
            List<PassiveEffect> list2 = Objects._minEffectController.EffectGroups[0].PassiveEffects;
            for (int i = list2.Count - 1; i >= 0; i--)
            {
                PassiveEffect effect3 = list2[i];
                if (effect3.Type == item.Type)
                {
                    list2.RemoveAt(i);
                }
            }
            Objects._minEffectController.PassivesIndex.Add(passiveEffects);
            Objects._minEffectController.EffectGroups[0].PassiveEffects.Add(item);
        }

        public static void CheatPassiveEffect(bool toggle, PassiveEffects passive, float modifier, PassiveEffect.ValueModifierTypes VMT)
        {
            if ((Objects.ELP != null) && Setting.SB["IsGameStarted"])
            {
                if (toggle)
                {
                    AddPassive(passive, modifier, VMT);
                }
                else if (!toggle)
                {
                    RemovePassive(passive);
                }
            }
        }

        public static void ClearCheatBuff()
        {
            Debug.LogWarning("Clearing CheatBuff");
            Objects._minEffectController.EffectGroups[0].PassiveEffects.Clear();
            Objects._minEffectController.PassivesIndex.Clear();
        }

        public static void CmDm()
        {
            GameStats.Set(EnumGameStats.ShowSpawnWindow, Setting.RB["CmDm"]);
            GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, Setting.RB["CmDm"]);
            GamePrefs.Set(EnumGamePrefs.DebugMenuEnabled, Setting.RB["CmDm"]);
        }

        private static void DisplayToggleButton(PassiveEffects effect)
        {
            if (!Setting.PVETState.ContainsKey(effect))
            {
                Setting.PVETState[effect] = false;
            }
            bool flag = Setting.PVETState[effect];
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(150f) };
            bool flag2 = CGUILayout.Button(effect.ToString(), options);
            Setting.PVETState[effect] = flag2;
            if (flag2)
            {
                inputPassiveEffects = effect.ToString();
            }
        }

        public static void editMode()
        {
        }

        public static void GetList(bool _bool, EntityPlayerLocal entityLocalPlayer, List<BuffClass> ListOFClass, string searchText)
        {
            if (ListOFClass != null)
            {
                if ((entityLocalPlayer != null) || (ListOFClass != null))
                {
                    if (ListOFClass.Count <= 0)
                    {
                        GUILayout.Label("没有找到buff.", Array.Empty<GUILayoutOption>());
                    }
                    else
                    {
                        foreach (BuffClass class2 in ListOFClass)
                        {
                            if ((searchText == "") || class2.Name.Contains(searchText))
                            {
                                if (GUILayout.Button(class2.Name, Array.Empty<GUILayoutOption>()))
                                {
                                    entityLocalPlayer.Buffs.AddBuff(class2.Name, -1, true, false, false, 99999f);
                                    Debug.LogWarning(class2.Name + " Added to player " + Objects.ELP.gameObject.name);
                                }
                                if (_bool)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    GUILayout.Label("Not ingame", Array.Empty<GUILayoutOption>());
                }
            }
        }

        public static void GetListCBuffs(EntityPlayerLocal entityLocalPlayer, List<BuffClass> ListOFClass)
        {
            if (ListOFClass != null)
            {
                if ((entityLocalPlayer != null) || (ListOFClass != null))
                {
                    if (ListOFClass.Count <= 0)
                    {
                        GUILayout.Label("找不到Buff.", Array.Empty<GUILayoutOption>());
                    }
                    else
                    {
                        foreach (BuffClass class2 in ListOFClass)
                        {
                            string name = class2.Name;
                            if (!Setting.ButtonTState.ContainsKey(name))
                            {
                                Setting.ButtonTState[name] = false;
                            }
                            bool flag = Setting.ButtonTState[name];
                            bool flag2 = GUILayout.Toggle(flag, name, Array.Empty<GUILayoutOption>());
                            if (flag2 != flag)
                            {
                                Setting.ButtonTState[name] = flag2;
                                if (flag2)
                                {
                                    entityLocalPlayer.Buffs.AddBuff(name, -1, true, false, false, -1f);
                                }
                                else
                                {
                                    entityLocalPlayer.Buffs.RemoveBuff(name, true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    GUILayout.Label("Not ingame", Array.Empty<GUILayoutOption>());
                }
            }
        }

        public static void GetListPassiveEffects(string searchText)
        {
            Action <>9__3;
            Action <>9__4;
            PassiveEffects[] effectsArray = (from effect in Enum.GetValues(typeof(PassiveEffects)).Cast<PassiveEffects>()
                orderby effect.ToString()
                select effect).ToArray<PassiveEffects>();
            PassiveEffects[] source = string.IsNullOrEmpty(searchText) ? effectsArray : (from effect in effectsArray
                where effect.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                select effect).ToArray<PassiveEffects>();
            int count = Mathf.CeilToInt(((float) effectsArray.Length) / 2f);
            PassiveEffects[] leftColumnEffects = source.Take<PassiveEffects>(count).ToArray<PassiveEffects>();
            PassiveEffects[] rightColumnEffects = source.Skip<PassiveEffects>(count).ToArray<PassiveEffects>();
            CGUILayout.BeginHorizontal(delegate {
                CGUILayout.BeginVertical(<>9__3 ?? (<>9__3 = delegate {
                    foreach (PassiveEffects effects in leftColumnEffects)
                    {
                        DisplayToggleButton(effects);
                    }
                }));
                CGUILayout.BeginVertical(<>9__4 ?? (<>9__4 = delegate {
                    foreach (PassiveEffects effects in rightColumnEffects)
                    {
                        DisplayToggleButton(effects);
                    }
                }));
            }, Array.Empty<GUILayoutOption>());
        }

        public static void Getplayer()
        {
            Debug.LogError("player ID: " + Objects.ELP.name.ToString());
        }

        public static void InstantQuestFinish()
        {
            if (Setting.RB["_QuestComplete"] && (Objects.ELP != null))
            {
                foreach (Quest quest in Objects.ELP.QuestJournal.quests)
                {
                    if ((quest.Tracked || quest.Active) && (quest.CurrentState == Quest.QuestState.InProgress))
                    {
                        quest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                    }
                }
            }
        }

        public static void KillSelf()
        {
            Objects.ELP.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 0x1869f, false, 1f);
            SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Gave 99999 damage to entity ");
        }

        public static void levelup()
        {
            if (Objects.ELP != null)
            {
                Progression progression = Objects.ELP.Progression;
                progression.AddLevelExp(progression.ExpToNextLevel, "_xpOther", Progression.XPTypes.Other, true);
            }
        }

        public static void ListPGV(string search)
        {
            Func<ProgressionValue, bool> <>9__3;
            if (Objects._listProgressionValue.Count > 0)
            {
                foreach (KeyValuePair<ProgressionType, List<ProgressionValue>> pair in (from pgv in Objects._listProgressionValue group pgv by pgv.ProgressionClass.Type).ToDictionary<IGrouping<ProgressionType, ProgressionValue>, ProgressionType, List<ProgressionValue>>(g => g.Key, g => g.ToList<ProgressionValue>()))
                {
                    ProgressionType key = pair.Key;
                    List<ProgressionValue> values = pair.Value;
                    if (!string.IsNullOrEmpty(search))
                    {
                        values = values.Where<ProgressionValue>(((Func<ProgressionValue, bool>) (<>9__3 ?? (<>9__3 = pgv => pgv.Name.Contains(search))))).ToList<ProgressionValue>();
                    }
                    string str = key.ToString();
                    if (!Setting.MenuDropTState.ContainsKey(str))
                    {
                        Setting.MenuDropTState[str] = false;
                    }
                    bool toggle = Setting.MenuDropTState[str];
                    CGUILayout.DropDownForMethods("Progression Type: " + str, delegate {
                        using (List<ProgressionValue>.Enumerator enumerator = values.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                ProgressionValue PGV = enumerator.Current;
                                string id = PGV.Name;
                                CGUILayout.BeginHorizontal(delegate {
                                    GUILayout.Label(id, Array.Empty<GUILayoutOption>());
                                    GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                                    if (GUILayout.Button("+1", options))
                                    {
                                        int level = PGV.Level;
                                        int maxLevel = PGV.ProgressionClass.MaxLevel;
                                        if (level < maxLevel)
                                        {
                                            PGV.Level++;
                                        }
                                    }
                                    GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.MaxWidth(50f) };
                                    if (GUILayout.Button("MAX", optionArray2))
                                    {
                                        PGV.Level = PGV.ProgressionClass.MaxLevel;
                                    }
                                }, Array.Empty<GUILayoutOption>());
                            }
                        }
                    }, ref toggle, Array.Empty<GUILayoutOption>());
                    Setting.MenuDropTState[str] = toggle;
                }
            }
        }

        public static void ListPlayer1()
        {
            if (Objects.PlayerList.Count > 1)
            {
                using (List<EntityPlayer>.Enumerator enumerator = Objects.PlayerList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Action <>9__1;
                        EntityPlayer player = enumerator.Current;
                        if (((player != null) && (player != Objects.ELP)) && player.IsAlive())
                        {
                            string str = player.entityId.ToString();
                            string key = player.EntityName + str;
                            string str4 = player.EntityName.ToString() + player.entityId.ToString();
                            string str5 = player.EntityName.ToString();
                            if (!Setting.MenuDropTState.ContainsKey(key))
                            {
                                Setting.MenuDropTState[key] = false;
                            }
                            bool toggle = Setting.MenuDropTState[key];
                            CGUILayout.DropDownForMethods(key, () => CGUILayout.BeginHorizontal(<>9__1 ?? (<>9__1 = delegate {
                                if (GUILayout.Button("传送", Array.Empty<GUILayoutOption>()))
                                {
                                    Objects.ELP.TeleportToPosition(player.GetPosition(), false, null);
                                }
                                if (GUILayout.Button("杀死", Array.Empty<GUILayoutOption>()))
                                {
                                    player.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 0x1869f, false, 1f);
                                }
                            }), Array.Empty<GUILayoutOption>()), ref toggle, Array.Empty<GUILayoutOption>());
                            Setting.MenuDropTState[key] = toggle;
                        }
                    }
                    return;
                }
            }
            GUILayout.Label("找不到玩家.", Array.Empty<GUILayoutOption>());
        }

        public static void ListZombie1()
        {
            if (Objects._listEntityEnemy.Count > 1)
            {
                using (List<EntityEnemy>.Enumerator enumerator = Objects._listEntityEnemy.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Action <>9__1;
                        EntityEnemy enemy = enumerator.Current;
                        if (((enemy != null) && (enemy != Objects.ELP)) && enemy.IsAlive())
                        {
                            string str = enemy.entityId.ToString();
                            string key = enemy.EntityName + str;
                            if (!Setting.MenuDropTState.ContainsKey(key))
                            {
                                Setting.MenuDropTState[key] = false;
                            }
                            bool toggle = Setting.MenuDropTState[key];
                            CGUILayout.DropDownForMethods(key, () => CGUILayout.BeginHorizontal(<>9__1 ?? (<>9__1 = delegate {
                                if (GUILayout.Button("传送", Array.Empty<GUILayoutOption>()))
                                {
                                    Objects.ELP.TeleportToPosition(enemy.GetPosition(), false, null);
                                }
                                if (GUILayout.Button("杀死", Array.Empty<GUILayoutOption>()))
                                {
                                    enemy.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 0x1869f, false, 1f);
                                }
                            }), Array.Empty<GUILayoutOption>()), ref toggle, Array.Empty<GUILayoutOption>());
                            Setting.MenuDropTState[key] = toggle;
                        }
                    }
                    return;
                }
            }
            GUILayout.Label("找不到僵尸.", Array.Empty<GUILayoutOption>());
        }

        public static void LoopLASTQuestRewards()
        {
            if ((Setting.RB["_LOQuestRewards"] && (Objects.ELP != null)) && (Objects.ELP.QuestJournal.quests.Count > 0))
            {
                int num = Objects.ELP.QuestJournal.quests.Count - 1;
                Quest quest = Objects.ELP.QuestJournal.quests[num];
                if (quest.CurrentState == Quest.QuestState.Completed)
                {
                    quest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                    Debug.LogWarning(" " + quest.ID + " is ready for turn in");
                }
            }
        }

        public static void MaxSkill()
        {
            if (Objects._listProgressionValue.Count > 0)
            {
                foreach (ProgressionValue value2 in Objects._listProgressionValue)
                {
                    int level = value2.Level;
                    int maxLevel = value2.ProgressionClass.MaxLevel;
                    if (level < maxLevel)
                    {
                        value2.Level = maxLevel;
                    }
                }
            }
        }

        private void OnGUI()
        {
        }

        public void OnHud()
        {
        }

        public static void RemoveAllBuff()
        {
            foreach (BuffValue value2 in Objects.ELP.Buffs.ActiveBuffs)
            {
                Objects.ELP.Buffs.RemoveBuff(value2.BuffName, true);
                if (Setting.ButtonTState.ContainsKey(value2.BuffName))
                {
                    Setting.ButtonTState[value2.BuffName] = false;
                }
            }
        }

        private static void RemovePassive(PassiveEffects passiveEffects)
        {
            List<PassiveEffect> list = Objects._minEffectController.EffectGroups[0].PassiveEffects;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                PassiveEffect effect = list[i];
                if (effect.Type == passiveEffects)
                {
                    list.RemoveAt(i);
                }
                Objects._minEffectController.PassivesIndex.Remove(passiveEffects);
            }
        }

        public static void skillpoints()
        {
            if (Objects.ELP != null)
            {
                Progression progression = Objects.ELP.Progression;
                progression.SkillPoints += 10;
                Log.Out($"Skillpoints added by 10is now {progression.SkillPoints}");
            }
        }

        public static void SOMECONSOLEPRINTOUT()
        {
            string debugNameInfo = null;
            string str2 = "SETT.cmDm";
            if (Objects.ELP != null)
            {
                debugNameInfo = Objects.ELP.DebugNameInfo;
            }
            Debug.developerConsoleVisible = true;
            Console.WriteLine("THIS IS WRITE LINE" + str2);
            Debug.LogWarning("Debug <color=_col>LogWarnig</color> for " + str2 + ": " + debugNameInfo);
            Debug.LogError("Debug <color=_col>LogError</color> for " + str2 + ": " + debugNameInfo);
            Debug.Log("Debug <color=_col>Log</color> for " + str2 + ": " + str2);
            MonoBehaviour.print("This is a <color=_col_col>Print Message</color> for " + str2 + ": " + debugNameInfo);
            MonoBehaviour.print("This is Actually the <color=green>INF/Print </color> console out for " + str2 + " +cm+: " + debugNameInfo);
            Log.Out("BYYYYYYYYYYY");
        }

        private void Start()
        {
            Debug.LogWarning("THIS IS Start CH!!!!");
        }

        public static void Trader()
        {
            if ((Objects.Etrader != null) && (Objects.ELP != null))
            {
                Objects.Etrader.IsDancing = true;
                ulong num = 0L;
                Objects.Etrader.TraderInfo.CloseTime = num;
                Objects.Etrader.TraderInfo.OpenTime = num;
            }
            else if (Objects.Etrader == null)
            {
                Setting.RB["_EtraderOpen"] = false;
            }
        }

        private void Update()
        {
            if (Setting.SB["IsGameStarted"])
            {
                CheatPassiveEffect(Setting.RB["_BL_Blockdmg"], PassiveEffects.BlockDamage, Settings._FL_blokdmg, PassiveEffect.ValueModifierTypes.perc_add);
                CheatPassiveEffect(Setting.RB["_BL_Kill"], PassiveEffects.EntityDamage, Settings._FL_killdmg, PassiveEffect.ValueModifierTypes.perc_add);
                CheatPassiveEffect(Setting.RB["_BL_Harvest"], PassiveEffects.HarvestCount, Settings._FL_harvest, PassiveEffect.ValueModifierTypes.perc_add);
                CheatPassiveEffect(Setting.RB["_BL_Jmp"], PassiveEffects.JumpStrength, Settings._FL_jmp, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_BL_APM"], PassiveEffects.AttacksPerMinute, Settings._FL_APM, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_BL_Run"], PassiveEffects.RunSpeed, Settings._FL_run, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_instantScrap"], PassiveEffects.ScrappingTime, 0f, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_instantCraft"], PassiveEffects.CraftingTime, 0f, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_instantSmelt"], PassiveEffects.CraftingSmeltTime, 0f, PassiveEffect.ValueModifierTypes.base_set);
                CheatPassiveEffect(Setting.RB["_infDurability"], PassiveEffects.DegradationPerUse, 0f, PassiveEffect.ValueModifierTypes.base_set);
                if (Setting.RB["_LOQuestRewards"])
                {
                    LoopLASTQuestRewards();
                }
                if (Setting.RB["_QuestComplete"])
                {
                    InstantQuestFinish();
                }
                if (Setting.RB["_EtraderOpen"])
                {
                    Trader();
                }
                if ((Setting.RB["_ignoreByAI"] || !Setting.RB["_ignoreByAI"]) && (Objects.ELP != null))
                {
                    Objects.ELP.SetIgnoredByAI(Setting.RB["_ignoreByAI"]);
                }
                if (Settings.noWeaponBob && (Objects.ELP != null))
                {
                    vp_FPWeapon weapon = Objects.ELP.vp_FPWeapon;
                    if (weapon != null)
                    {
                        weapon.BobRate = Vector4.zero;
                        weapon.ShakeAmplitude = Vector3.zero;
                        weapon.RenderingFieldOfView = 120f;
                        weapon.StepForceScale = 0f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (Objects.ELP == null)
                    {
                        return;
                    }
                    Inventory inventory = Objects.ELP.inventory;
                    if (inventory != null)
                    {
                        ItemActionAttack holdingGun = inventory.GetHoldingGun();
                        if (holdingGun != null)
                        {
                            holdingGun.InfiniteAmmo = !holdingGun.InfiniteAmmo;
                        }
                    }
                }
                Input.GetKeyDown(KeyCode.F9);
                if (Setting.RB["CmDm"] || !Setting.RB["CmDm"])
                {
                    CmDm();
                }
                if (Setting.RB["_isEditmode"] || !Setting.RB["_isEditmode"])
                {
                    editMode();
                }
            }
            if ((Setting.SB.Count > 1) && (Setting.SB["_nameScramble"] || !Setting.SB["_nameScramble"]))
            {
                if (Setting.SB["_nameScramble"])
                {
                    if ((Objects._gameManager.persistentLocalPlayer != null) && !Setting.RB["NSCRAM1"])
                    {
                        Objects._gameManager.persistentLocalPlayer.PlayerName = Extras.ScrambleString(Objects._gameManager.persistentLocalPlayer.PlayerName);
                        Objects._gameManager.persistentLocalPlayer.PlayerName = Extras.ScrambleString(Objects._gameManager.persistentLocalPlayer.PlayerName);
                        Setting.RB["NSCRAM1"] = true;
                    }
                    if ((Objects.ELP != null) && !Setting.RB["NSCRAM2"])
                    {
                        if (Objects.ELP.EntityName != null)
                        {
                            Setting.SS["Playername"] = Objects.ELP.EntityName;
                            Objects.ELP.EntityName = Extras.ScrambleString(Objects.ELP.EntityName);
                        }
                        Setting.RB["NSCRAM2"] = true;
                    }
                }
                else if ((!Setting.SB["_nameScramble"] && Setting.RB["NSCRAM1"]) && Setting.RB["NSCRAM2"])
                {
                    Objects.ELP.EntityName = Setting.SS["Playername"];
                    Objects._gameManager.persistentLocalPlayer.PlayerName = Setting.SS["Playername"];
                    Setting.RB["NSCRAM1"] = false;
                    Setting.RB["NSCRAM2"] = false;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Cheat.<>c <>9 = new Cheat.<>c();
            public static Func<ProgressionValue, ProgressionType> <>9__19_0;
            public static Func<IGrouping<ProgressionType, ProgressionValue>, ProgressionType> <>9__19_1;
            public static Func<IGrouping<ProgressionType, ProgressionValue>, List<ProgressionValue>> <>9__19_2;
            public static Func<PassiveEffects, string> <>9__22_0;

            internal string <GetListPassiveEffects>b__22_0(PassiveEffects effect) => 
                effect.ToString();

            internal ProgressionType <ListPGV>b__19_0(ProgressionValue pgv) => 
                pgv.ProgressionClass.Type;

            internal ProgressionType <ListPGV>b__19_1(IGrouping<ProgressionType, ProgressionValue> g) => 
                g.Key;

            internal List<ProgressionValue> <ListPGV>b__19_2(IGrouping<ProgressionType, ProgressionValue> g) => 
                g.ToList<ProgressionValue>();
        }
    }
}

