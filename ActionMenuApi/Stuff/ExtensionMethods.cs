using System;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2
using UnityEngine;

namespace ActionMenuApi
{
    public static class ExtensionMethods
    {
        
        public static PedalOption AddOption(this ActionMenu menu)
        {
            return menu.Method_Private_PedalOption_0(); //This should be safe for a while unless they add another similar method
        }
        public static string setText(this PedalOption pedal, string text)
        {
            return pedal.prop_String_0 = text; //Likewise... should be safe
        }
        public static string getText(this PedalOption pedal)
        {
            return pedal.prop_String_0; //Likewise... should be safe
        }
        public static ActionMenuPage PushPage(this ActionMenu menu, Il2CppSystem.Action openFunc, Il2CppSystem.Action closeFunc = null, Texture2D icon = null, string text = null)
        {
            return menu.Method_Public_ObjectNPublicAcTeAcStGaUnique_Action_Action_Texture2D_String_0(openFunc, closeFunc, icon, text); //Likewise... should be safe but reflection is pretty easy for it
        }
        public static Texture2D setIcon(this PedalOption pedal, Texture2D icon)
        {
            //PropertyInfo texture = typeof(PedalOption).GetProperties().Where(p => p.PropertyType == typeof(Texture2D)).First(); meh
            return pedal.prop_Texture2D_0 = icon;
        }
        public static Texture2D getIcon(this PedalOption pedal)
        {
            return pedal.prop_Texture2D_0;
        }
        public static bool isOpen(this ActionMenuOpener actionMenuOpener)
        {
            return actionMenuOpener.field_Private_Boolean_0;
        }
        
        public static void SetAngle(this RadialPuppetMenu radialPuppet, float angle)
        {
            radialPuppet.fill.angleMax = angle;
            radialPuppet.UpdateDisplay();
        }

        public static void SetValue(this RadialPuppetMenu radialPuppet, float value)
        {
            radialPuppet.fill.angleMax = (value / 100) * 360;
            radialPuppet.UpdateDisplay();
        }

        public static void UpdateDisplay(this RadialPuppetMenu radialPuppet)
        {
            radialPuppet.centerText.text = (Math.Round(radialPuppet.fill.angleMax / 360 * 100)) + "%";
            radialPuppet.fill.UpdateGeometry();
        }

        public static void UpdateArrow(this RadialPuppetMenu radialPuppet, double angleOriginal, double eulerAngle)
        {
            radialPuppet.arrow.transform.localPosition = new Vector3((float)(120 * Math.Cos(angleOriginal / Constants.radToDeg)), (float)(120 * Math.Sin(angleOriginal / Constants.radToDeg)), radialPuppet.arrow.transform.localPosition.z);
            radialPuppet.arrow.transform.localEulerAngles = new Vector3(radialPuppet.arrow.transform.localEulerAngles.x, radialPuppet.arrow.transform.localEulerAngles.y, (float)(180 - eulerAngle));
        }
    }
}