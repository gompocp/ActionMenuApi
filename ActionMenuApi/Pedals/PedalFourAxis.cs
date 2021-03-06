﻿using System;
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
        
        public PedalFourAxis(string text, Vector2 startingValue, Texture2D icon, Action<Vector2> onUpdate, string topButtonText, 
            string rightButtonText, string downButtonText, string leftButtonText)
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
                FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
            };
            this.Type = PedalType.FourAxisPuppet;
        }
        
    }
}