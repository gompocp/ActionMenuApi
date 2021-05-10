using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2
using UnityEngine;
using Object = UnityEngine.Object;

namespace ActionMenuApi
{
    internal static class ExtensionMethods
    {
        
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

        private static PropertyInfo radialPuppetCursorProperty;
        private static GameObject GetRadialCursorGameObject(RadialPuppetMenu radialPuppetMenu) //Build 1088 radialPuppetMenu.field_Public_GameObject_0
        {
            if (radialPuppetCursorProperty != null) return (GameObject)radialPuppetCursorProperty.GetValue(radialPuppetMenu);
            radialPuppetCursorProperty = typeof(RadialPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single( 
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(radialPuppetMenu)).name.Equals("Cursor")
            );
            return (GameObject)radialPuppetCursorProperty.GetValue(radialPuppetMenu);
        }
        public static GameObject GetCursor(this RadialPuppetMenu radialPuppetMenu) => GetRadialCursorGameObject(radialPuppetMenu);
        
        private static PropertyInfo axisPuppetCursorProperty;
        private static GameObject GetAxisCursorGameObject(AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_GameObject_0
        {
            if (axisPuppetCursorProperty != null) return (GameObject)axisPuppetCursorProperty.GetValue(axisPuppetMenu);
            axisPuppetCursorProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(axisPuppetMenu)).name.Equals("Cursor")
            );
            return (GameObject)axisPuppetCursorProperty.GetValue(axisPuppetMenu);
        }
        public static GameObject GetCursor(this AxisPuppetMenu axisPuppetMenu) => GetAxisCursorGameObject(axisPuppetMenu);
        
        private static PropertyInfo radialPuppetArrowProperty;
        private static GameObject GetRadialArrowGameObject(RadialPuppetMenu radialPuppetMenu) //Build 1088 radialPuppetMenu.field_Public_GameObject_0
        {
            if (radialPuppetArrowProperty != null) return (GameObject)radialPuppetArrowProperty.GetValue(radialPuppetMenu);
            radialPuppetArrowProperty = typeof(RadialPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single( 
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(radialPuppetMenu)).name.Equals("Arrow")
            );
            return (GameObject)radialPuppetArrowProperty.GetValue(radialPuppetMenu);
        }
        public static GameObject GetArrow(this RadialPuppetMenu radialPuppetMenu) => GetRadialArrowGameObject(radialPuppetMenu);
        public static PedalGraphic GetFill(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_PedalGraphic_0; //only one
        public static TextMeshProUGUI GetTitle(this RadialPuppetMenu radialPuppetMenu) => ((PuppetMenu)radialPuppetMenu).field_Public_TextMeshProUGUI_0; //only one
        public static TextMeshProUGUI GetTitle(this AxisPuppetMenu axisPuppetMenu) => ((PuppetMenu)axisPuppetMenu).field_Public_TextMeshProUGUI_0; //only one
        public static TextMeshProUGUI GetCenterText(this RadialPuppetMenu radialPuppetMenu) => radialPuppetMenu.field_Public_TextMeshProUGUI_0; //only one
        
        private static PropertyInfo axisPuppetFillUpProperty;
        public static PedalGraphic GetFillUp(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_PedalGraphic_0
        {
            if (axisPuppetFillUpProperty != null) return (PedalGraphic)axisPuppetFillUpProperty.GetValue(axisPuppetMenu);
            axisPuppetFillUpProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(PedalGraphic) && ((PedalGraphic)p.GetValue(axisPuppetMenu)).name.Equals("Fill Up")
            );
            return (PedalGraphic)axisPuppetFillUpProperty.GetValue(axisPuppetMenu);
        }
        
        private static PropertyInfo axisPuppetFillRightProperty;
        public static PedalGraphic GetFillRight(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_PedalGraphic_1
        {
            if (axisPuppetFillRightProperty != null) return (PedalGraphic)axisPuppetFillRightProperty.GetValue(axisPuppetMenu);
            axisPuppetFillRightProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(PedalGraphic) && ((PedalGraphic)p.GetValue(axisPuppetMenu)).name.Equals("Fill Right")
            );
            return (PedalGraphic)axisPuppetFillRightProperty.GetValue(axisPuppetMenu);
        }
  
        private static PropertyInfo axisPuppetFillDownProperty;
        public static PedalGraphic GetFillDown(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_PedalGraphic_2
        {
            if (axisPuppetFillDownProperty != null) return (PedalGraphic)axisPuppetFillDownProperty.GetValue(axisPuppetMenu);
            axisPuppetFillDownProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(PedalGraphic) && ((PedalGraphic)p.GetValue(axisPuppetMenu)).name.Equals("Fill Down")
            );
            return (PedalGraphic)axisPuppetFillDownProperty.GetValue(axisPuppetMenu);
        }

        private static PropertyInfo axisPuppetFillLeftProperty;
        public static PedalGraphic GetFillLeft(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_PedalGraphic_3
        {
            if (axisPuppetFillLeftProperty != null) return (PedalGraphic)axisPuppetFillLeftProperty.GetValue(axisPuppetMenu);
            axisPuppetFillLeftProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(PedalGraphic) && ((PedalGraphic)p.GetValue(axisPuppetMenu)).name.Equals("Fill Left")
            );
            return (PedalGraphic)axisPuppetFillLeftProperty.GetValue(axisPuppetMenu);
        }

        private static PropertyInfo actionButtonTextProperty; 
        public static void SetButtonText(this ActionButton actionButton, string text) //actionButton.prop_String_0
        {
            if (actionButtonTextProperty != null)
            {
                actionButtonTextProperty.SetValue(actionButton, text);
                return;
            }
            actionButtonTextProperty = typeof(ActionButton).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(string) && ((string)p.GetValue(actionButton)).Equals("Button Text")
            );
            actionButtonTextProperty.SetValue(actionButton, text);
        }
        
        private static PropertyInfo axisPuppetButtonUpProperty;
        public static ActionButton GetButtonUp(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_ActionButton_0
        {
            if (axisPuppetButtonUpProperty != null) return (ActionButton)axisPuppetButtonUpProperty.GetValue(axisPuppetMenu);
            axisPuppetButtonUpProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(ActionButton) && ((ActionButton)p.GetValue(axisPuppetMenu)).name.Equals("ButtonUp")
            );
            return (ActionButton)axisPuppetButtonUpProperty.GetValue(axisPuppetMenu);
        }
        
        private static PropertyInfo axisPuppetButtonRightProperty;
        public static ActionButton GetButtonRight(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_ActionButton_1
        {
            if (axisPuppetButtonRightProperty != null) return (ActionButton)axisPuppetButtonRightProperty.GetValue(axisPuppetMenu);
            axisPuppetButtonRightProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(ActionButton) && ((ActionButton)p.GetValue(axisPuppetMenu)).name.Equals("ButtonRight")
            );
            return (ActionButton)axisPuppetButtonRightProperty.GetValue(axisPuppetMenu);
        }
        
        private static PropertyInfo axisPuppetButtonDownProperty;
        public static ActionButton GetButtonDown(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_ActionButton_2
        {
            if (axisPuppetButtonDownProperty != null) return (ActionButton)axisPuppetButtonDownProperty.GetValue(axisPuppetMenu);
            axisPuppetButtonDownProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(ActionButton) && ((ActionButton)p.GetValue(axisPuppetMenu)).name.Equals("ButtonDown")
            );
            return (ActionButton)axisPuppetButtonDownProperty.GetValue(axisPuppetMenu);
        }
        
        private static PropertyInfo axisPuppetButtonLeftProperty;
        public static ActionButton GetButtonLeft(this AxisPuppetMenu axisPuppetMenu) //Build 1088 axisPuppetMenu.field_Public_ActionButton_3
        {
            if (axisPuppetButtonLeftProperty != null) return (ActionButton)axisPuppetButtonLeftProperty.GetValue(axisPuppetMenu);
            axisPuppetButtonLeftProperty = typeof(AxisPuppetMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single(
                p => p.PropertyType == typeof(ActionButton) && ((ActionButton)p.GetValue(axisPuppetMenu)).name.Equals("ButtonLeft")
            );
            return (ActionButton)axisPuppetButtonLeftProperty.GetValue(axisPuppetMenu);
        }
        
        private static PropertyInfo pedalOptionPrefabProperty;
        public static GameObject GetPedalOptionPrefab(this ActionMenu actionMenu) //Build 1093 menu.field_Public_GameObject_1
        {
            if (pedalOptionPrefabProperty != null) return (GameObject)pedalOptionPrefabProperty.GetValue(actionMenu);
            pedalOptionPrefabProperty = typeof(ActionMenu).GetProperties(BindingFlags.Public | BindingFlags.Instance).Single( 
                p => p.PropertyType == typeof(GameObject) && ((GameObject)p.GetValue(actionMenu)).name.Equals("PedalOption")
            );
            return (GameObject)pedalOptionPrefabProperty.GetValue(actionMenu);
        }

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
            radialPuppet.GetArrow().transform.localPosition = new Vector3((float)(120 * Math.Cos(angleOriginal / Constants.RAD_TO_DEG)), (float)(120 * Math.Sin(angleOriginal / Constants.RAD_TO_DEG)), radialPuppet.GetArrow().transform.localPosition.z);
            radialPuppet.GetArrow().transform.localEulerAngles = new Vector3(radialPuppet.GetArrow().transform.localEulerAngles.x, radialPuppet.GetArrow().transform.localEulerAngles.y, (float)(180 - eulerAngle));
        }
        
                private static ClosePuppetMenusDelegate GetClosePuppetMenusDelegate
        {
            get
            {
                //Build 1088 menu.Method_Public_Void_Boolean_2()
                if (closePuppetMenusDelegate != null) return closePuppetMenusDelegate;
                MethodInfo closePuppetMenusMethod = typeof(ActionMenu).GetMethods().Single(
                    m => m.Name.StartsWith("Method_Public_Void_Boolean_")
                         && m.GetParameters().Length == 1
                         && m.GetParameters()[0].IsOptional
                         && !m.Name.Contains("PDM")
                );
                closePuppetMenusDelegate = (ClosePuppetMenusDelegate)Delegate.CreateDelegate(
                    typeof(ClosePuppetMenusDelegate),
                    null,
                    closePuppetMenusMethod);
                return closePuppetMenusDelegate;
            }
        }
        public static void ClosePuppetMenus(this ActionMenu actionMenu, bool canResetValue)
        {
            GetClosePuppetMenusDelegate(actionMenu, canResetValue);
        }
        private static ClosePuppetMenusDelegate closePuppetMenusDelegate;
        private delegate void ClosePuppetMenusDelegate(ActionMenu actionMenu, bool canResetValue);
        
        private static DestroyPageDelegate GetDestroyPageDelegate
        {
            get
            {
                //Build 1088 menu.Method_Private_Void_ObjectNPublicAcTeAcStGaUnique_0()
                if (destroyPageDelegate != null) return destroyPageDelegate;
                MethodInfo destroyPageMethod = typeof(ActionMenu).GetMethods().Single(
                    m =>
                        m.Name.StartsWith("Method_Private_Void_ObjectNPublicAcTeAcStGaUnique_")
                        && m.GetParameters().Length == 1
                        && !m.Name.Contains("PDM")
                );
                destroyPageDelegate = (DestroyPageDelegate)Delegate.CreateDelegate(
                    typeof(DestroyPageDelegate),
                    null,
                    destroyPageMethod);
                return destroyPageDelegate;
            }
        }
        public static void DestroyPage(this ActionMenu actionMenu, ActionMenuPage page)
        {
            GetDestroyPageDelegate(actionMenu, page);
        }
        private static DestroyPageDelegate destroyPageDelegate;
        private delegate void DestroyPageDelegate(ActionMenu actionMenu, ActionMenuPage page);
        
        public static List<List<Action>> Split(this List<Action> mods, int chunkSize)  
        {        
            var list = new List<List<Action>>();
            for (int i = 0; i < mods.Count; i += chunkSize) 
            { 
                list.Add(mods.GetRange(i, Math.Min(chunkSize, mods.Count - i))); 
            }
            return list; 
        }

        public static bool HasStringLiterals(this MethodInfo m)
        {
            foreach (var instance in XrefScanner.XrefScan(m))
            {
                try
                {
                    if (instance.Type == XrefType.Global && instance.ReadAsObject() != null) return true;
                }
                catch { }
            }
            return false;
        }
        public static bool CheckStringsCount(this MethodInfo m, int count)
        {
            int total = 0;
            foreach (var instance in XrefScanner.XrefScan(m))
            {
                try
                {
                    if (instance.Type == XrefType.Global && instance.ReadAsObject() != null) total++;
                }
                catch
                {
                }
            }
            return total == count;
        }

        public static bool HasMethodCallWithName(this MethodInfo m, string txt)
        {
            foreach (var instance in XrefScanner.XrefScan(m))
            {
                try
                {
                    if (instance.Type == XrefType.Method && instance.TryResolve() != null)
                    {
                        try
                        {
                            if (instance.TryResolve().Name.Contains(txt)) return true;
                        }catch(Exception e) {MelonLogger.Warning(e);}
                    }
                }
                catch { }
            }
            return false;
        }
        public static bool SameClassMethodCallCount(this MethodInfo m, int calls)
        {
            int count = 0;
            foreach (var instance in XrefScanner.XrefScan(m))
            {
                try
                {
                    if (instance.Type == XrefType.Method && instance.TryResolve() != null)
                    {
                        try
                        {
                            if (m.DeclaringType == instance.TryResolve().DeclaringType) count++;
                        }catch(Exception e) {MelonLogger.Warning(e);}
                    }
                }
                catch { }
            }
            return count == calls;
        }

        public static bool HasMethodWithDeclaringType(this MethodInfo m, Type declaringType)
        {
            foreach (var instance in XrefScanner.XrefScan(m))
            {
                try
                {
                    if (instance.Type == XrefType.Method && instance.TryResolve() != null)
                    {
                        try
                        {
                            if (declaringType == instance.TryResolve().DeclaringType) return true;
                        }catch(Exception e) {MelonLogger.Warning(e);}
                    }
                }
                catch { }
            }
            return false;
        }
        
        public static GameObject Clone(this GameObject gameObject)
        {
            return Object
                .Instantiate(gameObject.transform, gameObject.transform.parent)
                .gameObject;
        }

        public static GameObject GetChild(this GameObject gameObject, string childName)
        {
            //MelonLogger.Msg($"Gameobject: {gameObject.name},   Child Searching for: {childName}");
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                //MelonLogger.Msg("   "+child.name);
                if (child.name.Equals(childName)) return child;
            }
            return null;
        }
        
        //These things might change, just a bit tricky to identify the correct ones using reflection
        public static void SetFillAngle(this PedalGraphic pedalGraphic, float angle) => pedalGraphic.field_Public_Single_3 = angle;
        public static float GetFillAngle(this PedalGraphic pedalGraphic) => pedalGraphic.field_Public_Single_3;
        public static Vector2 GetCursorPos(this ActionMenu actionMenu) => actionMenu.field_Private_Vector2_0;
        public static void SetPedalTypeIcon(this PedalOption pedalOption, Texture2D icon) => pedalOption.GetActionButton().prop_Texture2D_2 = icon; //No choice needs to be hardcoded in sadly
    }
}