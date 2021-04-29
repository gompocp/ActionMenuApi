using System;
using ActionMenuApi.Pedals;
using UnityEngine;

namespace ActionMenuApi.ModMenu
{
    public class ModFolder : PedalSubMenu
    {
        public ModFolder(Action openFunc, string text = null, Texture2D icon = null, Action closeFunc = null) : base(openFunc, text, icon, closeFunc)
        {
            AMAPI.AddSubMenuToSubMenu("Mods", openFunc, icon);
        }
    }
}