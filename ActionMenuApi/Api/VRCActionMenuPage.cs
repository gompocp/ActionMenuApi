using System;
using ActionMenuApi.Pedals;
using ActionMenuApi.Types;
using MelonLoader;
using UnityEngine;

namespace ActionMenuApi.Api
{
    public static class VRCActionMenuPage
    {
        /// <summary>
        /// Add a button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">(optional) Determines whether or not the button is added before or after VRChat's buttons for the target page</param>
        public static PedalButton AddButton(ActionMenuPage pageType, string text, Action triggerEvent, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalButton(text, icon, triggerEvent);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }
        
        internal static void AddPedalToList(ActionMenuPage pageType, PedalStruct customPedal, Insertion insertion)
        {
            switch (pageType)
            {
                case ActionMenuPage.SDK2Expression:
                    if(insertion == Insertion.Pre) Patches.sdk2ExpressionPagePre.Add(customPedal);
                    else if(insertion == Insertion.Post) Patches.sdk2ExpressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Config:
                    if (insertion == Insertion.Pre) Patches.configPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.configPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Emojis:
                    if (insertion == Insertion.Pre) Patches.emojisPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.emojisPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Expression:
                    if (insertion == Insertion.Pre) Patches.expressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.expressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Main:
                    if (insertion == Insertion.Pre) Patches.mainPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.mainPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.MenuOpacity:
                    if (insertion == Insertion.Pre) Patches.menuOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuOpacityPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.MenuSize:
                    MelonLogger.Warning("Adding to the MenuSize page hasn't been implemented yet. Please use a different page");
                    return;
                    if (insertion == Insertion.Pre) Patches.menuSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.menuSizePagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Nameplates:
                    if (insertion == Insertion.Pre) Patches.nameplatesPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.NameplatesOpacity:
                    if (insertion == Insertion.Pre) Patches.nameplatesOpacityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesOpacityPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.NameplatesSize:
                    if (insertion == Insertion.Pre) Patches.nameplatesSizePagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesSizePagePost.Add(customPedal);
                    return;
                case ActionMenuPage.NameplatesVisibilty:
                    if (insertion == Insertion.Pre) Patches.nameplatesVisibilityPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.nameplatesVisibilityPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Options:
                    if (insertion == Insertion.Pre) Patches.optionsPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) Patches.optionsPagePost.Add(customPedal);
                    return;
            }
        }
    }
    
}