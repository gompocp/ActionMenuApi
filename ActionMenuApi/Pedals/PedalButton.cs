using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public sealed class PedalButton : PedalStruct
    {
        public PedalButton(string text, Texture2D icon, System.Action triggerEvent, bool locked = false)
        {
            this.text = text;
            this.icon = icon;
            this.triggerEvent = triggerEvent;
            this.Type = PedalType.Button;
            this.locked = locked;
        }
    }
}