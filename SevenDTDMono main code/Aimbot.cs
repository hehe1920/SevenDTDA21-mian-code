namespace SevenDTDMono
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class Aimbot : MonoBehaviour
    {
        public static bool infiniteAmmo;

        private void _Aimbot()
        {
            float num = 9999f;
            Vector2 zero = Vector2.zero;
            foreach (EntityZombie zombie in Objects._listZombies)
            {
                if ((zombie != null) && zombie.IsAlive())
                {
                    Vector3 bellyPosition = zombie.emodel.GetBellyPosition();
                    Vector3 position = ESP.mainCam.WorldToScreenPoint(bellyPosition);
                    if ((Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(position.x, position.y)) <= 150f) && ESPUtils.IsOnScreen(position))
                    {
                        float num2 = Math.Abs(Vector2.Distance(new Vector2(position.x, Screen.height - position.y), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2))));
                        if (num2 < num)
                        {
                            num = num2;
                            zero = new Vector2(position.x, Screen.height - position.y);
                        }
                    }
                }
            }
            foreach (EntityPlayer player in Objects.PlayerList)
            {
                if ((player != null) && player.IsAlive())
                {
                    Vector3 vector4 = player.emodel.GetHeadTransform().position;
                    Vector3 vector5 = Camera.main.WorldToScreenPoint(vector4);
                    if ((Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector5.x, vector5.y)) <= 150f) && IsOnScreen(vector5))
                    {
                        float num3 = Math.Abs(Vector2.Distance(new Vector2(vector5.x, Screen.height - vector5.y), new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2))));
                        if (num3 < num)
                        {
                            num = num3;
                            zero = new Vector2(vector5.x, Screen.height - vector5.y);
                        }
                    }
                }
            }
            if (zero != Vector2.zero)
            {
                double num4 = zero.x - (((float) Screen.width) / 2f);
                double num5 = zero.y - (((float) Screen.height) / 2f);
                num4 /= 10.0;
                num5 /= 10.0;
                mouse_event(1, (int) num4, (int) num5, 0, 0);
            }
        }

        private void _MagicBullet()
        {
            EntityZombie killedEntity = null;
            EntityPlayer player = null;
            foreach (EntityZombie zombie2 in Objects._listZombies)
            {
                if ((zombie2 != null) && zombie2.IsAlive())
                {
                    Vector3 position = zombie2.emodel.GetHeadTransform().position;
                    Vector3 vector2 = ESP.mainCam.WorldToScreenPoint(position);
                    if (Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector2.x, vector2.y)) <= 120f)
                    {
                        killedEntity = zombie2;
                    }
                }
            }
            foreach (EntityPlayer player2 in Objects.PlayerList)
            {
                if (((player2 != null) && player2.IsAlive()) && (player2 != Objects.ELP))
                {
                    Vector3 vector3 = player2.emodel.GetHeadTransform().position;
                    Vector3 vector4 = ESP.mainCam.WorldToScreenPoint(vector3);
                    if (Vector2.Distance(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2)), new Vector2(vector4.x, vector4.y)) <= 120f)
                    {
                        player = player2;
                    }
                }
            }
            if (player != null)
            {
                DamageSource source = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Concuss);
                killedEntity.DamageEntity(source, 100, false, 1f);
                killedEntity.AwardKill(Objects.ELP);
            }
            if (killedEntity != null)
            {
                DamageSource source1 = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Concuss) {
                    CreatorEntityId = Objects.ELP.entityId
                };
                killedEntity.DamageEntity(source1, 100, false, 1f);
                killedEntity.AwardKill(Objects.ELP);
                Objects.ELP.AddKillXP(killedEntity, 1f);
            }
        }

        public static bool IsOnScreen(Vector3 position) => 
            (((position.y > 0.01f) && (position.y < (Screen.height - 5f))) && (position.z > 0.01f));

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) && Settings.magicBullet)
            {
                this._MagicBullet();
            }
            if (Input.GetKey(KeyCode.LeftAlt) && Settings.aimbot)
            {
                this._Aimbot();
            }
        }
    }
}

