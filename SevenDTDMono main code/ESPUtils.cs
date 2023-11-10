namespace SevenDTDMono
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    internal static class ESPUtils
    {
        private static readonly GUIStyle __outlineStyle = new GUIStyle();
        private static readonly GUIStyle __style = new GUIStyle();
        private static readonly Texture2D drawingTex = new Texture2D(1, 1);
        private static readonly Material drawMaterial;
        private static Color lastTexColour;
        private static readonly Texture2D whiteTexture = Texture2D.whiteTexture;

        static ESPUtils()
        {
            Material material1 = new Material(Shader.Find("Hidden/Internal-Colored")) {
                hideFlags = HideFlags.HideAndDontSave
            };
            drawMaterial = material1;
            drawMaterial.SetInt("_SrcBlend", 5);
            drawMaterial.SetInt("_DstBlend", 10);
            drawMaterial.SetInt("_Cull", 0);
            drawMaterial.SetInt("_ZWrite", 0);
        }

        public static void CornerBox(Vector2 Head, float Width, float Height, float thickness, Color color, bool outline)
        {
            int num = (int) (Width / 4f);
            int num2 = num;
            if (outline)
            {
                RectFilled((Head.x - (Width / 2f)) - 1f, Head.y - 1f, (float) (num + 2), 3f, Color.black);
                RectFilled((Head.x - (Width / 2f)) - 1f, Head.y - 1f, 3f, (float) (num2 + 2), Color.black);
                RectFilled(((Head.x + (Width / 2f)) - num) - 1f, Head.y - 1f, (float) (num + 2), 3f, Color.black);
                RectFilled((Head.x + (Width / 2f)) - 1f, Head.y - 1f, 3f, (float) (num2 + 2), Color.black);
                RectFilled((Head.x - (Width / 2f)) - 1f, (Head.y + Height) - 4f, (float) (num + 2), 3f, Color.black);
                RectFilled((Head.x - (Width / 2f)) - 1f, ((Head.y + Height) - num2) - 4f, 3f, (float) (num2 + 2), Color.black);
                RectFilled(((Head.x + (Width / 2f)) - num) - 1f, (Head.y + Height) - 4f, (float) (num + 2), 3f, Color.black);
                RectFilled((Head.x + (Width / 2f)) - 1f, ((Head.y + Height) - num2) - 4f, 3f, (float) (num2 + 3), Color.black);
            }
            RectFilled(Head.x - (Width / 2f), Head.y, (float) num, 1f, color);
            RectFilled(Head.x - (Width / 2f), Head.y, 1f, (float) num2, color);
            RectFilled((Head.x + (Width / 2f)) - num, Head.y, (float) num, 1f, color);
            RectFilled(Head.x + (Width / 2f), Head.y, 1f, (float) num2, color);
            RectFilled(Head.x - (Width / 2f), (Head.y + Height) - 3f, (float) num, 1f, color);
            RectFilled(Head.x - (Width / 2f), ((Head.y + Height) - num2) - 3f, 1f, (float) num2, color);
            RectFilled((Head.x + (Width / 2f)) - num, (Head.y + Height) - 3f, (float) num, 1f, color);
            RectFilled(Head.x + (Width / 2f), ((Head.y + Height) - num2) - 3f, 1f, (float) (num2 + 1), color);
        }

        public static void DrawCircle(Color Col, Vector2 Center, float Radius)
        {
            GL.PushMatrix();
            if (!drawMaterial.SetPass(0))
            {
                GL.PopMatrix();
            }
            else
            {
                GL.Begin(1);
                GL.Color(Col);
                for (float i = 0f; i < 6.283185f; i += 0.05f)
                {
                    GL.Vertex(new Vector3((Mathf.Cos(i) * Radius) + Center.x, (Mathf.Sin(i) * Radius) + Center.y));
                    GL.Vertex(new Vector3((Mathf.Cos(i + 0.05f) * Radius) + Center.x, (Mathf.Sin(i + 0.05f) * Radius) + Center.y));
                }
                GL.End();
                GL.PopMatrix();
            }
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Color color2 = GUI.color;
            double num = 57.295779513082323;
            Vector2 vector = end - start;
            float angle = ((float) num) * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
            {
                angle += 180f;
            }
            int num3 = (int) Mathf.Ceil(width / 2f);
            GUIUtility.RotateAroundPivot(angle, start);
            GUI.color = color;
            GUI.DrawTexture(new Rect(start.x, start.y - num3, vector.magnitude, width), Texture2D.whiteTexture, ScaleMode.StretchToFill);
            GUIUtility.RotateAroundPivot(-angle, start);
            GUI.color = color2;
        }

        public static void DrawString(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = 1, int depth = 1)
        {
            __style.fontSize = size;
            __style.richText = true;
            __style.normal.textColor = color;
            __style.fontStyle = fontStyle;
            __outlineStyle.fontSize = size;
            __outlineStyle.richText = true;
            __outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            __outlineStyle.fontStyle = fontStyle;
            GUIContent content = new GUIContent(text);
            GUIContent content2 = new GUIContent(text);
            if (center)
            {
                pos.x -= __style.CalcSize(content).x / 2f;
            }
            switch (depth)
            {
                case 0:
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    return;

                case 1:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    return;

                case 2:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    return;

                case 3:
                    GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), content2, __outlineStyle);
                    GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), content, __style);
                    return;
            }
        }

        public static Color GetHealthColour(float health, float maxHealth)
        {
            Color green = Color.green;
            float num = health / maxHealth;
            if (num >= 0.75f)
            {
                green = Color.green;
            }
            else
            {
                green = Color.yellow;
            }
            if (num <= 0.25f)
            {
                green = Color.red;
            }
            return green;
        }

        public static bool IsOnScreen(Vector3 position) => 
            (((position.y > 0.01f) && (position.y < (Screen.height - 5f))) && (position.z > 0.01f));

        public static void OutlineBox(Vector2 pos, Vector2 size, Color colour)
        {
            Color color = GUI.color;
            GUI.color = colour;
            GUI.DrawTexture(new Rect(pos.x, pos.y, 1f, size.y), whiteTexture);
            GUI.DrawTexture(new Rect(pos.x + size.x, pos.y, 1f, size.y), whiteTexture);
            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, 1f), whiteTexture);
            GUI.DrawTexture(new Rect(pos.x, pos.y + size.y, size.x, 1f), whiteTexture);
            GUI.color = color;
        }

        public static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(x, y, width, height), drawingTex);
        }
    }
}

