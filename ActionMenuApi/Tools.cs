using ActionMenuApi.Pedals;
using UnityEngine;

namespace ActionMenuApi.API
{
    public static class Tools
    {
        public static void AddButtonPedalToMenu(ActionMenuPageType pageType, System.Action triggerEvent, string text = "Button Text", Texture2D icon = null, Insertion insertion = Insertion.Post)
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
        
        public static void AddRadialPedalToMenu(ActionMenuPageType pageType, System.Action<float> onUpdate, string text = "Button Text", float startingValue = 0, Texture2D icon = null, Insertion insertion = Insertion.Post)
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

        public static void AddSubMenuToMenu(ActionMenuPageType pageType, System.Action openFunc, string text = null,
            Texture2D icon = null, System.Action closeFunc = null, Insertion insertion = Insertion.Post)
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