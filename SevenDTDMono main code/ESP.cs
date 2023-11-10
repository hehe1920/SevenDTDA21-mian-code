namespace SevenDTDMono
{
    using System;
    using UnityEngine;

    public class ESP : MonoBehaviour
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
                if (Settings.fovCircle)
                {
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 149f);
                    ESPUtils.DrawCircle(Color.black, new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 151f);
                    ESPUtils.DrawCircle((Color) new Color32(30, 0x90, 0xff, 0xff), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), 150f);
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
                if ((Objects._listZombies.Count > 0) && ((Settings.zombieName || Settings.zombieBox) || Settings.zombieHealth))
                {
                    foreach (EntityZombie zombie in Objects._listZombies)
                    {
                        if ((zombie != null) && zombie.IsAlive())
                        {
                            Vector3 position = mainCam.WorldToScreenPoint(zombie.transform.position);
                            if (ESPUtils.IsOnScreen(position))
                            {
                                Vector3 vector6 = mainCam.WorldToScreenPoint(zombie.emodel.GetHeadTransform().position);
                                float y = Mathf.Abs((float) (vector6.y - position.y));
                                float x = position.x - (y * 0.3f);
                                float num3 = Screen.height - vector6.y;
                                if (Settings.zombieBox)
                                {
                                    ESPUtils.OutlineBox(new Vector2(x - 1f, num3 - 1f), new Vector2((y / 2f) + 2f, y + 2f), this.blackCol);
                                    ESPUtils.OutlineBox(new Vector2(x, num3), new Vector2(y / 2f, y), this.entityBoxCol);
                                    ESPUtils.OutlineBox(new Vector2(x + 1f, num3 + 1f), new Vector2((y / 2f) - 2f, y - 2f), this.blackCol);
                                }
                                else if (Settings.zombieCornerBox)
                                {
                                    ESPUtils.CornerBox(new Vector2(vector6.x, num3), y / 2f, y, 2f, this.entityBoxCol, true);
                                }
                                if (Settings.zombieName)
                                {
                                    ESPUtils.DrawString(new Vector2(position.x, (Screen.height - position.y) + 8f), zombie.EntityName.Replace("zombie", "Zombie_"), Color.red, true, 12, FontStyle.Normal, 1);
                                }
                                if (Settings.zombieHealth)
                                {
                                    float health = zombie.Health;
                                    int maxHealth = zombie.GetMaxHealth();
                                    float num6 = health / ((float) maxHealth);
                                    float height = y * num6;
                                    Color healthColour = ESPUtils.GetHealthColour(health, (float) maxHealth);
                                    ESPUtils.RectFilled(x - 5f, num3, 4f, y, this.blackCol);
                                    ESPUtils.RectFilled(x - 4f, ((num3 + y) - height) - 1f, 2f, height, healthColour);
                                }
                            }
                        }
                    }
                }
                if ((Objects.PlayerList.Count > 1) && ((Settings.playerName || Settings.playerBox) || Settings.playerHealth))
                {
                    foreach (EntityPlayer player in Objects.PlayerList)
                    {
                        if (((player != null) && (player != Objects.ELP)) && player.IsAlive())
                        {
                            Vector3 vector7 = mainCam.WorldToScreenPoint(player.transform.position);
                            if (ESPUtils.IsOnScreen(vector7))
                            {
                                Vector3 vector8 = mainCam.WorldToScreenPoint(player.emodel.GetHeadTransform().position);
                                float num8 = Mathf.Abs((float) (vector8.y - vector7.y));
                                float num9 = vector7.x - (num8 * 0.3f);
                                float num10 = Screen.height - vector8.y;
                                if (Settings.playerBox)
                                {
                                    ESPUtils.OutlineBox(new Vector2(num9 - 1f, num10 - 1f), new Vector2((num8 / 2f) + 2f, num8 + 2f), this.blackCol);
                                    ESPUtils.OutlineBox(new Vector2(num9, num10), new Vector2(num8 / 2f, num8), this.entityBoxCol);
                                    ESPUtils.OutlineBox(new Vector2(num9 + 1f, num10 + 1f), new Vector2((num8 / 2f) - 2f, num8 - 2f), this.blackCol);
                                }
                                else if (Settings.playerCornerBox)
                                {
                                    ESPUtils.CornerBox(new Vector2(vector8.x, num10), num8 / 2f, num8, 2f, this.entityBoxCol, true);
                                }
                                if (Settings.playerName)
                                {
                                    ESPUtils.DrawString(new Vector2(vector7.x, (Screen.height - vector7.y) + 8f), player.EntityName, Color.red, true, 12, FontStyle.Normal, 1);
                                }
                                if (Settings.playerHealth)
                                {
                                    float num11 = player.Health;
                                    int num12 = player.GetMaxHealth();
                                    float num13 = num11 / ((float) num12);
                                    float num14 = num8 * num13;
                                    Color color = ESPUtils.GetHealthColour(num11, (float) num12);
                                    ESPUtils.RectFilled(num9 - 5f, num10, 4f, num8, this.blackCol);
                                    ESPUtils.RectFilled(num9 - 4f, ((num10 + num8) - num14) - 1f, 2f, num14, color);
                                }
                            }
                        }
                    }
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

        private void Update()
        {
            if (Settings.zombieCornerBox)
            {
                Settings.zombieBox = false;
            }
            else if (Settings.zombieBox && Settings.zombieCornerBox)
            {
                Settings.zombieCornerBox = false;
            }
            if (Settings.playerCornerBox)
            {
                Settings.playerBox = false;
            }
            else if (Settings.playerBox && Settings.playerCornerBox)
            {
                Settings.playerCornerBox = false;
            }
            if (Objects.ELP != null)
            {
                Objects.ELP.weaponCrossHairAlpha = Settings.crosshair ? 0f : 255f;
            }
        }
    }
}

