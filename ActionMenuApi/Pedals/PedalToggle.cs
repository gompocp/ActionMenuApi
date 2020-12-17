using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalToggle : PedalStruct
    {
        public bool toggled { get; set; }
        public PedalToggle(string text, System.Action<bool> onToggle, Texture2D icon = null)
        {
            this.text = text;   
            this.icon = icon;
            this.triggerEvent = triggerEvent;
            this.Type = PedalType.Toggle;
        }
    }
}