using System;
using System.Collections.Generic;
using ActionMenuApi.Pedals;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.ModMenu
{
    internal class ModsFolder
    {
        public List<Action> mods = new ();
        public static ModsFolder instance;
        private string text;
        private Texture2D icon;
        private Action openFunc;
        public ModsFolder(string text, Texture2D icon = null)
        {
            this.text = text;
            this.icon = icon;
            instance = this;
            openFunc = () => {
                foreach (var action in mods) action.Invoke();
            };
        }

        public void AddMod(Action openingAction)
        {
            mods.Add(openingAction);
        }

        public void RemoveMod(Action openingAction)
        {
            mods.Remove(openingAction);
        }
        
        public void AddMainPageButton()
        {
            AMAPI.AddSubMenuToSubMenu(text, openFunc, icon);
        }

        public static void CreateInstance()
        {
            new ModsFolder("Mods", null);
        }
    }
}