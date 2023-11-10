namespace SevenDTDMono
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;
    using UnityEngine;

    public class CBuffs : MonoBehaviour
    {
        public static void LoadCustomXml(string rss)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Debug.LogWarning($"Searching in {executingAssembly}");
            using (Stream stream = executingAssembly.GetManifestResourceStream(rss))
            {
                Debug.LogWarning("Looking for " + rss + "...");
                if (stream != null)
                {
                    Debug.LogWarning(rss + " was found, loading..");
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        XElement element = XElement.Load(reader);
                        Debug.LogWarning("Adding Buffs from " + rss);
                        foreach (XElement element2 in element.Elements())
                        {
                            ParseAddBuff(element2);
                        }
                        return;
                    }
                }
                Debug.LogWarning(rss + " Could not be found!!");
            }
        }

        public static void ParseAddBuff(XElement _element)
        {
            BuffClass class2 = new BuffClass("");
            if (!_element.HasAttribute("name"))
            {
                throw new Exception("buff must have an name!");
            }
            class2.Name = _element.GetAttribute("name").ToLower();
            class2.NameTag = FastTags.Parse(_element.GetAttribute("name"));
            if (_element.HasAttribute("name_key"))
            {
                class2.LocalizedName = Localization.Get(_element.GetAttribute("name_key"));
            }
            else
            {
                class2.LocalizedName = Localization.Get(class2.Name);
            }
            if (_element.HasAttribute("description_key"))
            {
                class2.DescriptionKey = _element.GetAttribute("description_key");
                class2.Description = Localization.Get(class2.DescriptionKey);
            }
            if (_element.HasAttribute("tooltip_key"))
            {
                class2.TooltipKey = _element.GetAttribute("tooltip_key");
                class2.Tooltip = Localization.Get(class2.TooltipKey);
            }
            if (_element.HasAttribute("icon"))
            {
                class2.Icon = _element.GetAttribute("icon");
            }
            if (_element.HasAttribute("hidden"))
            {
                class2.Hidden = StringParsers.ParseBool(_element.GetAttribute("hidden"), 0, -1, true);
            }
            else
            {
                class2.Hidden = false;
            }
            if (_element.HasAttribute("showonhud"))
            {
                class2.ShowOnHUD = StringParsers.ParseBool(_element.GetAttribute("showonhud"), 0, -1, true);
            }
            else
            {
                class2.ShowOnHUD = true;
            }
            if (_element.HasAttribute("update_rate"))
            {
                class2.UpdateRate = StringParsers.ParseFloat(_element.GetAttribute("update_rate"), 0, -1, NumberStyles.Any);
            }
            else
            {
                class2.UpdateRate = 1f;
            }
            if (_element.HasAttribute("remove_on_death"))
            {
                class2.RemoveOnDeath = StringParsers.ParseBool(_element.GetAttribute("remove_on_death"), 0, -1, true);
            }
            if (_element.HasAttribute("display_type"))
            {
                class2.DisplayType = EnumUtils.Parse<EnumEntityUINotificationDisplayMode>(_element.GetAttribute("display_type"), false);
            }
            else
            {
                class2.DisplayType = EnumEntityUINotificationDisplayMode.IconOnly;
            }
            if (_element.HasAttribute("icon_color"))
            {
                class2.IconColor = StringParsers.ParseColor32(_element.GetAttribute("icon_color"));
            }
            else
            {
                class2.IconColor = Color.white;
            }
            if (_element.HasAttribute("icon_blink"))
            {
                class2.IconBlink = StringParsers.ParseBool(_element.GetAttribute("icon_blink"), 0, -1, true);
            }
            class2.DamageSource = EnumDamageSource.Internal;
            class2.DamageType = EnumDamageTypes.None;
            class2.StackType = BuffEffectStackTypes.Replace;
            class2.DurationMax = 0f;
            foreach (XElement element in _element.Elements())
            {
                if ((element.Name == "display_value") && element.HasAttribute("value"))
                {
                    class2.DisplayValueCVar = element.GetAttribute("value");
                }
                if ((element.Name == "display_value_key") && element.HasAttribute("value"))
                {
                    class2.DisplayValueKey = element.GetAttribute("value");
                }
                if (((element.Name == "display_value_format") && element.HasAttribute("value")) && !Enum.TryParse<BuffClass.CVarDisplayFormat>(element.GetAttribute("value"), true, out class2.DisplayValueFormat))
                {
                    class2.DisplayValueFormat = BuffClass.CVarDisplayFormat.None;
                }
                if ((element.Name == "damage_source") && element.HasAttribute("value"))
                {
                    class2.DamageSource = EnumUtils.Parse<EnumDamageSource>(element.GetAttribute("value"), true);
                }
                if ((element.Name == "damage_type") && element.HasAttribute("value"))
                {
                    class2.DamageType = EnumUtils.Parse<EnumDamageTypes>(element.GetAttribute("value"), true);
                }
                if ((element.Name == "stack_type") && element.HasAttribute("value"))
                {
                    class2.StackType = EnumUtils.Parse<BuffEffectStackTypes>(element.GetAttribute("value"), true);
                }
                if ((element.Name == "tags") && element.HasAttribute("value"))
                {
                    class2.Tags = FastTags.Parse(element.GetAttribute("value"));
                }
                if (element.Name == "cures")
                {
                    if (element.HasAttribute("value"))
                    {
                        char[] separator = new char[] { ',' };
                        class2.Cures = new List<string>(element.GetAttribute("value").Split(separator));
                    }
                    else
                    {
                        class2.Cures = new List<string>();
                    }
                }
                else
                {
                    class2.Cures = new List<string>();
                }
                if ((element.Name == "duration") && element.HasAttribute("value"))
                {
                    class2.DurationMax = StringParsers.ParseFloat(element.GetAttribute("value"), 0, -1, NumberStyles.Any);
                }
                if ((element.Name == "update_rate") && element.HasAttribute("value"))
                {
                    class2.UpdateRate = StringParsers.ParseFloat(element.GetAttribute("value"), 0, -1, NumberStyles.Any);
                }
                if ((element.Name == "remove_on_death") && element.HasAttribute("value"))
                {
                    class2.RemoveOnDeath = StringParsers.ParseBool(element.GetAttribute("value"), 0, -1, true);
                }
                if (element.Name == "requirement")
                {
                    IRequirement item = RequirementBase.ParseRequirement(_element);
                    if (item != null)
                    {
                        class2.Requirements.Add(item);
                    }
                }
                if (element.Name == "requirements")
                {
                    parseBuffRequirements(class2, element);
                }
                if (element.Name == "effect_group")
                {
                    class2.Effects = MinEffectController.ParseXml(_element, null, MinEffectController.SourceParentType.BuffClass, class2.Name);
                }
            }
            Debug.LogWarning("adding " + class2.Name + " to O._listCbuffs");
            Objects._listCbuffs.Add(class2);
            Debug.LogWarning("adding " + class2.Name + " to BuffManager");
            BuffManager.AddBuff(class2);
        }

        private static void parseBuffRequirements(BuffClass _buff, XElement _element)
        {
            if (_element.HasAttribute("compare_type") && _element.GetAttribute("compare_type").EqualsCaseInsensitive("or"))
            {
                _buff.OrCompare = true;
            }
            foreach (XElement element in _element.Elements("requirement"))
            {
                IRequirement item = RequirementBase.ParseRequirement(_element);
                if (item != null)
                {
                    _buff.Requirements.Add(item);
                }
            }
        }
    }
}

