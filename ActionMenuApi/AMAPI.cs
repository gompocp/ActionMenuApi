﻿using System;
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
    public static class AMAPI
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
        /// Add a button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        [Obsolete("Use AddSubMenuToMenu(pageType, text, openFunc, icon, locked, closeFunc, insertion) instead, you can leave the locked parameter = false", false)]
        public static void AddButtonPedalToMenu(ActionMenuPageType pageType, string text, Action triggerEvent, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalButton(text, icon, triggerEvent);
            AddPedalToList(pageType, pedal, insertion);
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
            return AMAPI.AddButtonPedalToCustomMenu(text, triggerEvent, false, icon);
        }
        
        /// <summary>
        /// Add a lockable button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: 1. can be null if both action menus are open 2. The gameobject that it is attached to is destroyed when you change page on the action menu)</returns>
        public static PedalOption AddButtonPedalToCustomMenu(string text, Action triggerEvent, bool locked, Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            if (!locked) pedalOption.SetPedalTriggerEvent(DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(triggerEvent));
            else ResourcesManager.AddLockChildIcon(pedalOption.GetActionButton().gameObject.GetChild("Inner"));
            return pedalOption; 
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
            AddPedalToList(
                pageType, 
                new PedalRadial(
                    text, 
                    startingValue, 
                    icon, 
                    onUpdate
                ),
                insertion
            );
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
            return AMAPI.AddLockableRadialPedalToSubMenu(text, onUpdate, false, startingValue, icon);
        }

        /// <summary>
        /// Add a lockable radial puppet button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddLockableRadialPedalToSubMenu(string text, Action<float> onUpdate, bool locked, float startingValue = 0, Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.SetButtonPercentText($"{Math.Round(startingValue*100)}%");
            pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeRadial);
            if(!locked) pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    var combinedAction = (Action<float>)Delegate.Combine(new Action<float>(delegate(float f)
                    {
                        startingValue = f;
                        pedalOption.SetButtonPercentText($"{Math.Round(startingValue*100)}%");
                    }), onUpdate);
                    RadialPuppetManager.OpenRadialMenu(startingValue, combinedAction, text, pedalOption);
                }))
            );
            else ResourcesManager.AddLockChildIcon(pedalOption.GetActionButton().gameObject.GetChild("Inner"));
            return pedalOption;
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
            AddPedalToList(
                pageType, 
                new PedalFourAxis(
                    text,
                    icon,
                    onUpdate,
                    topButtonText,
                    rightButtonText,
                    downButtonText,
                    leftButtonText
                ),
                insertion
            );
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
            return AMAPI.AddLockableFourAxisPedalToSubMenu(text, onUpdate, false, icon, topButtonText, rightButtonText, downButtonText, leftButtonText);
        }

        /// <summary>
        /// Add a lockable four axis puppet button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a Vector2 with x and y being between -1 and 1 depending on the current value of the four axis puppet</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="topButtonText">(optional) Top Button Button text On Four Axis Puppet</param>
        /// <param name="rightButtonText">(optional) Right Button Button text On Four Axis Puppet</param>
        /// <param name="downButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddLockableFourAxisPedalToSubMenu(string text, Action<Vector2> onUpdate, bool locked, Texture2D icon = null, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeAxis);
            if(!locked) pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    FourAxisPuppetManager.OpenFourAxisMenu(text, onUpdate, pedalOption);
                    FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                    FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                    FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                    FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
                }))
            );
            else ResourcesManager.AddLockChildIcon(pedalOption.GetActionButton().gameObject.GetChild("Inner"));
            return pedalOption;
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
            AddPedalToList(
                pageType, 
                new PedalSubMenu(
                    openFunc, 
                    text, 
                    icon, 
                    closeFunc
                ),
                insertion
            );
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
            return AMAPI.AddLockableSubMenuToSubMenu(text, openFunc, false, icon, closeFunc);
        }

        /// <summary>
        /// Add a lockable submenu button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when page opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="closeFunc">(optional) Function called when page closes</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddLockableSubMenuToSubMenu(string text, Action openFunc, bool locked, Texture2D icon = null, Action closeFunc = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            //pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeFolder);
            if(!locked) pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    actionMenuOpener.GetActionMenu().PushPage(openFunc, closeFunc, icon, text);
                }))
            );
            else ResourcesManager.AddLockChildIcon(pedalOption.GetActionButton().gameObject.GetChild("Inner"));
            return pedalOption;
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
            AddPedalToList(
                pageType, 
                new PedalToggle(
                    text, 
                    onToggle, 
                    startingState,
                    icon
                ),
                insertion
            );
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
            return AMAPI.AddLockableTogglePedalToSubMenu(text, startingState, onToggle, false, icon);
        }

        /// <summary>
        /// Add a lockable toggle button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="startingState">Starting value of the toggle pedal everytime the pedal is created</param>
        /// <param name="onToggle">Calls action with a bool depending on the current value of the toggle</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddLockableTogglePedalToSubMenu(string text, bool startingState, Action<bool> onToggle, bool locked, Texture2D icon = null)
        {
            
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            if (startingState) pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOn);
            else pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOff);
            if(!locked) pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    startingState = !startingState;
                    if (startingState)
                        pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOn);
                    else 
                        pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOff);
                    onToggle.Invoke(startingState);
                }))
            );
            else ResourcesManager.AddLockChildIcon(pedalOption.GetActionButton().gameObject.GetChild("Inner"));
            return pedalOption;
        }

        /// <summary>
        /// Add a mod to a dedicated section of the action menu with other mods
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when your mod page is opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        public static void AddModFolder(string text, Action openFunc, Texture2D icon = null)
        {
            ModsFolderManager.AddMod(() =>
            {
                AMAPI.AddSubMenuToSubMenu(text, openFunc, icon, null);
            });
        }

        private static void AddPedalToList(ActionMenuPageType pageType, PedalStruct customPedal, Insertion insertion)
        {
            switch (pageType)
            {
                case ActionMenuPageType.SDK2Expression:
                    if(insertion == Insertion.Pre) Patches.sdk2ExpressionPagePre.Add(customPedal);
                    else if(insertion == Insertion.Post) Patches.sdk2ExpressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Config:
                    if (insertion == Insertion.Pre) Patches.configPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.configPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Emojis:
                    if (insertion == Insertion.Pre) Patches.emojisPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.emojisPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Expression:

                    if (insertion == Insertion.Pre) Patches.expressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.expressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Main:
                    if (insertion == Insertion.Pre) Patches.mainPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.mainPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.MenuOpacity:
                    if (insertion == Insertion.Pre) Patches.menuOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuOpacityPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.MenuSize:
                    MelonLogger.Warning("Adding to the MenuSize page hasn't been implemented yet. Please use a different page");
                    return;
                    if (insertion == Insertion.Pre) Patches.menuSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuSizePagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Nameplates:
                    if (insertion == Insertion.Pre) Patches.nameplatesPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.NameplatesOpacity:
                    if (insertion == Insertion.Pre) Patches.nameplatesOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesOpacityPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.NameplatesSize:
                    if (insertion == Insertion.Pre) Patches.nameplatesSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesSizePagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.NameplatesVisibilty:
                    if (insertion == Insertion.Pre) Patches.nameplatesVisibilityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesVisibilityPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Options:
                    if (insertion == Insertion.Pre) Patches.optionsPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.optionsPagePost.Add(customPedal);
                    return;
            }
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
        public static PedalSubMenu AddSubMenuToMenu(ActionMenuPageType pageType, string text, Action openFunc, Texture2D icon = null, bool locked = false, Action closeFunc = null, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalSubMenu(openFunc, text, icon, closeFunc, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }
        
    }
}