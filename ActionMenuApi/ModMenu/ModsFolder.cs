using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ActionMenuApi.Pedals;
using MelonLoader;
using UnhollowerRuntimeLib;
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
            if (instance != null) return;
            AssetBundle iconsAssetBundle;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActionMenuApi.actionmenuapi.icons"))
            using (var tempStream = new MemoryStream((int)stream.Length))
            {
                stream.CopyTo(tempStream);
                
                iconsAssetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                iconsAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }
            Texture2D modsSectionIcon = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/vrcmg.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            modsSectionIcon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            new ModsFolder("Mods", modsSectionIcon); //TEMP Texture //TODO: Swap to a different texture
        }
    }
}