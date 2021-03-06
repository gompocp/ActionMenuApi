﻿using System;
using TMPro;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2
using UnityEngine;

namespace ActionMenuApi
{
    internal static class ExtensionMethods
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

        public static ActionMenuOpener GetLeftOpener(this ActionMenuDriver actionMenuDriver) => actionMenuDriver.field_Public_ActionMenuOpener_0;
        public static ActionMenuOpener GetRightOpener(this ActionMenuDriver actionMenuDriver) => actionMenuDriver.field_Public_ActionMenuOpener_1;
        public static ActionMenu GetActionMenu(this ActionMenuOpener actionMenuOpener) => actionMenuOpener.field_Public_ActionMenu_0;
        public static GameObject GetCursor(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_GameObject_0;
        public static GameObject GetCursor(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_GameObject_0;
        public static GameObject GetArrow(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_GameObject_1;
        public static PedalGraphic GetFill(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_PedalGraphic_0;
        public static TextMeshProUGUI GetTitle(this RadialPuppetMenu radialPuppetMenu) => ((PuppetMenu)radialPuppetMenu).field_Public_TextMeshProUGUI_0;
        public static TextMeshProUGUI GetTitle(this AxisPuppetMenu axisPuppetMenu) => ((PuppetMenu)axisPuppetMenu).field_Public_TextMeshProUGUI_0;
        public static TextMeshProUGUI GetCenterText(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_TextMeshProUGUI_0;
        public static PedalGraphic GetFillUp(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_PedalGraphic_0;
        public static PedalGraphic GetFillRight(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_PedalGraphic_1;
        public static PedalGraphic GetFillDown(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_PedalGraphic_2;
        public static PedalGraphic GetFillLeft(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_PedalGraphic_3;
        public static void SetButtonText(this ActionButton actionButton, string text) => actionButton.prop_String_0 = text;
        public static ActionButton GetButtonUp(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_ActionButton_0;
        public static ActionButton GetButtonRight(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_ActionButton_1;
        public static ActionButton GetButtonDown(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_ActionButton_2;
        public static ActionButton GetButtonLeft(this AxisPuppetMenu axisPuppetMenu) => axisPuppetMenu.field_Public_ActionButton_3;
        
        public static void SetAlpha(this PedalGraphic pedalGraphic, float amount)
        {
            Color temp = pedalGraphic.color;
            temp.a = amount;
            pedalGraphic.color = temp;
        }
        
        public static void SetAngle(this RadialPuppetMenu radialPuppet, float angle)
        {
            radialPuppet.GetFill().field_Public_Single_3 = angle;
            radialPuppet.UpdateDisplay();
        }

        public static void SetValue(this RadialPuppetMenu radialPuppet, float value)
        {
            radialPuppet.GetFill().field_Public_Single_3  = (value / 100) * 360;
            radialPuppet.UpdateDisplay();
        }

        public static void UpdateDisplay(this RadialPuppetMenu radialPuppet)
        {
            radialPuppet.GetCenterText().text = (Math.Round(radialPuppet.GetFill().field_Public_Single_3  / 360 * 100)) + "%";
            radialPuppet.GetFill().UpdateGeometry();
        }

        public static void UpdateArrow(this RadialPuppetMenu radialPuppet, double angleOriginal, double eulerAngle)
        {
            radialPuppet.GetArrow().transform.localPosition = new Vector3((float)(120 * Math.Cos(angleOriginal / Constants.radToDeg)), (float)(120 * Math.Sin(angleOriginal / Constants.radToDeg)), radialPuppet.GetArrow().transform.localPosition.z);
            radialPuppet.GetArrow().transform.localEulerAngles = new Vector3(radialPuppet.GetArrow().transform.localEulerAngles.x, radialPuppet.GetArrow().transform.localEulerAngles.y, (float)(180 - eulerAngle));
        }
    }
}