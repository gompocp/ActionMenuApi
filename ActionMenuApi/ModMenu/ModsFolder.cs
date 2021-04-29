using System;
using ActionMenuApi.Pedals;
using UnityEngine;

namespace ActionMenuApi.ModMenu
{
    public class ModsFolder : PedalSubMenu
    {
        public ModsFolder(Action openFunc, string text = null, Texture2D icon = null, Action closeFunc = null) : base(openFunc, text, icon, closeFunc)
        {
            this.Type = PedalType.ModFolder;
        }
    }
}