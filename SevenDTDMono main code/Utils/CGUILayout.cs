namespace SevenDTDMono.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public static class CGUILayout
    {
        private static Color Active = Color.green;
        private static Color Hover = Color.cyan;
        private static Color Inactive = Color.yellow;
        private static bool isResizing = false;
        private static Vector2 mouseStartPos;
        private static Rect originalWinRect;
        public static Dictionary<string, bool> RBu = new Dictionary<string, bool>();
        public static Dictionary<string, bool> SBu = new Dictionary<string, bool>();

        public static void BeginHorizontal(Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndHorizontal();
        }

        public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
            GUILayout.EndHorizontal();
        }

        public static void BeginHorizontal(GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndHorizontal();
        }

        public static void BeginHorizontal(string _string, GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(_string, style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndHorizontal();
        }

        public static void BeginHorizontal(GUIContent content, GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(content, style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndHorizontal();
        }

        public static void BeginScrollView(Vector2 vector2, Action content, params GUILayoutOption[] options)
        {
            vector2 = GUILayout.BeginScrollView(vector2, options);
            if (content != null)
            {
                content();
            }
            GUILayout.EndScrollView();
        }

        public static void BeginVertical(Action content)
        {
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            if (content != null)
            {
                content();
            }
            GUILayout.EndVertical();
        }

        public static void BeginVertical(Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndVertical();
        }

        public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
            GUILayout.EndVertical();
        }

        public static void BeginVertical(GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndVertical();
        }

        public static void BeginVertical(string _string, GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(_string, style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndVertical();
        }

        public static void BeginVertical(GUIContent content, GUIStyle style, Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(content, style, options);
            if (contentActions != null)
            {
                contentActions();
            }
            GUILayout.EndVertical();
        }

        public static bool Button(string label, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            return GUILayout.Button(label, style, options);
        }

        public static bool Button(string label, Action onClickAction, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag && (onClickAction != null))
            {
                onClickAction();
            }
            return flag;
        }

        public static bool Button(ref bool buttonState, string label, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            Color color = buttonState ? Active : Inactive;
            style.normal.textColor = color;
            style.hover.textColor = Hover;
            style.active.textColor = color;
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                buttonState = !buttonState;
            }
            return flag;
        }

        public static bool Button(string label, ref int currentIndex, params GUILayoutOption[] buttonOptions)
        {
            bool flag = GUILayout.Button(label + " " + ((Enum) Enum.ToObject(typeof(PassiveEffect.ValueModifierTypes), (int) currentIndex)).ToString(), buttonOptions);
            if (flag)
            {
                currentIndex = (currentIndex + 1) % Enum.GetValues(typeof(PassiveEffect.ValueModifierTypes)).Length;
            }
            return flag;
        }

        public static bool Button(string label, Action onClickAction, ref bool toggle, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = toggle ? Active : Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag && (onClickAction != null))
            {
                onClickAction();
                toggle = !toggle;
            }
            return flag;
        }

        public static bool Button(string label, ref bool toggle, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = toggle ? Active : Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                toggle = !toggle;
                if (onClickAction != null)
                {
                    onClickAction();
                }
            }
            return flag;
        }

        public static bool Button(string label, bool toggle, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = toggle ? Active : Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                toggle = !toggle;
                if (onClickAction != null)
                {
                    onClickAction();
                }
            }
            return flag;
        }

        public static bool Button(ref bool buttonState, string label, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            Color color = buttonState ? Active : Inactive;
            style.normal.textColor = color;
            style.hover.textColor = hover;
            style.active.textColor = color;
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                buttonState = !buttonState;
            }
            return flag;
        }

        public static void Button(ref Dictionary<string, bool> buttonStates, string label, Action onClickAction = null, params GUILayoutOption[] options)
        {
            if (!buttonStates.ContainsKey(label))
            {
                buttonStates[label] = false;
            }
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = buttonStates[label] ? Color.green : Color.red },
                active = { textColor = Color.green },
                hover = { textColor = Color.green }
            };
            if (GUILayout.Button(label, style, options))
            {
                buttonStates[label] = !buttonStates[label];
                if (onClickAction != null)
                {
                    onClickAction();
                }
            }
        }

        public static bool Button(string label, Color inactive, Color active, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = inactive },
                active = { textColor = active },
                hover = { textColor = Hover }
            };
            return GUILayout.Button(label, style, options);
        }

        public static bool Button(string label, Dictionary<string, bool> boolDictionary, string key, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = boolDictionary[key] ? Active : Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                boolDictionary[key] = !boolDictionary[key];
                if (onClickAction != null)
                {
                    onClickAction();
                }
            }
            return flag;
        }

        public static bool Button(ref bool buttonState, string label, Color active, Color inactive, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            Color color = buttonState ? active : inactive;
            style.normal.textColor = color;
            style.hover.textColor = Hover;
            style.active.textColor = color;
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                buttonState = !buttonState;
            }
            return flag;
        }

        public static bool Button(string label, Color inactive, Color active, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = inactive },
                active = { textColor = active },
                hover = { textColor = hover }
            };
            return GUILayout.Button(label, style, options);
        }

        public static bool Button(ref bool buttonState, string label, Color active, Color inactive, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            Color color = buttonState ? active : inactive;
            style.normal.textColor = color;
            style.hover.textColor = hover;
            style.active.textColor = color;
            bool flag = GUILayout.Button(label, style, options);
            if (flag)
            {
                buttonState = !buttonState;
            }
            return flag;
        }

        public static bool Button1(string label, Action[] onClickActions, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                normal = { textColor = Inactive },
                active = { textColor = Active },
                hover = { textColor = Hover }
            };
            bool flag = GUILayout.Button(label, style, options);
            if (flag && (onClickActions != null))
            {
                foreach (Action action in onClickActions)
                {
                    if (action != null)
                    {
                        action();
                    }
                }
            }
            return flag;
        }

        public static bool CustomDropDown(bool toggle, string label, Action content, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box) {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            };
            if (toggle)
            {
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.green;
            }
            else
            {
                style.fontStyle = FontStyle.Italic;
                style.normal.textColor = Color.yellow;
            }
            Rect position = GUILayoutUtility.GetRect(30f, 20f, style);
            Rect rect2 = new Rect(position.y, position.x, 10f, 20f);
            GUI.Box(position, label, style);
            Event current = Event.current;
            EventType type = current.type;
            if ((current.type == EventType.MouseDown) && position.Contains(current.mousePosition))
            {
                toggle = !toggle;
                current.Use();
            }
            if (toggle && (content != null))
            {
                content();
            }
            return toggle;
        }

        public static bool CustomDropDown(string label, Action content, float width, params GUILayoutOption[] options)
        {
            bool flag = false;
            GUIStyle style = new GUIStyle(GUI.skin.box) {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            };
            if (flag)
            {
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.green;
            }
            else
            {
                style.fontStyle = FontStyle.Italic;
                style.normal.textColor = Color.yellow;
            }
            Rect position = GUILayoutUtility.GetRect(width, 20f, style);
            Rect rect2 = new Rect(position.y, position.x, 10f, 20f);
            GUI.Box(position, label, style);
            Event current = Event.current;
            EventType type = current.type;
            if ((current.type == EventType.MouseDown) && position.Contains(current.mousePosition))
            {
                flag = !flag;
                current.Use();
            }
            if (flag && (content != null))
            {
                content();
            }
            return flag;
        }

        public static void CustomDropDown(string label, Action content, ref bool toggle, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box) {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            };
            if (toggle)
            {
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.green;
            }
            else
            {
                style.fontStyle = FontStyle.Italic;
                style.normal.textColor = Color.yellow;
            }
            Rect position = GUILayoutUtility.GetRect(30f, 20f, style);
            GUI.Box(position, label, style);
            if ((Event.current.type == EventType.MouseDown) && position.Contains(Event.current.mousePosition))
            {
                toggle = !toggle;
                Event.current.Use();
            }
            if (toggle && (content != null))
            {
                content();
            }
        }

        public static void DrawLine(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = Color.white;
        }

        public static void DropDownForMethods(string label, Action content, ref bool toggle, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.button) {
                alignment = TextAnchor.MiddleRight,
                fontSize = 15
            };
            if (!toggle)
            {
                style.fontStyle = FontStyle.Bold;
            }
            else
            {
                style.fontStyle = FontStyle.BoldAndItalic;
            }
            Rect position = GUILayoutUtility.GetRect(300f, 30f, style);
            float height = 10f;
            Rect rect = new Rect(position.x, position.y + ((position.height - height) * 0.5f), 30f, height);
            DrawLine(rect, toggle ? Color.green : Color.yellow);
            GUI.Box(position, label, style);
            if ((Event.current.type == EventType.MouseDown) && position.Contains(Event.current.mousePosition))
            {
                toggle = !toggle;
                Event.current.Use();
            }
            if (toggle)
            {
                BeginVertical(GUI.skin.box, delegate {
                    if (content != null)
                    {
                        Action action1 = content;
                        action1();
                    }
                    else
                    {
                        Action expressionStack_9_0 = content;
                    }
                }, Array.Empty<GUILayoutOption>());
            }
        }

        public static bool FoldableMenuHorizontal(bool display, string label, Action content, float width, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box) {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            };
            if (display)
            {
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.green;
            }
            else
            {
                style.fontStyle = FontStyle.Italic;
                style.normal.textColor = Color.yellow;
            }
            Rect position = GUILayoutUtility.GetRect(width, 20f, style);
            Rect rect2 = new Rect(position.y, position.x, 10f, 20f);
            float height = 10f;
            Rect rect = new Rect(position.x, position.y + ((position.height - height) * 0.5f), 30f, height);
            DrawLine(rect, display ? Color.green : Color.yellow);
            GUI.Box(position, label, style);
            Event current = Event.current;
            EventType type = current.type;
            if ((current.type == EventType.MouseDown) && position.Contains(current.mousePosition))
            {
                display = !display;
                current.Use();
            }
            if (display && (content != null))
            {
                content();
            }
            return display;
        }

        public static float HorizontalScrollbarWithLabel(string label, ref float Modifier, float rightMaxValue)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label) {
                alignment = TextAnchor.LowerCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, -4, 0)
            };
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(100f) };
            GUILayout.Label(label, style, options);
            Modifier = GUILayout.HorizontalScrollbar(Modifier, 0f, 0f, rightMaxValue, Array.Empty<GUILayoutOption>());
            GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.MaxWidth(40f) };
            GUILayout.Label(((float) Modifier).ToString("F1"), style, optionArray2);
            GUILayout.EndHorizontal();
            return Modifier;
        }

        public static bool RButton(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            bool flag = Setting.RB.ContainsKey(boolKey) ? Setting.RB[boolKey] : false;
            if (Button(label, Setting.RB[boolKey], onClickAction, options))
            {
                Setting.RB[boolKey] = !flag;
            }
            return Setting.RB[boolKey];
        }

        public static bool SButton(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            if (!Setting.SB.ContainsKey(boolKey))
            {
                Setting.SB[boolKey] = false;
            }
            bool flag = Setting.SB.ContainsKey(boolKey) ? Setting.SB[boolKey] : false;
            if (Button(label, Setting.SB[boolKey], onClickAction, options))
            {
                Setting.SB[boolKey] = !flag;
            }
            return Setting.SB[boolKey];
        }

        public static bool Toggle(bool value, string label, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle) {
                normal = { textColor = Inactive },
                onNormal = { textColor = Active },
                active = { textColor = Active },
                onActive = { textColor = Inactive },
                hover = { textColor = Hover },
                onHover = { textColor = Hover }
            };
            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.Width(120f) };
            value = GUILayout.Toggle(value, label, style, optionArray1);
            return value;
        }

        public static bool Toggle(bool value, string label, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle) {
                normal = { textColor = Inactive },
                onNormal = { textColor = Active },
                active = { textColor = Active },
                onActive = { textColor = Inactive },
                hover = { textColor = hover },
                onHover = { textColor = hover }
            };
            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.Width(120f) };
            value = GUILayout.Toggle(value, label, style, optionArray1);
            return value;
        }

        public static bool Toggle(bool value, string label, Color active, Color inactive, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle) {
                normal = { textColor = inactive },
                onNormal = { textColor = active },
                active = { textColor = active },
                onActive = { textColor = inactive },
                hover = { textColor = Hover },
                onHover = { textColor = Hover }
            };
            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.Width(120f) };
            value = GUILayout.Toggle(value, label, style, optionArray1);
            return value;
        }

        public static bool Toggle(bool value, string label, Color active, Color inactive, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle) {
                normal = { textColor = inactive },
                onNormal = { textColor = active },
                active = { textColor = active },
                onActive = { textColor = inactive },
                hover = { textColor = hover },
                onHover = { textColor = hover }
            };
            GUILayoutOption[] optionArray1 = new GUILayoutOption[] { GUILayout.Width(120f) };
            value = GUILayout.Toggle(value, label, style, optionArray1);
            return value;
        }

        public static int Toolbar4(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
        {
            int num = selected;
            int length = texts.Length;
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            for (int i = 0; i < length; i++)
            {
                Rect position = GUILayoutUtility.GetRect(new GUIContent(texts[i]), style, options);
                Color color = (i == selected) ? Active : Inactive;
                style.normal.textColor = color;
                style.hover.textColor = Hover;
                if (GUI.Button(position, texts[i], style))
                {
                    num = i;
                }
            }
            GUILayout.EndHorizontal();
            if (num != selected)
            {
                return num;
            }
            return selected;
        }
    }
}

