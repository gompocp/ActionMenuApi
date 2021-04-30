using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ActionMenuApi.Pedals;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using PedalOptionTriggerEvent = PedalOption.MulticastDelegateNPublicSealedBoUnique; //Will this change?, ¯\_(ツ)_/¯
using ActionMenuPage = ActionMenu.ObjectNPublicAcTeAcStGaUnique;  //Will this change?, ¯\_(ツ)_/¯x2

namespace ActionMenuApi
{
    internal static class Utilities
    {
        public static bool checkXref(MethodBase m, params  string[] keywords)
        {
            try
            {
                foreach (string keyword in keywords)
                {

                    if (!XrefScanner.XrefScan(m).Any(
                        instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString()
                            .Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }
        
        public static bool checkXref(MethodBase m, List<string> keywords)
        {
            try
            {
                foreach (string keyword in keywords)
                {

                    if (!XrefScanner.XrefScan(m).Any(
                        instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString()
                            .Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }

        public static void AddPedalsInList(List<PedalStruct> list, ActionMenu instance)
        {
            foreach (var pedalStruct in list)
            {
                PedalOption pedalOption = instance.AddOption();
                pedalOption.setText(pedalStruct.text);
                pedalOption.setIcon(pedalStruct.icon);
                pedalOption.SetPedalTriggerEvent(DelegateSupport.ConvertDelegate<PedalOptionTriggerEvent>(pedalStruct.triggerEvent));
                //Additional setup for pedals
                switch (pedalStruct.Type)
                {
                    case PedalType.SubMenu:
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeFolder);
                        break;
                    case PedalType.RadialPuppet:
                        PedalRadial pedalRadial = (PedalRadial) pedalStruct;
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeRadial);
                        pedalOption.SetInfoText($"{Math.Round(pedalRadial.currentValue)}%");
                        pedalRadial.pedal = pedalOption;
                        break;
                    case PedalType.Toggle:
                        PedalToggle pedalToggle = (PedalToggle) pedalStruct;
                        if (pedalToggle.toggled)
                            pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeToggleOn);
                        else
                            pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeToggleOff);
                        pedalToggle.pedal = pedalOption;
                        break;
                    case PedalType.FourAxisPuppet:
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeAxis);
                        break;
                }
            }
        }
        
        public static double ConvertFromDegToEuler(double angle)
        {
            //TODO: Rewrite/Remove Unnecessary Addition/Subtraction
            if (angle >= 0 && angle <= 90) return 90 - angle;
            if (angle > 90 && angle <= 180) return 360 - (angle - 90);
            if (angle <= -90 && angle >= -180) return 270 - (angle + 180);
            if (angle <= 0 && angle >= -90) return 180 - (angle + 180) + 90;
            return 0;
        }
        public static double ConvertFromEuler(double angle)
        {
            //TODO: Rewrite/Remove Unnecessary Addition/Subtraction
            if (angle >= 90 && angle <= 270) return (angle - 90) * -1;
            if (angle <= 360 && angle > 270) return 180 - (angle - 270);
            if (angle < 90 && angle >= 0) return (90 - angle);
            return 0;
        }
        public static Vector2 GetCursorPosLeft()
        {
            if (UnityEngine.XR.XRDevice.isPresent)
                return new Vector2(Input.GetAxis(InputAxes.LeftHorizontal), Input.GetAxis(InputAxes.LeftVertical)) * 16;
            return ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener().GetActionMenu().GetCursorPos();
        }
        public static Vector2 GetCursorPosRight()
        {
            if (UnityEngine.XR.XRDevice.isPresent)
                return new Vector2(Input.GetAxis(InputAxes.RightHorizontal), Input.GetAxis(InputAxes.RightVertical)) * 16;
            return ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener().GetActionMenu().GetCursorPos();
        }
        
        public static GameObject CloneGameObject(string pathToGameObject, string pathToParent)
        {
            return GameObject.Instantiate(GameObject.Find(pathToGameObject).transform, GameObject.Find(pathToParent).transform).gameObject;
        }

        public static ActionMenuDriver.ExpressionIcons GetExpressionsIcons() => ActionMenuDriver.prop_ActionMenuDriver_0.field_Public_ExpressionIcons_0;

        // Didnt know what to name this function
        public static ActionMenuHand GetActionMenuHand()
        {
            if (!ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener().isOpen() && ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener().isOpen())
            {
                return ActionMenuHand.Right;
            }
            if (ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener().isOpen() && !ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener().isOpen())
            {
                return ActionMenuHand.Left;
            }
            return ActionMenuHand.Invalid;
        }
        
        public static ActionMenuOpener GetActionMenuOpener()
        {
            if (!ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener().isOpen() && ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener().isOpen())
            {
                return ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener();
            }
            if (ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener().isOpen() && !ActionMenuDriver.prop_ActionMenuDriver_0.GetRightOpener().isOpen())
            {
                return ActionMenuDriver.prop_ActionMenuDriver_0.GetLeftOpener();
            }
            return null;
            /*
            else if (ActionMenuDriver._instance.openerL.isOpen() && ActionMenuDriver._instance.openerR.isOpen())
            {
                return null; //Which one to return ¯\_(ツ)_/¯ Mystery till I figure something smart out
            }
            */
        }
    }
}