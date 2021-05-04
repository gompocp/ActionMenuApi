using System;
using System.Linq;
using System.Reflection;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2
using UnityEngine;

namespace ActionMenuApi
{
    internal static class ExtensionMethods
    {
        public static void PrefetchDelegatesAndProps()
        {
            _ = GetAddOptionDelegate;
            _ = GetPushPageDelegate;
        }
        
        private static AddOptionDelegate GetAddOptionDelegate
        {
            get
            {
                //Build 1088 menu.Method_Private_PedalOption_0()
                if (addOptionDelegate != null) return addOptionDelegate;
                MethodInfo addOptionMethod = typeof(ActionMenu).GetMethods(BindingFlags.Public | BindingFlags.Instance).First(
                    m => m.GetParameters().Length == 0 && m.Name.StartsWith("Method_Private_PedalOption_") && !m.Name.Contains("PDM"));

                addOptionDelegate = (AddOptionDelegate)Delegate.CreateDelegate(
                    typeof(AddOptionDelegate),
                    null,
                    addOptionMethod);
                return addOptionDelegate;
            }
        }
        public static PedalOption AddOption(this ActionMenu menu) => GetAddOptionDelegate.Invoke(menu); //Honestly I have no idea how or why this works but hey... it works
        private static AddOptionDelegate addOptionDelegate;
        private delegate PedalOption AddOptionDelegate(ActionMenu actionMenu);
        
        
        public static void SetText(this PedalOption pedal, string text) => pedal.prop_String_0 = text;
        public static string GetText(this PedalOption pedal) => pedal.prop_String_0; //Only string prop on PedalOption. shouldnt change unless drastic changes are made to the action menu
        
        
        
        private static PushPageDelegate GetPushPageDelegate
        {
            get
            {
                //Build 1088 menu.Method_Public_ObjectNPublicAcTeAcStGaUnique_Action_Action_Texture2D_String_0(openFunc, closeFunc, icon, text);
                if (pushPageDelegate != null) return pushPageDelegate;
                MethodInfo pushPageMethod = typeof(ActionMenu).GetMethods(BindingFlags.Public | BindingFlags.Instance).First( //Not scuffed I swear (theres only 1 of these methods( ignoring pdms))
                    m => m.GetParameters().Length == 4 
                         && m.ReturnType == typeof(ActionMenuPage) 
                         && m.GetParameters()[0].ParameterType == typeof(Il2CppSystem.Action)
                         && m.GetParameters()[1].ParameterType == typeof(Il2CppSystem.Action)
                         && m.GetParameters()[2].ParameterType == typeof(Texture2D)
                         && m.GetParameters()[3].ParameterType == typeof(String)
                         && !m.Name.Contains("PDM")
                );

                pushPageDelegate = (PushPageDelegate)Delegate.CreateDelegate(
                    typeof(PushPageDelegate),
                    null,
                    pushPageMethod);
                return pushPageDelegate;
            }
        }
        public static void PushPage(this ActionMenu menu, Il2CppSystem.Action openFunc, Il2CppSystem.Action closeFunc = null, Texture2D icon = null, string text = null) => GetPushPageDelegate.Invoke(menu, openFunc, closeFunc, icon, text);
        private static PushPageDelegate pushPageDelegate;
        private delegate ActionMenuPage PushPageDelegate(ActionMenu actionMenu, Il2CppSystem.Action openFunc, Il2CppSystem.Action closeFunc = null, Texture2D icon = null, string text = null);
        
        
        
        public static Texture2D SetIcon(this PedalOption pedal, Texture2D icon) => pedal.prop_Texture2D_0 = icon;
        public static Texture2D GetIcon(this PedalOption pedal) => pedal.prop_Texture2D_0; //Only texture2d prop on PedalOption. shouldnt change unless drastic changes are made to the action menu
        public static bool isOpen(this ActionMenuOpener actionMenuOpener) => actionMenuOpener.field_Private_Boolean_0; //only bool on action menu opener, shouldnt change
        
        private static PropertyInfo actionButtonPercentProperty;
        public static void SetButtonPercentText(this PedalOption pedalOption, string text) //pedalOption.GetActionButton().prop_String_1
        {
            if (actionButtonPercentProperty != null)
            {
                actionButtonPercentProperty.SetValue(pedalOption.GetActionButton(), text);
                return;
            }
            var button = pedalOption.GetActionButton();
            actionButtonPercentProperty = typeof(ActionButton).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(string) && ((string)p.GetValue(button)).Equals("100%")
            );
            actionButtonPercentProperty.SetValue(pedalOption.GetActionButton(), text);
        }
        
        public static ActionButton GetActionButton(this PedalOption pedalOption) => pedalOption.field_Public_ActionButton_0; //only one
        public static void SetPedalTriggerEvent(this PedalOption pedalOption, PedalOptionTriggerEvent triggerEvent) => pedalOption.field_Public_MulticastDelegateNPublicSealedBoUnique_0 = triggerEvent; //only one

        public static ActionMenuOpener GetLeftOpener(this ActionMenuDriver actionMenuDriver)
        {
            if (actionMenuDriver.field_Public_ActionMenuOpener_0.field_Public_EnumNPublicSealedvaLeRi3vUnique_0 == ActionMenuOpener.EnumNPublicSealedvaLeRi3vUnique.Left)
                return actionMenuDriver.field_Public_ActionMenuOpener_0;
            return actionMenuDriver.field_Public_ActionMenuOpener_1;
        }

        public static ActionMenuOpener GetRightOpener(this ActionMenuDriver actionMenuDriver)
        {
            if (actionMenuDriver.field_Public_ActionMenuOpener_1.field_Public_EnumNPublicSealedvaLeRi3vUnique_0 == ActionMenuOpener.EnumNPublicSealedvaLeRi3vUnique.Right)
                return actionMenuDriver.field_Public_ActionMenuOpener_1;
            return actionMenuDriver.field_Public_ActionMenuOpener_0;
        } 
        public static ActionMenu GetActionMenu(this ActionMenuOpener actionMenuOpener) => actionMenuOpener.field_Public_ActionMenu_0;

        private static PropertyInfo radialPuppetcursorProperty;
        private static GameObject GetRadialCursorGameObject(RadialPuppetMenu radialPuppetMenu) //Build 1088 radialPuppetMenu.field_Public_GameObject_0
        {
            if (radialPuppetcursorProperty != null) return (GameObject)radialPuppetcursorProperty.GetValue(radialPuppetMenu);
            radialPuppetcursorProperty = typeof(RadialPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single( 
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(radialPuppetMenu)).name.Equals("Cursor")
            );
            return (GameObject)radialPuppetcursorProperty.GetValue(radialPuppetMenu);
        }
        public static GameObject GetCursor(this RadialPuppetMenu radialPuppetMenu) => GetRadialCursorGameObject(radialPuppetMenu);
        
        private static PropertyInfo axisPuppetcursorProperty;
        private static GameObject GetAxisCursorGameObject(AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_GameObject_0
        {
            if (axisPuppetcursorProperty != null) return (GameObject)axisPuppetcursorProperty.GetValue(axisPuppetMenu);
            axisPuppetcursorProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(axisPuppetMenu)).name.Equals("Cursor")
            );
            return (GameObject)axisPuppetcursorProperty.GetValue(axisPuppetMenu);
        }
        public static GameObject GetCursor(this AxisPuppetMenu axisPuppetMenu) => GetAxisCursorGameObject(axisPuppetMenu);
        
        public static GameObject GetArrow(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_GameObject_1;
        public static PedalGraphic GetFill(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_PedalGraphic_0; //Only
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
        public static void SetFillAngle(this PedalGraphic pedalGraphic, float angle) => pedalGraphic.field_Public_Single_3 = angle;
        public static float GetFillAngle(this PedalGraphic pedalGraphic) => pedalGraphic.field_Public_Single_3;
        
        public static void SetAlpha(this PedalGraphic pedalGraphic, float amount)
        {
            Color temp = pedalGraphic.color;
            temp.a = amount;
            pedalGraphic.color = temp;
        }
        
        public static void SetAngle(this RadialPuppetMenu radialPuppet, float angle)
        {
            radialPuppet.GetFill().SetFillAngle(angle);
            radialPuppet.UpdateDisplay();
        }

        public static void SetValue(this RadialPuppetMenu radialPuppet, float value)
        {
            radialPuppet.GetFill().SetFillAngle((value / 100) * 360);
            radialPuppet.UpdateDisplay();
        }

        public static void UpdateDisplay(this RadialPuppetMenu radialPuppet)
        {
            //MelonLogger.Msg($"Original: {radialPuppet.GetFill().field_Public_Single_3}, Math:{(radialPuppet.GetFill().field_Public_Single_3  / 360f)*100f}");
            radialPuppet.GetCenterText().text = Math.Round((radialPuppet.GetFill().GetFillAngle()  / 360f)*100f) + "%";
            radialPuppet.GetFill().UpdateGeometry();
        }

        public static void UpdateArrow(this RadialPuppetMenu radialPuppet, double angleOriginal, double eulerAngle)
        {
            //MelonLogger.Msg($"Original: {angleOriginal}, Euler Angle:{eulerAngle}");
            radialPuppet.GetArrow().transform.localPosition = new Vector3((float)(120 * Math.Cos(angleOriginal / Constants.radToDeg)), (float)(120 * Math.Sin(angleOriginal / Constants.radToDeg)), radialPuppet.GetArrow().transform.localPosition.z);
            radialPuppet.GetArrow().transform.localEulerAngles = new Vector3(radialPuppet.GetArrow().transform.localEulerAngles.x, radialPuppet.GetArrow().transform.localEulerAngles.y, (float)(180 - eulerAngle));
        }
        
        public static Vector2 GetCursorPos(this ActionMenu actionMenu) => actionMenu.field_Private_Vector2_0;
        public static void SetPedalTypeIcon(this PedalOption pedalOption, Texture2D icon) => pedalOption.GetActionButton().prop_Texture2D_2 = icon; //No choice needs to be hardcoded in sadly

    }
}