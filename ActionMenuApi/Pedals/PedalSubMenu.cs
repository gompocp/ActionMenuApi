using System;
using Il2CppSystem.Linq;
using Transmtn;
using UnityEngine;

namespace ActionMenuApi.Pedals
{
    public class PedalSubMenu : PedalStruct
    {

        public Action openFunc { get; set; }
        public Action closeFunc { get; set; }
        
        public PedalSubMenu(System.Action openFunc, string text = null,
            Texture2D icon = null, System.Action closeFunc = null)
        {
            this.text = text;
            this.icon = icon;
            this.openFunc = openFunc;
            this.closeFunc = closeFunc;
            this.triggerEvent =  delegate
            {
                if (Utilities.GetActionMenuOpener() == null) return;
                Utilities.GetActionMenuOpener().actionMenu.PushPage(openFunc, closeFunc, icon, text);
            };
            this.Type = PedalType.SubMenu;
        }
    }
}