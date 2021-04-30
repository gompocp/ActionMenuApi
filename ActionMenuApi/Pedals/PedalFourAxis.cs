using System;
using ActionMenuApi.Managers;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalFourAxis : PedalStruct
    {
        public PedalOption pedal { get; set; }
        
        public PedalFourAxis(string text, Texture2D icon, Action<Vector2> onUpdate, string topButtonText, 
            string rightButtonText, string downButtonText, string leftButtonText)
        {
            this.text = text;
            this.icon = icon;
            this.triggerEvent = delegate {
                FourAxisPuppetManager.OpenFourAxisMenu(text, onUpdate, pedal);
                FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
            };
            this.Type = PedalType.FourAxisPuppet;
        }
        
    }
}