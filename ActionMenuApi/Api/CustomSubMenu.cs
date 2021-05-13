using System;
using ActionMenuApi.Managers;
using ActionMenuApi.Pedals;
using ActionMenuApi.Types;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
// ReSharper disable HeuristicUnreachableCode

namespace ActionMenuApi.Api
{
    public static class CustomSubMenu
    {
        /// <summary>
        /// Add a lockable button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: 1. can be null if both action menus are open 2. The gameobject that it is attached to is destroyed when you change page on the action menu)</returns>
        public static PedalOption AddButton(string text, Action triggerEvent, bool locked, Texture2D icon = null)
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
        /// Add a lockable radial puppet button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddRadialPuppet(string text, Action<float> onUpdate, bool locked, float startingValue = 0, Texture2D icon = null)
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
        public static PedalOption AddFourAxisPuppet(string text, Action<Vector2> onUpdate, bool locked, Texture2D icon = null, string topButtonText = "Up", 
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
        /// Add a lockable submenu button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when page opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="closeFunc">(optional) Function called when page closes</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddSubMenu(string text, Action openFunc, bool locked, Texture2D icon = null, Action closeFunc = null)
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
        /// Add a lockable toggle button pedal to a custom submenu
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="startingState">Starting value of the toggle pedal everytime the pedal is created</param>
        /// <param name="onToggle">Calls action with a bool depending on the current value of the toggle</param>
        /// <param name="locked">The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddToggle(string text, bool startingState, Action<bool> onToggle, bool locked, Texture2D icon = null)
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
    }
}