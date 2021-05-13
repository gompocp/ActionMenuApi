using System;
using ActionMenuApi.Pedals;
using ActionMenuApi.Types;
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
        public static PedalButton AddButton(ActionMenuPageType pageType, string text, Action triggerEvent, Texture2D icon = null, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalButton(text, icon, triggerEvent);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }
    }
    
}