using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ActionMenuApi.Types;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ActionMenuApi.ModMenu
{
    internal class ModsFolder
    {
        public List<Action> mods = new ();
        public List<List<Action>> splitMods;
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
                if (mods.Count <= Constants.MAX_PEDALS_PER_PAGE)
                {
                    foreach (var action in mods) action.Invoke();
                }
                else
                {
                    if(splitMods == null) splitMods = mods.Split((int)Constants.MAX_PEDALS_PER_PAGE);
                    for (int i = 0; i < splitMods.Count && i < Constants.MAX_PEDALS_PER_PAGE; i++)
                    {
                        int index = i;
                        AMAPI.AddSubMenuToSubMenu($"Page {i+1}", () =>
                        {
                            foreach (var action in splitMods[index]) action.Invoke();
                        }, GetPageIcon(i+1));
                    }
                }
            };
        }

        public void AddMod(Action openingAction)
        {
            mods.Add(openingAction);
        }

        /*public void RemoveMod(Action openingAction)
        {
            mods.Remove(openingAction);
        }*/
        
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
            pageOne = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/1.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageOne.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageTwo = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/2.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageTwo.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageThree = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/3.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageThree.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageFour = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/4.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageFour.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageFive = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/5.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageFive.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageSix = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/6.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageSix.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            pageSeven = iconsAssetBundle.LoadAsset_Internal("Assets/ActionMenuApi/7.png", Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            pageSeven.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            new ModsFolder("Mods", modsSectionIcon); //TEMP Texture //TODO: Swap to a different texture
        }
        
        private static Texture2D GetPageIcon(int pageIndex)
        {
            switch (pageIndex)
            {
                case 1:
                    return pageOne;
                case 2:
                    return pageTwo;
                case 3:
                    return pageThree;
                case 4:
                    return pageFour;
                case 5:
                    return pageFive;
                case 6:
                    return pageSix;
                case 7:
                    return pageSeven;
                default:
                    return null;
            }
        }
        
        private static Texture2D pageOne;
        private static Texture2D pageTwo;
        private static Texture2D pageThree;
        private static Texture2D pageFour;
        private static Texture2D pageFive;
        private static Texture2D pageSix;
        private static Texture2D pageSeven;
    }
}