using System;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalFourAxis : PedalStruct
    {
        public Action<Vector2> onClose;
        public Vector2 currentValue;
        public Action<Vector2> onUpdate;
        public PedalOption pedal { get; set; }
        
        public PedalFourAxis(string text, Vector2 startingValue, Texture2D icon, Action<Vector2> onUpdate)
        {
            this.text = text;
            this.currentValue = startingValue;
            this.icon = icon;
            this.onUpdate = onUpdate;
            this.onClose = delegate(Vector2 v)
            {
                currentValue = v;
                //MelonLogger.Msg(v.ToString());
            };
            this.triggerEvent = delegate {
                FourAxisPuppetManager.OpenFourAxisMenu(startingValue, onClose, text, onUpdate);
            };
            this.Type = PedalType.FourAxisPuppet;
        }
        
    }
}