using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    internal sealed class PedalButton : PedalStruct
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