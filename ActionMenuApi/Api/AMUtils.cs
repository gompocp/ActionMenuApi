using System;
using ActionMenuApi.Managers;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Api
{
    public static class AMUtils
    {
        /// <summary>
        /// Trigger a refresh for the action menus. Will only run if they are open
        /// </summary>
        public static void RefreshActionMenu()
        {
            try
            {
                Utilities.RefreshAM();
            }
            catch(Exception e)
            {
                MelonLogger.Warning($"Refresh failed (oops). This may or may not be an oof if another exception immediately follows after this exception: {e}");
                //This is semi-abusable if this fails so its probably a good idea to have a fail-safe to protect sensitive functions that are meant to be locked
                Utilities.ResetMenu();
            }
        }
        
        /// <summary>
        /// Add a mod to a dedicated section of the action menu with other mods
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when your mod page is opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        public static void AddToModsFolder(string text, Action openFunc, Texture2D icon = null)
        {
            ModsFolderManager.AddMod(() =>
            {
                CustomSubMenu.AddSubMenu(text, openFunc, icon, null);
            });
        }
    }
}