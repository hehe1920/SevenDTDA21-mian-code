namespace SevenDTDMono.Utils
{
    using UnityEngine;

    [CreateAssetMenu(fileName="ToggleColors", menuName="Custom UI/Toggle Colors")]
    public class ToggleColors : ScriptableObject
    {
        public Color activeColor = Color.green;
        public Color inactiveColor = Color.red;
    }
}

