using System;
using System.Diagnostics;
using ActionMenuApi.Managers;
using ActionMenuApi.ModMenu;
using ActionMenuApi.Pedals;
using ActionMenuApi.Types;
using MelonLoader;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2
// ReSharper disable HeuristicUnreachableCode

namespace ActionMenuApi
{
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
            AddPedalToList(
                pageType, 
                new PedalButton(
                    text,
                    icon, 
                    triggerEvent
                ),
                insertion
            );
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
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon); 
            pedalOption.SetPedalTriggerEvent(DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(triggerEvent));
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
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.SetButtonPercentText($"{Math.Round(startingValue*100)}%");
            pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeRadial);
            pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    Action<float> combinedAction = (Action<float>)Delegate.Combine(new Action<float>(delegate(float f)
                    {
                        startingValue = f;
                        pedalOption.SetButtonPercentText($"{Math.Round(startingValue*100)}%");
                    }), onUpdate);
                    RadialPuppetManager.OpenRadialMenu(startingValue, combinedAction, text, pedalOption);
                }))
            );
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
        /// <param name="bottomButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        public static void AddFourAxisPedalToMenu(ActionMenuPageType pageType, string text, Action<Vector2> onUpdate,Texture2D icon = null, Insertion insertion = Insertion.Post, string topButtonText = "Up", 
            string rightButtonText = "Right", string bottomButtonText = "Down", string leftButtonText = "Left")
        {
            AddPedalToList(
                pageType, 
                new PedalFourAxis(
                    text,
                    icon,
                    onUpdate,
                    topButtonText,
                    rightButtonText,
                    bottomButtonText,
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
        /// <param name="bottomButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        /// <returns> PedalOption Instance (Note: the gameobject that it is attached to is destroyed when you change page on the action menu</returns>
        public static PedalOption AddFourAxisPedalToSubMenu(string text, Action<Vector2> onUpdate,Texture2D icon = null, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeAxis);
            pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    FourAxisPuppetManager.OpenFourAxisMenu(text, onUpdate, pedalOption);
                    FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                    FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                    FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                    FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
                }))
            );
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
        public static void AddSubMenuToMenu(ActionMenuPageType pageType, string text, Action openFunc,
            Texture2D icon = null, Action closeFunc = null, Insertion insertion = Insertion.Post)
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
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            //pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeFolder);
            pedalOption.SetPedalTriggerEvent(
                DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
                {
                    actionMenuOpener.GetActionMenu().PushPage(openFunc, closeFunc, icon, text);
                }))
            );
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
            
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            if (startingState) pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOn);
            else pedalOption.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOff);
            pedalOption.SetPedalTriggerEvent(
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
            return pedalOption;
        }

        /// <summary>
        /// Add a mod a dedicated section of the action menu with other mods
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">Function called when your mod page is opened. Add your methods calls to other AMAPI methods such AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked</param>
        /// <param name="icon">(optional) The Button Icon</param>
        public static void AddModFolder(string text, Action openFunc, Texture2D icon = null)
        {
            if(ModsFolder.instance == null) ModsFolder.CreateInstance();
            ModsFolder.instance.AddMod(() =>
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
                    MelonLogger.Warning("Adding to the Expression page hasn't been implemented yet. Please use a different page");
                    return;
                    if (insertion == Insertion.Pre) Patches.expressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.expressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.Main:
                    if (insertion == Insertion.Pre) Patches.mainPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.mainPagePost.Add(customPedal);
                    return;
                case ActionMenuPageType.MenuOpacity:
                    MelonLogger.Warning("Adding to the MenuOpacity page hasn't been implemented yet. Please use a different page");
                    return;
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
                    MelonLogger.Warning("Adding to the NameplatesSize page hasn't been implemented yet. Please use a different page");
                    return;
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
        [Obsolete("This method is obsolete. Use the override AddButtonPedalToMenu(pageType, text, triggerEvent, icon, insertion)", true)]
        public static void AddButtonPedalToMenu(ActionMenuPageType pageType, Action triggerEvent, string text = "Button Text", Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            AddPedalToList(
                pageType, 
                new PedalButton(
                    text,
                    icon, 
                    triggerEvent
                ),
                insertion
            );
        }

        [Obsolete("This method is obsolete. Use the override AddButtonPedalToSubMenu(text, triggerEvent, icon)", true)]
        public static PedalOption AddButtonPedalToSubMenu(Action triggerEvent, string text = "Button Text", Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon); 
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(triggerEvent);
            return pedalOption;
        }
        
        [Obsolete("This method is obsolete. Use the override AddRadialPedalToMenu(pageType, text, onUpdate, startingValue, icon, insertion)", true)]
        public static void AddRadialPedalToMenu(ActionMenuPageType pageType, Action<float> onUpdate, string text = "Button Text", float startingValue = 0, Texture2D icon = null, Insertion insertion = Insertion.Post)
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
        
        [Obsolete("This method is obsolete. Use the override AddRadialPedalToSubMenu(text, onUpdate, startingValue, icon)", true)]
        public static PedalOption AddRadialPedalToSubMenu(Action<float> onUpdate, string text = "Button Text", float startingValue = 0, Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.field_Public_ActionButton_0.prop_String_1 = $"{Math.Round(startingValue)}%";
            pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeRadial;
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
            {
                Action<float> combinedAction = (System.Action<float>)Delegate.Combine(new Action<float>(delegate(float f)
                {
                    startingValue = f;
                    
                    pedalOption.field_Public_ActionButton_0.prop_String_1 = $"{Math.Round(startingValue)}%";
                }), onUpdate);
                RadialPuppetManager.OpenRadialMenu(startingValue, combinedAction, text, pedalOption);
            }));
            return pedalOption;
        }
        
        [Obsolete("This method is obsolete. Use the override AddFourAxisPedalToMenu(pageType, text, onUpdate, icon, insertion, topButtonText, rightButtonText, downButtonText, leftButtonText)", true)]
        public static void AddFourAxisPedalToMenu(ActionMenuPageType pageType, string text, Vector2 startingValue, Action<Vector2> onUpdate,Texture2D icon = null, Insertion insertion = Insertion.Post, string topButtonText = "Up", 
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
        
        [Obsolete("This method is obsolete. Use the override AddFourAxisPedalToSubMenu(text, onUpdate, icon, topButtonText, rightButtonText, downButtonText, leftButtonText)", true)]
        public static PedalOption AddFourAxisPedalToSubMenu(string text, Vector2 startingValue, Action<Vector2> onUpdate,Texture2D icon = null, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeAxis;
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
            {
                FourAxisPuppetManager.OpenFourAxisMenu(text, v => startingValue = v, pedalOption);
                    FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                    FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                    FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                    FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
                }));
            return pedalOption;
        }

        [Obsolete("This method is obsolete. Use the override AddSubMenuToMenu(pageType, text, openFunc, icon, closeFunc, insertion)", true)]
        public static void AddSubMenuToMenu(ActionMenuPageType pageType, Action openFunc, string text = null,
            Texture2D icon = null, Action closeFunc = null, Insertion insertion = Insertion.Post)
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
        
        [Obsolete("This method is obsolete. Use the override AddTogglePedalToMenu(pageType, text, startingState, onToggle, icon, insertion)", true)]
        public static void AddTogglePedalToMenu(ActionMenuPageType pageType,bool startingState, System.Action<bool> onToggle, string text, Texture2D icon = null, Insertion insertion = Insertion.Post)
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
        
        [Obsolete("This method is obsolete. Use the override AddTogglePedalToSubMenu(text, startingState, onToggle, icon)", true)]
        public static PedalOption AddTogglePedalToSubMenu(Action<bool> onToggle, bool startingState, string text, Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.SetText(text); 
            pedalOption.SetIcon(icon);
            if (startingState) pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOn;
            else pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOff;
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
            {
                startingState = !startingState;
                if (startingState)
                    pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOn;
                else 
                    pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeToggleOff;
                onToggle.Invoke(startingState);
            }));
            return pedalOption;
        }
    }
}