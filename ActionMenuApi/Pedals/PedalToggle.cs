using ActionMenuApi.Types;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    internal sealed class PedalToggle : PedalStruct
    {
        public bool toggled { get; set; }
        
        public PedalOption pedal { get; set; }
        public PedalToggle(string text, System.Action<bool> onToggle, bool toggled, Texture2D icon = null)
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
        }
    }
}