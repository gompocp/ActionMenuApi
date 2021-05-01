using System;
using System.Collections.Generic;
using ActionMenuApi.Pedals;
using UnityEngine;

namespace ActionMenuApi.ModMenu
{
    public class ModsFolder : PedalSubMenu
    {
        public List<ModFolder> mods;
        public static ModsFolder instance;
        public ModsFolder(string text, Action openFunc, Texture2D icon = null, Action closeFunc = null) : base(openFunc, text, icon, closeFunc)
        {
            instance = this;
            this.mods = new List<ModFolder>();
            this.Type = PedalType.ModsFolder;
        }

        public void AddMainPageButton()
        {
            AMAPI.AddSubMenuToMenu(ActionMenuPageType.Main, text, openFunc, icon, closeFunc);
        }
    }
}