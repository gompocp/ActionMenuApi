using System;
using ActionMenuApi.Api;
using ActionMenuApi.Managers;
using ActionMenuApi.Pedals;
using ActionMenuApi.Types;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
// ReSharper disable HeuristicUnreachableCode

namespace ActionMenuApi
{
    /// <summary>
    /// API for adding pedals to the action menu
    /// </summary>
    [Obsolete("This class is only here for compatibility reasons! Please use ActionMenuApi.Api.CustomSubMenu/VRCActionMenuPage/AMUtils for new updated methods to integrate with the action menu", true)]
    public static class AMAPI
    {
        
        /// <summary>
        /// Add a button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        public static void AddButtonPedalToMenu(ActionMenuPageType pageType, string text, Action triggerEvent, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            VRCActionMenuPage.AddPedalToList((ActionMenuPage)pageType, new PedalButton(text, icon, triggerEvent), insertion);
        }
        
        /// <summary>
        /// Add button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddButtonPedalToSubMenu(string text, Action triggerEvent, Texture2D icon = null)
        {
            return CustomSubMenu.AddButton(text, triggerEvent, false, icon);
        }
        
        /// <summary>
        /// Add a radial puppet button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        public static void AddRadialPedalToMenu(ActionMenuPageType pageType, string text, Action<float> onUpdate, float startingValue = 0, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            VRCActionMenuPage.AddPedalToList((ActionMenuPage)pageType, new PedalRadial(text, startingValue, icon, onUpdate), insertion);
        }
        
        /// <summary>
        /// Add radial puppet button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddRadialPedalToSubMenu(string text, Action<float> onUpdate, float startingValue = 0, Texture2D icon = null)
        {
            return CustomSubMenu.AddRadialPuppet(text, onUpdate, false, startingValue, icon);
        }
        
        
        /// <summary>
        /// Add a four axis puppet button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a Vector2 with x and y being between -1 and 1 depending on the current value of the four axis puppet</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        /// <param name="topButtonText">(optional) Top Button Button text On Four Axis Puppet</param>
        /// <param name="rightButtonText">(optional) Right Button Button text On Four Axis Puppet</param>
        /// <param name="downButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        public static void AddFourAxisPedalToMenu(ActionMenuPageType pageType, string text, Action<Vector2> onUpdate,Texture2D icon = null, Insertion insertion = Insertion.Post, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            VRCActionMenuPage.AddPedalToList((ActionMenuPage)pageType, new PedalFourAxis(text, icon, onUpdate, topButtonText, rightButtonText, downButtonText, leftButtonText), insertion);
        }
        
        /// <summary>
        /// Add a four axis puppet button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a Vector2 with x and y being between -1 and 1 depending on the current value of the four axis puppet</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="topButtonText">(optional) Top Button Button text On Four Axis Puppet</param>
        /// <param name="rightButtonText">(optional) Right Button Button text On Four Axis Puppet</param>
        /// <param name="downButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddFourAxisPedalToSubMenu(string text, Action<Vector2> onUpdate,Texture2D icon = null, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            return CustomSubMenu.AddFourAxisPuppet(text, onUpdate, false, icon, topButtonText, rightButtonText, downButtonText, leftButtonText);
        }
        
        /// <summary>
        /// Add a submenu to an ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when page opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="closeFunc">(optional) Function called when page closes</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        public static void AddSubMenuToMenu(ActionMenuPageType pageType, string text, Action openFunc, Texture2D icon = null, Action closeFunc = null, Insertion insertion = Insertion.Post)
        {
            VRCActionMenuPage.AddPedalToList((ActionMenuPage)pageType, new PedalSubMenu(openFunc, text, icon, closeFunc), insertion);
        }
        
   
        /// <summary>
        /// Add a submenu button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when page opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="closeFunc">(optional) Function called when page closes</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddSubMenuToSubMenu(string text, Action openFunc, Texture2D icon = null, Action closeFunc = null)
        {
            return CustomSubMenu.AddSubMenu(text, openFunc, false, icon, closeFunc);
        }
        
        /// <summary>
        /// Add a toggle button pedal to an ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="startingState">Starting value of the toggle pedal everytime the pedal is created</param>
        /// <param name="onToggle">Calls action with a bool depending on the current value of the toggle</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        public static void AddTogglePedalToMenu(ActionMenuPageType pageType, string text, bool startingState, Action<bool> onToggle, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            VRCActionMenuPage.AddPedalToList((ActionMenuPage)pageType, new PedalToggle(text, onToggle, startingState, icon), insertion);
        }
        
        /// <summary>
        /// Add a toggle button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="startingState">Starting value of the toggle pedal everytime the pedal is created</param>
        /// <param name="onToggle">Calls action with a bool depending on the current value of the toggle</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddTogglePedalToSubMenu(string text, bool startingState, Action<bool> onToggle, Texture2D icon = null)
        {
            return CustomSubMenu.AddToggle(text, startingState, onToggle, false, icon);
        }
        
        /// <summary>
        /// Add a mod to a dedicated section of the action menu with other mods
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when your mod page is opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        public static void AddModFolder(string text, Action openFunc, Texture2D icon = null)
        {
            AMUtils.AddToModsFolder(text, openFunc, icon);
        }
    }
}