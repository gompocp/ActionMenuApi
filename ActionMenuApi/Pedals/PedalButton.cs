using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalButton : PedalStruct
    {
        public PedalButton(string text, Texture2D icon, System.Action triggerEvent)
        {
            this.text = text;
            this.icon = icon;
            this.triggerEvent = triggerEvent;
            this.Type = PedalType.Button;
        }
    }
}