using System;
using ActionMenuApi.Pedals;
using UnhollowerRuntimeLib;
using UnityEngine;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2

namespace ActionMenuApi
{
    public static class AMAPI
    {
        
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

        public static PedalOption AddButtonPedalToSubMenu(Action triggerEvent, string text = "Button Text", Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.setText(text); 
            pedalOption.setIcon(icon); 
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(triggerEvent);
            return pedalOption;
        }
        
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
        
        public static PedalOption AddRadialPedalToSubMenu(Action<float> onUpdate, string text = "Button Text", float startingValue = 0, Texture2D icon = null)
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.setText(text); 
            pedalOption.setIcon(icon);
            pedalOption.field_Public_ActionButton_0.prop_String_1 = $"{Math.Round(startingValue)}%";
            pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeRadial;
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
            {
                Action<float> combinedAction = (System.Action<float>)Delegate.Combine(new Action<float>(delegate(float f)
                {
                    startingValue = f;
                    
                    pedalOption.field_Public_ActionButton_0.prop_String_1 = $"{Math.Round(startingValue)}%";
                }), onUpdate);
                RadialPuppetManager.OpenRadialMenu(startingValue, combinedAction, text);
            }));
            return pedalOption;
        }
        
        public static void AddFourAxisPedalToMenu(ActionMenuPageType pageType, string text, Vector2 startingValue, Action<Vector2> onUpdate,Texture2D icon = null, Insertion insertion = Insertion.Post, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            AddPedalToList(
                pageType, 
                new PedalFourAxis(
                    text, 
                    startingValue, 
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
        
        public static PedalOption AddFourAxisPedalToSubMenu(string text, Vector2 startingValue, Action<Vector2> onUpdate,Texture2D icon = null, string topButtonText = "Up", 
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left")
        {
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.setText(text); 
            pedalOption.setIcon(icon);
            pedalOption.field_Public_ActionButton_0.prop_Texture2D_2 = Utilities.GetExpressionsIcons().typeAxis;
            pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(new Action(delegate
            {
                FourAxisPuppetManager.OpenFourAxisMenu(startingValue, v => startingValue = v, text, onUpdate);
                    FourAxisPuppetManager.current.GetButtonUp().SetButtonText(topButtonText);
                    FourAxisPuppetManager.current.GetButtonRight().SetButtonText(rightButtonText);
                    FourAxisPuppetManager.current.GetButtonDown().SetButtonText(downButtonText);
                    FourAxisPuppetManager.current.GetButtonLeft().SetButtonText(leftButtonText);
                }));
            return pedalOption;
        }

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
        
        
        public static PedalOption AddTogglePedalToSubMenu(Action<bool> onToggle, bool startingState, string text, Texture2D icon = null)
        {
            
            ActionMenuOpener actionMenuOpener = Utilities.GetActionMenuOpener();
            if (actionMenuOpener == null) return null;
            PedalOption pedalOption = actionMenuOpener.GetActionMenu().AddOption();
            pedalOption.setText(text); 
            pedalOption.setIcon(icon);
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
        
        


        private static void AddPedalToList(ActionMenuPageType pageType, PedalStruct customPedal, Insertion insertion)
        {
            switch (pageType)
            {
                case ActionMenuPageType.SDK2Expression:
                    if(insertion == Insertion.Pre) Patches.sdk2ExpressionPagePre.Add(customPedal);
                    else if(insertion == Insertion.Post) Patches.sdk2ExpressionPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Config:
                    if (insertion == Insertion.Pre) Patches.configPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.configPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Emojis:
                    if (insertion == Insertion.Pre) Patches.emojisPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.emojisPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Expression:
                    if (insertion == Insertion.Pre) Patches.expressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.expressionPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Main:
                    if (insertion == Insertion.Pre) Patches.mainPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.mainPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.MenuOpacity:
                    if (insertion == Insertion.Pre) Patches.menuOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuOpacityPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.MenuSize:
                    if (insertion == Insertion.Pre) Patches.menuSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuSizePagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Nameplates:
                    if (insertion == Insertion.Pre) Patches.nameplatesPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.NameplatesOpacity:
                    if (insertion == Insertion.Pre) Patches.nameplatesOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesOpacityPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.NameplatesSize:
                    if (insertion == Insertion.Pre) Patches.nameplatesSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesSizePagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.NameplatesVisibilty:
                    if (insertion == Insertion.Pre) Patches.nameplatesVisibilityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesVisibilityPagePost.Add(customPedal);
                    break;
                case ActionMenuPageType.Options:
                    if (insertion == Insertion.Pre) Patches.optionsPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.optionsPagePost.Add(customPedal);
                    break;
            }
        }
        
        
    }
}