using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalToggle : PedalStruct
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
                this.toggled = !this.toggled;
                if (this.toggled)
                    pedal.button.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOn;
                else 
                    pedal.button.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOff;
            };
            this.Type = PedalType.Toggle;
        }
    }
}