﻿using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public sealed class PedalToggle : PedalStruct
    {
        public bool toggled { get; set; }
        
        public PedalOption pedal { get; set; }
        public PedalToggle(string text, System.Action<bool> onToggle, bool toggled, Texture2D icon = null, bool locked = false)
        {
            this.text = text;
            this.toggled = toggled;
            this.icon = icon;
            this.triggerEvent = delegate
            {
                //MelonLogger.Msg($"Old state: {this.toggled}, New state: {!this.toggled}");
                this.toggled = !this.toggled;
                if (this.toggled)
                    pedal.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOn);
                else 
                    pedal.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOff);
                onToggle.Invoke(toggled);
            };
            this.Type = PedalType.Toggle;
            this.locked = locked;
        }
    }
}