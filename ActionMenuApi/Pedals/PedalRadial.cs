﻿using System;
using ActionMenuApi.Managers;
using ActionMenuApi.Types;
using UnityEngine;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique;

namespace ActionMenuApi.Pedals
{
    public sealed class PedalRadial : PedalStruct
    {
        public float currentValue;
        public PedalOption pedal { get; set; }

        public PedalRadial(string text, float startingValue, Texture2D icon, Action<float> onUpdate, bool locked = false)
        {
            this.text = text;
            this.currentValue = startingValue;
            this.icon = icon;
            this.triggerEvent = delegate {
                Action<float> combinedAction = (Action<float>) Delegate.Combine(new Action<float>(delegate(float f)
                {
                    startingValue = f;
                    pedal.SetButtonPercentText($"{Math.Round(startingValue * 100)}%");
                }), onUpdate);
                RadialPuppetManager.OpenRadialMenu(startingValue, combinedAction, text, pedal);
            };
            this.Type = PedalType.RadialPuppet;
            this.locked = locked;
        }
    }
}