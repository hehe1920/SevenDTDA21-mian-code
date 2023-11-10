namespace SevenDTDMono
{
    using System;
    using UnityEngine;

    public class Visuals : MonoBehaviour
    {
        private int _Color;
        private Material chamsMaterial;
        private float lastChamTime;

        private void ApplyChams(Entity entity, Color color)
        {
            foreach (Renderer renderer in entity.GetComponentsInChildren<Renderer>())
            {
                renderer.material = this.chamsMaterial;
                renderer.material.SetColor(this._Color, color);
            }
        }

        private void Start()
        {
            this.lastChamTime = Time.time + 10f;
            Material material1 = new Material(Shader.Find("Hidden/Internal-Colored")) {
                hideFlags = HideFlags.HideAndDontSave
            };
            this.chamsMaterial = material1;
            this._Color = Shader.PropertyToID("_Color");
            this.chamsMaterial.SetInt("_SrcBlend", 5);
            this.chamsMaterial.SetInt("_DstBlend", 10);
            this.chamsMaterial.SetInt("_Cull", 0);
            this.chamsMaterial.SetInt("_ZTest", 8);
            this.chamsMaterial.SetInt("_ZWrite", 0);
            this.chamsMaterial.SetColor(this._Color, Color.magenta);
        }

        private void Update()
        {
            if ((Time.time >= this.lastChamTime) && Settings.chams)
            {
                foreach (Entity entity in Object.FindObjectsOfType<Entity>())
                {
                    if (entity != null)
                    {
                        switch (entity.entityType)
                        {
                            case EntityType.Unknown:
                                this.ApplyChams(entity, Color.white);
                                break;

                            case EntityType.Player:
                                this.ApplyChams(entity, Color.cyan);
                                break;

                            case EntityType.Zombie:
                                this.ApplyChams(entity, Color.red);
                                break;

                            case EntityType.Animal:
                                this.ApplyChams(entity, Color.yellow);
                                break;
                        }
                    }
                }
                this.lastChamTime = Time.time + 10f;
            }
        }
    }
}

