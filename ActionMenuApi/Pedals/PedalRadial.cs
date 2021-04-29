using System;
using UnityEngine;
using ActionMenuApi.Managers;

namespace ActionMenuApi.Pedals
{
    public class PedalRadial : PedalStruct
    {
        public System.Action<float> onClose;
        public float currentValue;
        public Action<float> onUpdate;
        public PedalOption pedal { get; set; }
        
        public PedalRadial(string text, float startingValue, Texture2D icon, Action<float> onUpdate)
        {
            this.text = text;
            this.currentValue = startingValue;
            this.icon = icon;
            this.onUpdate = onUpdate;
            this.onClose = delegate(float f)
            {
                currentValue = f;
                pedal.field_Public_ActionButton_0.prop_String_1 = $"{Math.Round(currentValue*100f)}%";
            };
            this.triggerEvent = delegate {
                RadialPuppetManager.OpenRadialMenu(currentValue, onClose, text, pedal);
            };
            this.Type = PedalType.RadialPuppet;
        }
    }
}