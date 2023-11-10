namespace SevenDTDMono
{
    using System;
    using UnityEngine;

    internal class Render : MonoBehaviour
    {
        private Color blackCol;
        private Color crosshairCol;
        private readonly float crosshairScale = 14f;
        private Color entityBoxCol;
        private readonly float lineThickness = 1.75f;
        public static Camera mainCam;

        private void OnGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                if (mainCam == null)
                {
                    mainCam = Camera.main;
                }
                if (Settings.crosshair)
                {
                    Vector2 start = new Vector2((Screen.width / 2) - this.crosshairScale, (float) (Screen.height / 2));
                    Vector2 end = new Vector2((Screen.width / 2) + this.crosshairScale, (float) (Screen.height / 2));
                    Vector2 vector3 = new Vector2((float) (Screen.width / 2), (Screen.height / 2) - this.crosshairScale);
                    Vector2 vector4 = new Vector2((float) (Screen.width / 2), (Screen.height / 2) + this.crosshairScale);
                    ESPUtils.DrawLine(start, end, this.crosshairCol, this.lineThickness);
                    ESPUtils.DrawLine(vector3, vector4, this.crosshairCol, this.lineThickness);
                }
                if (Settings.fovCircle)
                {
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 149f);
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 151f);
                    ESPUtils.DrawCircle((Color) new Color32(30, 0x90, 0xff, 0xff), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 150f);
                }
            }
        }

        private void Start()
        {
            mainCam = Camera.main;
            this.blackCol = new Color(0f, 0f, 0f, 120f);
            this.entityBoxCol = new Color(0.42f, 0.36f, 0.9f, 1f);
            this.crosshairCol = (Color) new Color32(30, 0x90, 0xff, 0xff);
        }
    }
}

