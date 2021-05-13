﻿using System;
using ActionMenuApi.Types;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public sealed class PedalSubMenu : PedalStruct
    {

        public Action openFunc { get; set; }
        public Action closeFunc { get; set; }
        
        public PedalSubMenu(Action openFunc, string text = null, Texture2D icon = null, Action closeFunc = null, bool locked = false)
        {
            this.text = text;
            this.icon = icon;
            this.openFunc = openFunc;
            this.closeFunc = closeFunc;
            this.triggerEvent =  delegate
            {
                if (Utilities.GetActionMenuOpener() == null) return;
                Utilities.GetActionMenuOpener().GetActionMenu().PushPage(openFunc, closeFunc, icon, text);
            };
            this.Type = PedalType.SubMenu;
            this.locked = locked;
        }
    }
}